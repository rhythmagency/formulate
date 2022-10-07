"use strict";

(function () {
    // Controller.
    function controller(formulateFields, $scope) {

        // Variables.
        $scope.kinds = {
            values: []
        };

        // Get the button kinds.
        formulateFields.getButtonKinds().then(function (kinds) {
            $scope.kinds.values = kinds.map(function (item) {
                const kind = item.kind;
                return {
                    label: kind,
                    value: kind
                };
            });
        });
    }

    // Directive.
    function directive() {
        return {
            restrict: "E",
            replace: true,
            templateUrl: "/App_Plugins/FormulateBackOffice/directives/editors/formFields/button/button-field.editor.html",
            controller: controller,
            scope: {
                configuration: "="
            }
        };
    }

    // Associate directive/controller.
    angular.module("umbraco").directive("formulateButtonField", directive);

})();