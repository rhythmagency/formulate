"use strict";

(function () {
    // Controller.
    function controller($scope) {


    }

    // Directive.
    function directive() {
        return {
            restrict: "E",
            replace: true,
            templateUrl: "/app_plugins/formulate/directives/editors/formFields/header/header-field.editor.html",
            controller: controller,
            scope: {
                configuration: "="
            }
        };
    }

    // Associate directive/controller.
    angular.module("umbraco").directive("formulateHeaderField", directive);

})();