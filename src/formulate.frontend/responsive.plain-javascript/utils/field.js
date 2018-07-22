// Variables.
let idCount = 0;

/**
 * Utility functions for working with form fields.
 * @constructor
 */
function Field() {
}

/**
 * Initializes a field renderer prototype to ensure it has all the necessary functions.
 * @param fieldPrototype The prototype for the field renderer.
 */
Field.initializeFieldPrototype = function (fieldPrototype) {

    // Use a fallback for setData?
    if (!fieldPrototype.setData) {
        fieldPrototype.setData = function () {};
    }

    // Use a fallback for checkValidity?
    if (!fieldPrototype.checkValidity) {
        fieldPrototype.checkValidity = function () {
            return [];
        };
    }

};

/**
 * Adds the data for a field to the specified instance of either FormData or an object.
 * @param data The FormData or object to set the field data on.
 * @param value The value to set.
 * @param options {{rawDataByAlias: boolean}} Optional. The options for setting the data.
 * @param alias The field alias.
 * @param id The field ID.
 */
Field.setData = function (data, value, options, alias, id) {

    // Adjust options.
    options = options || {};
    options.rawDataByAlias = options.rawDataByAlias || false;

    // Set data.
    if (options.rawDataByAlias && alias) {
        data[alias] = value;
    } else {
        data.append(id, value);
    }

};

/**
 * Initializes a field renderer.
 * @param fieldRenderer The field renderer to initialize.
 * @param fieldData The field data that should be used to render the field.
 * @param fieldValidators The associative array of the field validating functions.
 * @param options {{type: string, usePlaceholder: boolean, useLabel: boolean, useWrapper: boolean, cssClasses: string[], nodeName: string}}
 *        The options to use when constructing the field.
 */
Field.initializeField = function (fieldRenderer, fieldData, fieldValidators, options) {

    // Variables.
    let fieldElement, wrapperElement, labelElement, useWrapper, fieldId;

    // Create element.
    fieldElement = document.createElement(options.nodeName || "input");
    if (options.type) {
        fieldElement.type = options.type;
    }

    // Set aria label.
    fieldElement.setAttribute("aria-label", fieldData.label);

    // Create wrapper element, or just use the field element as the wrapper.
    useWrapper = options.useWrapper !== false;
    wrapperElement = useWrapper
        ? document.createElement("div")
        : fieldElement;

    // Attach CSS classes.
    if (options.cssClasses) {
        require("./add-classes")(wrapperElement, options.cssClasses);
    }

    // Add placeholder?
    if (options.usePlaceholder !== false) {
        fieldElement.setAttribute("placeholder", fieldData.label);
    }

    // Create label element?
    if (options.useLabel !== false) {
        fieldId = generateId("formulate-field-");
        fieldElement.setAttribute("id", fieldId);
        labelElement = document.createElement("label");
        labelElement.setAttribute("for", fieldId);
        labelElement.appendChild(document.createTextNode(fieldData.label));
        wrapperElement.appendChild(labelElement);
    }

    // Add element to wrapper?
    if (useWrapper) {
        wrapperElement.appendChild(fieldElement);
    }

    // Retain DOM elements and field properties.
    if (useWrapper) {
        fieldRenderer.wrapper = wrapperElement;
    }
    fieldRenderer.element = fieldElement;
    fieldRenderer.id = fieldData.id;
    fieldRenderer.alias = fieldData.alias;

    // Prepare the validators and retain them for later use.
    fieldRenderer.validators = require("./validation").prepareValidators(fieldData.validations, fieldValidators);

};

/**
 * Generates a unique ID for an HTML element.
 * @param prefix {string} The prefix to use for the ID.
 * @returns {string} The unique ID.
 */
function generateId(prefix) {
    idCount++;
    return prefix + idCount.toString();
}

// Export the form field utility functions.
module.exports = Field;