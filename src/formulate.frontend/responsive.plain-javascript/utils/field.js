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

    // Use fallback for getCategory?
    if (!fieldPrototype.getCategory) {
        fieldPrototype.getCategory = function () {
            return null;
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
    if (options.rawDataByAlias) {
        if (alias) {
            data[alias] = value;
        }
    } else {
        data.append(id, value);
    }

};

/**
 * Initializes a field renderer.
 * @param fieldRenderer The field renderer to initialize.
 * @param fieldData The field data that should be used to render the field.
 * @param fieldValidators The associative array of the field validating functions.
 * @param options {{type: string, usePlaceholder: boolean, useLabel: boolean, useWrapper: boolean, cssClasses: string[], nodeName: string, nestFieldInLabel: boolean, wrapperElement: HTMLElement, wrapLabelText: boolean, fieldBeforeLabelText: boolean, name: string, label: string, label2: string}}
 *        The options to use when constructing the field.
 */
Field.initializeField = function (fieldRenderer, fieldData, fieldValidators, options) {

    // Variables.
    let fieldElement, labelElement, fieldId, textNode, labelTextWrapper,
        useWrapper = options.useWrapper !== false,
        wrapperElement = options.wrapperElement,
        useLabel = options.useLabel !== false,
        labelText = options.hasOwnProperty("label")
            ? options.label
            : fieldData.label,
        hasLabel2 = options.hasOwnProperty("label2");

    // Create element.
    fieldElement = document.createElement(options.nodeName || "input");
    if (options.type) {
        fieldElement.type = options.type;
    }
    if (options.hasOwnProperty("name")) {
        fieldElement.name = options.name;
    }

    // Add the catagory to the field.
    if (fieldData.category) {
        fieldElement.setAttribute("data-category", fieldData.category);
    }

    // Set value?
    if (options.hasOwnProperty("value")) {
        fieldElement.value = options.value;
    }

    // Set aria label.
    if (useLabel) {
        fieldElement.setAttribute("aria-label", labelText);
    }

    // Create wrapper element, or just use the field element as the wrapper.
    wrapperElement = useWrapper
        ? (wrapperElement || document.createElement("div"))
        : fieldElement;

    // Attach CSS classes.
    if (options.cssClasses) {
        require("./add-classes")(wrapperElement, options.cssClasses);
    }

    // Add placeholder?
    if (options.usePlaceholder !== false) {
        fieldElement.setAttribute("placeholder", labelText);
    }

    // Create label element?
    if (useLabel) {
        fieldId = Field.generateId("formulate-field-");
        fieldElement.setAttribute("id", fieldId);
        labelElement = document.createElement("label");
        labelElement.setAttribute("for", fieldId);
        labelElement.classList.add("formulate__field__label");
        textNode = document.createTextNode(labelText);
        if (options.wrapLabelText) {
            labelTextWrapper = document.createElement("span");
            labelTextWrapper.classList.add("formulate__field__label-text");
            labelTextWrapper.appendChild(textNode);
            labelElement.appendChild(labelTextWrapper);
            if (hasLabel2) {
                labelTextWrapper = labelTextWrapper.cloneNode();
                textNode = document.createTextNode(options.label2);
                labelTextWrapper.appendChild(textNode);
                labelElement.appendChild(labelTextWrapper);
            }
        } else {
            labelElement.appendChild(textNode);
        }
        wrapperElement.appendChild(labelElement);
    }

    // Add element to wrapper?
    if (useWrapper) {
        if (options.nestFieldInLabel) {
            if (options.fieldBeforeLabelText) {
                labelElement.insertBefore(fieldElement, labelElement.childNodes[0]);
            } else {
                labelElement.appendChild(fieldElement);
            }
        } else {
            if (options.fieldBeforeLabelText || window.labelAfterTextInput) {
                wrapperElement.insertBefore(fieldElement, wrapperElement.childNodes[0]);
            } else {
                wrapperElement.appendChild(fieldElement);
            }
        }
    }

    // Retain DOM elements and field properties.
    if (useWrapper) {
        fieldRenderer.wrapper = wrapperElement;
    }
    fieldRenderer.element = fieldElement;
    fieldRenderer.id = fieldData.id;
    fieldRenderer.alias = fieldData.alias;
    fieldRenderer.label = labelElement;

    // Prepare the validators and retain them for later use.
    fieldRenderer.validators = require("./validation").prepareValidators(fieldData.validations, fieldValidators);

};

/**
 * Generates a unique ID for an HTML element.
 * @param prefix {string} The prefix to use for the ID.
 * @returns {string} The unique ID.
 */
Field.generateId = function (prefix) {
    idCount++;
    return prefix + idCount.toString();
};

// Export the form field utility functions.
module.exports = Field;
