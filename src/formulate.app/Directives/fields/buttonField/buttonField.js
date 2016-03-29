// Variables.
var app = angular.module("umbraco");

// Associate directive.
app.directive("formulateButtonField", directive);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        replace: true,
        template: formulateDirectives.get("fields/buttonField/buttonField.html")
    };
}