// Requirements.
var getDirective = require("../Utilities/getDirective.js");

// Variables.
var app = angular.module("umbraco");

// Service to help with directives.
app.factory("formulateDirectives", function() {

    /**
     * Handles retrieval of directives.
     * @module services/formulateDirectives
     */
    return {

        /**
         * Retrieves the markup for a directive.
         * @param {string} path - The path to the directive (e.g., "layoutDesigner/designer.html").
         * @returns {string} The markup for the directive.
         */
        get: function(path) {
            return getDirective(path);
        }

    };
});