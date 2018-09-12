// Dependencies.
let FieldUtility = require("../utils/field");

/**
 * Renders a date field.
 * @param fieldData The field data that should be used to render the date field.
 * @param fieldValidators The associative array of the field validating functions.
 * @param cssClasses The CSS classes to attach to the element.
 * @constructor
 */
function RenderDate(fieldData, fieldValidators, cssClasses) {
    FieldUtility.initializeField(this, fieldData, fieldValidators, {
        type: "date",
        cssClasses: cssClasses
    });
}

/**
 * Returns the DOM element for the date field.
 * @returns {HTMLDivElement} The DOM element for the date field.
 */
RenderDate.prototype.getElement = function () {
    return this.wrapper;
};

/**
 * Adds the data for this field on the specified FormData instance.
 * @param data {FormData} The FormData instance to set the field data on.
 * @param options {{rawDataByAlias: boolean}} Optional. The options for setting the data.
 */
RenderDate.prototype.setData = function (data, options) {
    FieldUtility.setData(data, this.element.value, options, this.alias, this.id);
};

/**
 * Checks the validity of the value in this field (adding inline validation messages if necessary).
 * @returns {Promise[]} An array of promises that resolve to validation results.
 */
RenderDate.prototype.checkValidity = function () {
    return require("../utils/validation")
        .checkTextValidity(this, this.validators, this.element.value, this.wrapper);
};

/**
 * Ensure the prototype has the necessary functions.
 */
FieldUtility.initializeFieldPrototype(RenderDate.prototype);

// Export the field renderer configuration.
module.exports = {
    key: "date",
    renderer: RenderDate
};