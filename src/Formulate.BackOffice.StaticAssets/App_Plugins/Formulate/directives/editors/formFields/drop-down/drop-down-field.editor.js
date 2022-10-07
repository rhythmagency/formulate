"use strict";

(function () {
    // Directive.
    function directive() {
        return {
            restrict: "E",
            replace: true,
            templateUrl: "/App_Plugins/FormulateBackOffice/directives/editors/formFields/drop-down/drop-down-field.editor.html",
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
    angular.module("umbraco").directive("formulateDropDownField", directive);

})();