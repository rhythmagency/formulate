// Variables.
var app = angular.module("umbraco");

// Service to help with server-side configuration.
app.factory("formulateServerConfig", function (formulateVars, formulateServer) {

    // Variables.
    var services = {
        formulateVars: formulateVars,
        formulateServer: formulateServer
    };

    // Return service.
    return {

        // Checks whether or not recaptcha has been configured.
        hasConfiguredRecaptcha: getHasConfiguredRecaptcha(services)

    };

});

// Returns the function that checks if the server has been configured for recaptcha.
function getHasConfiguredRecaptcha(services) {
    return function () {

        // Variables.
        var url = services.formulateVars.HasConfiguredRecaptcha;

        // Check if the server has configured recaptcha.
        return services.formulateServer.get(url, {}, function (data) {

            // Return whether or not the server is configured for recaptcha.
            return data.HasConfigured

        });

    };
}