// Variables.
var app = angular.module("umbraco");

// Associate directive/controller.
app.directive("formulateHandlerEditor", directive);

// Directive.
function directive(formulateDirectives, $compile) {
    return {
        restrict: "E",
        template: formulateDirectives.get("handlerEditor/handlerEditor.html"),
        replace: true,
        scope: {
            directive: "=",
            configuration: "=",
            fields: "="
        },
        link: function (scope, element) {

            // Create directive.
            var markup = "<" + scope.directive + " configuration=\"configuration\" fields=\"fields\"></" + scope.directive + ">";
            var directive = $compile(markup)(scope);
            element.replaceWith(directive);

        }
    };
}