// Variables.
var app = angular.module("umbraco");

// Register directive/controller.
app.controller("formulate.formPickerDialog", controller);
app.directive("formulateFormPickerDialog", directive);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        controller: "formulate.formPickerDialog",
        template: formulateDirectives.get(
            "dialogs/formPicker/formPicker.html"),
        scope: {
            maxCount: "="
        }
    };
}

// Controller.
function controller($scope, formulateVars) {

    // Initialize scope variables.
    $scope.selection = [];
    $scope.entityKinds = ["Form"];
    $scope.rootId = formulateVars["Form.RootId"];

    // Set scope functions.
    $scope.cancel = function() {
        $scope.$parent.close();
    };
    $scope.select = function() {
        $scope.$parent.submit($scope.selection);
    }

}