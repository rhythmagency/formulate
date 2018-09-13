// Dependencies.
let FieldUtility = require("../utils/field");

/**
 * Renders a checkbox field.
 * @param fieldData The field data that should be used to render the checkbox field.
 * @param fieldValidators The associative array of the field validating functions.
 * @param cssClasses The CSS classes to attach to the element.
 * @constructor
 */
function RenderCheckbox(fieldData, fieldValidators, cssClasses) {
    FieldUtility.initializeField(this, fieldData, fieldValidators, {
        type: "checkbox",
        cssClasses: cssClasses,
        usePlaceholder: false
    });
}

/**
 * Returns the DOM element for the checkbox field.
 * @returns {HTMLDivElement} The DOM element for the checkbox field.
 */
RenderCheckbox.prototype.getElement = function () {
    return this.wrapper;
};

/**
 * Adds the data for this field on the specified FormData instance.
 * @param data {FormData} The FormData instance to set the field data on.
 * @param options {{rawDataByAlias: boolean}} Optional. The options for setting the data.
 */
RenderCheckbox.prototype.setData = function (data, options) {
    FieldUtility.setData(data, this.element.checked, options, this.alias, this.id);
};

/**
 * Checks the validity of the value in this field (adding inline validation messages if necessary).
 * @returns {Promise[]} An array of promises that resolve to validation results.
 */
RenderCheckbox.prototype.checkValidity = function () {
    return require("../utils/validation")
        .checkBoolValidity(this, this.validators, this.element.checked, this.wrapper);
};

/**
 * Ensure the prototype has the necessary functions.
 */
FieldUtility.initializeFieldPrototype(RenderCheckbox.prototype);

// Export the field renderer configuration.
module.exports = {
    key: "checkbox",
    renderer: RenderCheckbox
};