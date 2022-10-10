"use strict";

(function () {

    // Variables.
    var app = angular.module("umbraco");

    // Associate directive.
    app.directive("formulateHiddenField", directive);

    // Directive.
    function directive() {
        return {
            restrict: "E",
            replace: true,
            templateUrl: "/app_plugins/formulate/editors/form-fields/text-constant/text-constant-field.editor.html",
            scope: {
                configuration: "="
            }
        };
    }
})();