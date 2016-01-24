// Variables.
var app = angular.module("umbraco");

// Associate directive/controller.
app.directive("formulateDataValueEditor", directive);

// Directive.
function directive(formulateDirectives, $compile) {
    return {
        restrict: "E",
        template: formulateDirectives.get("dataValueEditor/dataValueEditor.html"),
        replace: true,
        scope: {
            directive: "=",
            data: "="
        },
        link: function (scope, element) {

            // Create directive.
            var markup = "<" + scope.directive + " data=\"data\"></" + scope.directive + ">";
            var directive = $compile(markup)(scope);
            element.replaceWith(directive);

        }
    };
}