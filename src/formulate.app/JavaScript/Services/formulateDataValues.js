// Variables.
var app = angular.module("umbraco");

// Service to help with Formulate data values.
app.factory("formulateDataValues", function (formulateVars,
    formulateServer) {

    // Variables.
    var services = {
        formulateVars: formulateVars,
        formulateServer: formulateServer
    };

    // Return service.
    return {

        // Gets the data value info for the data value with the specified ID.
        getDataValueInfo: getGetDataValueInfo(services),

        // Gets the data value info for the data values with
        // the specified ID's.
        getDataValuesInfo: getGetDataValuesInfo(services),

        // Saves the data value on the server.
        persistDataValue: getPersistDataValue(services),

        // Deletes a data value from the server.
        deleteDataValue: getDeleteDataValue(services),

        // Gets the kind of data values from the server.
        getKinds: getGetKinds(services),

        // Moves a data value to a new parent on the server.
        moveDataValue: getMoveDataValue(services),

        // Gets the data value suppliers from the server.
        getSuppliers: getGetSuppliers(services)

    };

});

// Returns the function that gets information about a data value.
function getGetDataValueInfo(services) {
    return function (id) {

        // Variables.
        var url = services.formulateVars.GetDataValueInfo;
        var params = {
            DataValueId: id
        };

        // Get data value info from server.
        return services.formulateServer.get(url, params, function (data) {
            return {
                kindId: data.KindId,
                dataValueId: data.DataValueId,
                path: data.Path,
                name: data.Name,
                alias: data.Alias,
                directive: data.Directive,
                data: data.Data
            };
        });

    };
}

// Returns the function that gets information about data values.
function getGetDataValuesInfo(services) {
    return function (ids) {

        // Variables.
        var url = services.formulateVars.GetDataValuesInfo;
        var params = {
            DataValueIds: ids
        };

        // Get data value info from server.
        return services.formulateServer.get(url, params, function (data) {
            return data.DataValues.map(function (item) {
                return {
                    kindId: item.KindId,
                    dataValueId: item.DataValueId,
                    path: item.Path,
                    name: item.Name,
                    alias: item.Alias,
                    directive: item.Directive,
                    data: item.Data
                };
            });
        });


    };
}

// Returns the function that persists a data value on the server.
function getPersistDataValue(services) {
    return function (dataValueInfo) {

        // Variables.
        var url = services.formulateVars.PersistDataValue;
        var data = {
            KindId: dataValueInfo.kindId,
            ParentId: dataValueInfo.parentId,
            DataValueId: dataValueInfo.dataValueId,
            DataValueName: dataValueInfo.name,
            DataValueAlias: dataValueInfo.alias,
            Data: dataValueInfo.data
        };

        // Send request to create the data value.
        return services.formulateServer.post(url, data, function (data) {

            // Return data value information.
            return {
                id: data.Id,
                path: data.Path
            };

        });

    };
}

// Returns the function that deletes a data value from the server.
function getDeleteDataValue(services) {
    return function(dataValueId) {

        // Variables.
        var url = services.formulateVars.DeleteDataValue;
        var data = {
            DataValueId: dataValueId
        };

        // Send request to delete the data value.
        return services.formulateServer.post(url, data, function (data) {

            // Return empty data.
            return {};

        });

    };
}

// Returns the function that gets the data value kinds.
function getGetKinds(services) {
    return function () {

        // Variables.
        var url = services.formulateVars.GetDataValueKinds;

        // Get data value kinds from server.
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

// Returns the function that gets the data value suppliers.
function getGetSuppliers(services) {
    return function () {

        // Variables.
        var url = services.formulateVars.GetDataValueSuppliers;

        // Get data value kinds from server.
        return services.formulateServer.get(url, {}, function (data) {
            return data.Kinds.map(function (item) {
                return {
                    name: item.Name,
                    className: item.ClassName
                };
            });
        });

    };
}

// Returns the function that moves a data value.
function getMoveDataValue(services) {
    return function (dataValueId, newParentId) {

        // Variables.
        var url = services.formulateVars.MoveDataValue;
        var data = {
            DataValueId: dataValueId,
            NewParentId: newParentId
        };

        // Send request to persist the data value.
        return services.formulateServer.post(url, data, function (data) {

            // Return data value info.
            return {
                id: data.Id,
                path: data.Path
            };

        });

    };
}