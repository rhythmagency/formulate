// Variables.
var app = angular.module("umbraco");

// Service to help with Formulate handlers.
app.factory("formulateHandlers", function (formulateVars,
    formulateServer) {

    // Variables.
    var services = {
        formulateVars: formulateVars,
        formulateServer: formulateServer
    };

    // Return service.
    return {

        // Gets the handler types.
        getHandlerTypes: getGetHandlerTypes(services)

    };

});

// Returns the function that gets handler types.
function getGetHandlerTypes(services) {
    return function () {

        // Variables.
        var url = services.formulateVars.GetHandlerTypes;

        // Get handler types from server.
        return services.formulateServer.get(url, {}, function (data) {

            // Return handler types.
            return data.HandlerTypes.map(function (handler) {
                return {
                    icon: handler.Icon,
                    typeLabel: handler.TypeLabel,
                    directive: handler.Directive,
                    typeFullName: handler.TypeFullName
                };
            });

        });

    };
}