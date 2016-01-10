// Variables.
var app = angular.module("umbraco");

// Register directive/controller.
app.controller("formulate.deleteDataValueConfirmation", Controller);
app.directive("formulateDeleteDataValueConfirmation", Directive);

// Directive.
function Directive(formulateDirectives) {
    return {
        restrict: "E",
        template: formulateDirectives.get("deleteDataValueConfirmation/deleteDataValue.html")
    };
}

// Controller.
function Controller($scope, $location, notificationsService, $q,
    $http, navigationService, formulateDataValues, treeService) {

    // Variables.
    var dataValueId = $scope.currentNode.id;

    // Variable containing the common services (easier to pass around).
    var services = {
        $scope: $scope,
        $location: $location,
        notificationsService: notificationsService,
        $q: $q,
        $http: $http,
        navigationService: navigationService,
        formulateDataValues: formulateDataValues,
        treeService: treeService
    };

    // Assign scope variables.
    $scope.initialized = false;

    // Assign functions on scope.
    $scope.deleteDataValue = getDeleteDataValue(services);
    $scope.cancel = getCancel(services);

    // Load data value information.
    loadDataValueInfo(dataValueId, services);

}

// Loads information about the data value.
function loadDataValueInfo(dataValueId, services) {
    services.formulateDataValues.getDataValueInfo(dataValueId).then(function(dataValue) {
        services.$scope.dataValueId = dataValue.dataValueId;
        services.$scope.dataValueName = dataValue.name;
        services.$scope.initialized = true;
    });
}

// Returns a function that deletes a data value.
function getDeleteDataValue(services) {
    return function() {

        // Variables.
        var node = services.$scope.currentNode;
        var dataValueId = services.$scope.dataValueId;
        var dataValuePromise = services.formulateDataValues.getDataValueInfo(dataValueId);

        // Once we have the data value information...
        dataValuePromise.then(function (dataValue) {

            // Variables.
            var path = dataValue.path;
            var partialPath = path.slice(0, path.length - 1);

            // Delete data value.
            services.formulateDataValues.deleteDataValue(dataValueId)
                .then(function () {

                    // Remove the node from the tree.
                    services.treeService.removeNode(node);

                    // Update tree (down to the deleted dataValue's parent).
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