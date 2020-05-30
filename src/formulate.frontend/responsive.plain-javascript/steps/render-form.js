/**
 * Renders a Formulate form.
 * @param formData The data to render a form for.
 * @param formElement The DOM element to insert elements into.
 * @param placeholderElement The element that the form will be inserted before.
 * @param fieldRenderers The associative array of field rendering functions.
 * @param fieldValidators The associative array of the field validating functions.
 */
function renderForm(formData, formElement, placeholderElement, fieldRenderers, fieldValidators) {

    // Variables.
    let i, j, k, row, rows, rowElement, cells, cell, fields, fieldId,
        columnCount, cellElement, fieldElement, fieldsData, fieldMap,
        field, renderedFields, renderedField, stepIndex, isActiveStep;

    // Map fields to an associative array for quick lookups.
    fieldsData = formData.data.fields;
    fieldMap = require("../utils/map-fields-by-id")(fieldsData);

    // Process each row in the form.
    rows = formData.data.rows;
    renderedFields = [];
    isActiveStep = true;
    stepIndex = 0;
    for (i = 0; i < rows.length; i++) {

        // Create the row.
        row = rows[i];
        cells = row.cells;
        if (row.isStep) {
            stepIndex++;
            isActiveStep = false;
            continue;
        } else {
            rowElement = require("./render-row")(stepIndex, isActiveStep);
            formElement.appendChild(rowElement);
        }

        // Process each cell in this row.
        for (j = 0; j < cells.length; j++) {

            // Create the cell.
            cell = cells[j];
            fields = cell.fields;
            columnCount = cell.columns;
            cellElement = require("./render-cell")(columnCount);
            rowElement.appendChild(cellElement);

            // Process each field in this cell.
            for (k = 0; k < fields.length; k++) {

                // Create the field.
                fieldId = fields[k].id;
                field = fieldMap[fieldId];
                renderedField = require("./render-field")(field, fieldRenderers, fieldValidators, {
                    formElement: formElement,
                    placeholderElement: placeholderElement,
                    stepIndex
                });
                renderedFields.push(renderedField);
                fieldElement = renderedField.getElement();
                prepopulateValue(fieldElement, field.initialValue)
                cellElement.appendChild(fieldElement);

            }

        }

    }

    // Return the rendered fields.
    return renderedFields;

}

/**
 * Sets the initial values of fields.
 * @param element The field wrapper element.
 * @param values The array of values.
 */
function prepopulateValue(element, values) {

    // Exit early if the element or values are invalid.
    if (!element || !values || !values.length) {
        return;
    }

    // Variables.
    let inputs = element.querySelectorAll('input, textarea, select'),
        input, i;

    // Exit early if there are no inputs.
    if (!inputs.length) {
        return;
    }

    // Is this a single input, or a list of inputs?
    if (inputs.length === 1) {

        // Is this a checkbox?
        input = inputs[0];
        if (input.type === 'checkbox') {
            input.checked = true;
        }
        else if (values.length === 1) {

            // Likely a text field.
            input.value = values[0];

        }

    } else {

        // Loop over list of inputs (checkboxes or radio buttons).
        for (i = 0; i < inputs.length; i++) {
            input = inputs[i];
            if (['checkbox', 'radio'].indexOf(input.type) >= 0) {
                if (values.indexOf(input.value) >= 0) {
                    input.checked = true;
                }
            }
        }

    }

}

// Export the function that renders a form.
module.exports = renderForm;