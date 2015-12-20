// Variables.
var app = angular.module("umbraco");

// Service to help with Formulate layouts.
app.factory("formulateLayouts", function ($http, $q) {

    // Variables.
    var services = {
        $http: $http,
        $q: $q,
        notificationsService: notificationsService
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
        //TODO: Use server variables to get this URL.
        var url = "/umbraco/backoffice/formulate/Layouts/GetLayoutInfo";
        var options = {
            cache: false,
            params: {
                LayoutId: id,
                // Cache buster ensures requests aren't cached.
                CacheBuster: Math.random()
            }
        };

        // Get path from server.
        return $http.get(url, options).then(function (response) {

            // Variables.
            var data = response.data;

            // Was the request successful?
            if (data.Success) {
                return {
                    layoutId: data.Id,
                    path: data.Path,
                    name: data.Name,
                    alias: data.Alias
                };
            } else {
                return $q.reject();
            }

        }, function () {
            return $q.reject();
        });

    };
}

// Returns the function that persists a layout on the server.
function getPersistLayout(services) {
    return function (layoutInfo) {

        // Variables.
        //TODO: Use server variables to get this URL.
        var url = "/umbraco/backoffice/formulate/Layouts/PersistLayout";
        var data = {
            ParentId: layoutInfo.parentId,
            LayoutId: layoutInfo.layoutId,
            LayoutName: layoutInfo.layoutName,
            LayoutAlias: layoutInfo.layoutAlias
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
        //TODO: Use server variables to get this URL.
        var url = "/umbraco/backoffice/formulate/Layouts/DeleteLayout";
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