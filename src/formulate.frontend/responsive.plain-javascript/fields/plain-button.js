// Dependencies.
let FieldUtility = require("../utils/field");

/**
 * Renders a submit button.
 * @param fieldData The field data that should be used to render the button.
 * @param fieldValidators The associative array of the field validating functions.
 * @param cssClasses The CSS classes to attach to the element.
 * @param extraOptions Additional options that are less commonly used.
 * @constructor
 */
function RenderButton(fieldData, fieldValidators, cssClasses, extraOptions) {

    // Initialize field.
    this.buttonType = this.getTypeValue(fieldData.configuration.buttonKind);
    FieldUtility.initializeField(this, fieldData, fieldValidators, {
        nodeName: "button",
        type: this.buttonType,
        cssClasses: cssClasses,
        usePlaceholder: false,
        useLabel: false
    });

    // Add text to button.
    this.element.appendChild(document.createTextNode(fieldData.label));

    // Disable submit button if this is a multi-step form.
    if (extraOptions.stepIndex > 0) {
        this.toggleSubmitButton(false);
    }

    // Store instance variables.
    this.formElement = extraOptions.formElement;
    this.buttonKind = fieldData.configuration.buttonKind;

    // Listen for form events.
    this.listenForSubmit();
    this.listenForFailureEvents();
    this.listenForButtonClicks();

}

/**
 * Toggle the disabled attribute on the submit button.
 * @param enabled Should the button be enabled or disabled?
 */
RenderButton.prototype.toggleSubmitButton = function (enabled) {

    // Only toggle the enabled state on the submit button.
    if (this.buttonType === "submit") {
        this.element.disabled = !enabled;
    }

};

/**
 * Listen for previous/next button clicks.
 */
RenderButton.prototype.listenForButtonClicks = function () {

    // Variables.
    let formElement = this.formElement,
        buttonKind = this.buttonKind;

    // When a button is clicked, emit an event to advance the form to the previous or next step.
    this.element.addEventListener("click", function () {
        if (buttonKind === "Previous") {
            dispatchEvent("formulate: submit: previous", formElement);
        } else if (buttonKind === "Next") {
            dispatchEvent("formulate: submit: next", formElement);
        }
    });

};

/**
 * Get the value to set on the type attribute for the button.
 * @param buttonKind The kind of button (e.g., "Previous" or "Submit").
 * @returns {string} The value (e.g., "button" or "submit").
 */
RenderButton.prototype.getTypeValue = function (buttonKind) {
    if (buttonKind === "Previous" || buttonKind === "Next") {
        return "button";
    } else {
        return "submit";
    }
};

/**
 * Listens for failure events on the form (in which case the button should be enabled again).
 */
RenderButton.prototype.listenForFailureEvents = function () {
    let formElement = this.formElement,
        element = this.element,
        handleError = function () {
            element.disabled = false;
        };
    formElement.addEventListener("formulate: submit: validation errors", handleError, true);
    formElement.addEventListener("formulate form: submit: failure", handleError, true);
    formElement.addEventListener("formulate: submit: cancelled", handleError, true);
};

/**
 * Listens for submit events on the form (in which case the button should be disabled).
 */
RenderButton.prototype.listenForSubmit = function () {
    let self = this;
    self.formElement.addEventListener("formulate: submit: started", function () {
        self.element.disabled = true;
    }, true);
};

/**
 * Returns the DOM element for the button.
 * @returns {HTMLDivElement} The DOM element for the button.
 */
RenderButton.prototype.getElement = function () {
    return this.wrapper;
};

/**
 * Returns the kind of button.
 * @returns {string} The kind of button, or null.
 */
RenderButton.prototype.getButtonKind = function () {
    return this.buttonKind;
};

/**
 * Ensure the prototype has the necessary functions.
 */
FieldUtility.initializeFieldPrototype(RenderButton.prototype);

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

// Export the field renderer configuration.
module.exports = {
    key: "button",
    renderer: RenderButton
};