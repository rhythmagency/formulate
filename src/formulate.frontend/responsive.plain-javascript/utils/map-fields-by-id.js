/**
 * Maps an array of Formulate fields into an associative array, with the field
 * ID as the key and the field as the value.
 * @param fields The array of fields to map.
 * @returns {{}} The associative array of fields.
 */
function mapFields(fields) {

    // Variables.
    let i, fieldMap, field, fieldId;

    // Process each field.
    fieldMap = {};
    for (i = 0; i < fields.length; i++) {

        // Store the field in the associative array.
        field = fields[i];
        fieldId = field.id;
        fieldMap[fieldId] = field;

    }

    // Return the associative array of fields.
    return fieldMap;

}

// Export the function that maps fields.
module.exports = mapFields;