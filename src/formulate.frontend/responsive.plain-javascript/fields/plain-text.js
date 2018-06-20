/**
 * Renders a text field.
 * @param fieldData The field data that should be used to render the text field.
 * @param cssClasses The CSS classes to attach to the element.
 * @constructor
 */
function RenderText(fieldData, cssClasses) {

    // Variables.
    let fieldElement;

    // Create element.
    fieldElement = document.createElement("input");
    fieldElement.type = "text";

    // Attach CSS classes.
    require("../utils/add-classes")(fieldElement, cssClasses);

    // Retain text field DOM element.
    this.element = fieldElement;
    this.id = fieldData.id;

}

/**
 * Returns the DOM element for the text field.
 * @returns {HTMLInputElement} The DOM element for the text field.
 */
RenderText.prototype.getElement = function () {
    return this.element;
};

/**
 * Adds the data for this field on the specified FormData instance.
 * @param data {FormData} The FormData instance to set the field data on.
 */
RenderText.prototype.setData = function (data) {
    data.append(this.id, this.element.value);
};

// Export the field renderer configuration.
module.exports = {
    key: "text",
    renderer: RenderText
};