// Variables.
var app = angular.module("umbraco");

// Register directive/controller.
app.controller("formulate.dataValuePickerDialog", controller);
app.directive("formulateDataValuePickerDialog", directive);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        controller: "formulate.dataValuePickerDialog",
        template: formulateDirectives.get(
            "dialogs/dataValuePicker/dataValuePicker.html"),
        scope: {
            maxCount: "="
        }
    };
}

// Controller.
function controller($scope, formulateVars) {

    // Initialize scope variables.
    $scope.selection = [];
    $scope.entityKinds = ["DataValue"];
    $scope.rootId = formulateVars["DataValue.RootId"];

    // Set scope functions.
    $scope.cancel = function() {
        $scope.$parent.close();
    };
    $scope.select = function() {
        $scope.$parent.submit($scope.selection);
    }

}