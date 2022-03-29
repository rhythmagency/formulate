"use strict";

(function () {

    // Variables.
    const app = angular.module("umbraco");

    // Associate directive.
    app.directive("formulateFieldChooser", directive);

    // Directive.
    function directive() {
        return {
            restrict: "E",
            templateUrl: "/app_plugins/formulatebackoffice/directives/overlays/formfieldchooser/form-field-chooser.html",
            controller: controller,
            scope: {
                model: "=",
            }
        };
    }

    // Controller.
    function controller($scope, formulateTypeDefinitionResource) {

        // Handle chosen item.
        $scope.choseItem = function(item) {
            $scope.model.chosen({
                field: item
            });
        };

        // Get the form handler definitions.
        formulateTypeDefinitionResource.getFieldDefinitions()
            .then((response) => {
                $scope.items = response;
            });

    }

})();