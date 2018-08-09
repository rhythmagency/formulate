/**
 * Renders an upload field.
 * @param fieldData The field data that should be used to render the text field.
 * @param fieldValidators The associative array of the field validating functions.
 * @param cssClasses The CSS classes to attach to the element.
 * @constructor
 */
function RenderUpload(fieldData, fieldValidators, cssClasses) {
    require("../utils/field").initializeField(this, fieldData, fieldValidators, {
        type: "file",
        cssClasses: cssClasses
    });
}

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
    require("../utils/field").setData(data, this.getFile(), options, this.alias, this.id);
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

// Export the field renderer configuration.
module.exports = {
    key: "upload",
    renderer: RenderUpload
};