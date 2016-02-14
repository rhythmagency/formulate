// Variables.
var app = angular.module("umbraco");

// Service to help with Formulate trees.
app.factory("formulateTrees", function (navigationService) {

    // Variables.
    var services = {
        navigationService: navigationService
    };

    // Return service.
    return {

        // Activates the specified entity in the tree.
        activateEntity: getActivateEntity(services)

    };

});

// Shows/highlights the node in the Formulate tree.
function getActivateEntity(services) {
    return function (entity) {
        var options = {
            tree: "formulate",
            path: entity.path,
            forceReload: true,
            activate: true
        };
        services.navigationService.syncTree(options);
    };
}