/**
 * Renders rich text.
 * @param fieldData The field data that should be used to render the rich text.
 * @param fieldValidators The associative array of the field validating functions.
 * @param cssClasses The CSS classes to attach to the element.
 * @constructor
 */
function RichText(fieldData, fieldValidators, cssClasses) {

    // Variables.
    let fieldElement;

    // Create element.
    fieldElement = document.createElement("div");
    fieldElement.innerHTML = fieldData.configuration.text;

    // Add CSS classes.
    require("../utils/add-classes")(fieldElement, cssClasses);

    // Retain rich text DOM element.
    this.element = fieldElement;

}

/**
 * Returns the DOM element for the rich text.
 * @returns {HTMLButtonElement} The DOM element for the rich text.
 */
RichText.prototype.getElement = function () {
    return this.element;
};

/**
 * Does nothing, as this field has no data.
 */
RichText.prototype.setData = function () {};

/**
 * Does nothing, as this field has no data to validate.
 * @returns {Promise[]} An empty array.
 */
RichText.prototype.checkValidity = function () {
    return [];
};

// Export the field renderer configuration.
module.exports = {
    key: "rich-text",
    renderer: RichText
};