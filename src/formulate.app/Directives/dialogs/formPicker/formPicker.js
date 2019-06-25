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
            maxCount: "=",
            titleKey: "@",
            forms: "=",
            model: "="
        }
    };
}

// Controller.
function controller($scope, formulateVars, localizationService) {
    // Initialize scope variables.
    $scope.selection = [];
    $scope.previouslySelectedIds = $scope.model.forms || [];
    $scope.entityKinds = ["Form"];
    $scope.rootId = formulateVars["Form.RootId"];
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
        localizationService.localize($scope.titleKey).then(function (value) {
            $scope.title = value;
        });
    }
}