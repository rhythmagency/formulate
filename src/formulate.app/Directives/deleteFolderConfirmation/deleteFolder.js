// Variables.
var app = angular.module("umbraco");

// Register directive/controller.
app.controller("formulate.deleteFolderConfirmation", controller);
app.directive("formulateDeleteFolderConfirmation", directive);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        template: formulateDirectives.get("deleteFolderConfirmation/deleteFolder.html"),
        controller: "formulate.deleteFolderConfirmation"
    };
}

// Controller.
function controller($scope, $location, $q, $http, navigationService,
    formulateFolders, treeService) {

    // Variables.
    var folderId = $scope.currentNode.id;

    // Variable containing the common services (easier to pass around).
    var services = {
        $scope: $scope,
        $location: $location,
        $q: $q,
        $http: $http,
        navigationService: navigationService,
        formulateFolders: formulateFolders,
        treeService: treeService
    };

    // Assign scope variables.
    $scope.initialized = false;

    // Assign functions on scope.
    $scope.deleteFolder = getDeleteFolder(services);
    $scope.cancel = getCancel(services);

    // Load folder information.
    loadFolderInfo(folderId, services);

}

// Loads information about the folder.
function loadFolderInfo(folderId, services) {
    services.formulateFolders.getFolderInfo(folderId).then(function(folder) {
        services.$scope.folderId = folder.folderId;
        services.$scope.folderName = folder.name;
        services.$scope.initialized = true;
    });
}

// Returns a function that deletes a folder.
function getDeleteFolder(services) {
    return function() {

        // Variables.
        var node = services.$scope.currentNode;
        var folderId = services.$scope.folderId;
        var folderPromise = services.formulateFolders.getFolderInfo(folderId);

        // Once we have the folder information...
        folderPromise.then(function (folder) {

            // Variables.
            var path = folder.path;
            var partialPath = path.slice(0, path.length - 1);

            // Delete folder.
            services.formulateFolders.deleteFolder(folderId)
                .then(function () {

                    // Remove the node from the tree.
                    services.treeService.removeNode(node);

                    // Update tree (down to the deleted folder's parent).
                    var options = {
                        tree: "formulate",
                        path: partialPath,
                        forceReload: true,
                        activate: false
                    };
                    services.navigationService.syncTree(options);

                    // Close dialog.
                    services.navigationService.hideDialog();

                });

        });

    };
}

// Returns the function that cancels the deletion.
function getCancel(services) {
    return function () {
        services.navigationService.hideDialog();
    };
}