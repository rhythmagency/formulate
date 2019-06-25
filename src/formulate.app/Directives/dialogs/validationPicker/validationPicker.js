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
        template: formulateDirectives.get(
            "dialogs/validationPicker/validationPicker.html"),
        scope: {
            model: "="
        }
    };
}

// Controller.
function controller($scope, formulateVars, localizationService) {
    // Initialize scope variables.
    $scope.selection = [];
    $scope.previouslySelectedIds = $scope.model.validations || [];
    $scope.entityKinds = ["Validation"];
    $scope.rootId = formulateVars["Validation.RootId"];
    localizeTitle();

    // Set scope functions.
    $scope.cancel = function () {
        if ($scope.model.close) {
            $scope.model.close();
        }
    }
    $scope.select = function () {
        if ($scope.model.submit) {
            $scope.model.submit($scope.selection);
        }
    }

    // Private helper functions
    function localizeTitle() {
        localizationService.localize($scope.model.titleKey).then(function (value) {
            $scope.title = value;
        });
    }
}