/**
 * Renders a submit button.
 * @param fieldData The field data that should be used to render the button.
 * @param cssClasses The CSS classes to attach to the element.
 * @constructor
 */
function RenderButton(fieldData, cssClasses) {

    // Variables.
    let fieldElement;

    // Create element.
    fieldElement = document.createElement("button");
    fieldElement.type = "submit";
    fieldElement.appendChild(document.createTextNode(fieldData.label));

    // Add CSS classes.
    require("../utils/add-classes")(fieldElement, cssClasses);

    // Retain button DOM element.
    this.element = fieldElement;

}

/**
 * Returns the DOM element for the button.
 * @returns {HTMLButtonElement} The DOM element for the button.
 */
RenderButton.prototype.getElement = function () {
    return this.element;
};

/**
 * Does nothing, as this field has no data.
 */
RenderButton.prototype.setData = function () {
};

// Export the field renderer configuration.
module.exports = {
    key: "button",
    renderer: RenderButton
};