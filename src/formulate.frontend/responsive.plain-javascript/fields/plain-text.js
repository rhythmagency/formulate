/**
 * Renders a text field.
 * @param fieldData The field data that should be used to render the text field.
 * @param cssClasses The CSS classes to attach to the element.
 * @returns {HTMLInputElement} The DOM element for the text field.
 */
function renderText(fieldData, cssClasses) {

    // Variables.
    let i, fieldElement;

    // Create element.
    fieldElement = document.createElement("input");
    fieldElement.type = "text";

    // Attach CSS classes.
    require("../utils/add-classes")(fieldElement, cssClasses);

    // Return text field DOM element.
    return fieldElement;

}

// Export the field renderer configuration.
module.exports = {
    key: "text",
    renderer: renderText
};