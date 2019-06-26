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
            maxCount: "=",
            titleKey: "@",
            model: "="
        }
    };
}

// Controller.
function controller($scope, formulateVars, localizationService) {

    // Initialize scope variables.
    $scope.selection = [];
    $scope.previouslySelectedIds = $scope.model.layouts || [];
    $scope.entityKinds = ["Layout"];
    $scope.rootId = formulateVars["Layout.RootId"];
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