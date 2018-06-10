/**
 * Renders a Formulate form.
 * @param formData The data to render a form for.
 * @param formElement The DOM element to insert elements into.
 * @param fieldRenderers The associative array of field rendering functions.
 */
function renderForm(formData, formElement, fieldRenderers) {

    // Variables.
    let i, j, k, row, rows, rowElement, cells, cell, fields, fieldId,
        columnCount, cellElement, fieldElement, fieldsData, fieldMap,
        field;

    // Map fields to an associative array for quick lookups.
    fieldsData = formData.data.fields;
    fieldMap = require("../utils/map-fields-by-id")(fieldsData);

    // Process each row in the form.
    rows = formData.data.rows;
    for(i = 0; i < rows.length; i++) {

        // Create the row.
        row = rows[i];
        cells = row.cells;
        rowElement = require("./render-row")();
        formElement.appendChild(rowElement);

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
                fieldElement = require("./render-field")(field, fieldRenderers);
                cellElement.appendChild(fieldElement);

            }

        }

    }

}

// Export the function that renders a form.
module.exports = renderForm;