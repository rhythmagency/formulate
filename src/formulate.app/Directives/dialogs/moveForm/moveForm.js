// Variables.
var app = angular.module("umbraco");

// Register directive/controller.
app.controller("formulate.moveFormDialog", controller);
app.directive("formulateMoveFormDialog", directive);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        controller: "formulate.moveFormDialog",
        template: formulateDirectives.get(
            "dialogs/moveForm/moveForm.html")
    };
}

// Controller.
function controller($scope, formulateVars) {

    // Initialize scope variables.
    $scope.selection = [];
    $scope.entityKinds = ["Folder", "Root"];
    $scope.rootId = formulateVars["Form.RootId"];

    // Set scope functions.
    $scope.cancel = function() {
        $scope.$parent.close();
    };
    $scope.move = getMove();

}


function getMove() {
    return function() {

        var entityId = $scope.currentNode.id;
        var selection = $scope.selection;
        if (selection.length === 1) {
            // TODO: Move to selection.
            // TODO: Update tree.
        } else {
            //TODO: User made no selection.
        }

        // Close dialog.
        services.navigationService.hideDialog();

    };
}