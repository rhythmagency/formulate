// Variables.
var app = angular.module("umbraco");

// Register directive/controller.
app.controller("formulate.createFolder", controller);
app.directive("formulateCreateFolder", directive);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        template: formulateDirectives.get("createFolder/createFolder.html")
    };
}

// Controller.
function controller($scope, $location, notificationsService, $q,
    $http, navigationService, formulateForms, treeService) {

    // Variable containing the common services (easier to pass around).
    var services = {
        $scope: $scope,
        $location: $location,
        notificationsService: notificationsService,
        $q: $q,
        $http: $http,
        navigationService: navigationService,
        formulateForms: formulateForms,
        treeService: treeService
    };

    // Assign functions on scope.
    $scope.createFolder = getCreateFolder(services);
    $scope.cancel = getCancel(services);

}

// Returns a function that creates a folder.
function getCreateFolder(services) {
    return function() {

        // Variables.
        var entityId = services.$scope.currentNode.id;
        var folderName = services.$scope.folderName;
        //TODO: Need to create formulateFolders service.
        var folderPromise = services.formulateFolders
            .createFolder(entityId, folderName);

        // Once we have created the folder...
        folderPromise.then(function (folder) {

            // Variables.
            var path = folder.path;

            // Update tree.
            var options = {
                tree: "formulate",
                path: path,
                forceReload: true,
                activate: false
            };
            services.navigationService.syncTree(options);

            // Close dialog.
            services.navigationService.hideDialog();

        });

    };
}

// Returns the function that cancels the creation.
function getCancel(services) {
    return function () {
        services.navigationService.hideDialog();
    };
}