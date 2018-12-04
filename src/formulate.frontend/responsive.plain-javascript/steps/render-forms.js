/**
 * Renders the Formulate forms, inserting them into the appropriate place
 * in the DOM.
 * @param forms The data for the forms to render.
 * @param fieldRenderers The associative array of the field rendering functions.
 * @param fieldValidators The associative array of the field validating functions.
 */
function renderForms(forms, fieldRenderers, fieldValidators) {

    // Variables.
    let i, form, formId, placeholderElement, formElement, formContainer, fields;

    // Process each form.
    for (i = 0; i < forms.length; i++) {

        // Variables.
        form = forms[i];

        // Create the form DOM element.
        formElement = document.createElement("form");

        // Add CSS class to DOM element.
        formElement.classList.add("formulate__form");

        // Get the placeholder element to insert the form before.
        formId = "formulate-form-" + form.data.randomId;
        placeholderElement = document.getElementById(formId);

        // Render the contents of the form.
        fields = require("./render-form")(form, formElement, placeholderElement, fieldRenderers, fieldValidators);

        // Insert the form before the placeholder, and remove the placeholder.
        formContainer = placeholderElement.parentNode;
        formContainer.insertBefore(formElement, placeholderElement);
        formContainer.removeChild(placeholderElement);

        // Handle submits.
        attachSubmitHandler(formElement, fields, form.data.payload, form.data.url);

    }

}

/**
 * Attaches the function that handles the submit event.
 * @param form {HTMLFormElement} The HTML form DOM element.
 * @param fields {Array} The fields in this form.
 * @param payload {Object} The additional data to send with the submission.
 * @param url {string} The URL to send the submission to.
 */
function attachSubmitHandler(form, fields, payload, url) {

    // Variables.
    let validationData;

    // Listen for submit events.
    form.addEventListener("submit", function (e) {

        // Cancel submit (since we'll be doing it with AJAX instead).
        e.preventDefault();

        // Dispatch event to indicate the form submission has started.
        dispatchEvent("formulate: submit: started", form);

        // First, ensure all fields are valid.
        checkValidity(form, fields)
            .then(function (validationResult) {
                if (validationResult.success) {

                    // Dispatch event to indicate the form submission validation succeeded.
                    validationData = {
                        cancelSubmit: false,
                        fields: fields,
                        payload: payload
                    };
                    dispatchEvent("formulate: validation: success", form, validationData);

                    // Should the submit be cancelled?
                    if (validationData.cancelSubmit) {
                        dispatchEvent("formulate: submit: cancelled", form);
                        return;
                    }

                    // Use the data from the validation event, in case it was modified by a listener.
                    fields = validationData.fields;
                    payload = validationData.payload;

                    // Populate submission with initial payload.
                    sendPayloadToServer(form, fields, payload, url);

                } else {

                    // Validation failed.
                    handleInvalidFields(validationResult.messages, form);

                }
            });

    }, true);

}

/**
 * Handles invalid fields by dispatching an event with the validation errors.
 * @param messages The messages for the validation errors.
 * @param form The form.
 */
function handleInvalidFields(messages, form) {
    dispatchEvent("formulate: submit: validation errors", form, {
        messages: messages
    });
}

/**
 * Checks the form for validation errors.
 * @param form The form.
 * @param fields The fields in the form.
 * @returns {*} A promise that will resolve to the result of the validations.
 */
function checkValidity(form, fields) {

    // Variables.
    let i, field, validationPromises = [], fieldPromises;

    // Start validating each field.
    for(i = 0; i < fields.length; i++) {
        field = fields[i];
        fieldPromises = field.checkValidity();
        validationPromises = validationPromises.concat(fieldPromises);
    }

    // Finalize the validation of the fields.
    return require("../utils/validation").aggregateValidations(validationPromises);

}

/**
 * Sends the payload for the form to the server.
 * @param form The form element.
 * @param fields The fields for the form.
 * @param payload The data to send to the server.
 * @param url The URL to send the data to.
 */
function sendPayloadToServer(form, fields, payload, url) {

    // Variables.
    let i, data, payloadKey, dataByAlias = {};

    // Populate submission with initial payload.
    data = new FormData();
    for(payloadKey in payload) {
        if (payload.hasOwnProperty(payloadKey)) {
            data.append(payloadKey, payload[payloadKey]);
        }
    }

    // Populate submission with data from fields.
    for (i = 0; i < fields.length; i++) {
        fields[i].setData(data);
        fields[i].setData(dataByAlias, {
            rawDataByAlias: true
        });
    }

    // Send data as AJAX submission.
    new (require("../utils/ajax"))(url, data).then(function(result) {

        // Was the request successful?
        let success = JSON.parse(result.text).Success;
        if (success) {

            // Dispatch success event.
            dispatchEvent("formulate form: submit: success", form, {
                dataByAlias: dataByAlias
            });

        } else {

            // Dispatch failure event.
            dispatchEvent("formulate form: submit: failure", form);

        }

    }).catch(function() {

        // Dispatch failure event.
        dispatchEvent("formulate form: submit: failure", form);

    });

}

/**
 * Dispatches the specified event.
 * @param eventName The event to dispatch.
 * @param form The form element to dispatch the element on.
 * @param data The data to send with the event.
 */
function dispatchEvent(eventName, form, data) {
    let event;
    if (typeof window.CustomEvent === "function") {

        // Typical implementation for CustomEvent.
        event = new CustomEvent(eventName, {
            bubbles: true,
            detail: data
        });
        form.dispatchEvent(event);

    } else {

        // IE11 implementation for CustomEvent.
        event = document.createEvent("CustomEvent");
        event.initCustomEvent(eventName, true, false, data);
        form.dispatchEvent(event);

    }
}

// Export the function that renders forms.
module.exports = renderForms;