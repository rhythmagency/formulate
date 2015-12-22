// Variables.
var app = angular.module("umbraco");

// Service to help with Formulate folders.
app.factory("formulateFolders", function ($http, $q, notificationsService,
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

        // Creates the folder.
        createFolder: getCreateFolder(services)

    };

});

// Returns the function that creates a folder on the server.
function getCreateFolder(services) {
    return function (folderData) {

        // Format data so the server can consume it.
        var data = {
            ParentId: folderData.parentId,
            FolderName: folderData.folderName
        };

        // Prepare request.
        var url = services.formulateVars.PersistFolder;
        var strData = JSON.stringify(data);
        var options = {
            headers: {
                "Content-Type": "application/json"
            }
        };

        // Send request to create the folder.
        return services.$http.post(url, strData, options)
            .then(getHandleResponse(services, function (data) {

                // Return form ID.
                return {
                    folderId: data.FolderId,
                    path: data.Path
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