// Variables.
var app = angular.module("umbraco");

// Service to help with Formulate layouts.
app.factory("formulateLayouts", function ($http, $q, notificationsService,
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

        // Gets the layout info for the layout with the specified ID.
        getLayoutInfo: getGetLayoutInfo(services),

        // Saves the layout on the server.
        persistLayout: getPersistLayout(services),

        // Deletes a layout from the server.
        deleteLayout: getDeleteLayout(services)

    };

});

// Returns the function that gets information about a layout.
function getGetLayoutInfo(services) {
    return function (id) {

        // Variables.
        var url = services.formulateVars.GetLayoutInfo;
        var options = {
            cache: false,
            params: {
                LayoutId: id,
                // Cache buster ensures requests aren't cached.
                CacheBuster: Math.random()
            }
        };

        // Get layout info from server.
        return services.$http.get(url, options)
            .then(getHandleResponse(services, function (data) {
                return {
                    kindId: data.KindId,
                    layoutId: data.LayoutId,
                    path: data.Path,
                    name: data.Name,
                    alias: data.Alias
                };
            }), getHandleServerError(services));


    };
}

// Returns the function that persists a layout on the server.
function getPersistLayout(services) {
    return function (layoutInfo) {

        // Variables.
        var url = services.formulateVars.PersistLayout;
        var data = {
            KindId: layoutInfo.kindId,
            ParentId: layoutInfo.parentId,
            LayoutId: layoutInfo.layoutId,
            LayoutName: layoutInfo.name,
            LayoutAlias: layoutInfo.alias
        };
        var strData = JSON.stringify(data);
        var options = {
            headers: {
                "Content-Type": "application/json"
            }
        };

        // Send request to create the layout.
        return services.$http.post(url, strData, options)
            .then(getHandleResponse(services, function (data) {

                // Return layout information.
                return {
                    id: data.Id,
                    path: data.Path
                };

            }), getHandleServerError(services));

    };
}

// Returns the function that deletes a layout from the server.
function getDeleteLayout(services) {
    return function(layoutId) {

        // Format data so the server can consume it.
        var data = {
            LayoutId: layoutId
        };

        // Prepare request.
        var url = services.formulateVars.DeleteLayout;
        var strData = JSON.stringify(data);
        var options = {
            headers: {
                "Content-Type": "application/json"
            }
        };

        // Send request to delete the layout.
        return services.$http.post(url, strData, options)
            .then(getHandleResponse(services, function (data) {

                // Return empty data.
                return {};

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