// Variables.
let forms, renderers;

// Add Promise support (for IE11).
if (typeof Promise === "undefined") {
    require("promiscuous/promiscuous-browser-full");
}

// Get field renderers.
renderers = require("./steps/get-field-renderers")();

// Get form data.
forms = require("./steps/get-form-data")();

// Render the forms.
require("./steps/render-forms")(forms, renderers);