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