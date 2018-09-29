// Dependencies.
let FieldUtility = require("../utils/field"),
    AddClasses = require("../utils/add-classes"),
    Validation = require("../utils/validation");

/**
 * Renders a recaptcha field.
 * @param fieldData The field data that should be used to render the recaptcha field.
 * @param fieldValidators The associative array of the field validating functions.
 * @param cssClasses The CSS classes to attach to the element.
 * @constructor
 */
function RenderRecaptcha(fieldData, fieldValidators, cssClasses) {

    // Create field and attach classes/attributes.
    let el = document.createElement("div");
    el.classList.add("g-recaptcha");
    el.setAttribute("data-sitekey", fieldData.configuration.key);
    if (cssClasses) {
        AddClasses(el, cssClasses);
    }

    // Retain field and ID for later reference.
    this.wrapper = el;
    this.id = fieldData.id;

    // Prepare the validators and retain them for later use.
    this.validators = Validation.prepareValidators(fieldData.validations, fieldValidators);

}

/**
 * Returns the DOM element for the recaptcha field.
 * @returns {HTMLDivElement} The DOM element for the recaptcha field.
 */
RenderRecaptcha.prototype.getElement = function () {
    return this.wrapper;
};

/**
 * Returns the DOM element for the recaptcha field.
 * @returns {HTMLDivElement} The DOM element that wraps the recaptcha field.
 */
RenderRecaptcha.prototype.getHiddenElement = function () {
    return this.wrapper.querySelector("[name='g-recaptcha-response']");
};

/**
 * Adds the data for this field on the specified FormData instance.
 * @param data {FormData} The FormData instance to set the field data on.
 * @param options {{rawDataByAlias: boolean}} Optional. The options for setting the data.
 */
RenderRecaptcha.prototype.setData = function (data, options) {
    let element = this.getHiddenElement(),
        value = element
            ? element.value
            : null;
    FieldUtility.setData(data, value, options, this.alias, this.id);
};

/**
 * Checks the validity of the value in this field (adding inline validation messages if necessary).
 * @returns {Promise[]} An array of promises that resolve to validation results.
 */
RenderRecaptcha.prototype.checkValidity = function () {
    //TODO: Should I block the form from validating when the captcha hasn't been filled out?
    let element = this.getHiddenElement(),
        value = element
            ? element.value
            : null;
    return require("../utils/validation")
        .checkTextValidity(this, this.validators, value, this.wrapper);
};

/**
 * Ensure the prototype has the necessary functions.
 */
FieldUtility.initializeFieldPrototype(RenderRecaptcha.prototype);

// Export the field renderer configuration.
module.exports = {
    key: "recaptcha",
    renderer: RenderRecaptcha
};