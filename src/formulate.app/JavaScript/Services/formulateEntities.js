// Variables.
var app = angular.module("umbraco");

// Service to help with Formulate entities.
app.factory("formulateEntities", function (formulateVars,
    formulateServer) {

    // Variables.
    var services = {
        formulateVars: formulateVars,
        formulateServer: formulateServer
    };

    // Return service.
    return {

        // Gets the children for the entity with the specified ID.
        getEntityChildren: getGetEntityChildren(services),

        // Gets the entity with the specified ID.
        getEntity: getGetEntity(services)

    };

});

// Returns the function that gets children for an entity.
function getGetEntityChildren(services) {
    return function (id) {

        // Variables.
        var url = services.formulateVars.GetEntityChildren;
        var params = {
            EntityId: id
        };

        // Get entity children from server.
        return services.formulateServer.get(url, params, function (data) {

            // Return entity children.
            return {
                children: data.Children.map(function (child) {
                    return {
                        id: child.Id,
                        name: child.Name,
                        icon: child.Icon,
                        kind: child.Kind,
                        hasChildren: child.HasChildren,
                        children: []
                    };
                })
            };

        });

    };
}


// Returns the function that gets an entity.
function getGetEntity(services) {
    return function (id) {

        // Variables.
        var url = services.formulateVars.GetEntity;
        var params = {
            EntityId: id
        };

        // Get entity from server.
        return services.formulateServer.get(url, params, function (data) {

            // Return entity.
            return {
                id: data.Id,
                name: data.Name,
                icon: data.Icon,
                kind: data.Kind,
                hasChildren: data.HasChildren,
                children: []
            };

        });

    };
}