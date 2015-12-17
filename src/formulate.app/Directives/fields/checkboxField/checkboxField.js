// Variables.
var app = angular.module("umbraco");

// Associate directive/controller.
app.directive("formulateCheckboxField", checkboxFieldDirective);

// Directive.
function checkboxFieldDirective(formulateDirectives) {
    return {
        restrict: "E",
        replace: true,
        template: formulateDirectives.get("fields/checkboxField/checkboxField.html")
    };
}