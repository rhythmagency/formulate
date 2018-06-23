/**
 * Validates a field to ensure its value matches a regular expression.
 * @constructor
 */
function RegexValidator(regexPattern, flags) {
    this.regex = new RegExp(regexPattern, flags);
}

/**
 * Validates the the specified text matches the regex.
 * @param value {string} The text value to validate.
 * @returns {boolean} True, if the text is valid; otherwise, false.
 */
RegexValidator.prototype.validateText = function (value) {
    return (new (require("../polyfills/promise")))(function (resolve) {
        let isValueSet = require("../utils/validation").isValueSet;
        if (!isValueSet()) {
            value = "";
        }
        resolve(this.regex.test(value));
    });
};

/**
 * Validates that the specified array of text values all match the regex and that the array is not null/empty.
 * @param value {string[]} The array of text values.
 * @returns {boolean} True, if the array of text values is valid; otherwise, false.
 */
RegexValidator.prototype.validateTextArray = function (value) {
    return (new (require("../polyfills/promise")))(function (resolve) {

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

        // Check if each value in the array matches the regex.
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
 * Validates that the specified filename matches the regex.
 * @param value {*} The file value.
 * @returns {boolean} True, if the file is selected.
 */
RegexValidator.prototype.validateFile = function (value) {
    return (new (require("../polyfills/promise")))(function (resolve) {
        //TODO: ...
        resolve(true);
    });
};

// Export the field validator configuration.
module.exports = {
    key: "regex",
    validator: RegexValidator
};