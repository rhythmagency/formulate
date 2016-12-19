// Variables.
var app = angular.module("umbraco");

// Directive.
app.directive("formulateStoreDataHandler", directive);
function directive(formulateDirectives) {
    return {
        restrict: "E",
        replace: true,
        template: formulateDirectives.get(
            "handlers/storeDataHandler/storeDataHandler.html"),
        scope: {
            configuration: "=",
            fields: "="
        },
        controller: controller
    };
}

// Controller.
function controller($scope) {
}