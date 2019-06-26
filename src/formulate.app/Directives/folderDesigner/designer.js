//TODO: Disable buttons during folder save.
// Variables.
var app = angular.module("umbraco");

// Associate directive/controller.
app.directive("formulateFolderDesigner", directive);
app.controller("formulate.folderDesigner", controller);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        template: formulateDirectives.get("folderDesigner/designer.html"),
        controller: "formulate.folderDesigner"
    };
}

// Controller.
function controller($scope, $routeParams, $route, formulateTrees,
    formulateFolders) {

    // Variables.
    var id = $routeParams.id;
    var services = {
        formulateTrees: formulateTrees,
        formulateFolders: formulateFolders,
        $scope: $scope,
        $route: $route
    };

    // Set scope variables.
    $scope.folderId = id;
    $scope.info = {
        folderName: null
    };
    $scope.parentId = null;

    // Set scope functions.
    $scope.save = getSaveFolder(services);
    $scope.canSave = getCanSave(services);
    
    // Initializes folder.
    initializeFolder({
        id: id
    }, services);

    // Handle events.
    handleFolderMoves(services);

}

// Handles updating a folder when it's moved.
function handleFolderMoves(services) {
    var $scope = services.$scope;
    $scope.$on("formulateEntityMoved", function(event, data) {
        var id = data.id;
        var newPath = data.path;
        if ($scope.folderId === id) {

            // Store new path.
            $scope.folderPath = newPath;

            // Activate in tree.
            services.formulateTrees.activateEntity(data);

        }
    });
}

// Saves the folder.
function getSaveFolder(services) {
    return function () {

        // Variables.
        var $scope = services.$scope;
        var parentId = getParentId($scope);

        // Get folder data.
        var folderData = {
            parentId: parentId,
            folderId: $scope.folderId,
            name: $scope.info.folderName
        };

        // Persist folder on server.
        services.formulateFolders.persistFolder(folderData)
            .then(function() {

                // Even existing folders reload (e.g., to get new data).
                services.$route.reload();

            });

    };
}

// Gets the ID path to the folder.
function getFolderPath($scope) {
    var path = $scope.folderPath;
    if (!path) {
        path = [];
    }
    return path;
}

// Gets the ID of the folder's parent.
function getParentId($scope) {
    if ($scope.parentId) {
        return $scope.parentId;
    }
    var path = getFolderPath($scope);
    var parentId = path.length > 0
        ? path[path.length - 2]
        : null;
    return parentId;
}

// Initializes the folder.
function initializeFolder(options, services) {

    // Variables.
    var id = options.id;
    var $scope = services.$scope;

    // Disable folder saving until the data is populated.
    $scope.initialized = false;

    // Get the folder info.
    services.formulateFolders.getFolderInfo(id)
        .then(function(folder) {

            // Update tree.
            services.formulateTrees.activateEntity(folder);

            // Set the folder info.
            $scope.folderId = folder.folderId;
            $scope.info.folderName = folder.name;
            $scope.folderPath = folder.path;

            // The folder can be saved now.
            $scope.initialized = true;

        });

}

// Returns the function that indicates whether or not the folder can be saved.
function getCanSave(services) {
    return function() {
        return services.$scope.initialized;
    };
}