// Dependencies.
let FieldUtility = require("../utils/field");

/**
 * Renders a radio button list field.
 * @param fieldData The field data that should be used to render the radio button list field.
 * @param fieldValidators The associative array of the field validating functions.
 * @param cssClasses The CSS classes to attach to the element.
 * @constructor
 */
function RenderRadioList(fieldData, fieldValidators, cssClasses) {

    // Variables.
    let i, item, label, value, wrapperElement, labelElement,
        elements = [],
        items = fieldData.configuration.items,
        name = FieldUtility.generateId("radio-button-list-");

    // Add each radio button.
    for (i = 0; i < items.length; i++) {

        // Variables.
        item = items[i];
        value = item.value;
        label = item.label;

        // Add radio button.
        FieldUtility.initializeField(this, fieldData, fieldValidators, {
            type: "radio",
            cssClasses: cssClasses,
            usePlaceholder: false,
            wrapperElement: wrapperElement,
            nestFieldInLabel: true,
            value: value,
            label: label,
            wrapLabelText: true,
            fieldBeforeLabelText: true,
            name: name
        });

        // Remember wrapper element for next iteration.
        wrapperElement = this.wrapper;

        // Remember the radio button input element.
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
 * Returns the DOM element that wraps the radio buttons.
 * @returns {HTMLDivElement} The DOM element that wraps the radio buttons.
 */
RenderRadioList.prototype.getElement = function () {
    return this.wrapper;
};

/**
 * Adds the data for this field on the specified FormData instance.
 * @param data {FormData} The FormData instance to set the field data on.
 * @param options {{rawDataByAlias: boolean}} Optional. The options for setting the data.
 */
RenderRadioList.prototype.setData = function (data, options) {
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
RenderRadioList.prototype.checkValidity = function () {
    let value,
        values = this.elements
        .filter(function (element) {
            return element.checked;
        })
        .map(function (element) {
            return element.checked
                ? element.value
                : null;
        });
    if (values.length) {
        value = values[0];
    } else {
        value = null;
    }
    return require("../utils/validation")
        .checkTextValidity(this, this.validators, value, this.wrapper);
};

/**
 * Ensure the prototype has the necessary functions.
 */
FieldUtility.initializeFieldPrototype(RenderRadioList.prototype);

// Export the field renderer configuration.
module.exports = {
    key: "radio-list",
    renderer: RenderRadioList
};