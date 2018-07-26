// Dependencies.
let FieldUtility = require("../utils/field");

/**
 * Renders a header.
 * @param fieldData The field data that should be used to render the header.
 * @param fieldValidators The associative array of the field validating functions.
 * @param cssClasses The CSS classes to attach to the element.
 * @constructor
 */
function RenderHeader(fieldData, fieldValidators, cssClasses) {

    // Initialize field.
    FieldUtility.initializeField(this, fieldData, fieldValidators, {
        nodeName: "h2",
        cssClasses: cssClasses,
        usePlaceholder: false,
        useLabel: false,
        useWrapper: false
    });

    // Add text to element.
    this.element.appendChild(document.createTextNode(fieldData.configuration.text));

}

/**
 * Returns the DOM element for the header.
 * @returns {HTMLButtonElement} The DOM element for the header.
 */
RenderHeader.prototype.getElement = function () {
    return this.element;
};

/**
 * Ensure the prototype has the necessary functions.
 */
FieldUtility.initializeFieldPrototype(RenderHeader.prototype);

// Export the field renderer configuration.
module.exports = {
    key: "header",
    renderer: RenderHeader
};