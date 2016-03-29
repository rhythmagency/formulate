// Variables.
var app = angular.module("umbraco");

// Associate directive.
app.directive("formulateTextField", directive);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        replace: true,
        template: formulateDirectives.get("fields/textField/textField.html")
    };
}