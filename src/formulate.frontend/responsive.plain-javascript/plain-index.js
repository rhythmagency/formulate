// Variables.
let forms, renderers;

// Get field renderers.
renderers = require("./steps/get-field-renderers")();

// Get form data.
forms = require("./steps/get-form-data")();

// Render the forms.
require("./steps/render-forms")(forms, renderers);