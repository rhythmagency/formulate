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

        // Gets the folder info for the folder with the specified ID.
        getFolderInfo: getGetFolderInfo(services),

        // Saves the folder on the server.
        persistFolder: getPersistFolder(services),

        // Creates the folder.
        createFolder: getCreateFolder(services),

        // Moves a folder to a new parent on the server.
        moveFolder: getMoveFolder(services)

    };

});

// Returns the function that gets information about a folder.
function getGetFolderInfo(services) {
    return function (id) {

        // Variables.
        var url = services.formulateVars.GetFolderInfo;
        var params = {
            FolderId: id
        };

        // Get folder info from server.
        return services.formulateServer.get(url, params, function (data) {
            return {
                folderId: data.FolderId,
                path: data.Path,
                name: data.Name
            };
        });

    };
}

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

// Returns the function that persists a folder on the server.
function getPersistFolder(services) {
    return function (folderInfo) {

        // Variables.
        var url = services.formulateVars.PersistFolder;
        var data = {
            ParentId: folderInfo.parentId,
            FolderId: folderInfo.folderId,
            FolderName: folderInfo.name
        };

        // Send request to create the folder.
        return services.formulateServer.post(url, data, function (data) {

            // Return folder information.
            return {
                id: data.Id,
                path: data.Path
            };

        });

    };
}

// Returns the function that moves a folder.
function getMoveFolder(services) {
    return function (folderId, newParentId) {

        // Variables.
        var url = services.formulateVars.MoveFolder;
        var data = {
            FolderId: folderId,
            NewParentId: newParentId
        };

        // Send request to persist the folder.
        return services.formulateServer.post(url, data, function (data) {

            // Return folder info.
            return {
                id: data.Id,
                path: data.Path,
                descendants: data.Descendants.map(function (item) {
                    return {
                        id: item.Id,
                        path: item.Path
                    };
                })
            };

        });

    };
}