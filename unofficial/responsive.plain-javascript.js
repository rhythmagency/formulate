(function(){function e(t,n,r){function s(o,u){if(!n[o]){if(!t[o]){var a=typeof require=="function"&&require;if(!u&&a)return a(o,!0);if(i)return i(o,!0);var f=new Error("Cannot find module '"+o+"'");throw f.code="MODULE_NOT_FOUND",f}var l=n[o]={exports:{}};t[o][0].call(l.exports,function(e){var n=t[o][1][e];return s(n?n:e)},l,l.exports,e,t,n,r)}return n[o].exports}var i=typeof require=="function"&&require;for(var o=0;o<r.length;o++)s(r[o]);return s}return e})()({1:[function(require,module,exports){
// Dependencies.
let FieldUtility = require("../utils/field");

/**
 * Renders a submit button.
 * @param fieldData The field data that should be used to render the button.
 * @param fieldValidators The associative array of the field validating functions.
 * @param cssClasses The CSS classes to attach to the element.
 * @param extraOptions Additional options that are less commonly used.
 * @constructor
 */
function RenderButton(fieldData, fieldValidators, cssClasses, extraOptions) {

    // Initialize field.
    FieldUtility.initializeField(this, fieldData, fieldValidators, {
        nodeName: "button",
        type: "submit",
        cssClasses: cssClasses,
        usePlaceholder: false,
        useLabel: false
    });

    // Add text to button.
    this.element.appendChild(document.createTextNode(fieldData.label));

    // Store instance variables.
    this.formElement = extraOptions.formElement;
    this.buttonKind = fieldData.configuration.buttonKind;

    // Listen for form events.
    this.listenForSubmit();
    this.listenForFailureEvents();

}

/**
 * Listens for failure events on the form (in which case the button should be enabled again).
 */
RenderButton.prototype.listenForFailureEvents = function () {
    let formElement = this.formElement,
        element = this.element,
        handleError = function () {
            element.disabled = false;
        };
    formElement.addEventListener("formulate: submit: validation errors", handleError, true);
    formElement.addEventListener("formulate form: submit: failure", handleError, true);
    formElement.addEventListener("formulate: submit: cancelled", handleError, true);
};

/**
 * Listens for submit events on the form (in which case the button should be disabled).
 */
RenderButton.prototype.listenForSubmit = function () {
    let self = this;
    self.formElement.addEventListener("formulate: submit: started", function () {
        self.element.disabled = true;
    }, true);
};

/**
 * Returns the DOM element for the button.
 * @returns {HTMLDivElement} The DOM element for the button.
 */
RenderButton.prototype.getElement = function () {
    return this.wrapper;
};

/**
 * Returns the kind of button.
 * @returns {string} The kind of button, or null.
 */
RenderButton.prototype.getButtonKind = function () {
    return this.buttonKind;
};

/**
 * Ensure the prototype has the necessary functions.
 */
FieldUtility.initializeFieldPrototype(RenderButton.prototype);

// Export the field renderer configuration.
module.exports = {
    key: "button",
    renderer: RenderButton
};
},{"../utils/field":28}],2:[function(require,module,exports){
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
},{"../utils/field":28,"../utils/validation":30}],3:[function(require,module,exports){
// Dependencies.
let FieldUtility = require("../utils/field");

/**
 * Renders a checkbox field.
 * @param fieldData The field data that should be used to render the checkbox field.
 * @param fieldValidators The associative array of the field validating functions.
 * @param cssClasses The CSS classes to attach to the element.
 * @constructor
 */
function RenderCheckbox(fieldData, fieldValidators, cssClasses) {
    FieldUtility.initializeField(this, fieldData, fieldValidators, {
        type: "checkbox",
        cssClasses: cssClasses,
        usePlaceholder: false
    });
}

/**
 * Returns the DOM element for the checkbox field.
 * @returns {HTMLDivElement} The DOM element for the checkbox field.
 */
RenderCheckbox.prototype.getElement = function () {
    return this.wrapper;
};

/**
 * Adds the data for this field on the specified FormData instance.
 * @param data {FormData} The FormData instance to set the field data on.
 * @param options {{rawDataByAlias: boolean}} Optional. The options for setting the data.
 */
RenderCheckbox.prototype.setData = function (data, options) {
    FieldUtility.setData(data, this.element.checked, options, this.alias, this.id);
};

/**
 * Checks the validity of the value in this field (adding inline validation messages if necessary).
 * @returns {Promise[]} An array of promises that resolve to validation results.
 */
RenderCheckbox.prototype.checkValidity = function () {
    return require("../utils/validation")
        .checkBoolValidity(this, this.validators, this.element.checked, this.wrapper);
};

/**
 * Ensure the prototype has the necessary functions.
 */
FieldUtility.initializeFieldPrototype(RenderCheckbox.prototype);

// Export the field renderer configuration.
module.exports = {
    key: "checkbox",
    renderer: RenderCheckbox
};
},{"../utils/field":28,"../utils/validation":30}],4:[function(require,module,exports){
// Dependencies.
let FieldUtility = require("../utils/field");

/**
 * Renders a date field.
 * @param fieldData The field data that should be used to render the date field.
 * @param fieldValidators The associative array of the field validating functions.
 * @param cssClasses The CSS classes to attach to the element.
 * @constructor
 */
function RenderDate(fieldData, fieldValidators, cssClasses) {
    FieldUtility.initializeField(this, fieldData, fieldValidators, {
        type: "date",
        cssClasses: cssClasses
    });
}

/**
 * Returns the DOM element for the date field.
 * @returns {HTMLDivElement} The DOM element for the date field.
 */
RenderDate.prototype.getElement = function () {
    return this.wrapper;
};

/**
 * Adds the data for this field on the specified FormData instance.
 * @param data {FormData} The FormData instance to set the field data on.
 * @param options {{rawDataByAlias: boolean}} Optional. The options for setting the data.
 */
RenderDate.prototype.setData = function (data, options) {
    FieldUtility.setData(data, this.element.value, options, this.alias, this.id);
};

/**
 * Checks the validity of the value in this field (adding inline validation messages if necessary).
 * @returns {Promise[]} An array of promises that resolve to validation results.
 */
RenderDate.prototype.checkValidity = function () {
    return require("../utils/validation")
        .checkTextValidity(this, this.validators, this.element.value, this.wrapper);
};

/**
 * Ensure the prototype has the necessary functions.
 */
FieldUtility.initializeFieldPrototype(RenderDate.prototype);

// Export the field renderer configuration.
module.exports = {
    key: "date",
    renderer: RenderDate
};
},{"../utils/field":28,"../utils/validation":30}],5:[function(require,module,exports){
// Dependencies.
let FieldUtility = require("../utils/field");

/**
 * Renders an extended radio button list field.
 * @param fieldData The field data that should be used to render the radio button list field.
 * @param fieldValidators The associative array of the field validating functions.
 * @param cssClasses The CSS classes to attach to the element.
 * @constructor
 */
function RenderRadioList(fieldData, fieldValidators, cssClasses) {

    // Variables.
    let i, item, primary, secondary, wrapperElement, labelElement,
        elements = [],
        items = fieldData.configuration.items,
        name = FieldUtility.generateId("radio-button-list-");

    // Add each radio button.
    for (i = 0; i < items.length; i++) {

        // Variables.
        item = items[i];
        primary = item.primary;
        secondary = item.secondary;

        // Add radio button.
        FieldUtility.initializeField(this, fieldData, fieldValidators, {
            type: "radio",
            cssClasses: cssClasses,
            usePlaceholder: false,
            wrapperElement: wrapperElement,
            nestFieldInLabel: true,
            value: primary,
            label: primary,
            label2: secondary,
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
    key: "extended-radio-list",
    renderer: RenderRadioList
};
},{"../utils/field":28,"../utils/validation":30}],6:[function(require,module,exports){
// Dependencies.
let FieldUtility = require("../utils/field");

/**
 * Renders a header.
 * @param fieldData The field data that should be used to render the header.
 * @param fieldValidators The associative array of the field validating functions.
 * @param cssClasses The CSS classes to attach to the element.
 * @constructor
 */
function RenderHeader(fieldData, fieldValidators, cssClasses) {

    // Initialize field.
    FieldUtility.initializeField(this, fieldData, fieldValidators, {
        nodeName: "h2",
        cssClasses: cssClasses,
        usePlaceholder: false,
        useLabel: false,
        useWrapper: false
    });

    // Add text to element.
    this.element.appendChild(document.createTextNode(fieldData.configuration.text));

}

/**
 * Returns the DOM element for the header.
 * @returns {HTMLButtonElement} The DOM element for the header.
 */
RenderHeader.prototype.getElement = function () {
    return this.element;
};

/**
 * Ensure the prototype has the necessary functions.
 */
FieldUtility.initializeFieldPrototype(RenderHeader.prototype);

// Export the field renderer configuration.
module.exports = {
    key: "header",
    renderer: RenderHeader
};
},{"../utils/field":28}],7:[function(require,module,exports){
// Dependencies.
let FieldUtility = require("../utils/field");

/**
 * Renders a hidden field.
 * @param fieldData The field data that should be used to render the hidden field.
 * @param fieldValidators The associative array of the field validating functions.
 * @param cssClasses The CSS classes to attach to the element.
 * @constructor
 */
function RenderHidden(fieldData, fieldValidators, cssClasses) {
    FieldUtility.initializeField(this, fieldData, fieldValidators, {
        type: "hidden",
        cssClasses: cssClasses,
        useLabel: false
    });
}

/**
 * Returns the DOM element for the hidden field.
 * @returns {HTMLDivElement} The DOM element for the hidden field.
 */
RenderHidden.prototype.getElement = function () {
    return this.wrapper;
};

/**
 * Adds the data for this field on the specified FormData instance.
 * @param data {FormData} The FormData instance to set the field data on.
 * @param options {{rawDataByAlias: boolean}} Optional. The options for setting the data.
 */
RenderHidden.prototype.setData = function (data, options) {
    FieldUtility.setData(data, this.element.value, options, this.alias, this.id);
};

/**
 * Checks the validity of the value in this field (adding inline validation messages if necessary).
 * @returns {Promise[]} An array of promises that resolve to validation results.
 */
RenderHidden.prototype.checkValidity = function () {
    return require("../utils/validation")
        .checkTextValidity(this, this.validators, this.element.value, this.wrapper);
};

/**
 * Ensure the prototype has the necessary functions.
 */
FieldUtility.initializeFieldPrototype(RenderHidden.prototype);

// Export the field renderer configuration.
module.exports = {
    key: "hidden",
    renderer: RenderHidden
};
},{"../utils/field":28,"../utils/validation":30}],8:[function(require,module,exports){
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
},{"../utils/field":28,"../utils/validation":30}],9:[function(require,module,exports){
// Dependencies.
let FieldUtility = require("../utils/field"),
    AddClasses = require("../utils/add-classes"),
    Validation = require("../utils/validation");

// Variables.
let scriptInjected = false;

/**
 * Renders a recaptcha field.
 * @param fieldData The field data that should be used to render the recaptcha field.
 * @param fieldValidators The associative array of the field validating functions.
 * @param cssClasses The CSS classes to attach to the element.
 * @constructor
 */
function RenderRecaptcha(fieldData, fieldValidators, cssClasses) {

    // Notify the developer if they haven't configured recaptcha in the web.config yet.
    if (!fieldData.configuration.key) {

        // Using eval so build tools are less likely to remove this line.
        eval("alert('The reCAPTCHA keys need to be configured in the web.config. They should already be there; you just need to fill in the values.');");

    }

    // Create field and attach classes/attributes.
    let el = document.createElement("div");
    el.classList.add("g-recaptcha");
    el.setAttribute("data-sitekey", fieldData.configuration.key);
    if (cssClasses) {
        AddClasses(el, cssClasses);
    }

    // Retain field and ID for later reference.
    this.wrapper = el;
    this.id = fieldData.id;

    // Ensure there is at least one validator (to ensure the recaptcha is solved).
    let fieldValidations = this.ensureValidator(fieldData.validations);

    // Prepare the validators and retain them for later use.
    this.validators = Validation.prepareValidators(fieldValidations, fieldValidators);

    // Inject the Google Recaptcha JavaScript if it hasn't been added to the page yet.
    this.ensureGoogleScript();

}

/**
 * Ensures that there is at least one validator for the recaptcha.
 * @param validations {Array<{}>} The validations that have been specified on the field already.
 * @returns {Array<{}>} The specified validations, or an array containing a single newly created validation.
 */
RenderRecaptcha.prototype.ensureValidator = function (validations) {
    if (!validations.length) {
        return [{
            alias: "recaptchaRequired",
            configuration: {
                message: "Please solve the recaptcha."
            },
            validationType: "required"
        }];
    } else {
        return validations;
    }
};

/**
 * Ensures the Google Recaptcha script is injected.
 */
RenderRecaptcha.prototype.ensureGoogleScript = function () {

    // If the script was already injected, exit early.
    if (scriptInjected || typeof(grecaptcha) !== "undefined") {
        scriptInjected = true;
        return;
    }


    // Inject the script.
    let script = document.createElement("script");
    script.src = "https://www.google.com/recaptcha/api.js";
    script.async = true;
    document.head.appendChild(script);
    scriptInjected = true;

};

/**
 * Returns the DOM element for the recaptcha field.
 * @returns {HTMLDivElement} The DOM element for the recaptcha field.
 */
RenderRecaptcha.prototype.getElement = function () {
    return this.wrapper;
};

/**
 * Returns the DOM element for the recaptcha field.
 * @returns {HTMLDivElement} The DOM element that wraps the recaptcha field.
 */
RenderRecaptcha.prototype.getHiddenElement = function () {
    return this.wrapper.querySelector("[name='g-recaptcha-response']");
};

/**
 * Adds the data for this field on the specified FormData instance.
 * @param data {FormData} The FormData instance to set the field data on.
 * @param options {{rawDataByAlias: boolean}} Optional. The options for setting the data.
 */
RenderRecaptcha.prototype.setData = function (data, options) {
    let element = this.getHiddenElement(),
        value = element
            ? element.value
            : null;
    FieldUtility.setData(data, value, options, this.alias, this.id);
};

/**
 * Checks the validity of the value in this field (adding inline validation messages if necessary).
 * @returns {Promise[]} An array of promises that resolve to validation results.
 */
RenderRecaptcha.prototype.checkValidity = function () {
    let element = this.getHiddenElement(),
        value = element
            ? element.value
            : null;
    return require("../utils/validation")
        .checkTextValidity(this, this.validators, value, this.wrapper);
};

/**
 * Ensure the prototype has the necessary functions.
 */
FieldUtility.initializeFieldPrototype(RenderRecaptcha.prototype);

// Export the field renderer configuration.
module.exports = {
    key: "recaptcha",
    renderer: RenderRecaptcha
};
},{"../utils/add-classes":25,"../utils/field":28,"../utils/validation":30}],10:[function(require,module,exports){
// Dependencies.
let FieldUtility = require("../utils/field");

/**
 * Renders rich text.
 * @param fieldData The field data that should be used to render the rich text.
 * @param fieldValidators The associative array of the field validating functions.
 * @param cssClasses The CSS classes to attach to the element.
 * @constructor
 */
function RichText(fieldData, fieldValidators, cssClasses) {

    // Initialize field.
    FieldUtility.initializeField(this, fieldData, fieldValidators, {
        nodeName: "div",
        cssClasses: cssClasses,
        usePlaceholder: false,
        useLabel: false,
        useWrapper: false
    });

    // Add text to element.
    this.element.innerHTML = fieldData.configuration.text;

}

/**
 * Returns the DOM element for the rich text.
 * @returns {HTMLButtonElement} The DOM element for the rich text.
 */
RichText.prototype.getElement = function () {
    return this.element;
};

/**
 * Ensure the prototype has the necessary functions.
 */
FieldUtility.initializeFieldPrototype(RichText.prototype);

// Export the field renderer configuration.
module.exports = {
    key: "rich-text",
    renderer: RichText
};
},{"../utils/field":28}],11:[function(require,module,exports){
// Dependencies.
let FieldUtility = require("../utils/field"),
    dispatchEvent = require("../utils/events");

/**
 * Renders a drop down field.
 * @param fieldData The field data that should be used to render the drop down field.
 * @param fieldValidators The associative array of the field validating functions.
 * @param cssClasses The CSS classes to attach to the element.
 * @constructor
 */
function RenderSelect(fieldData, fieldValidators, cssClasses) {

    // Initialize field.
    FieldUtility.initializeField(this, fieldData, fieldValidators, {
        nodeName: "select",
        cssClasses: cssClasses
    });
    this.addOptions(fieldData);

    // Listen for events.
    this.addChangeEvent(fieldData);

}

/**
 * Add event listener for the change event
 * @param fieldData The field data that should be used to render the drop down field.
 */
RenderSelect.prototype.addChangeEvent = function(fieldData) {

    // Variables.
    let category = fieldData.category,
        element = this.element;

    // Add change event listener.
    element.addEventListener('change', function() {

        // Dispatch event indicating the drop down value changed.
        dispatchEvent(element, "formulate form: select changed", {
            category: category,
            element: element
        });

    });

};

/**
 * Adds the options to the select.
 * @param fieldData The field data that should be used to render the drop down field.
 */
RenderSelect.prototype.addOptions = function (fieldData) {

    // Variables.
    let i, item, option, fragment = document.createDocumentFragment(),
        items = fieldData.configuration.items || [];

    // Are there any options to add?
    if (items.length === 0) {
        return;
    }

    // Add the initial option.
    option = RenderSelect.createOption({
        value: "",
        label: fieldData.label
    }, true);
    fragment.appendChild(option);

    // Add the options.
    for (i = 0; i < items.length; i++) {
        item = items[i];
        option = RenderSelect.createOption(item, false);
        fragment.appendChild(option);
    }

    // Append the options to the select.
    this.element.appendChild(fragment);

};

/**
 * Creates an HTML option element.
 * @param item The item to create the option for.
 * @param isInitial {boolean} Is this the initial option in a list of options?
 * @returns {HTMLElement} The option element.
 */
RenderSelect.createOption = function (item, isInitial) {

    // Variables.
    let option = document.createElement("option"),
        cssClass = "formulate__field__select__option";

    // Set attributes, label, and classes.
    option.value = item.value;
    option.appendChild(document.createTextNode(item.label));
    option.classList.add(cssClass);
    if (isInitial) {
        option.classList.add(cssClass + "--initial");
    }

    // Return option element.
    return option;

};

/**
 * Returns the DOM element for the drop down field.
 * @returns {HTMLDivElement} The DOM element for the drop down field.
 */
RenderSelect.prototype.getElement = function () {
    return this.wrapper;
};

/**
 * Adds the data for this field on the specified FormData instance.
 * @param data {FormData} The FormData instance to set the field data on.
 * @param options {{rawDataByAlias: boolean}} Optional. The options for setting the data.
 */
RenderSelect.prototype.setData = function (data, options) {
    FieldUtility.setData(data, this.element.value, options, this.alias, this.id);
};

/**
 * Checks the validity of the value in this field (adding inline validation messages if necessary).
 * @returns {Promise[]} An array of promises that resolve to validation results.
 */
RenderSelect.prototype.checkValidity = function () {
    return require("../utils/validation")
        .checkTextValidity(this, this.validators, this.element.value, this.wrapper);
};

/**
 * Ensure the prototype has the necessary functions.
 */
FieldUtility.initializeFieldPrototype(RenderSelect.prototype);

// Export the field renderer configuration.
module.exports = {
    key: "select",
    renderer: RenderSelect
};
},{"../utils/events":27,"../utils/field":28,"../utils/validation":30}],12:[function(require,module,exports){
// Dependencies.
let FieldUtility = require("../utils/field");

/**
 * Renders a text field.
 * @param fieldData The field data that should be used to render the text field.
 * @param fieldValidators The associative array of the field validating functions.
 * @param cssClasses The CSS classes to attach to the element.
 * @constructor
 */
function RenderText(fieldData, fieldValidators, cssClasses) {
    FieldUtility.initializeField(this, fieldData, fieldValidators, {
        type: "text",
        cssClasses: cssClasses
    });
}

/**
 * Returns the DOM element for the text field.
 * @returns {HTMLDivElement} The DOM element for the text field.
 */
RenderText.prototype.getElement = function () {
    return this.wrapper;
};

/**
 * Adds the data for this field on the specified FormData instance.
 * @param data {FormData} The FormData instance to set the field data on.
 * @param options {{rawDataByAlias: boolean}} Optional. The options for setting the data.
 */
RenderText.prototype.setData = function (data, options) {
    FieldUtility.setData(data, this.element.value, options, this.alias, this.id);
};

/**
 * Checks the validity of the value in this field (adding inline validation messages if necessary).
 * @returns {Promise[]} An array of promises that resolve to validation results.
 */
RenderText.prototype.checkValidity = function () {
    return require("../utils/validation")
        .checkTextValidity(this, this.validators, this.element.value, this.wrapper);
};

/**
 * Ensure the prototype has the necessary functions.
 */
FieldUtility.initializeFieldPrototype(RenderText.prototype);

// Export the field renderer configuration.
module.exports = {
    key: "text",
    renderer: RenderText
};
},{"../utils/field":28,"../utils/validation":30}],13:[function(require,module,exports){
// Dependencies.
let FieldUtility = require("../utils/field");

/**
 * Renders a textarea field.
 * @param fieldData The field data that should be used to render the textarea field.
 * @param fieldValidators The associative array of the field validating functions.
 * @param cssClasses The CSS classes to attach to the element.
 * @constructor
 */
function RenderText(fieldData, fieldValidators, cssClasses) {
    FieldUtility.initializeField(this, fieldData, fieldValidators, {
        nodeName: "textarea",
        cssClasses: cssClasses
    });
}

/**
 * Returns the DOM element for the textarea field.
 * @returns {HTMLDivElement} The DOM element for the textarea field.
 */
RenderText.prototype.getElement = function () {
    return this.wrapper;
};

/**
 * Adds the data for this field on the specified FormData instance.
 * @param data {FormData} The FormData instance to set the field data on.
 * @param options {{rawDataByAlias: boolean}} Optional. The options for setting the data.
 */
RenderText.prototype.setData = function (data, options) {
    FieldUtility.setData(data, this.element.value, options, this.alias, this.id);
};

/**
 * Checks the validity of the value in this field (adding inline validation messages if necessary).
 * @returns {Promise[]} An array of promises that resolve to validation results.
 */
RenderText.prototype.checkValidity = function () {
    return require("../utils/validation")
        .checkTextValidity(this, this.validators, this.element.value, this.wrapper);
};

/**
 * Ensure the prototype has the necessary functions.
 */
FieldUtility.initializeFieldPrototype(RenderText.prototype);

// Export the field renderer configuration.
module.exports = {
    key: "textarea",
    renderer: RenderText
};
},{"../utils/field":28,"../utils/validation":30}],14:[function(require,module,exports){
// Dependencies.
let FieldUtility = require("../utils/field");

/**
 * Renders an upload field.
 * @param fieldData The field data that should be used to render the text field.
 * @param fieldValidators The associative array of the field validating functions.
 * @param cssClasses The CSS classes to attach to the element.
 * @constructor
 */
function RenderUpload(fieldData, fieldValidators, cssClasses) {
    FieldUtility.initializeField(this, fieldData, fieldValidators, {
        type: "file",
        cssClasses: cssClasses,
        nestFieldInLabel: true
    });
    this.listenForChanges();
}

/**
 * Listens for changes to the selected file.
 */
RenderUpload.prototype.listenForChanges = function () {
    let self = this;
    this.element.addEventListener("change", function () {

        // Variables.
        let file = self.getFile(),
            hasFile = file !== null,
            name = hasFile
                ? file.name
                : null;

        // Was a file selected (or deselected)?
        if (hasFile) {

            // Change the filename and add the deselect button.
            self.addFilename(name);
            self.addDeselectButton();

        } else {

            // Some browsers (e.g., IE11) will get here if the file is deselected.
            // Remove the filename and the deselect button.
            self.removeFilename();
            self.removeDeselectButton();

        }

    });
};

/**
 * Removes the "Deselect File" button.
 */
RenderUpload.prototype.removeDeselectButton = function () {
    if (this.deselectElement) {
        this.deselectElement.parentNode.removeChild(this.deselectElement);
        this.deselectElement = null;
    }
};

/**
 * Removes the filename element.
 */
RenderUpload.prototype.removeFilename = function () {
    if (this.filenameElement) {
        this.filenameElement.parentNode.removeChild(this.filenameElement);
        this.filenameElement = null;
    }
};

/**
 * Adds the filename element.
 * @param name The name of the file.
 */
RenderUpload.prototype.addFilename = function (name) {
    let filenameElement = document.createElement("div");
    this.removeFilename();
    this.filenameElement = filenameElement;
    filenameElement.appendChild(document.createTextNode(name));
    filenameElement.classList.add("formulate__field--upload__filename");
    this.wrapper.insertBefore(filenameElement, this.label.nextSibling);
};

/**
 * Adds the "Deselect File" button.
 */
RenderUpload.prototype.addDeselectButton = function () {

    // Variables.
    let deselectElement = document.createElement("button"), previousSibling;

    // Remove existing button.
    this.removeDeselectButton();

    /// Create new button.
    this.deselectElement = deselectElement;
    deselectElement.appendChild(document.createTextNode("Deselect File"));
    deselectElement.classList.add("formulate__field--upload__deselect");
    deselectElement.type = "button";

    // Add after the filename (if it exists) or the label.
    previousSibling = this.filenameElement || this.label;
    this.wrapper.insertBefore(deselectElement, previousSibling.nextSibling);

    // Listen for clicks of the "Deselect File" button.
    this.listenForDeselect();

};

/**
 * Sets up the event handler that deselects the currently selected file.
 */
RenderUpload.prototype.listenForDeselect = function () {
    let self = this;
    this.deselectElement.addEventListener("click", function () {
        self.element.value = "";
        self.removeDeselectButton();
        self.removeFilename();
    });
};

/**
 * Returns the DOM element for the upload field.
 * @returns {HTMLDivElement} The DOM element for the upload field.
 */
RenderUpload.prototype.getElement = function () {
    return this.wrapper;
};

/**
 * Adds the data for this field on the specified FormData instance.
 * @param data {FormData} The FormData instance to set the field data on.
 * @param options {{rawDataByAlias: boolean}} Optional. The options for setting the data.
 */
RenderUpload.prototype.setData = function (data, options) {
    FieldUtility.setData(data, this.getFile(), options, this.alias, this.id);
};

/**
 * Gets the currently selected file.
 * @returns {*} The file, or null.
 */
RenderUpload.prototype.getFile = function () {
    let files = this.element.files,
        hasFile = files.length > 0,
        file = hasFile
            ? files[0]
            : null;
    return file;
};

/**
 * Checks the validity of the value in this field (adding inline validation messages if necessary).
 * @returns {Promise[]} An array of promises that resolve to validation results.
 */
RenderUpload.prototype.checkValidity = function () {
    return require("../utils/validation")
        .checkFileValidity(this, this.validators, this.getFile(), this.wrapper);
};

/**
 * Ensure the prototype has the necessary functions.
 */
FieldUtility.initializeFieldPrototype(RenderUpload.prototype);

// Export the field renderer configuration.
module.exports = {
    key: "upload",
    renderer: RenderUpload
};
},{"../utils/field":28,"../utils/validation":30}],15:[function(require,module,exports){
// Variables.
let forms, renderers, validators;

// Get field renderers.
renderers = require("./steps/get-field-renderers")();

// Get field validators.
validators = require("./steps/get-field-validators")();

// Get form data.
forms = require("./steps/get-form-data")();

// Render the forms.
require("./steps/render-forms")(forms, renderers, validators);
},{"./steps/get-field-renderers":17,"./steps/get-field-validators":18,"./steps/get-form-data":19,"./steps/render-forms":23}],16:[function(require,module,exports){
// Variables.
let FormulatePromise;

// Get either the promise polyfill or the native promise.
if (typeof Promise === "undefined") {
    FormulatePromise = require("promiscuous/dist/promiscuous-browser-shim-full");
} else {
    FormulatePromise = Promise;
}

// Export the promise function (either native or the polyfill).
module.exports = FormulatePromise;
},{"promiscuous/dist/promiscuous-browser-shim-full":33}],17:[function(require,module,exports){
/**
 * Returns the field renderers in an associative array with the keys being the
 * type of field renderer (e.g., "text" or "button") and the value being the
 * function that renders the field.
 * @returns {{}} The associative array of field renderers.
 */
function getFieldRenderers() {

    // Variables.
    let i, fields, extraFields, field, fieldKey, fieldMap;

    // Get the field renderers.
    fields = [
        require("../fields/plain-text"),
        require("../fields/plain-button"),
        require("../fields/plain-rich-text"),
        require("../fields/plain-checkbox"),
        require("../fields/plain-textarea"),
        require("../fields/plain-hidden"),
        require("../fields/plain-select"),
        require("../fields/plain-header"),
        require("../fields/plain-upload"),
        require("../fields/plain-checkbox-list"),
        require("../fields/plain-date"),
        require("../fields/plain-radio-button-list"),
        require("../fields/plain-extended-radio-button-list"),
        require("../fields/plain-recaptcha")
    ];
    extraFields = window["formulate-plain-js-fields"] || [];
    fields = fields.concat(extraFields);

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
},{"../fields/plain-button":1,"../fields/plain-checkbox":3,"../fields/plain-checkbox-list":2,"../fields/plain-date":4,"../fields/plain-extended-radio-button-list":5,"../fields/plain-header":6,"../fields/plain-hidden":7,"../fields/plain-radio-button-list":8,"../fields/plain-recaptcha":9,"../fields/plain-rich-text":10,"../fields/plain-select":11,"../fields/plain-text":12,"../fields/plain-textarea":13,"../fields/plain-upload":14}],18:[function(require,module,exports){
/**
 * Returns the field validators in an associative array with the keys being the
 * type of field validator (e.g., "regex" or "required") and the value being the
 * function that validates the field.
 * @returns {{}} The associative array of field validators.
 */
function getFieldValidators() {

    // Variables.
    let i, validators, extraValidators, field, fieldKey, validatorMap;

    // Get the field validators.
    validators = [
        require("../validators/regex"),
        require("../validators/required")
    ];
    extraValidators = window["formulate-plain-js-validators"] || [];
    validators = validators.concat(extraValidators);

    // Store the field validators to an associative array.
    validatorMap = {};
    for (i = 0; i < validators.length; i++) {
        field = validators[i];
        fieldKey = field.key;
        validatorMap[fieldKey] = field.validator;
    }

    // Return the associative array of field validators.
    return validatorMap;

}

// Export the function that gets the field validators.
module.exports = getFieldValidators;
},{"../validators/regex":31,"../validators/required":32}],19:[function(require,module,exports){
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
},{}],20:[function(require,module,exports){
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
},{}],21:[function(require,module,exports){
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

    // Return the rendered field (an object that has information about the rendered field).
    return renderResult;

}

// Export the function that renders a field.
module.exports = renderField;
},{}],22:[function(require,module,exports){
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
        field, renderedFields, renderedField;

    // Map fields to an associative array for quick lookups.
    fieldsData = formData.data.fields;
    fieldMap = require("../utils/map-fields-by-id")(fieldsData);

    // Process each row in the form.
    rows = formData.data.rows;
    renderedFields = [];
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
                renderedField = require("./render-field")(field, fieldRenderers, fieldValidators, {
                    formElement: formElement,
                    placeholderElement: placeholderElement
                });
                renderedFields.push(renderedField);
                fieldElement = renderedField.getElement();
                cellElement.appendChild(fieldElement);

            }

        }

    }

    // Return the rendered fields.
    return renderedFields;

}

// Export the function that renders a form.
module.exports = renderForm;
},{"../utils/map-fields-by-id":29,"./render-cell":20,"./render-field":21,"./render-row":24}],23:[function(require,module,exports){
// Dependencies.
let dispatchEvent = require("../utils/events");

/**
 * Renders the Formulate forms, inserting them into the appropriate place
 * in the DOM.
 * @param forms The data for the forms to render.
 * @param fieldRenderers The associative array of the field rendering functions.
 * @param fieldValidators The associative array of the field validating functions.
 */
function renderForms(forms, fieldRenderers, fieldValidators) {

    // Variables.
    let i, form, formId, placeholderElement, formElement, formContainer, fields;

    // Process each form.
    for (i = 0; i < forms.length; i++) {

        // Variables.
        form = forms[i];

        // Create the form DOM element.
        formElement = document.createElement("form");

        // Add CSS class to DOM element.
        formElement.classList.add("formulate__form");

        // Get the placeholder element to insert the form before.
        formId = "formulate-form-" + form.data.randomId;
        placeholderElement = document.getElementById(formId);

        // Render the contents of the form.
        fields = require("./render-form")(form, formElement, placeholderElement, fieldRenderers, fieldValidators);

        // Insert the form before the placeholder, and remove the placeholder.
        formContainer = placeholderElement.parentNode;
        formContainer.insertBefore(formElement, placeholderElement);
        formContainer.removeChild(placeholderElement);

        // Handle submits.
        attachSubmitHandler(formElement, fields, form.data.payload, form.data.url);

    }

}

/**
 * Attaches the function that handles the submit event.
 * @param form {HTMLFormElement} The HTML form DOM element.
 * @param fields {Array} The fields in this form.
 * @param payload {Object} The additional data to send with the submission.
 * @param url {string} The URL to send the submission to.
 */
function attachSubmitHandler(form, fields, payload, url) {

    // Variables.
    let validationData;

    // Listen for submit events.
    form.addEventListener("submit", function (e) {

        // Cancel submit (since we'll be doing it with AJAX instead).
        e.preventDefault();

        // Dispatch event to indicate the form submission has started.
        dispatchEvent(form, "formulate: submit: started");

        // First, ensure all fields are valid.
        checkValidity(form, fields)
            .then(function (validationResult) {
                if (validationResult.success) {

                    // Dispatch event to indicate the form submission validation succeeded.
                    validationData = {
                        cancelSubmit: false,
                        fields: fields,
                        payload: payload
                    };
                    dispatchEvent(form, "formulate: validation: success", validationData);

                    // Should the submit be cancelled?
                    if (validationData.cancelSubmit) {
                        dispatchEvent(form, "formulate: submit: cancelled");
                        return;
                    }

                    // Use the data from the validation event, in case it was modified by a listener.
                    fields = validationData.fields;
                    payload = validationData.payload;

                    // Populate submission with initial payload.
                    sendPayloadToServer(form, fields, payload, url);

                } else {

                    // Validation failed.
                    handleInvalidFields(validationResult.messages, form);

                }
            });

    }, true);

}

/**
 * Handles invalid fields by dispatching an event with the validation errors.
 * @param messages The messages for the validation errors.
 * @param form The form.
 */
function handleInvalidFields(messages, form) {
    dispatchEvent(form, "formulate: submit: validation errors", {
        messages: messages
    });
}

/**
 * Checks the form for validation errors.
 * @param form The form.
 * @param fields The fields in the form.
 * @returns {*} A promise that will resolve to the result of the validations.
 */
function checkValidity(form, fields) {

    // Variables.
    let i, field, validationPromises = [], fieldPromises;

    // Start validating each field.
    for(i = 0; i < fields.length; i++) {
        field = fields[i];
        fieldPromises = field.checkValidity();
        validationPromises = validationPromises.concat(fieldPromises);
    }

    // Finalize the validation of the fields.
    return require("../utils/validation").aggregateValidations(validationPromises);

}

/**
 * Sends the payload for the form to the server.
 * @param form The form element.
 * @param fields The fields for the form.
 * @param payload The data to send to the server.
 * @param url The URL to send the data to.
 */
function sendPayloadToServer(form, fields, payload, url) {

    // Variables.
    let i, data, payloadKey, dataByAlias = {};

    // Populate submission with initial payload.
    data = new FormData();
    for(payloadKey in payload) {
        if (payload.hasOwnProperty(payloadKey)) {
            data.append(payloadKey, payload[payloadKey]);
        }
    }

    // Populate submission with data from fields.
    for (i = 0; i < fields.length; i++) {
        fields[i].setData(data);
        fields[i].setData(dataByAlias, {
            rawDataByAlias: true
        });
    }

    // Send data as AJAX submission.
    new (require("../utils/ajax"))(url, data).then(function(result) {

        // Was the request successful?
        let success = JSON.parse(result.text).Success;
        if (success) {

            // Dispatch success event.
            dispatchEvent(form, "formulate form: submit: success", {
                dataByAlias: dataByAlias
            });

        } else {

            // Dispatch failure event.
            dispatchEvent(form, "formulate form: submit: failure");

        }

    }).catch(function() {

        // Dispatch failure event.
        dispatchEvent(form, "formulate form: submit: failure");

    });

}

// Export the function that renders forms.
module.exports = renderForms;
},{"../utils/ajax":26,"../utils/events":27,"../utils/validation":30,"./render-form":22}],24:[function(require,module,exports){
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
},{}],25:[function(require,module,exports){
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
},{}],26:[function(require,module,exports){
// A readyState of 4 indicates the request has completed: https://developer.mozilla.org/en-US/docs/Web/API/XMLHttpRequest/readyState
const STATE_DONE = 4;

// An HTTP response code of 200 indicates success.
const STATUS_SUCCESS = 200;

// HTTP request method to post data to the server: https://developer.mozilla.org/en-US/docs/Web/HTTP/Methods/POST
const METHOD_POST = "POST";

/**
 * Posts a form to the server with AJAX.
 * @param url {string} The URL to post the form to.
 * @param payload {FormData} The form data to send with the request.
 * @returns {Promise<any>} A promise that resolves once the request is complete.
 * @constructor
 */
function SendRequest(url, payload) {

    // Variables.
    let self = this, Promise = require("../polyfills/promise"), state;

    // Instance variables.
    this.request = new XMLHttpRequest();

    // Return a promise that will resolve once the AJAX request has returned from the server.
    return new Promise(function (resolve, reject) {

        // Initiate the AJAX request.
        self.request.onreadystatechange = function () {
            state = self.handleStateChange();
            if (state && state.status === STATUS_SUCCESS) {
                resolve(state);
            } else if (state && state.status !== STATUS_SUCCESS) {
                reject(state);
            }
        };
        self.request.open(METHOD_POST, url, true);
        self.request.send(payload);

    });

}

/**
 * Called whenever the state changes for the AJAX call.
 * @returns {{} | null} The result of the state change, or null if it's an irrelevant state change.
 */
SendRequest.prototype.handleStateChange = function () {

    // Is the request done?
    if (this.request.readyState === STATE_DONE) {

        // Was the request successful?
        if (this.request.status === STATUS_SUCCESS){

            // Success.
            return {
                status: STATUS_SUCCESS,
                text: this.request.responseText
            };

        } else {

            // Error.
            return {
                status: this.request.status
            };

        }

    }

    // Not a state change we care about.
    return null;

};

// Export the function that sends an AJAX request.
module.exports = SendRequest;
},{"../polyfills/promise":16}],27:[function(require,module,exports){
/**
 * Dispatches the specified event.
 * @param element The element to dispatch the element on.
 * @param eventName The event to dispatch.
 * @param data The data to send with the event.
 */
function dispatchEvent(element, eventName, data) {
    let event;
    if (typeof window.CustomEvent === "function") {

        // Typical implementation for CustomEvent.
        event = new CustomEvent(eventName, {
            bubbles: true,
            detail: data
        });
        element.dispatchEvent(event);

    } else {

        // IE11 implementation for CustomEvent.
        event = document.createEvent("CustomEvent");
        event.initCustomEvent(eventName, true, false, data);
        element.dispatchEvent(event);

    }
}

// Export the function that dispatches an event.
module.exports = dispatchEvent;
},{}],28:[function(require,module,exports){
// Dependencies.
let AddClasses = require("./add-classes");

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
        AddClasses(wrapperElement, options.cssClasses);
    }
    AddClasses(wrapperElement, Object.keys(fieldValidators).map(function (x) {
        return "formulate__validation-type--" + x;
    }));

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

},{"./add-classes":25,"./validation":30}],29:[function(require,module,exports){
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
},{}],30:[function(require,module,exports){
/**
 * A collection of validation utility functions.
 * @constructor
 */
function ValidationUtilities() {
}

/**
 * Is the specified value set to something that is not null or undefined?
 * @param value {*} The value.
 * @returns {boolean} True, if the value is set to something; otherwise, false.
 */
ValidationUtilities.isValueSet = function (value) {
    return value !== null && value !== undefined && typeof(value) !== "undefined";
};

/**
 * Prepares the validators by passing configuration to their constructors.
 * @param validationData The data to pass to the validators.
 * @param fieldValidators The associative array of the field validating functions.
 * @returns {*} An array of prepared validators.
 */
ValidationUtilities.prepareValidators = function (validationData, fieldValidators) {

    // Validate input.
    if (!validationData || !fieldValidators) {
        return [];
    }

    // Variables.
    let i, validationOptions, validator, key, preparedValidators;

    // Process the validation data to prepare the validators.
    preparedValidators = [];
    for(i = 0; i < validationData.length; i++) {
        validationOptions = validationData[i];
        key = validationOptions.validationType;
        validator = fieldValidators[key];
        preparedValidators.push({
            validator: new validator(validationOptions.configuration),
            data: validationOptions
        });
    }

    // Return the prepared validators.
    return preparedValidators;

};

/**
 * Adds inline validation messages to a container.
 * @param containerElement {HTMLElement} The container to add the inline validation messages to.
 * @param messages {string[]} The validation messages to add.
 * @param priorListElement {HTMLUListElement} The list element returned the last time this
 * function was called.
 * @returns {HTMLUListElement} The list element containing the validation messages.
 */
ValidationUtilities.addValidationMessages = function (containerElement, messages, priorListElement) {

    // If there are no messages, remove the prior list element and return early.
    if (!messages || messages.length === 0) {
        if (priorListElement) {
            priorListElement.parentNode.removeChild(priorListElement);
        }
        return null;
    }

    // Variables.
    let i, listElement, message, itemElement;

    // Create the list element that contains the messages.
    listElement = document.createElement("ul");
    listElement.classList.add("formulate__inline-validation-summary");

    // Add the messages to the list element.
    for (i = 0; i < messages.length; i++) {
        message = messages[i];
        itemElement = document.createElement("li");
        itemElement.classList.add("formulate__inline-validation-summary__error");
        itemElement.appendChild(document.createTextNode(message));
        listElement.appendChild(itemElement);
    }

    // Remove the prior list element.
    if (priorListElement) {
        priorListElement.parentNode.removeChild(priorListElement);
    }

    // Add the new list element to the container.
    containerElement.appendChild(listElement);

    // Return the new list element (expected to be passed in on the subsequent call as the
    // prior list element).
    return listElement;

};

/**
 * Aggregates validation results into a single validation result.
 * @param validationPromises {Promise[]} An array of promises that will resolve to a validation result.
 * @returns {Promise} A promise that will resolve to the aggregate validation result.
 */
ValidationUtilities.aggregateValidations = function (validationPromises) {

    // Variables.
    let Promise = require("../polyfills/promise");

    // Return a promise that resolves to the result of all of the validations.
    return Promise.all(validationPromises)
        .then(function (results) {

            // Variables.
            let i, result, success, failures;

            // Extract all the failures from the validation results.
            failures = [];
            for (i = 0; i < results.length; i++) {
                result = results[i];
                if (!result.success) {
                    failures.push(result);
                }
            }

            // Success if there are no failures.
            success = failures.length === 0;
            if (success) {

                // Success.
                return {
                    success: true
                };

            } else {

                // Failure. Return validation messages for the failures.
                return {
                    success: false,
                    messages: failures.map(function (x) {
                        return x.message;
                    })
                };

            }

        });

};

/**
 * Checks the validity of a text array field, adding inline validation messages if there are
 * any validations that fail.
 * @param fieldRenderer The instance of the Formulate field renderer.
 * @param validators The prepared validator functions.
 * @param value The value to check the validity of.
 * @param containerElement The container element to add validation messages to.
 * @returns {Promise[]} An array of promises that resolve to validation results.
 */
ValidationUtilities.checkTextArrayValidity = function (fieldRenderer, validators, value, containerElement) {
    return ValidationUtilities.checkValidityCommon(fieldRenderer, validators, value, containerElement, "validateTextArray");
};

/**
 * Checks the validity of a text-based field, adding inline validation messages if there are
 * any validations that fail.
 * @param fieldRenderer The instance of the Formulate field renderer.
 * @param validators The prepared validator functions.
 * @param value The value to check the validity of.
 * @param containerElement The container element to add validation messages to.
 * @returns {Promise[]} An array of promises that resolve to validation results.
 */
ValidationUtilities.checkTextValidity = function (fieldRenderer, validators, value, containerElement) {
    return ValidationUtilities.checkValidityCommon(fieldRenderer, validators, value, containerElement, "validateText");
};

/**
 * Checks the validity of a boolean-based field, adding inline validation messages if there are
 * any validations that fail.
 * @param fieldRenderer The instance of the Formulate field renderer.
 * @param validators The prepared validator functions.
 * @param value The value to check the validity of.
 * @param containerElement The container element to add validation messages to.
 * @returns {Promise[]} An array of promises that resolve to validation results.
 */
ValidationUtilities.checkBoolValidity = function (fieldRenderer, validators, value, containerElement) {
    return ValidationUtilities.checkValidityCommon(fieldRenderer, validators, value, containerElement, "validateBool");
};

/**
 * Checks the validity of a file-based field, adding inline validation messages if there are
 * any validations that fail.
 * @param fieldRenderer The instance of the Formulate field renderer.
 * @param validators The prepared validator functions.
 * @param value The value to check the validity of.
 * @param containerElement The container element to add validation messages to.
 * @returns {Promise[]} An array of promises that resolve to validation results.
 */
ValidationUtilities.checkFileValidity = function (fieldRenderer, validators, value, containerElement) {
    return ValidationUtilities.checkValidityCommon(fieldRenderer, validators, value, containerElement, "validateFile");
};

/**
 * Checks the validity of a field, adding inline validation messages if there are any validations that fail.
 * @param fieldRenderer The instance of the Formulate field renderer.
 * @param validators The prepared validator functions.
 * @param value The value to check the validity of.
 * @param containerElement The container element to add validation messages to.
 * @param validityFnName The name of the validity function (e.g., "validateText").
 * @returns {Promise[]} An array of promises that resolve to validation results.
 */
ValidationUtilities.checkValidityCommon = function (fieldRenderer, validators, value, containerElement, validityFnName) {

    // Variables.
    let i, validator, validationResults;

    // Check each validator for the validity of the value in this field.
    validationResults = [];
    for (i = 0; i < validators.length; i++) {
        validator = validators[i];
        validationResults.push(checkValidity(validator, value, validityFnName));
    }

    // Add inline validation messages.
    ValidationUtilities.aggregateValidations(validationResults)
        .then(function (result) {

            // Add inline validation messages.
            fieldRenderer.validationListElement = ValidationUtilities.addValidationMessages(
                containerElement, result.messages, fieldRenderer.validationListElement);

            // Add or remove validation error CSS class.
            if (result.success) {
                containerElement.classList.remove("formulate__field--validation-error");
            } else {
                containerElement.classList.add("formulate__field--validation-error");
            }

        });

    // Return the validation results.
    return validationResults;

};

/**
 * Validates the specified value against the specified validator.
 * @param validator The validator.
 * @param value The value to validate.
 * @param validityFnName The name of the validity function (e.g., "validateText").
 * @returns {Promise} A promise that will resolve to the validation result.
 */
function checkValidity(validator, value, validityFnName) {
    return validator.validator[validityFnName](value)
        .then(function (result) {
            if (result) {

                // Success.
                return {
                    success: true
                };

            } else {

                // Failure. Return validation message.
                return {
                    success: false,
                    message: validator.data.configuration.message
                };

            }
        });
}

// Export the validation utility functions.
module.exports = ValidationUtilities;
},{"../polyfills/promise":16}],31:[function(require,module,exports){
/**
 * Validates a field to ensure its value matches a regular expression.
 * @param configuration The configuration for this regex validator.
 * @constructor
 */
function RegexValidator(configuration) {
    this.regex = new RegExp(configuration.pattern);
}

/**
 * Validates the the specified text matches the regex.
 * @param value {string} The text value to validate.
 * @returns {Promise} A promise that resolves to true, if the text is valid; otherwise, false.
 */
RegexValidator.prototype.validateText = function (value) {
    let self = this;
    return new (require("../polyfills/promise"))(function (resolve) {
        let isValueSet = require("../utils/validation").isValueSet;
        if (!isValueSet(value)) {
            value = "";
        }
        resolve(self.regex.test(value));
    });
};

/**
 * Validates the the specified boolean matches the regex.
 * @param value {boolean} The boolean value to validate.
 * @returns {Promise} A promise that resolves to true, if the boolean matches the regex; otherwise, false.
 */
RegexValidator.prototype.validateBool = function (value) {
    let self = this,
        textValue = value === true
            ? "true"
            : "false";
    return new (require("../polyfills/promise"))(function (resolve) {
        resolve(self.regex.test(textValue));
    });
};

/**
 * Validates that the specified array of text values all match the regex and that the array is not null/empty.
 * @param value {string[]} The array of text values.
 * @returns {Promise} A promise that resolves to true, if the array of text values is valid; otherwise, false.
 */
RegexValidator.prototype.validateTextArray = function (value) {
    let self = this;
    return new (require("../polyfills/promise"))(function (resolve) {

        // Variables.
        let i, item,
            isValueSet = require("../utils/validation").isValueSet;

        // Check if the value is a valid array.
        if (!isValueSet(value) || !Array.isArray(value)) {
            resolve(false);
            return;
        }

        // Check if the array has at least one value.
        if (value.length === 0) {
            resolve(false);
            return;
        }

        // Check if each value in the array matches the regex.
        for (i = 0; i < value.length; i++) {
            item = value[i];
            if (!self.validateText(item)) {
                resolve(false);
                return;
            }
        }

        // All tests passed. Array value is valid.
        resolve(true);

    });
};

/**
 * Validates that the specified filename matches the regex.
 * @param value {*} The file value.
 * @returns {Promise} A promise that resolves to true, if the filename matches; otherwise, false.
 */
RegexValidator.prototype.validateFile = function (value) {
    let hasFile = !!value,
        filename = hasFile
            ? value.name
            : null;
    return this.validateText(filename);
};

// Export the field validator configuration.
module.exports = {
    key: "regex",
    validator: RegexValidator
};
},{"../polyfills/promise":16,"../utils/validation":30}],32:[function(require,module,exports){
/**
 * Validates a field to ensure its value is set.
 * @constructor
 */
function RequiredValidator() {
}

/**
 * Validates the the specified text is set (i.e., not null/empty).
 * @param value {string} The text value to validate.
 * @returns {Promise} A promise that resolves to true, if the text is valid; otherwise, false.
 */
RequiredValidator.prototype.validateText = function (value) {
    return new (require("../polyfills/promise"))(function (resolve) {
        let isValueSet = require("../utils/validation").isValueSet,
            valid = isValueSet(value) && value.hasOwnProperty("length") && value.length > 0;
        resolve(valid);
    });
};

/**
 * Validates the the specified boolean is true.
 * @param value {boolean} The boolean value to validate.
 * @returns {Promise} A promise that resolves to true, if the boolean is true; otherwise, false.
 */
RequiredValidator.prototype.validateBool = function (value) {
    return new (require("../polyfills/promise"))(function (resolve) {
        resolve(value === true);
    });
};

/**
 * Validates that the specified array of text values are all set and that the array is not null/empty.
 * @param value {string[]} The array of text values.
 * @returns {Promise} A promise that resolves to true, if the array of text values is valid; otherwise, false.
 */
RequiredValidator.prototype.validateTextArray = function (value) {
    let self = this;
    return new (require("../polyfills/promise"))(function (resolve) {

        // Variables.
        let i, item,
            isValueSet = require("../utils/validation").isValueSet;

        // Check if the value is a valid array.
        if (!isValueSet(value) || !Array.isArray(value)) {
            resolve(false);
            return;
        }

        // Check if the array has at least one value.
        if (value.length === 0) {
            resolve(false);
            return;
        }

        // Check if each value in the array is set to some valid text.
        for (i = 0; i < value.length; i++) {
            item = value[i];
            if (!self.validateText(item)) {
                resolve(false);
                return;
            }
        }

        // All tests passed. Array value is valid.
        resolve(true);

    });
};

/**
 * Validates that the specified file has been selected.
 * @param value {*} The file value.
 * @returns {Promise} A promise that resolves to true, if the file is selected; otherwise, false.
 */
RequiredValidator.prototype.validateFile = function (value) {
    return new (require("../polyfills/promise"))(function (resolve) {
        let hasFile = !!value && !!value.name;
        resolve(hasFile);
    });
};

// Export the field validator configuration.
module.exports = {
    key: "required",
    validator: RequiredValidator
};
},{"../polyfills/promise":16,"../utils/validation":30}],33:[function(require,module,exports){
/**@license MIT-promiscuous-©Ruben Verborgh*/
(function (func, obj) {
  // Type checking utility function
  function is(type, item) { return (typeof item)[0] == type; }

  // Creates a promise, calling callback(resolve, reject), ignoring other parameters.
  function Promise(callback, handler) {
    // The `handler` variable points to the function that will
    // 1) handle a .then(resolved, rejected) call
    // 2) handle a resolve or reject call (if the first argument === `is`)
    // Before 2), `handler` holds a queue of callbacks.
    // After 2), `handler` is a finalized .then handler.
    handler = function pendingHandler(resolved, rejected, value, queue, then, i) {
      queue = pendingHandler.q;

      // Case 1) handle a .then(resolved, rejected) call
      if (resolved != is) {
        return Promise(function (resolve, reject) {
          queue.push({ p: this, r: resolve, j: reject, 1: resolved, 0: rejected });
        });
      }

      // Case 2) handle a resolve or reject call
      // (`resolved` === `is` acts as a sentinel)
      // The actual function signature is
      // .re[ject|solve](<is>, success, value)

      // Check if the value is a promise and try to obtain its `then` method
      if (value && (is(func, value) | is(obj, value))) {
        try { then = value.then; }
        catch (reason) { rejected = 0; value = reason; }
      }
      // If the value is a promise, take over its state
      if (is(func, then)) {
        try { then.call(value, transferState(1), rejected = transferState(0)); }
        catch (reason) { rejected(reason); }
      }
      // The value is not a promise; handle resolve/reject
      else {
        // Replace this handler with a finalized resolved/rejected handler
        handler = function (Resolved, Rejected) {
          // If the Resolved or Rejected parameter is not a function,
          // return the original promise (now stored in the `callback` variable)
          if (!is(func, (Resolved = rejected ? Resolved : Rejected)))
            return callback;
          // Otherwise, return a finalized promise, transforming the value with the function
          return Promise(function (resolve, reject) { finalize(this, resolve, reject, value, Resolved); });
        };
        // Resolve/reject pending callbacks
        i = 0;
        while (i < queue.length) {
          then = queue[i++];
          // If no callback, just resolve/reject the promise
          if (!is(func, resolved = then[rejected]))
            (rejected ? then.r : then.j)(value);
          // Otherwise, resolve/reject the promise with the result of the callback
          else
            finalize(then.p, then.r, then.j, value, resolved);
        }
      }
      // Returns a function that transfers the state of the promise
      function transferState(resolved) {
        return function (value) { then && (then = 0, pendingHandler(is, resolved, value)); };
      }
    };
    // The queue of pending callbacks; garbage-collected when handler is resolved/rejected
    handler.q = [];

    // Create and return the promise (reusing the callback variable)
    callback.call(callback = { then:    function (resolved, rejected) { return handler(resolved, rejected); },
                               "catch": function (rejected)           { return handler(0,        rejected); } },
                  function (value)  { handler(is, 1,  value); },
                  function (reason) { handler(is, 0, reason); });
    return callback;
  }

  // Finalizes the promise by resolving/rejecting it with the transformed value
  function finalize(promise, resolve, reject, value, transform) {
    setTimeout(function () {
      try {
        // Transform the value through and check whether it's a promise
        value = transform(value);
        transform = value && (is(obj, value) | is(func, value)) && value.then;
        // Return the result if it's not a promise
        if (!is(func, transform))
          resolve(value);
        // If it's a promise, make sure it's not circular
        else if (value == promise)
          reject(TypeError());
        // Take over the promise's state
        else
          transform.call(value, resolve, reject);
      }
      catch (error) { reject(error); }
    });
  }

  // Export the main module
  module.exports = Promise;

  // Creates a resolved promise
  Promise.resolve = ResolvedPromise;
  function ResolvedPromise(value) { return Promise(function (resolve) { resolve(value); }); }

  // Creates a rejected promise
  Promise.reject = function (reason) { return Promise(function (resolve, reject) { reject(reason); }); };

  // Transforms an array of promises into a promise for an array
  Promise.all = function (promises) {
    return Promise(function (resolve, reject, count, values) {
      // Array of collected values
      values = [];
      // Resolve immediately if there are no promises
      count = promises.length || resolve(values);
      // Transform all elements (`map` is shorter than `forEach`)
      promises.map(function (promise, index) {
        ResolvedPromise(promise).then(
          // Store the value and resolve if it was the last
          function (value) {
            values[index] = value;
            --count || resolve(values);
          },
          // Reject if one element fails
          reject);
      });
    });
  };

  // Returns a promise that resolves or rejects as soon as one promise in the array does
  Promise.race = function (promises) {
    return Promise(function (resolve, reject) {
      // Register to all promises in the array
      promises.map(function (promise) {
        ResolvedPromise(promise).then(resolve, reject);
      });
    });
  };
})('f', 'o');

},{}]},{},[15]);
