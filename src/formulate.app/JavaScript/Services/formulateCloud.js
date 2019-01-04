// Variables.
var app = angular.module("umbraco");

// Service to help with interacting with Umbraco Cloud.
app.factory("formulateCloud", function (formulateVars, formulateServer) {

    // Variables.
    var services = {
        formulateVars,
        formulateServer
    };

    // Return service.
    return {

        // Stores the entity with the specified ID to Umbraco Cloud.
        storeEntityToCloud: getStoreEntityToCloud(services),

        // Stores the entity with the specified ID to Umbraco Cloud.
        removeEntityFromCloud: getRemoveEntityFromCloud(services)

    };

});

// Returns the function that stores an entity to Umbraco Cloud.
function getStoreEntityToCloud(services) {
    return function (id) {

        // Variables.
        var url = services.formulateVars.StoreEntityToCloud;
        var params = {
            EntityId: id
        };

        // Store entity to Umbraco Cloud.
        return services.formulateServer.post(url, params, function () {

            // Return entity ID.
            return {
                id: id
            };

        });

    };
}

// Returns the function that removes an entity from Umbraco Cloud.
function getRemoveEntityFromCloud(services) {
    return function (id) {

        // Variables.
        var url = services.formulateVars.RemoveEntityFromCloud;
        var params = {
            EntityId: id
        };

        // Remove entity from Umbraco Cloud.
        return services.formulateServer.post(url, params, function () {

            // Return entity ID.
            return {
                id: id
            };

        });

    };
}