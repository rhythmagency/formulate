// Dependencies.
let FieldUtility = require("../utils/field"),
    AddClasses = require("../utils/add-classes"),
    Validation = require("../utils/validation");

// Variables.
let scriptInjected = false;

/**
 * Renders a recaptcha field.
 * @param fieldData The field data that should be used to render the recaptcha field.
 * @param fieldValidators The associative array of the field validating functions.
 * @param cssClasses The CSS classes to attach to the element.
 * @constructor
 */
function RenderRecaptcha(fieldData, fieldValidators, cssClasses) {

    // Notify the developer if they haven't configured recaptcha in the web.config yet.
    if (!fieldData.configuration.key) {

        // Using eval so build tools are less likely to remove this line.
        eval("alert('The reCAPTCHA keys need to be configured in the web.config. They should already be there; you just need to fill in the values.');");

    }

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

    // Ensure there is at least one validator (to ensure the recaptcha is solved).
    let fieldValidations = this.ensureValidator(fieldData.validations);

    // Prepare the validators and retain them for later use.
    this.validators = Validation.prepareValidators(fieldValidations, fieldValidators);

    // Inject the Google Recaptcha JavaScript if it hasn't been added to the page yet.
    this.ensureGoogleScript();

}

/**
 * Ensures that there is at least one validator for the recaptcha.
 * @param validations {Array<{}>} The validations that have been specified on the field already.
 * @returns {Array<{}>} The specified validations, or an array containing a single newly created validation.
 */
RenderRecaptcha.prototype.ensureValidator = function (validations) {
    if (!validations.length) {
        return [{
            alias: "recaptchaRequired",
            configuration: {
                message: "Please solve the recaptcha."
            },
            validationType: "required"
        }];
    } else {
        return validations;
    }
};

/**
 * Ensures the Google Recaptcha script is injected.
 */
RenderRecaptcha.prototype.ensureGoogleScript = function () {

    // If the script was already injected, exit early.
    if (scriptInjected || typeof(grecaptcha) !== "undefined") {
        scriptInjected = true;
        return;
    }


    // Inject the script.
    let script = document.createElement("script");
    script.src = "https://www.google.com/recaptcha/api.js";
    script.async = true;
    document.head.appendChild(script);
    scriptInjected = true;

};

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