// Variables.
var app = angular.module("umbraco");

// Associate directive/controller.
app.directive("formulateTextField", TextFieldDirective);

// Directive.
function TextFieldDirective(formulateDirectives) {
    return {
        restrict: "E",
        replace: true,
        template: formulateDirectives.get("fields/textField/textField.html")
    };
}