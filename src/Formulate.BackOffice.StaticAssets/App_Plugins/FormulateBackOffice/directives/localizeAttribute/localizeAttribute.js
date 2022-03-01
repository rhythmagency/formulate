// Variables.
var app = angular.module("umbraco");

// Register directive.
app.directive("formulateLocalizeAttribute", directive);

// Directive. Allows you to localize an attribute.
function directive(localizationService) {
    return {
        restrict: "A",
        link: getLocalizeLinker(localizationService),
    };
}

// Returns the link function.
function getLocalizeLinker(localizationService) {
    return function(scope, element, attrs) {

        // Variables.
        var attrName = attrs.formulateLocalizeAttribute;
        var key = attrs[attrName];

        // Localize.
        localizationService
            .localize(key)
            .then(function (value) {

                // Set attribute to translated value.
                attrs.$set(attrName, value);

            });

    };
}