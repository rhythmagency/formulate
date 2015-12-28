// Require the auto-generated file containing the directives.
var directives = require("../../../FormulateTemp/directives.js")();

// Gets a directive.
var getDirective = function (path) {
    return directives[path];
};

// Export function.
module.exports = getDirective;