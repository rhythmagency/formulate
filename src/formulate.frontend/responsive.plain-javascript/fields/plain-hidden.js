// Dependencies.
let FieldUtility = require("../utils/field");

/**
 * Renders a hidden field.
 * @param fieldData The field data that should be used to render the hidden field.
 * @param fieldValidators The associative array of the field validating functions.
 * @param cssClasses The CSS classes to attach to the element.
 * @constructor
 */
function RenderHidden(fieldData, fieldValidators, cssClasses) {
    FieldUtility.initializeField(this, fieldData, fieldValidators, {
        type: "hidden",
        cssClasses: cssClasses,
        useLabel: false
    });
}

/**
 * Returns the DOM element for the hidden field.
 * @returns {HTMLDivElement} The DOM element for the hidden field.
 */
RenderHidden.prototype.getElement = function () {
    return this.wrapper;
};

/**
 * Adds the data for this field on the specified FormData instance.
 * @param data {FormData} The FormData instance to set the field data on.
 * @param options {{rawDataByAlias: boolean}} Optional. The options for setting the data.
 */
RenderHidden.prototype.setData = function (data, options) {
    FieldUtility.setData(data, this.element.value, options, this.alias, this.id);
};

/**
 * Checks the validity of the value in this field (adding inline validation messages if necessary).
 * @returns {Promise[]} An array of promises that resolve to validation results.
 */
RenderHidden.prototype.checkValidity = function () {
    return require("../utils/validation")
        .checkTextValidity(this, this.validators, this.element.value, this.wrapper);
};

/**
 * Ensure the prototype has the necessary functions.
 */
FieldUtility.initializeFieldPrototype(RenderHidden.prototype);

// Export the field renderer configuration.
module.exports = {
    key: "hidden",
    renderer: RenderHidden
};