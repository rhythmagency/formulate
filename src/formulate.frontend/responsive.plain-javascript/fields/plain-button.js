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
    FieldUtility.initializeField(this, fieldData, fieldValidators, {
        nodeName: "button",
        type: "submit",
        cssClasses: cssClasses,
        usePlaceholder: false,
        useLabel: false
    });

    // Add text to button.
    this.element.appendChild(document.createTextNode(fieldData.label));

    // Store instance variables.
    this.formElement = extraOptions.formElement;
    this.buttonKind = fieldData.configuration.buttonKind;

    // Listen for form events.
    this.listenForSubmit();
    this.listenForFailureEvents();

}

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

// Export the field renderer configuration.
module.exports = {
    key: "button",
    renderer: RenderButton
};