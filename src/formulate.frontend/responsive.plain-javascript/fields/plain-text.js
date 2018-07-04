/**
 * Renders a text field.
 * @param fieldData The field data that should be used to render the text field.
 * @param fieldValidators The associative array of the field validating functions.
 * @param cssClasses The CSS classes to attach to the element.
 * @constructor
 */
function RenderText(fieldData, fieldValidators, cssClasses) {
    require("../utils/field").initializeField(this, fieldData, fieldValidators, {
        type: "text",
        cssClasses: cssClasses
    });
}

/**
 * Returns the DOM element for the text field.
 * @returns {HTMLDivElement} The DOM element for the text field.
 */
RenderText.prototype.getElement = function () {
    return this.wrapper;
};

/**
 * Adds the data for this field on the specified FormData instance.
 * @param data {FormData} The FormData instance to set the field data on.
 * @param options {{rawDataByAlias: boolean}} Optional. The options for setting the data.
 */
RenderText.prototype.setData = function (data, options) {
    require("../utils/field").setData(data, this.element.value, options, this.alias, this.id);
};

/**
 * Checks the validity of the value in this field (adding inline validation messages if necessary).
 * @returns {Promise[]} An array of promises that resolve to validation results.
 */
RenderText.prototype.checkValidity = function () {
    return require("../utils/validation")
        .checkTextValidity(this, this.validators, this.element.value, this.wrapper);
};

// Export the field renderer configuration.
module.exports = {
    key: "text",
    renderer: RenderText
};