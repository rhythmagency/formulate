// Variables.
var app = angular.module("umbraco");

// Register directive/controller.
app.controller("formulate.moveFolderDialog", controller);
app.directive("formulateMoveFolderDialog", directive);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        controller: "formulate.moveFolderDialog",
        template: formulateDirectives.get(
            "dialogs/moveEntity/moveEntity.html")
    };
}

// Controller.
function controller($scope, $rootScope, formulateVars, formulateFolders,
    navigationService, treeService) {

    // Variables.
    var services = {
        $scope: $scope,
        $rootScope: $rootScope,
        formulateFolders: formulateFolders,
        navigationService: navigationService,
        treeService: treeService
    };

    // Initialize scope variables.
    $scope.selection = [];
    $scope.entityKinds = ["Folder", "Root"];
    $scope.rootId = getRootId($scope.currentNode);

    // Set scope functions.
    $scope.cancel = getCancel(services);
    $scope.move = getMove(services);

}

// Gets the root ID for the specified node.
function getRootId(node) {
    var rootId;
    while (node.parentId) {
        rootId = node.id;
        node = node.parent();
    }
    return rootId;
}

// Returns the function that cancels the move.
function getCancel(services) {
    return function() {
        services.navigationService.hideDialog();
    };
}

// Returns the function that moves the folder.
function getMove(services) {
    return function() {

        // Variables.
        var $scope = services.$scope;
        var node = $scope.currentNode;
        var entityId = $scope.currentNode.id;
        var selection = $scope.selection;

        // Is a new parent selected?
        if (selection.length === 1) {

            // Move folder.
            var newParentId = selection[0];
            services.formulateFolders.moveFolder(entityId, newParentId).then(function(data) {

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

                // Send notifications that folder and descendants were moved.
                services.$rootScope.$broadcast("formulateEntityMoved", {
                    id: data.id,
                    path: data.path
                });
                for (var i = 0; i < data.descendants.length; i++) {
                    var entity = data.descendants[i];
                    services.$rootScope.$broadcast("formulateEntityMoved", {
                        id: entity.id,
                        path: entity.path
                    });
                }

                // Close dialog.
                services.navigationService.hideDialog();

            });

        } else {
            //TODO: Localize.
            alert("Make a selection first.");
        }

    };
}