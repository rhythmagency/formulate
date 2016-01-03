// Variables.
var app = angular.module("umbraco");

// Register directive/controller.
app.controller("formulate.validationPickerDialog", controller);
app.directive("formulateValidationPickerDialog", directive);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        controller: "formulate.validationPickerDialog",
        controllerAs: "ctrl",
        template: formulateDirectives.get(
            "dialogs/validationPicker/validationPicker.html"),
        scope: {
        }
    };
}

// Controller.
function controller($scope, formulateVars) {

    // Initialize scope variables.
    $scope.selection = [];
    $scope.entityKinds = ["Validation"];
    $scope.rootId = formulateVars["Validation.RootId"];

    // Set scope functions.
    $scope.cancel = function() {
        $scope.$parent.close();
    };
    $scope.select = function() {
        $scope.$parent.submit($scope.selection);
    }

}