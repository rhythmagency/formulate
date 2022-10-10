"use strict";

(function () {

    // Directive.
    function directive($compile) {
        return {
            restrict: "E",
            template: "<div></div>",
            replace: true,
            scope: {
                directive: "=",
                configuration: "=",
                fields: "="
            },
            link: function (scope, element) {

                // Create directive.
                const markup = "<" + scope.directive + " configuration=\"configuration\" fields=\"fields\"></" + scope.directive + ">";
                const directive = $compile(markup)(scope);
                element.replaceWith(directive);

            }
        };
    }

    // Associate directive/controller.
    angular.module("umbraco").directive("formulateHandlerEditor", directive);

})();