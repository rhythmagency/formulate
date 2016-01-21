// Variables.
var app = angular.module("umbraco");

// Service to help with Formulate folders.
app.factory("formulateFolders", function (formulateVars,
    formulateServer) {

    // Variables.
    var services = {
        formulateVars: formulateVars,
        formulateServer: formulateServer
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

        // Variables.
        var url = services.formulateVars.PersistFolder;
        var data = {
            ParentId: folderData.parentId,
            FolderName: folderData.folderName
        };

        // Send request to create the folder.
        return services.formulateServer.post(url, data, function (data) {

            // Return form ID.
            return {
                folderId: data.FolderId,
                path: data.Path
            };

        });

    };
}