/**
 * Renders a submit button.
 * @param fieldData The field data that should be used to render the button.
 * @param fieldValidators The associative array of the field validating functions.
 * @param cssClasses The CSS classes to attach to the element.
 * @constructor
 */
function RenderButton(fieldData, fieldValidators, cssClasses) {

    // Initialize field.
    require("../utils/field").initializeField(this, fieldData, fieldValidators, {
        nodeName: "button",
        type: "submit",
        cssClasses: cssClasses,
        usePlaceholder: false,
        useLabel: false
    });

    // Add text to button.
    this.element.appendChild(document.createTextNode(fieldData.label));

}

/**
 * Returns the DOM element for the button.
 * @returns {HTMLDivElement} The DOM element for the button.
 */
RenderButton.prototype.getElement = function () {
    return this.wrapper;
};

/**
 * Does nothing, as this field has no data.
 */
RenderButton.prototype.setData = function () {};

/**
 * Does nothing, as this field has no data to validate.
 * @returns {Promise[]} An empty array.
 */
RenderButton.prototype.checkValidity = function () {
    return [];
};

// Export the field renderer configuration.
module.exports = {
    key: "button",
    renderer: RenderButton
};