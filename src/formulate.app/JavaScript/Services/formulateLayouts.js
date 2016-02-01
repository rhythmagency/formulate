// Variables.
var app = angular.module("umbraco");

// Service to help with Formulate layouts.
app.factory("formulateLayouts", function (formulateVars,
    formulateServer) {

    // Variables.
    var services = {
        formulateVars: formulateVars,
        formulateServer: formulateServer
    };

    // Return service.
    return {

        // Gets the layout info for the layout with the specified ID.
        getLayoutInfo: getGetLayoutInfo(services),

        // Saves the layout on the server.
        persistLayout: getPersistLayout(services),

        // Deletes a layout from the server.
        deleteLayout: getDeleteLayout(services),

        // Gets the kind of layouts from the server.
        getKinds: getGetKinds(services)

    };

});

// Returns the function that gets information about a layout.
function getGetLayoutInfo(services) {
    return function (id) {

        // Variables.
        var url = services.formulateVars.GetLayoutInfo;
        var params = {
            LayoutId: id
        };

        // Get layout info from server.
        return services.formulateServer.get(url, params, function (data) {
            return {
                kindId: data.KindId,
                layoutId: data.LayoutId,
                path: data.Path,
                name: data.Name,
                alias: data.Alias,
                directive: data.Directive,
                data: data.Data
            };
        });

    };
}

// Returns the function that persists a layout on the server.
function getPersistLayout(services) {
    return function (layoutInfo) {

        // Variables.
        var url = services.formulateVars.PersistLayout;
        var data = {
            KindId: layoutInfo.kindId,
            ParentId: layoutInfo.parentId,
            LayoutId: layoutInfo.layoutId,
            LayoutName: layoutInfo.name,
            LayoutAlias: layoutInfo.alias,
            Data: layoutInfo.data
        };

        // Send request to create the layout.
        return services.formulateServer.post(url, data, function (data) {

            // Return layout information.
            return {
                id: data.Id,
                path: data.Path
            };

        });

    };
}

// Returns the function that deletes a layout from the server.
function getDeleteLayout(services) {
    return function(layoutId) {

        // Variables.
        var url = services.formulateVars.DeleteLayout;
        var data = {
            LayoutId: layoutId
        };

        // Send request to delete the layout.
        return services.formulateServer.post(url, data, function (data) {

            // Return empty data.
            return {};

        });

    };
}

// Returns the function that gets layout kinds.
function getGetKinds(services) {
    return function () {

        // Variables.
        var url = services.formulateVars.GetLayoutKinds;

        // Get layout kinds from server.
        return services.formulateServer.get(url, {}, function (data) {
            return data.Kinds.map(function (item) {
                return {
                    id: item.Id,
                    name: item.Name,
                    directive: item.Directive
                };
            });
        });

    };
}