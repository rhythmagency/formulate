"use strict";

(function () {
    // Directive.
    function directive() {
        return {
            restrict: "E",
            replace: true,
            templateUrl: "/app_plugins/formulate/directives/editors/formFields/checkbox-list/checkbox-list-field.editor.html",
            controller: controller,
            scope: {
                configuration: "="
            }
        };
    }

    // Controller.
    function controller($scope) {
        $scope.configuration = $scope.configuration || {};
    }
    // Associate directive/controller.
    angular.module("umbraco").directive("formulateCheckboxListField", directive);

})();