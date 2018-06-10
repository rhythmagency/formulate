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