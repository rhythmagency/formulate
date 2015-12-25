// Variables.
var app = angular.module("umbraco");

// Register directive/controller.
app.controller("formulate.deleteValidationConfirmation", controller);
app.directive("formulateDeleteValidationConfirmation", directive);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        template: formulateDirectives.get(
            "deleteValidationConfirmation/deleteValidation.html")
    };
}

// Controller.
function controller($scope, $location, notificationsService, $q,
    $http, navigationService, formulateValidations, treeService) {

    // Variables.
    var validationId = $scope.currentNode.id;

    // Variable containing the common services (easier to pass around).
    var services = {
        $scope: $scope,
        $location: $location,
        notificationsService: notificationsService,
        $q: $q,
        $http: $http,
        navigationService: navigationService,
        formulateValidations: formulateValidations,
        treeService: treeService
    };

    // Assign scope variables.
    $scope.initialized = false;

    // Assign functions on scope.
    $scope.deleteValidation = getDeleteValidation(services);
    $scope.cancel = getCancel(services);

    // Load validation information.
    loadValidationInfo(validationId, services);

}

// Loads information about the validation.
function loadValidationInfo(validationId, services) {
    services.formulateValidations.getValidationInfo(validationId)
        .then(function (validation) {
            services.$scope.validationId = validation.validationId;
            services.$scope.validationName = validation.name;
            services.$scope.initialized = true;
        });
}

// Returns a function that deletes a validation.
function getDeleteValidation(services) {
    return function() {

        // Variables.
        var node = services.$scope.currentNode;
        var validationId = services.$scope.validationId;
        var layotPromise = services.formulateValidations
            .getValidationInfo(validationId);

        // Once we have the validation information...
        layotPromise.then(function (validation) {

            // Variables.
            var path = validation.path;
            var partialPath = path.slice(0, path.length - 1);

            // Delete validation.
            services.formulateValidations.deleteValidation(validationId)
                .then(function () {

                    // Remove the node from the tree.
                    services.treeService.removeNode(node);

                    // Update tree (down to the deleted validation's parent).
                    var options = {
                        tree: "formulate",
                        path: partialPath,
                        forceReload: true,
                        activate: false
                    };
                    services.navigationService.syncTree(options);

                    // Close dialog.
                    services.navigationService.hideDialog();

                });

        });

    };
}

// Returns the function that cancels the deletion.
function getCancel(services) {
    return function () {
        services.navigationService.hideDialog();
    };
}