// Variables.
var app = angular.module("umbraco");

// Register directive/controller.
app.controller("formulate.moveLayoutDialog", controller);
app.directive("formulateMoveLayoutDialog", directive);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        controller: "formulate.moveLayoutDialog",
        template: formulateDirectives.get(
            "dialogs/moveEntity/moveEntity.html")
    };
}

// Controller.
function controller($scope, $rootScope, formulateVars, formulateLayouts,
    navigationService, treeService) {

    // Variables.
    var services = {
        $scope: $scope,
        $rootScope: $rootScope,
        formulateLayouts: formulateLayouts,
        navigationService: navigationService,
        treeService: treeService
    };

    // Initialize scope variables.
    $scope.selection = [];
    $scope.entityKinds = ["Folder", "Root"];
    $scope.rootId = formulateVars["Layout.RootId"];

    // Set scope functions.
    $scope.cancel = getCancel(services);
    $scope.move = getMove(services);

}

// Returns the function that cancels the move.
function getCancel(services) {
    return function() {
        services.navigationService.hideDialog();
    };
}

// Returns the function that moves the layout.
function getMove(services) {
    return function() {

        // Variables.
        var $scope = services.$scope;
        var node = $scope.currentNode;
        var entityId = $scope.currentNode.id;
        var selection = $scope.selection;

        // Is a new parent selected?
        if (selection.length === 1) {

            // Move layout.
            var newParentId = selection[0];
            services.formulateLayouts.moveLayout(entityId, newParentId).then(function(data) {

                // Remove the node from its old position in the tree.
                services.treeService.removeNode(node);

                // Update tree.
                var options = {
                    tree: "formulate",
                    path: data.path,
                    forceReload: true,
                    activate: false
                };
                services.navigationService.syncTree(options);

                // Send notification that layout was moved.
                services.$rootScope.$broadcast("formulateEntityMoved", {
                    id: data.id,
                    path: data.path
                });

                // Close dialog.
                services.navigationService.hideDialog();

            });

        } else {
            //TODO: Localize.
            alert("Make a selection first.");
        }

    };
}