// Variables.
var app = angular.module("umbraco");

// Associate directive.
app.directive("formulateDateField", directive);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        replace: true,
        template: formulateDirectives.get("fields/dateField/dateField.html")
    };
}