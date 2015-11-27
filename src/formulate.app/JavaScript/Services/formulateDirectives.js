// Requirements.
var getDirective = require("../Utilities/getDirective.js");

// Service.
var app = angular.module("umbraco");
app.factory("formulateDirectives", function() {
    return {
        get: function(directive) {
            return getDirective(directive);
        }
    };
});