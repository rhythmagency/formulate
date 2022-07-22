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
                configuration: "="
            },
            link: function (scope, element) {

                // Create directive.
                var markup = "<" + scope.directive + " configuration=\"configuration\"></" + scope.directive + ">";
                var directive = $compile(markup)(scope);
                element.replaceWith(directive);

            }
        };
    }

    // Associate directive/controller.
    angular.module("umbraco").directive("formulateFieldEditor", directive);

})();
