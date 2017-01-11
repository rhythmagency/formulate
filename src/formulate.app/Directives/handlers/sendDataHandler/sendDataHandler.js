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

/**
 * Controller for the "Send Data" handler directive.
 * @param $scope The injected Angular scope.
 * @constructor
 */
function Controller($scope) {

    // Variables.
    this.injected = {
        $scope: $scope
    };

    // Initialize.
    if (!$scope.configuration.method) {
        $scope.configuration.method = "GET";
    }
    if (!$scope.configuration.dataFormat) {
        $scope.configuration.dataFormat = "Query String";
    }

}