// Variables.
var app = angular.module("umbraco");

// Associate directive.
app.directive("formulateTextConstantField", directive);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        replace: true,
        template: formulateDirectives.get("fields/textConstantField/textConstantField.html"),
        scope: {
            configuration: "="
        }
    };
}