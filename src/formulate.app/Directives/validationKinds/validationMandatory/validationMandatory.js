// Variables.
var app = angular.module("umbraco");

// Associate directive.
app.directive("formulateValidationMandatory", directive);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        replace: true,
        template: formulateDirectives.get(
            "validationKinds/validationMandatory/validationMandatory.html"),
        scope: {
            data: "="
        }
    };
}