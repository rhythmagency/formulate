// Variables.
var app = angular.module("umbraco");

// Associate directive/controller.
app.directive("formulateSendDataHandler", directive);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        replace: true,
        template: formulateDirectives.get(
            "handlers/sendDataHandler/sendDataHandler.html"),
        scope: {
            configuration: "=",
            fields: "="
        },
        controller: Controller
    };
}

// Controller.
function Controller($scope) {

    // Variables.
    this.injected = {
        $scope: $scope
    };

}