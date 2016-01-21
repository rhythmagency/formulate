// Variables.
var app = angular.module("umbraco");

// Service to help with Formulate validations.
app.factory("formulateValidations", function (formulateVars,
    formulateServer) {

    // Variables.
    var services = {
        formulateVars: formulateVars,
        formulateServer: formulateServer
    };

    // Return service.
    return {

        // Gets the validation info for the validation with the specified ID.
        getValidationInfo: getGetValidationInfo(services),

        // Gets the info for the validations with the specified IDs.
        getValidationsInfo: getGetValidationsInfo(services),

        // Saves the validation on the server.
        persistValidation: getPersistValidation(services),

        // Deletes a validation from the server.
        deleteValidation: getDeleteValidation(services)

    };

});

// Returns the function that gets information about a validation.
function getGetValidationInfo(services) {
    return function (id) {

        // Variables.
        var url = services.formulateVars.GetValidationInfo;
        var params = {
            ValidationId: id
        };

        // Get validation info from server.
        return services.formulateServer.get(url, params, function (data) {
            return {
                kindId: data.KindId,
                validationId: data.ValidationId,
                path: data.Path,
                name: data.Name,
                alias: data.Alias
            };
        });

    };
}

// Returns the function that gets information about validations.
function getGetValidationsInfo(services) {
    return function (ids) {

        // Variables.
        var url = services.formulateVars.GetValidationsInfo;
        var params = {
            ValidationIds: ids
        };

        // Get validation info from server.
        return services.formulateServer.get(url, params, function (data) {
            return data.Validations.map(function(item) {
                return {
                    kindId: item.KindId,
                    validationId: item.ValidationId,
                    path: item.Path,
                    name: item.Name,
                    alias: item.Alias
                };
            });
        });

    };
}

// Returns the function that persists a validation on the server.
function getPersistValidation(services) {
    return function (validationInfo) {

        // Variables.
        var url = services.formulateVars.PersistValidation;
        var data = {
            KindId: validationInfo.kindId,
            ParentId: validationInfo.parentId,
            ValidationId: validationInfo.validationId,
            ValidationName: validationInfo.name,
            ValidationAlias: validationInfo.alias
        };

        // Send request to create the validation.
        return services.formulateServer.post(url, data, function (data) {

            // Return validation information.
            return {
                id: data.Id,
                path: data.Path
            };

        });

    };
}

// Returns the function that deletes a validation from the server.
function getDeleteValidation(services) {
    return function(validationId) {

        // Variables.
        var url = services.formulateVars.DeleteValidation;
        var data = {
            ValidationId: validationId
        };

        // Send request to delete the validation.
        return services.formulateServer.post(url, data, function (data) {

            // Return empty data.
            return {};

        });

    };
}