(function(){function e(t,n,r){function s(o,u){if(!n[o]){if(!t[o]){var a=typeof require=="function"&&require;if(!u&&a)return a(o,!0);if(i)return i(o,!0);var f=new Error("Cannot find module '"+o+"'");throw f.code="MODULE_NOT_FOUND",f}var l=n[o]={exports:{}};t[o][0].call(l.exports,function(e){var n=t[o][1][e];return s(n?n:e)},l,l.exports,e,t,n,r)}return n[o].exports}var i=typeof require=="function"&&require;for(var o=0;o<r.length;o++)s(r[o]);return s}return e})()({1:[function(require,module,exports){
/**
 * Renders a submit button.
 * @param fieldData The field data that should be used to render the button.
 * @param cssClasses The CSS classes to attach to the element.
 * @returns {HTMLButtonElement} The DOM element for the button.
 */
function renderButton(fieldData, cssClasses) {

    // Variables.
    let i, fieldElement;

    // Create element.
    fieldElement = document.createElement("button");
    fieldElement.type = "submit";
    fieldElement.appendChild(document.createTextNode(fieldData.label));

    // Add CSS classes.
    require("../utils/add-classes")(fieldElement, cssClasses);

    // Return button DOM element.
    return fieldElement;

}

// Export the field renderer configuration.
module.exports = {
    key: "button",
    renderer: renderButton
};
},{"../utils/add-classes":11}],2:[function(require,module,exports){
/**
 * Renders a text field.
 * @param fieldData The field data that should be used to render the text field.
 * @param cssClasses The CSS classes to attach to the element.
 * @returns {HTMLInputElement} The DOM element for the text field.
 */
function renderText(fieldData, cssClasses) {

    // Variables.
    let i, fieldElement;

    // Create element.
    fieldElement = document.createElement("input");
    fieldElement.type = "text";

    // Attach CSS classes.
    require("../utils/add-classes")(fieldElement, cssClasses);

    // Return text field DOM element.
    return fieldElement;

}

// Export the field renderer configuration.
module.exports = {
    key: "text",
    renderer: renderText
};
},{"../utils/add-classes":11}],3:[function(require,module,exports){
// Variables.
let forms, renderers;

// Get field renderers.
renderers = require("./steps/get-field-renderers")();

// Get form data.
forms = require("./steps/get-form-data")();

// Render the forms.
require("./steps/render-forms")(forms, renderers);
},{"./steps/get-field-renderers":4,"./steps/get-form-data":5,"./steps/render-forms":9}],4:[function(require,module,exports){
/**
 * Returns the field renderers in an associative array with the keys being the
 * type of field renderer (e.g., "text" or "button") and the value being the
 * function that renders the field.
 * @returns {{}} The associative array of field renderers.
 */
function getFieldRenderers() {

    // Variables.
    let i, fields, field, fieldKey, fieldMap;

    // Get the field renderers.
    fields = [
        require("../fields/plain-text"),
        require("../fields/plain-button")
    ];

    // Store the field renderers to an associative array.
    fieldMap = {};
    for (i = 0; i < fields.length; i++) {
        field = fields[i];
        fieldKey = field.key;
        fieldMap[fieldKey] = field.renderer;
    }

    // Return the associative array of field renderers.
    return fieldMap;

}

// Export the function that gets the field renderers.
module.exports = getFieldRenderers;
},{"../fields/plain-button":1,"../fields/plain-text":2}],5:[function(require,module,exports){
/**
 * Returns the data for all Formulate forms from the window object.
 * @returns {Array} The forms.
 */
function getFormData() {

    // Variables.
    let key, forms;

    // Get the forms from the window object.
    key = "formulate-plain-js-forms";
    forms = window[key] || [];

    // Reset the windows object in case subsequent forms are added later.
    window[key] = [];

    // Return the data for the forms.
    return forms;

}

// Export the function that gets the form data.
module.exports = getFormData;
},{}],6:[function(require,module,exports){
/**
 * Renders a cell within a row of a Formulate form.
 * @param columnCount The number of columns this cell spans.
 * @returns {HTMLDivElement} The DOM element for the cell.
 */
function renderCell(columnCount) {

    // Variables.
    let cellElement;

    // Create the element.
    cellElement = document.createElement("div");

    // Add CSS classes to element.
    cellElement.classList.add("formulate__cell");
    cellElement.classList.add("formulate__cell--" + columnCount.toString() + "-columns");

    // Return the DOM element for the cell.
    return cellElement;

}

// Export the function that renders a cell.
module.exports = renderCell;
},{}],7:[function(require,module,exports){
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
},{}],8:[function(require,module,exports){
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
},{"../utils/map-fields-by-id":12,"./render-cell":6,"./render-field":7,"./render-row":10}],9:[function(require,module,exports){
/**
 * Renders the Formulate forms, inserting them into the appropriate place
 * in the DOM.
 * @param forms The data for the forms to render.
 * @param fieldRenderers The associative array of the field rendering functions.
 */
function renderForms(forms, fieldRenderers) {

    // Variables.
    let i, form, formId, placeholderElement, formElement, formContainer;

    // Process each form.
    for (i = 0; i < forms.length; i++) {

        // Variables.
        form = forms[i];

        // Create the form DOM element.
        formElement = document.createElement("form");

        // Add CSS class to DOM element.
        formElement.classList.add("formulate__form");

        // Render the contents of the form.
        require("./render-form")(form, formElement, fieldRenderers);

        // Get the placeholder element to insert the form before.
        formId = "formulate-form-" + form.data.randomId;
        placeholderElement = document.getElementById(formId);

        // Insert the form before the placeholder.
        formContainer = placeholderElement.parentNode;
        formContainer.insertBefore(formElement, placeholderElement);

    }

}

// Export the function that renders forms.
module.exports = renderForms;
},{"./render-form":8}],10:[function(require,module,exports){
/**
 * Renders a row in a Formulate form.
 * @returns {HTMLDivElement} The DOM element for the row.
 */
function renderRow() {

    // Variables.
    let rowElement;

    // Create the DOM element for the row.
    rowElement = document.createElement("div");

    // Add a CSS class to the DOM element.
    rowElement.classList.add("formulate__row");

    // Return the DOM element for the row.
    return rowElement;

}

// Export the function that renders a row.
module.exports = renderRow;
},{}],11:[function(require,module,exports){
/**
 * Adds CSS classes to a DOM element
 * @param element The DOM element to add classes to.
 * @param cssClasses The CSS classes to add to the element.
 */
function addClasses(element, cssClasses) {

    // Variables.
    let i;

    // Add each CSS class to the element.
    for (i = 0; i < cssClasses.length; i++) {
        element.classList.add(cssClasses[i]);
    }

}

// Export the function that adds CSS classes to an element.
module.exports = addClasses;
},{}],12:[function(require,module,exports){
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
},{}]},{},[3]);
