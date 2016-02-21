// Variables.
var app = angular.module("umbraco");

// Register directive/controller.
app.controller("formulate.moveValidationDialog", controller);
app.directive("formulateMoveValidationDialog", directive);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        controller: "formulate.moveValidationDialog",
        template: formulateDirectives.get(
            "dialogs/moveEntity/moveEntity.html")
    };
}

// Controller.
function controller($scope, $rootScope, formulateVars, formulateValidations,
    navigationService, treeService) {

    // Variables.
    var services = {
        $scope: $scope,
        $rootScope: $rootScope,
        formulateValidations: formulateValidations,
        navigationService: navigationService,
        treeService: treeService
    };

    // Initialize scope variables.
    $scope.selection = [];
    $scope.entityKinds = ["Folder", "Root"];
    $scope.rootId = formulateVars["Validation.RootId"];

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

// Returns the function that moves the validation.
function getMove(services) {
    return function() {

        // Variables.
        var $scope = services.$scope;
        var node = $scope.currentNode;
        var entityId = $scope.currentNode.id;
        var selection = $scope.selection;

        // Is a new parent selected?
        if (selection.length === 1) {

            // Move validation.
            var newParentId = selection[0];
            services.formulateValidations.moveValidation(entityId, newParentId).then(function(data) {

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

                // Send notification that validation was moved.
                services.$rootScope.$broadcast("formulateEntityMoved", {
                    id: data.id,
                    path: data.path
                });

                // Close dialog.
                services.navigationService.hideDialog();

            });

        } else {
            alert("Make a selection first.");
        }

    };
}