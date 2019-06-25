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
    $scope.entityKinds = ["DataValue"];
    $scope.rootId = formulateVars["DataValue.RootId"];
    $scope.previouslySelectedIds = $scope.model.dataValues || [];
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