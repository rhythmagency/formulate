"use strict";

(function () {

    // Variables.
    let app = angular.module("umbraco");

    // Associate directive/controller.
    app.directive("formulateHandlerChooser", directive);
    app.controller("formulate.handlerChooser", controller);

    // Directive.
    function directive() {
        return {
            restrict: "E",
            templateUrl: "/app_plugins/formulatebackoffice/directives/overlays/formhandlerchooser/form-handler-chooser.html",
            controller: "formulate.handlerChooser",
            scope: {
                model: "=",
            }
        };
    }

    // Controller.
    function controller($scope, $element) {//, formulateHandlers) {

        // Set scope variables.
        /*formulateHandlers.getHandlerTypes().then(function(handlerTypes) {
            $scope.items = handlerTypes;
        });*/

        //TODO: Temp. Get from server.
        $scope.items = [
            {
                "icon": "icon-formulate-store-data",
                "typeLabel": "Store Data",
                "directive": "formulate-store-data-handler",
            },
            {
                "icon": "icon-formulate-email",
                "typeLabel": "Email",
                "directive": "formulate-email-handler",
            },
            {
                "icon": "icon-formulate-send-data",
                "typeLabel": "Send Data",
                "directive": "formulate-send-data-handler",
            },
        ];

        $scope.dialogStyles = {};

        // Handle chosen item.
        $scope.choseItem = function(item) {
            $scope.model.chosen({
                handler: item
            });
        };

    }

})();