// Dependencies.
let FieldUtility = require("../utils/field");

/**
 * Renders rich text.
 * @param fieldData The field data that should be used to render the rich text.
 * @param fieldValidators The associative array of the field validating functions.
 * @param cssClasses The CSS classes to attach to the element.
 * @constructor
 */
function RichText(fieldData, fieldValidators, cssClasses) {

    // Initialize field.
    FieldUtility.initializeField(this, fieldData, fieldValidators, {
        nodeName: "div",
        cssClasses: cssClasses,
        usePlaceholder: false,
        useLabel: false,
        useWrapper: false
    });

    // Add text to element.
    this.element.innerHTML = fieldData.configuration.text;

}

/**
 * Returns the DOM element for the rich text.
 * @returns {HTMLButtonElement} The DOM element for the rich text.
 */
RichText.prototype.getElement = function () {
    return this.element;
};

/**
 * Ensure the prototype has the necessary functions.
 */
FieldUtility.initializeFieldPrototype(RichText.prototype);

// Export the field renderer configuration.
module.exports = {
    key: "rich-text",
    renderer: RichText
};