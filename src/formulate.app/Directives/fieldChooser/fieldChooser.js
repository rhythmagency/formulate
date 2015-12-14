// Variables.
var app = angular.module("umbraco");

// Associate directive/controller.
app.directive("formulateFieldChooser", FieldChooserDirective);
app.controller("formulate.fieldChooser", FieldChooserController);

// Directive.
function FieldChooserDirective(formulateDirectives) {
    return {
        restrict: "E",
        template: formulateDirectives.get("fieldChooser/fieldChooser.html"),
        scope: {
            show: "=",
            chosen: "&"
        }
    };
}

// Controller.
function FieldChooserController($scope, $routeParams, navigationService,
    formulateForms, $location, $route) {

    //TODO: Should come from a service, which should get these from the server.
    $scope.items = [
        {
            icon: "icon-document-dashed-line",
            label: "Text",
            directive: "formulate-text-field",
            typeLabel: "Text"
        }, {
            icon: "icon-document-dashed-line",
            label: "Checkbox",
            directive: "formulate-checkbox-field",
            typeLabel: "Checkbox"
        }
    ];

    // Handle chosen item.
    $scope.choseItem = function(item) {
        $scope.chosen({
            field: item
        });
    };

    // Cancel choice.
    $scope.cancel = function() {
        $scope.chosen({
            field: null
        });
    };

}