// Variables.
var app = angular.module("umbraco");

// Associate directive.
app.directive("formulateHeaderField", directive);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        replace: true,
        template: formulateDirectives.get("fields/headerField/headerField.html")
    };
}