// Dependencies.
let FieldUtility = require("../utils/field");

/**
 * Renders a checkbox list field.
 * @param fieldData The field data that should be used to render the checkbox list field.
 * @param fieldValidators The associative array of the field validating functions.
 * @param cssClasses The CSS classes to attach to the element.
 * @constructor
 */
function RenderCheckboxList(fieldData, fieldValidators, cssClasses) {

    // Variables.
    let items = fieldData.configuration.items, i, item, label, value, wrapperElement, elements = [],
        labelElement;

    // Add each checkbox.
    for (i = 0; i < items.length; i++) {

        // Variables.
        item = items[i];
        value = item.value;
        label = item.label;

        // Add checkbox.
        FieldUtility.initializeField(this, fieldData, fieldValidators, {
            type: "checkbox",
            cssClasses: cssClasses,
            usePlaceholder: false,
            wrapperElement: wrapperElement,
            nestFieldInLabel: true,
            value: value,
            label: label,
            wrapLabelText: true,
            fieldBeforeLabelText: true
        });

        // Remember wrapper element for next iteration.
        wrapperElement = this.wrapper;

        // Remember the checkbox input element.
        elements.push(this.element);

        // Add a label if it hasn't been added yet.
        if (!labelElement) {
            labelElement = document.createElement("label");
            labelElement.classList.add("formulate__field__label");
            labelElement.classList.add("formulate__field__label--group");
            labelElement.appendChild(document.createTextNode(fieldData.label));
            wrapperElement.insertBefore(labelElement, this.element.parentNode);
        }

    }

    // Set instance variables.
    this.elements = elements;

}

/**
 * Returns the DOM element that wraps the checkboxes.
 * @returns {HTMLDivElement} The DOM element that wraps the checkboxes.
 */
RenderCheckboxList.prototype.getElement = function () {
    return this.wrapper;
};

/**
 * Adds the data for this field on the specified FormData instance.
 * @param data {FormData} The FormData instance to set the field data on.
 * @param options {{rawDataByAlias: boolean}} Optional. The options for setting the data.
 */
RenderCheckboxList.prototype.setData = function (data, options) {
    let i, element;
    for (i = 0; i < this.elements.length; i++) {
        element = this.elements[i];
        if (element.checked) {
            FieldUtility.setData(data, element.value, options, this.alias, this.id);
        }
    }
};

/**
 * Checks the validity of the value in this field (adding inline validation messages if necessary).
 * @returns {Promise[]} An array of promises that resolve to validation results.
 */
RenderCheckboxList.prototype.checkValidity = function () {
    let values = this.elements
        .filter(function (element) {
            return element.checked;
        })
        .map(function (element) {
            return element.checked
                ? element.value
                : null;
        });
    return require("../utils/validation")
        .checkTextArrayValidity(this, this.validators, values, this.wrapper);
};

/**
 * Ensure the prototype has the necessary functions.
 */
FieldUtility.initializeFieldPrototype(RenderCheckboxList.prototype);

// Export the field renderer configuration.
module.exports = {
    key: "checkbox-list",
    renderer: RenderCheckboxList
};