// Requirements.
var getDirective = require("../Utilities/getDirective.js");

// Variables.
var app = angular.module("umbraco");

// Service to help with directives.
app.factory("formulateDirectives", function() {
    return {
        get: function(directive) {
            return getDirective(directive);
        }
    };
});