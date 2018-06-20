/**
 * Renders the Formulate forms, inserting them into the appropriate place
 * in the DOM.
 * @param forms The data for the forms to render.
 * @param fieldRenderers The associative array of the field rendering functions.
 */
function renderForms(forms, fieldRenderers) {

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

        // Render the contents of the form.
        fields = require("./render-form")(form, formElement, fieldRenderers);

        // Get the placeholder element to insert the form before.
        formId = "formulate-form-" + form.data.randomId;
        placeholderElement = document.getElementById(formId);

        // Insert the form before the placeholder.
        formContainer = placeholderElement.parentNode;
        formContainer.insertBefore(formElement, placeholderElement);

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
    let i, data, payloadKey;

    // Listen for submit events.
    form.addEventListener("submit", function (e) {

        // Cancel submit (since we'll be doing it with AJAX instead).
        e.preventDefault();

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
        }

        // Send data as AJAX submission.
        new (require("../utils/ajax"))(url, data, function (status) {
            if (status === 200) {
                //TODO: Emit event to indicate success.
            }
        });

    }, true);

}

// Export the function that renders forms.
module.exports = renderForms;