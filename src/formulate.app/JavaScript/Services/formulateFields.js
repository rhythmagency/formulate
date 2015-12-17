// Variables.
var app = angular.module("umbraco");

// Service to help with Formulate fields.
app.factory("formulateFields", function ($http, $q, notificationsService) {

    // Variables.
    var services = {
        $http: $http,
        $q: $q,
        notificationsService: notificationsService
    };

    // Return service.
    return {

        // Gets the field types.
        getFieldTypes: getGetFieldTypes(services)

    };

});

// Returns the function that gets field types.
function getGetFieldTypes(services) {
    return function () {

        // Variables.
        //TODO: Use server variables to get this URL.
        var url = "/umbraco/backoffice/formulate/Fields/GetFieldTypes";
        var options = {
            cache: false,
            params: {
                // Cache buster ensures requests aren't cached.
                CacheBuster: Math.random()
            }
        };

        // Get field types from server.
        return services.$http.get(url, options)
            .then(getHandleResponse(services, function (data) {

                // Return field types.
                return data.FieldTypes.map(function (field) {
                    return {
                        icon: field.Icon,
                        typeLabel: field.TypeLabel,
                        directive: field.Directive,
                        typeFullName: field.TypeFullName
                    };
                });

            }), getHandleServerError(services));

    };
}

// Returns the function that handles a server error.
function getHandleServerError(services) {
    return function() {

        // Indicate error with notification.
        var title = "Server Error";
        var message = "There was an issue communicating with the server.";
        services.notificationsService.error(title, message);
        return services.$q.reject();

    };
}

// Returns the function that handles a server response.
function getHandleResponse(services, successCallback) {
    return function (response) {

        // Variables.
        var data = response.data;

        // Was the request successful?
        if (data.Success) {
            return successCallback(data);
        } else {

            // Error notification.
            var title = "Unexpected Error";
            var message = data.Reason;
            services.notificationsService.error(title, message);
            return services.$q.reject();

        }

    };
}