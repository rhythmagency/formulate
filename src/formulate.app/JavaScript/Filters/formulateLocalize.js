// Variables.
var app = angular.module("umbraco");
var translations = {};

// A filter that facilitates conditional localization.
app.filter("formulatelocalize", function (localizationService) {
    var filterFn = function(input, shouldLocalize) {
        if (shouldLocalize === false) {
            return input;
        } else if (translations[input]) {
            return translations[input];
        } else {

            // Store translation for later.
            return localizationService
                .localize(input)
                .then(function (value) {
                    translations[input] = value;
                });

        }
    };
    filterFn.$stateful = true;
    return filterFn;
});