// Variables.
var app = angular.module("umbraco");

// Associate directive/controller.
app.directive("formulateFieldChooser", fieldChooserDirective);
app.controller("formulate.fieldChooser", fieldChooserController);

// Directive.
function fieldChooserDirective(formulateDirectives) {
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
function fieldChooserController($scope, $routeParams, navigationService,
    formulateForms, $location, $route, $element) {

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
    $scope.dialogStyles = {};

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

    // When the dialog appears, position it in view.
    $scope.$watch("show", function(newValue, oldValue) {
        if (newValue) {
            var top = calculateDialogTop({
                $element: $element
            });
            $scope.dialogStyles.top = top;
        }
    });

}

// Calculates the top of the dialog.
function calculateDialogTop(options) {

    // This is a magic formula I copied from Archetype. Seems to work.
    var $element = options.$element;
    var offset = $element.offset();
    var scrollTop = $element.closest(".umb-panel-body").scrollTop();
    if (offset.top < 400) {
        return 300 + scrollTop;
    } else {
        return offset.top - 150 + scrollTop;
    }

}