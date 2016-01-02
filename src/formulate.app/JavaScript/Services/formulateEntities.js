// Variables.
var app = angular.module("umbraco");

// Service to help with Formulate entities.
app.factory("formulateEntities", function ($http, $q, notificationsService,
    formulateVars) {

    // Variables.
    var services = {
        $http: $http,
        $q: $q,
        notificationsService: notificationsService,
        formulateVars: formulateVars
    };

    // Return service.
    return {

        // Gets the children for the entity with the specified ID.
        getEntityChildren: getGetEntityChildren(services),

        // Gets the entity with the specified ID.
        getEntity: getGetEntity(services)

    };

});

// Returns the function that gets children for an entity.
function getGetEntityChildren(services) {
    return function (id) {

        // Variables.
        var url = services.formulateVars.GetEntityChildren;
        // TODO: Move this options construction into a service?
        var options = {
            cache: false,
            params: {
                EntityId: id,
                // Cache buster ensures requests aren't cached.
                CacheBuster: Math.random()
            }
        };

        // Get entity children from server.
        return services.$http.get(url, options)
            .then(getHandleResponse(services, function (data) {

                // Return entity children.
                return {
                    children: data.Children.map(function (child) {
                        return {
                            id: child.Id,
                            name: child.Name,
                            icon: child.Icon,
                            hasChildren: child.HasChildren,
                            children: []
                        };
                    })
                };

            }), getHandleServerError(services));

    };
}


// Returns the function that gets an entity.
function getGetEntity(services) {
    return function (id) {

        // Variables.
        var url = services.formulateVars.GetEntity;
        // TODO: Move this options construction into a service?
        var options = {
            cache: false,
            params: {
                EntityId: id,
                // Cache buster ensures requests aren't cached.
                CacheBuster: Math.random()
            }
        };

        // Get entity from server.
        return services.$http.get(url, options)
            .then(getHandleResponse(services, function (data) {

                // Return entity.
                return {
                    id: data.Id,
                    name: data.Name,
                    icon: data.Icon,
                    hasChildren: data.HasChildren,
                    children: []
                };

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