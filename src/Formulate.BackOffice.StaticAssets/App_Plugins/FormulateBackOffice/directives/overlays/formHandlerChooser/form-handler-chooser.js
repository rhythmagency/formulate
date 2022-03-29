"use strict";

(function () {

    // Variables.
    const app = angular.module("umbraco");

    // Associate directive.
    app.directive("formulateHandlerChooser", directive);

    // Directive.
    function directive() {
        return {
            restrict: "E",
            templateUrl: "/app_plugins/formulatebackoffice/directives/overlays/formhandlerchooser/form-handler-chooser.html",
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
                handler: item
            });
        };

        // Get the form handler definitions.
        formulateTypeDefinitionResource.getHandlerDefinitions()
            .then((response) => {
                $scope.items = response;
            });

    }

})();