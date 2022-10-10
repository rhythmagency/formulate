(function () {
    function formulateConfigurationEditorDirective($compile) {
        var directive = {
            template: "<div></div>",
            replace: true,
            scope: {
                directive: "=",
                config: "=",
                alias: "=",
            },
            link: function(scope, element) {
                if (typeof (scope.directive) === "undefined" || scope.directive.length === 0) {
                    console.error("No directive provided.");
                    return;
                }

                if (scope.directive === "formulate-configuration-editor") {
                    console.error("Operation would cause an infinite loop.");
                    return;
                }

                // Create directive.
                var markup = "<" + scope.directive + " config=\"config\" alias=\"alias\"></" + scope.directive + ">";
                var directive = $compile(markup)(scope);
                element.replaceWith(directive);
            }
        };

        return directive;
    }

    angular.module("umbraco.directives").directive("formulateConfigurationEditor", formulateConfigurationEditorDirective);
})();