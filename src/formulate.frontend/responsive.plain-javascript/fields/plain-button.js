/**
 * Renders a submit button.
 * @param fieldData The field data that should be used to render the button.
 * @param cssClasses The CSS classes to attach to the element.
 * @returns {HTMLButtonElement} The DOM element for the button.
 */
function renderButton(fieldData, cssClasses) {

    // Variables.
    let i, fieldElement;

    // Create element.
    fieldElement = document.createElement("button");
    fieldElement.type = "submit";
    fieldElement.appendChild(document.createTextNode(fieldData.label));

    // Add CSS classes.
    require("../utils/add-classes")(fieldElement, cssClasses);

    // Return button DOM element.
    return fieldElement;

}

// Export the field renderer configuration.
module.exports = {
    key: "button",
    renderer: renderButton
};