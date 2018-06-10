/**
 * Renders a formulate Field.
 * @param fieldData The data for the form field to render.
 * @param fieldRenderers The associative array of field rendering functions.
 * @returns {HTMLInputElement} The DOM element created by the field renderer.
 */
function renderField(fieldData, fieldRenderers) {

    // Variables.
    let renderer, fieldElement, cssClasses;

    // Get the field rendering function for the current field type.
    renderer = fieldRenderers[fieldData.fieldType];

    // Create CSS classes to be attached to the DOM element.
    cssClasses = [];
    cssClasses.push("formulate__field");
    cssClasses.push("formulate__field--" + fieldData.fieldType);

    // Render the field.
    fieldElement = renderer(fieldData, cssClasses);

    // Return the DOM element for the field.
    return fieldElement;

}

// Export the function that renders a field.
module.exports = renderField;