// Variables.
var app = angular.module("umbraco");

// Register directive.
app.directive("formulateLocalizeContent", directive);

// Directive.
// Allows you to localize content that requires AngularJS compilation.
function directive(localizationService, $compile) {
    return {
        restrict: "E",
        replace: true,
        link: getLocalizeLinker(localizationService, $compile)
    };
}

// Returns the link function.
function getLocalizeLinker(localizationService, $compile) {
    return function(scope, element, attrs) {

        // Variables.
        var key = attrs.key;

        // Localize.
        localizationService
            .localize(key)
            .then(function (value) {

                // Compile translated value and replace content.
                var template = angular.element($compile(value)(scope));
                element.replaceWith(template);

            });

    };
}