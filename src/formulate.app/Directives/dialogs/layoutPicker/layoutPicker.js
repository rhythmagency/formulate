// Variables.
var app = angular.module("umbraco");

// Register directive/controller.
app.controller("formulate.layoutPickerDialog", controller);
app.directive("formulateLayoutPickerDialog", directive);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        controller: "formulate.layoutPickerDialog",
        template: formulateDirectives.get(
            "dialogs/layoutPicker/layoutPicker.html"),
        scope: {
            maxCount: "="
        }
    };
}

// Controller.
function controller($scope, formulateVars) {

    // Initialize scope variables.
    $scope.selection = [];
    $scope.entityKinds = ["Layout"];
    $scope.rootId = formulateVars["Layout.RootId"];

    // Set scope functions.
    $scope.cancel = function() {
        $scope.$parent.close();
    };
    $scope.select = function() {
        $scope.$parent.submit($scope.selection);
    }

}