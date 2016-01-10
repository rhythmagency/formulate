// Variables.
var app = angular.module("umbraco");

// Service to help with Formulate data values.
app.factory("formulateDataValues", function ($http, $q, notificationsService,
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

        // Gets the data value info for the data value with the specified ID.
        getDataValueInfo: getGetDataValueInfo(services),

        // Saves the data value on the server.
        persistDataValue: getPersistDataValue(services),

        // Deletes a data value from the server.
        deleteDataValue: getDeleteDataValue(services)

    };

});

// Returns the function that gets information about a data value.
function getGetDataValueInfo(services) {
    return function (id) {

        // Variables.
        var url = services.formulateVars.GetDataValueInfo;
        var options = {
            cache: false,
            params: {
                DataValueId: id,
                // Cache buster ensures requests aren't cached.
                CacheBuster: Math.random()
            }
        };

        // Get data value info from server.
        return services.$http.get(url, options)
            .then(getHandleResponse(services, function (data) {
                return {
                    kindId: data.KindId,
                    dataValueId: data.DataValueId,
                    path: data.Path,
                    name: data.Name,
                    alias: data.Alias
                };
            }), getHandleServerError(services));


    };
}

// Returns the function that persists a data value on the server.
function getPersistDataValue(services) {
    return function (dataValueInfo) {

        // Variables.
        var url = services.formulateVars.PersistDataValue;
        var data = {
            KindId: dataValueInfo.kindId,
            ParentId: dataValueInfo.parentId,
            DataValueId: dataValueInfo.dataValueId,
            DataValueName: dataValueInfo.name,
            DataValueAlias: dataValueInfo.alias
        };
        var strData = JSON.stringify(data);
        var options = {
            headers: {
                "Content-Type": "application/json"
            }
        };

        // Send request to create the data value.
        return services.$http.post(url, strData, options)
            .then(getHandleResponse(services, function (data) {

                // Return data value information.
                return {
                    id: data.Id,
                    path: data.Path
                };

            }), getHandleServerError(services));

    };
}

// Returns the function that deletes a data value from the server.
function getDeleteDataValue(services) {
    return function(dataValueId) {

        // Format data so the server can consume it.
        var data = {
            DataValueId: dataValueId
        };

        // Prepare request.
        var url = services.formulateVars.DeleteDataValue;
        var strData = JSON.stringify(data);
        var options = {
            headers: {
                "Content-Type": "application/json"
            }
        };

        // Send request to delete the data value.
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