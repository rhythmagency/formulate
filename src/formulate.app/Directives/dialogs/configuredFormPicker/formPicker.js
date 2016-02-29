// Variables.
var app = angular.module("umbraco");

// Register directive/controller.
app.controller("formulate.configuredFormPickerDialog", controller);
app.directive("formulateConfiguredFormPickerDialog", directive);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        controller: "formulate.configuredFormPickerDialog",
        template: formulateDirectives.get(
            "dialogs/configuredFormPicker/formPicker.html"),
        scope: {
            maxCount: "="
        }
    };
}

// Controller.
function controller($scope, formulateVars) {

    // Initialize scope variables.
    $scope.selection = [];
    $scope.entityKinds = ["ConfiguredForm"];
    $scope.rootId = formulateVars["Form.RootId"];

    // Set scope functions.
    $scope.cancel = function() {
        $scope.$parent.close();
    };
    $scope.select = function() {
        $scope.$parent.submit($scope.selection);
    }

}