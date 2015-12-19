// Variables.
var app = angular.module("umbraco");

// Service to help with Formulate forms.
app.factory("formulateForms", function ($http, $q, notificationsService) {

    // Variables.
    var services = {
        $http: $http,
        $q: $q,
        notificationsService: notificationsService
    };

    // Return service.
    return {

        // Gets the form info for the form with the specified ID.
        getFormInfo: getGetFormInfo(services),

        // Saves the form on the server.
        persistForm: getPersistForm(services),

        // Deletes a form from the server.
        deleteForm: getDeleteForm(services)

    };

});

// Returns the function that gets information about a form.
function getGetFormInfo(services) {
    return function (id) {

        // Variables.
        //TODO: Use server variables to get this URL.
        var url = "/umbraco/backoffice/formulate/Forms/GetFormInfo";
        var options = {
            cache: false,
            params: {
                FormId: id,
                // Cache buster ensures requests aren't cached.
                CacheBuster: Math.random()
            }
        };

        // Get form info from server.
        return services.$http.get(url, options)
            .then(getHandleResponse(services, function (data) {

                // Return form information.
                return {
                    formId: data.FormId,
                    alias: data.Alias,
                    name: data.Name,
                    path: data.Path,
                    fields: data.Fields.map(function (field) {
                        return {
                            id: field.Id,
                            name: field.Name,
                            alias: field.Alias,
                            label: field.Label,
                            directive: field.Directive,
                            typeLabel: field.TypeLabel,
                            typeFullName: field.TypeFullName
                        };
                    })
                };

            }), getHandleServerError(services));

    };
}

// Returns the function that persists a form on the server.
function getPersistForm(services) {
    return function (formData, isNew) {

        // Format data so the server can consume it.
        var data = {
            ParentId: formData.parentId,
            Alias: formData.alias,
            Name: formData.name,
            Fields: formData.fields.map(function(field) {
                var result = {
                    Name: field.name,
                    Alias: field.alias,
                    Label: field.label,
                    TypeFullName: field.typeFullName
                };
                if (field.id) {
                    result.Id = field.id;
                }
                return result;
            })
        };
        if (!isNew) {
            data.FormId = formData.formId
        }

        // Prepare request.
        //TODO: Use server variables to get this URL.
        var url = "/umbraco/backoffice/formulate/Forms/PersistForm";
        var strData = JSON.stringify(data);
        var options = {
            headers: {
                "Content-Type": "application/json"
            }
        };

        // Send request to persist the form.
        return services.$http.post(url, strData, options)
            .then(getHandleResponse(services, function (data) {

                // Return form ID.
                return {
                    formId: data.FormId
                };

            }), getHandleServerError(services));

    };
}

// Returns the function that deletes a form from the server.
function getDeleteForm(services) {
    return function(formId) {

        // Format data so the server can consume it.
        var data = {
            FormId: formId
        };

        // Prepare request.
        //TODO: Use server variables to get this URL.
        var url = "/umbraco/backoffice/formulate/Forms/DeleteForm";
        var strData = JSON.stringify(data);
        var options = {
            headers: {
                "Content-Type": "application/json"
            }
        };

        // Send request to delete the form.
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