// Variables.
var app = angular.module("umbraco");

// Register directive.
app.directive("formulateInjectLocalization", directive);

// Directive.
// Allows you to localize text that is inject into an element.
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
        var key = attrs.formulateInjectLocalization;

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