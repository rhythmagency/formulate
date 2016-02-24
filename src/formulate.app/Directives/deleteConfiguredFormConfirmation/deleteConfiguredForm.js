// Variables.
var app = angular.module("umbraco");

// Register directive/controller.
app.controller("formulate.deleteConfiguredFormConfirmation", controller);
app.directive("formulateDeleteConfiguredFormConfirmation", directive);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        template: formulateDirectives.get("deleteConfiguredFormConfirmation/deleteConfiguredForm.html"),
        controller: "formulate.deleteConfiguredFormConfirmation"
    };
}

// Controller.
function controller($scope, $location, notificationsService, $q,
    $http, navigationService, formulateConfiguredForms, treeService) {

    // Variables.
    var conFormId = $scope.currentNode.id;

    // Variable containing the common services (easier to pass around).
    var services = {
        $scope: $scope,
        $location: $location,
        notificationsService: notificationsService,
        $q: $q,
        $http: $http,
        navigationService: navigationService,
        formulateConfiguredForms: formulateConfiguredForms,
        treeService: treeService
    };

    // Assign scope variables.
    $scope.initialized = false;

    // Assign functions on scope.
    $scope.deleteConfiguredForm = getDeleteConfiguredForm(services);
    $scope.cancel = getCancel(services);

    // Load configured form information.
    loadConfiguredFormInfo(conFormId, services);

}

// Loads information about the configured form.
function loadConfiguredFormInfo(conFormId, services) {
    services.formulateConfiguredForms.getConfiguredFormInfo(conFormId).then(function(conForm) {
        services.$scope.conFormId = conForm.conFormId;
        services.$scope.conFormName = conForm.name;
        services.$scope.initialized = true;
    });
}

// Returns a function that deletes a configured form.
function getDeleteConfiguredForm(services) {
    return function() {

        // Variables.
        var node = services.$scope.currentNode;
        var conFormId = services.$scope.conFormId;
        var conFormPromise = services.formulateConfiguredForms.getConfiguredFormInfo(conFormId);

        // Once we have the configured form information...
        conFormPromise.then(function (conForm) {

            // Variables.
            var path = conForm.path;
            var partialPath = path.slice(0, path.length - 1);

            // Delete configured form.
            services.formulateConfiguredForms.deleteConfiguredForm(conFormId)
                .then(function () {

                    // Remove the node from the tree.
                    services.treeService.removeNode(node);

                    // Update tree (down to the deleted configured form's parent).
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