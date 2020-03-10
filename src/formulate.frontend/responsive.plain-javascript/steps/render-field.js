/**
 * Renders a formulate Field.
 * @param fieldData The data for the form field to render.
 * @param fieldRenderers The associative array of field rendering functions.
 * @param fieldValidators The associative array of the field validating functions.
 * @param extraOptions Additional options that are less commonly used.
 * @returns {HTMLInputElement} The DOM element created by the field renderer.
 */
function renderField(fieldData, fieldRenderers, fieldValidators, extraOptions) {

    // Variables.
    let renderer, cssClasses, renderResult;

    // Get the field rendering function for the current field type.
    renderer = fieldRenderers[fieldData.fieldType];

    // Create CSS classes to be attached to the DOM element.
    cssClasses = [];
    cssClasses.push("formulate__field");
    cssClasses.push("formulate__field--" + fieldData.fieldType);

    // Render the field.
    if (!renderer) {
        throw Error("Unable to find renderer for field of type " + fieldData.fieldType + ".");
    }
    renderResult = new renderer(fieldData, fieldValidators, cssClasses, extraOptions);

    // Set the field step.
    renderResult.stepIndex = extraOptions.stepIndex;

    // Return the rendered field (an object that has information about the rendered field).
    return renderResult;

}

// Export the function that renders a field.
module.exports = renderField;