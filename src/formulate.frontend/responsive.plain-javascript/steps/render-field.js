/**
 * Renders a formulate Field.
 * @param fieldData The data for the form field to render.
 * @param fieldRenderers The associative array of field rendering functions.
 * @returns {HTMLInputElement} The DOM element created by the field renderer.
 * @param fieldValidators The associative array of the field validating functions.
 */
function renderField(fieldData, fieldRenderers, fieldValidators) {

    // Variables.
    let renderer, cssClasses, renderResult;

    //TODO: Do something with fieldValidators.

    // Get the field rendering function for the current field type.
    renderer = fieldRenderers[fieldData.fieldType];

    // Create CSS classes to be attached to the DOM element.
    cssClasses = [];
    cssClasses.push("formulate__field");
    cssClasses.push("formulate__field--" + fieldData.fieldType);

    // Render the field.
    renderResult = new renderer(fieldData, cssClasses);

    // Return the rendered field (an object that has information about the rendered field).
    return renderResult;

}

// Export the function that renders a field.
module.exports = renderField;