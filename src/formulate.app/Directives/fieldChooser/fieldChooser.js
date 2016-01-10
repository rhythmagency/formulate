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
        controller: "formulate.fieldChooser",
        scope: {
            show: "=",
            chosen: "&"
        }
    };
}

// Controller.
function fieldChooserController($scope, $element, formulateFields) {

    // Set scope variables.
    formulateFields.getFieldTypes().then(function(fieldTypes) {
        $scope.items = fieldTypes;
    });
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