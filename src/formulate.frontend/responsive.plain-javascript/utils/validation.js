/**
 * A collection of validation utility functions.
 * @constructor
 */
function ValidationUtilities() {
}

/**
 * Is the specified value set to something that is not null or undefined?
 * @param value {*} The value.
 * @returns {boolean} True, if the value is set to something; otherwise, false.
 */
ValidationUtilities.isValueSet = function (value) {
    return value !== null && value !== undefined && typeof(value) !== "undefined";
};

/**
 * Prepares the validators by passing configuration to their constructors.
 * @param validationData The data to pass to the validators.
 * @param fieldValidators The associative array of the field validating functions.
 * @returns {*} An array of prepared validators.
 */
ValidationUtilities.prepareValidators = function (validationData, fieldValidators) {

    // Validate input.
    if (!validationData || !fieldValidators) {
        return [];
    }

    // Variables.
    let i, validationOptions, validator, key, preparedValidators;

    // Process the validation data to prepare the validators.
    preparedValidators = [];
    for(i = 0; i < validationData.length; i++) {
        validationOptions = validationData[i];
        key = validationOptions.validationType;
        validator = fieldValidators[key];
        preparedValidators.push({
            validator: new validator(validationOptions.configuration),
            data: validationOptions
        });
    }

    // Return the prepared validators.
    return preparedValidators;

};

/**
 * Adds inline validation messages to a container.
 * @param containerElement {HTMLElement} The container to add the inline validation messages to.
 * @param messages {string[]} The validation messages to add.
 * @param priorListElement {HTMLUListElement} The list element returned the last time this
 * function was called.
 * @returns {HTMLUListElement} The list element containing the validation messages.
 */
ValidationUtilities.addValidationMessages = function (containerElement, messages, priorListElement) {

    // If there are no messages, remove the prior list element and return early.
    if (!messages || messages.length === 0) {
        if (priorListElement) {
            priorListElement.parentNode.removeChild(priorListElement);
        }
        return null;
    }

    // Variables.
    let i, listElement, message, itemElement;

    // Create the list element that contains the messages.
    listElement = document.createElement("ul");
    listElement.classList.add("formulate__inline-validation-summary");

    // Add the messages to the list element.
    for (i = 0; i < messages.length; i++) {
        message = messages[i];
        itemElement = document.createElement("li");
        itemElement.classList.add("formulate__inline-validation-summary__error");
        itemElement.appendChild(document.createTextNode(message));
        listElement.appendChild(itemElement);
    }

    // Remove the prior list element.
    if (priorListElement) {
        priorListElement.parentNode.removeChild(priorListElement);
    }

    // Add the new list element to the container.
    containerElement.appendChild(listElement);

    // Return the new list element (expected to be passed in on the subsequent call as the
    // prior list element).
    return listElement;

};

/**
 * Aggregates validation results into a single validation result.
 * @param validationPromises {Promise[]} An array of promises that will resolve to a validation result.
 * @returns {Promise} A promise that will resolve to the aggregate validation result.
 */
ValidationUtilities.aggregateValidations = function (validationPromises) {

    // Variables.
    let Promise = require("../polyfills/promise");

    // Return a promise that resolves to the result of all of the validations.
    return Promise.all(validationPromises)
        .then(function (results) {

            // Variables.
            let i, result, success, failures;

            // Extract all the failures from the validation results.
            failures = [];
            for (i = 0; i < results.length; i++) {
                result = results[i];
                if (!result.success) {
                    failures.push(result);
                }
            }

            // Success if there are no failures.
            success = failures.length === 0;
            if (success) {

                // Success.
                return {
                    success: true
                };

            } else {

                // Failure. Return validation messages for the failures.
                return {
                    success: false,
                    messages: failures.map(function (x) {
                        return x.message;
                    })
                };

            }

        });

};

/**
 * Checks the validity of a text array field, adding inline validation messages if there are
 * any validations that fail.
 * @param fieldRenderer The instance of the Formulate field renderer.
 * @param validators The prepared validator functions.
 * @param value The value to check the validity of.
 * @param containerElement The container element to add validation messages to.
 * @returns {Promise[]} An array of promises that resolve to validation results.
 */
ValidationUtilities.checkTextArrayValidity = function (fieldRenderer, validators, value, containerElement) {
    return ValidationUtilities.checkValidityCommon(fieldRenderer, validators, value, containerElement, "validateTextArray");
};

/**
 * Checks the validity of a text-based field, adding inline validation messages if there are
 * any validations that fail.
 * @param fieldRenderer The instance of the Formulate field renderer.
 * @param validators The prepared validator functions.
 * @param value The value to check the validity of.
 * @param containerElement The container element to add validation messages to.
 * @returns {Promise[]} An array of promises that resolve to validation results.
 */
ValidationUtilities.checkTextValidity = function (fieldRenderer, validators, value, containerElement) {
    return ValidationUtilities.checkValidityCommon(fieldRenderer, validators, value, containerElement, "validateText");
};

/**
 * Checks the validity of a boolean-based field, adding inline validation messages if there are
 * any validations that fail.
 * @param fieldRenderer The instance of the Formulate field renderer.
 * @param validators The prepared validator functions.
 * @param value The value to check the validity of.
 * @param containerElement The container element to add validation messages to.
 * @returns {Promise[]} An array of promises that resolve to validation results.
 */
ValidationUtilities.checkBoolValidity = function (fieldRenderer, validators, value, containerElement) {
    return ValidationUtilities.checkValidityCommon(fieldRenderer, validators, value, containerElement, "validateBool");
};

/**
 * Checks the validity of a file-based field, adding inline validation messages if there are
 * any validations that fail.
 * @param fieldRenderer The instance of the Formulate field renderer.
 * @param validators The prepared validator functions.
 * @param value The value to check the validity of.
 * @param containerElement The container element to add validation messages to.
 * @returns {Promise[]} An array of promises that resolve to validation results.
 */
ValidationUtilities.checkFileValidity = function (fieldRenderer, validators, value, containerElement) {
    return ValidationUtilities.checkValidityCommon(fieldRenderer, validators, value, containerElement, "validateFile");
};

/**
 * Checks the validity of a field, adding inline validation messages if there are any validations that fail.
 * @param fieldRenderer The instance of the Formulate field renderer.
 * @param validators The prepared validator functions.
 * @param value The value to check the validity of.
 * @param containerElement The container element to add validation messages to.
 * @param validityFnName The name of the validity function (e.g., "validateText").
 * @returns {Promise[]} An array of promises that resolve to validation results.
 */
ValidationUtilities.checkValidityCommon = function (fieldRenderer, validators, value, containerElement, validityFnName) {

    // Variables.
    let i, validator, validationResults;

    // Check each validator for the validity of the value in this field.
    validationResults = [];
    for (i = 0; i < validators.length; i++) {
        validator = validators[i];
        validationResults.push(checkValidity(validator, value, validityFnName));
    }

    // Add inline validation messages.
    ValidationUtilities.aggregateValidations(validationResults)
        .then(function (result) {

            // Add inline validation messages.
            fieldRenderer.validationListElement = ValidationUtilities.addValidationMessages(
                containerElement, result.messages, fieldRenderer.validationListElement);

            // Add or remove validation error CSS class.
            if (result.success) {
                containerElement.classList.remove("formulate__field--validation-error");
            } else {
                containerElement.classList.add("formulate__field--validation-error");
            }

        });

    // Return the validation results.
    return validationResults;

};

/**
 * Validates the specified value against the specified validator.
 * @param validator The validator.
 * @param value The value to validate.
 * @param validityFnName The name of the validity function (e.g., "validateText").
 * @returns {Promise} A promise that will resolve to the validation result.
 */
function checkValidity(validator, value, validityFnName) {
    return validator.validator[validityFnName](value)
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

// Export the validation utility functions.
module.exports = ValidationUtilities;