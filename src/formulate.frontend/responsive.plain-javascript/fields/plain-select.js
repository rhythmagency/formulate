// Dependencies.
let FieldUtility = require("../utils/field");

/**
 * Renders a drop down field.
 * @param fieldData The field data that should be used to render the drop down field.
 * @param fieldValidators The associative array of the field validating functions.
 * @param cssClasses The CSS classes to attach to the element.
 * @constructor
 */
function RenderSelect(fieldData, fieldValidators, cssClasses) {
    FieldUtility.initializeField(this, fieldData, fieldValidators, {
        nodeName: "select",
        cssClasses: cssClasses
    });
    this.addOptions(fieldData);
}

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

    // Add a CSS class to indicate no options have been selected yet.
    this.wrapper.classList.add("formulate__field--select--initial");

    // Listen for change events.
    this.element.addEventListener("change", this.handleValueChange.bind(this));

    // Append the options to the select.
    this.element.appendChild(fragment);

};

/**
 * Handles changes to the drop down value.
 */
RenderSelect.prototype.handleValueChange = function () {

    // Add (or remove) a CSS class depending on if an option has been selected.
    if (this.element.value === "") {
        this.wrapper.classList.add("formulate__field--select--initial");
    } else {
        this.wrapper.classList.remove("formulate__field--select--initial");
    }

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