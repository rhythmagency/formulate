// Variables.
var app = angular.module("umbraco");

// Service to help with Formulate validations.
app.factory("formulateValidations", function ($http, $q, notificationsService,
    formulateVars) {

    // Variables.
    var services = {
        $http: $http,
        $q: $q,
        notificationsService: notificationsService,
        formulateVars: formulateVars
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
        var options = {
            cache: false,
            params: {
                ValidationId: id,
                // Cache buster ensures requests aren't cached.
                CacheBuster: Math.random()
            }
        };

        // Get validation info from server.
        return services.$http.get(url, options)
            .then(getHandleResponse(services, function (data) {
                return {
                    kindId: data.KindId,
                    validationId: data.ValidationId,
                    path: data.Path,
                    name: data.Name,
                    alias: data.Alias
                };
            }), getHandleServerError(services));

    };
}

// Returns the function that gets information about validations.
function getGetValidationsInfo(services) {
    return function (ids) {

        // Variables.
        var url = services.formulateVars.GetValidationsInfo;
        var options = {
            cache: false,
            params: {
                ValidationIds: ids,
                // Cache buster ensures requests aren't cached.
                CacheBuster: Math.random()
            }
        };

        // Get validation info from server.
        return services.$http.get(url, options)
            .then(getHandleResponse(services, function (data) {
                return data.Validations.map(function(item) {
                    return {
                        kindId: item.KindId,
                        validationId: item.ValidationId,
                        path: item.Path,
                        name: item.Name,
                        alias: item.Alias
                    };
                });
            }), getHandleServerError(services));

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
        var strData = JSON.stringify(data);
        var options = {
            headers: {
                "Content-Type": "application/json"
            }
        };

        // Send request to create the validation.
        return services.$http.post(url, strData, options)
            .then(getHandleResponse(services, function (data) {

                // Return validation information.
                return {
                    id: data.Id,
                    path: data.Path
                };

            }), getHandleServerError(services));

    };
}

// Returns the function that deletes a validation from the server.
function getDeleteValidation(services) {
    return function(validationId) {

        // Format data so the server can consume it.
        var data = {
            ValidationId: validationId
        };

        // Prepare request.
        var url = services.formulateVars.DeleteValidation;
        var strData = JSON.stringify(data);
        var options = {
            headers: {
                "Content-Type": "application/json"
            }
        };

        // Send request to delete the validation.
        return services.$http.post(url, strData, options)
            .then(getHandleResponse(services, function (data) {

                // Return empty data.
                return {};

            }), getHandleServerError(services));

    };
}

// Returns the function that handles a server error.
function getHandleServerError(services) {
    return function() {

        // Indicate error with notification.
        var title = "Server Error";
        var message = "There was an issue communicating with the server.";
        services.notificationsService.error(title, message);
        return services.$q.reject();

    };
}

// Returns the function that handles a server response.
function getHandleResponse(services, successCallback) {
    return function (response) {

        // Variables.
        var data = response.data;

        // Was the request successful?
        if (data.Success) {
            return successCallback(data);
        } else {

            // Error notification.
            var title = "Unexpected Error";
            var message = data.Reason;
            services.notificationsService.error(title, message);
            return services.$q.reject();

        }

    };
}