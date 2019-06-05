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

        // Localizes the app labels.
        localizeApps: getLocalizeApps(services)

    };

});

// Returns the function that translates the apps.
function getLocalizeApps(services) {
    return function (apps) {
        for (var i = 0; i < apps.length; i++) {
            localizeApp(apps[i], services.localizationService);
        }
    };
}

// Translates the tab label.
function localizeApp(app, localizationService) {
    localizationService
        .localize("formulate-tabs_" + app.alias)
        .then(function (value) {

            // Set app name to translated value.
            app.name = value;

        });
}