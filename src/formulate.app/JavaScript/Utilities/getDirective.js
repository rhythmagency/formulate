var directives = require("../../../FormulateTemp/directives.js")();
var getDirective = function (path) {
    return directives[path];
};
module.exports = getDirective;