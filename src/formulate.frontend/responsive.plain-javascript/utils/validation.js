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

// Export the validation utility functions.
module.exports = ValidationUtilities;