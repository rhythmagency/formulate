// Variables.
var app = angular.module("umbraco");

// Associate directive.
app.directive("formulateUploadField", directive);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        replace: true,
        template: formulateDirectives.get("fields/uploadField/uploadField.html")
    };
}