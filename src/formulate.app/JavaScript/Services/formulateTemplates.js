// Variables.
var app = angular.module("umbraco");

// Service to help with Formulate templates.
app.factory("formulateTemplates", function (formulateVars,
    formulateServer) {

    // Variables.
    var services = {
        formulateVars: formulateVars,
        formulateServer: formulateServer
    };

    // Return service.
    return {

        // Gets the templates from the server.
        getTemplates: getGetTemplates(services)

    };

});

// Returns the function that gets templates.
function getGetTemplates(services) {
    return function () {

        // Variables.
        var url = services.formulateVars.GetTemplates;

        // Get templates from server.
        return services.formulateServer.get(url, {}, function (data) {
            return data.Templates.map(function (item) {
                return {
                    id: item.Id,
                    name: item.Name
                };
            });
        });

    };
}