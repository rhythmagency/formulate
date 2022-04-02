"use strict";

(() => {

    // Variables.
    const app = angular.module("umbraco");
    const directiveName = 'formulateInjectLocalization';

    // Register directive.
    app.directive(directiveName, directive);

    // Directive.
    // Allows you to localize text that is injected into an element.
    function directive(localizationService) {
        return {
            restrict: "A",
            link: getLocalizeLinker(localizationService)
        };
    }

    // Returns the link function.
    function getLocalizeLinker(localizationService) {
        return function(scope, element, attrs) {

            // Variables.
            const key = attrs[directiveName];

            // Localize.
            localizationService
                .localize(key)
                .then(function (value) {

                    // Set attribute to translated value.
                    element.html("");
                    element.append(angular.element(document.createTextNode(value)));

                });

        };
    }

})();