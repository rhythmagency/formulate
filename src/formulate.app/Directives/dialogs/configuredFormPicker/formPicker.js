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
    $scope.previouslySelectedIds = $scope.model.configureForms || [];
    $scope.entityKinds = ["ConfiguredForm"];
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

    // Localize the "wrong selection" message.
    localizationService.localize("formulate-errors_Pick Configured Form Instead").then(function (value) {
        $scope.wrongKindError = value;
    });

    // Private helper functions
    function localizeTitle() {
        localizationService.localize($scope.titleKey).then(function (value) {
            $scope.title = value;
        });
    }
}