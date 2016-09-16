// Variables.
var app = angular.module("umbraco");

// Register directive/controller.
app.controller("formulate.deleteLayoutConfirmation", controller);
app.directive("formulateDeleteLayoutConfirmation", directive);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        template: formulateDirectives.get("deleteLayoutConfirmation/deleteLayout.html"),
        controller: "formulate.deleteLayoutConfirmation"
    };
}

// Controller.
function controller($scope, $location, $q, $http, navigationService,
    formulateLayouts, treeService) {

    // Variables.
    var layoutId = $scope.currentNode.id;

    // Variable containing the common services (easier to pass around).
    var services = {
        $scope: $scope,
        $location: $location,
        $q: $q,
        $http: $http,
        navigationService: navigationService,
        formulateLayouts: formulateLayouts,
        treeService: treeService
    };

    // Assign scope variables.
    $scope.initialized = false;

    // Assign functions on scope.
    $scope.deleteLayout = getDeleteLayout(services);
    $scope.cancel = getCancel(services);

    // Load layout information.
    loadLayoutInfo(layoutId, services);

}

// Loads information about the layout.
function loadLayoutInfo(layoutId, services) {
    services.formulateLayouts.getLayoutInfo(layoutId).then(function(layout) {
        services.$scope.layoutId = layout.layoutId;
        services.$scope.layoutName = layout.name;
        services.$scope.initialized = true;
    });
}

// Returns a function that deletes a layout.
function getDeleteLayout(services) {
    return function() {

        // Variables.
        var node = services.$scope.currentNode;
        var layoutId = services.$scope.layoutId;
        var layoutPromise = services.formulateLayouts.getLayoutInfo(layoutId);

        // Once we have the layout information...
        layoutPromise.then(function (layout) {

            // Variables.
            var path = layout.path;
            var partialPath = path.slice(0, path.length - 1);

            // Delete layout.
            services.formulateLayouts.deleteLayout(layoutId)
                .then(function () {

                    // Remove the node from the tree.
                    services.treeService.removeNode(node);

                    // Update tree (down to the deleted layout's parent).
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