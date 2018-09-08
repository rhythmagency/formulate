// Dependencies.
let FieldUtility = require("../utils/field");

/**
 * Renders a textarea field.
 * @param fieldData The field data that should be used to render the textarea field.
 * @param fieldValidators The associative array of the field validating functions.
 * @param cssClasses The CSS classes to attach to the element.
 * @constructor
 */
function RenderText(fieldData, fieldValidators, cssClasses) {
    require("../utils/field").initializeField(this, fieldData, fieldValidators, {
        nodeName: "textarea",
        cssClasses: cssClasses
    });
}

/**
 * Returns the DOM element for the textarea field.
 * @returns {HTMLDivElement} The DOM element for the textarea field.
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

/**
 * Ensure the prototype has the necessary functions.
 */
FieldUtility.initializeFieldPrototype(RenderText.prototype);

// Export the field renderer configuration.
module.exports = {
    key: "textarea",
    renderer: RenderText
};