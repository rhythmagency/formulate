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
        nestFieldInLabel: true,
        wrapLabelText: true,
        fieldBeforeLabelText: true
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