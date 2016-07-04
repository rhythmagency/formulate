// Variables.
var app = angular.module("umbraco");

// Service to help with Formulate localization.
app.factory("formulateLocalization", function (localizationService) {

    // Variables.
    var services = {
        localizationService: localizationService
    };

    // Return service.
    return {

        // Localizes the tab labels.
        localizeTabs: getLocalizeTabs(services)

    };

});

// Returns the function that translates the tabs.
function getLocalizeTabs(services) {
    return function (tabs) {
        for (var i = 0; i < tabs.length; i++) {
            localizeTab(tabs[i], services.localizationService);
        }
    };
}

// Translates the tab label.
function localizeTab(tab, localizationService) {
    localizationService
        .localize("formulate-tabs_" + tab.label)
        .then(function (value) {

            // Set tab label to translated value.
            tab.label = value;

        });
}