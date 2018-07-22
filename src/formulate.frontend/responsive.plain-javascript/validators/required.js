/**
 * Validates a field to ensure its value is set.
 * @constructor
 */
function RequiredValidator() {
}

/**
 * Validates the the specified text is set (i.e., not null/empty).
 * @param value {string} The text value to validate.
 * @returns {Promise} A promise that resolves to true, if the text is valid; otherwise, false.
 */
RequiredValidator.prototype.validateText = function (value) {
    return new (require("../polyfills/promise"))(function (resolve) {
        let isValueSet = require("../utils/validation").isValueSet,
            valid = isValueSet(value) && value.hasOwnProperty("length") && value.length > 0;
        resolve(valid);
    });
};

/**
 * Validates the the specified boolean is true.
 * @param value {boolean} The boolean value to validate.
 * @returns {Promise} A promise that resolves to true, if the boolean is true; otherwise, false.
 */
RequiredValidator.prototype.validateBool = function (value) {
    return new (require("../polyfills/promise"))(function (resolve) {
        resolve(value === true);
    });
};

/**
 * Validates that the specified array of text values are all set and that the array is not null/empty.
 * @param value {string[]} The array of text values.
 * @returns {Promise} A promise that resolves to true, if the array of text values is valid; otherwise, false.
 */
RequiredValidator.prototype.validateTextArray = function (value) {
    return new (require("../polyfills/promise"))(function (resolve) {

        // Variables.
        let i, item,
            isValueSet = require("../utils/validation").isValueSet;

        // Check if the value is a valid array.
        if (!isValueSet(value) || !Array.isArray(value)) {
            resolve(false);
            return;
        }

        // Check if the array has at least one value.
        if (value.length === 0) {
            resolve(false);
            return;
        }

        // Check if each value in the array is set to some valid text.
        for (i = 0; i < value.length; i++) {
            item = value[i];
            if (!this.validateText(item)) {
                resolve(false);
                return;
            }
        }

        // All tests passed. Array value is valid.
        resolve(true);

    });
};

/**
 * Validates that the specified file has been selected.
 * @param value {*} The file value.
 * @returns {Promise} A promise that resolves to true, if the file is selected; otherwise, false.
 */
RequiredValidator.prototype.validateFile = function (value) {
    return new (require("../polyfills/promise"))(function (resolve) {
        //TODO: ...
        resolve(true);
    });
};

// Export the field validator configuration.
module.exports = {
    key: "required",
    validator: RequiredValidator
};