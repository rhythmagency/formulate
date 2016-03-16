// Variables.
var app = angular.module("umbraco");

// Associate directive/controller.
app.directive("formulateHandlerChooser", directive);
app.controller("formulate.handlerChooser", controller);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        template: formulateDirectives.get("handlerChooser/handlerChooser.html"),
        controller: "formulate.handlerChooser",
        scope: {
            show: "=",
            chosen: "&"
        }
    };
}

// Controller.
function controller($scope, $element, formulateHandlers) {

    // Set scope variables.
    formulateHandlers.getHandlerTypes().then(function(handlerTypes) {
        $scope.items = handlerTypes;
    });
    $scope.dialogStyles = {};

    // Handle chosen item.
    $scope.choseItem = function(item) {
        $scope.chosen({
            handler: item
        });
    };

    // Cancel choice.
    $scope.cancel = function() {
        $scope.chosen({
            handler: null
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