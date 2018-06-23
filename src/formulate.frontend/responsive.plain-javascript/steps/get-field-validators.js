/**
 * Returns the field validators in an associative array with the keys being the
 * type of field validator (e.g., "regex" or "required") and the value being the
 * function that validates the field.
 * @returns {{}} The associative array of field validators.
 */
function getFieldValidators() {

    // Variables.
    let i, validators, extraValidators, field, fieldKey, validatorMap;

    // Get the field validators.
    validators = [
        require("../validators/regex"),
        require("../validators/required")
    ];
    extraValidators = window["formulate-plain-js-validators"] || [];
    validators = validators.concat(extraValidators);

    // Store the field validators to an associative array.
    validatorMap = {};
    for (i = 0; i < validators.length; i++) {
        field = validators[i];
        fieldKey = field.key;
        validatorMap[fieldKey] = field.validator;
    }

    // Return the associative array of field validators.
    return validatorMap;

}

// Export the function that gets the field validators.
module.exports = getFieldValidators;