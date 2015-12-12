// Variables.
var app = angular.module("umbraco");

// Associate directive/controller.
app.directive("formulateFieldEditor", FieldEditorDirective);

// Directive.
function FieldEditorDirective(formulateDirectives, $compile) {
    return {
        restrict: "E",
        template: formulateDirectives.get("fieldEditor/fieldEditor.html"),
        replace: true,
        scope: {
            directive: "=directive"
        },
        link: function (scope, element) {

            // Create directive.
            var markup = "<" + scope.directive + "></" + scope.directive + ">";
            var directive = $compile(markup)(scope);
            element.replaceWith(directive);

        }
    };
}