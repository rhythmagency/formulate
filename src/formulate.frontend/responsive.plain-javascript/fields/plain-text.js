/**
 * Renders a text field.
 * @param fieldData The field data that should be used to render the text field.
 * @param fieldValidators The associative array of the field validating functions.
 * @param cssClasses The CSS classes to attach to the element.
 * @constructor
 */
function RenderText(fieldData, fieldValidators, cssClasses) {

    // Variables.
    let fieldElement, wrapperElement, labelElement;

    // Create wrapper element.
    wrapperElement = document.createElement("div");

    // Create label element.
    labelElement = document.createElement("label");
    labelElement.appendChild(document.createTextNode(fieldData.label));

    // Attach CSS classes.
    require("../utils/add-classes")(wrapperElement, cssClasses);

    // Create element.
    fieldElement = document.createElement("input");
    fieldElement.type = "text";
    fieldElement.setAttribute("placeholder", fieldData.label);

    // Add elements to wrapper.
    wrapperElement.appendChild(labelElement);
    wrapperElement.appendChild(fieldElement);

    // Retain DOM elements and field properties.
    this.wrapper = wrapperElement;
    this.element = fieldElement;
    this.id = fieldData.id;
    this.alias = fieldData.alias;

    // Prepare the validators and retain them for later use.
    this.validators = require("../utils/validation").prepareValidators(fieldData.validations, fieldValidators);

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

    // Variables.
    let value = this.element.value;

    // Adjust options.
    options = options || {};
    options.rawDataByAlias = options.rawDataByAlias || false;

    // Set data.
    if (options.rawDataByAlias && this.alias) {
        data[this.alias] = value;
    } else {
        data.append(this.id, value);
    }

};

//TODO: Make a "commonCheckValidity" or something to avoid writing this over and over.
/**
 * Checks the validity of the value in this field (adding inline validation messages if necessary).
 * @returns {Promise[]} An array of promises that resolve to validation results.
 */
RenderText.prototype.checkValidity = function () {

    // Dependencies.
    let aggregateValidations = require("../utils/validation").aggregateValidations,
        addValidationMessages = require("../utils/validation").addValidationMessages;

    // Variables.
    let self = this, i, validator, validationResults, value;

    // Check each validator for the validity of the value in this field.
    validationResults = [];
    value = this.element.value;
    for (i = 0; i < this.validators.length; i++) {
        validator = this.validators[i];
        validationResults.push(checkValidity(validator, value));
    }

    // Add inline validation messages.
    aggregateValidations(validationResults)
        .then(function (result) {

            // Add inline validation messages.
            self.validationListElement = addValidationMessages(
                self.wrapper, result.messages, self.validationListElement);

            // Add or remove validation error CSS class.
            if (result.success) {
                self.wrapper.classList.remove("formulate__field--validation-error");
            } else {
                self.wrapper.classList.add("formulate__field--validation-error");
            }

        });

    // Return the validation results.
    return validationResults;

};

/**
 * Validates the specified value against the specified validator.
 * @param validator The validator.
 * @param value The value to validate.
 * @returns {Promise} A promise that will resolve to the validation result.
 */
function checkValidity(validator, value) {
    return validator.validator.validateText(value)
        .then(function (result) {
            if (result) {

                // Success.
                return {
                    success: true
                };

            } else {

                // Failure. Return validation message.
                return {
                    success: false,
                    message: validator.data.configuration.message
                };

            }
        });
}

// Export the field renderer configuration.
module.exports = {
    key: "text",
    renderer: RenderText
};