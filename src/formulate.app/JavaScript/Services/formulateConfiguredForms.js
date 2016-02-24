// Variables.
var app = angular.module("umbraco");

// Service to help with Formulate configured forms.
app.factory("formulateConfiguredForms", function (formulateVars,
    formulateServer) {

    // Variables.
    var services = {
        formulateVars: formulateVars,
        formulateServer: formulateServer
    };

    // Return service.
    return {

        // Gets the configured form info for the configured form with the specified ID.
        getConfiguredFormInfo: getGetConfiguredFormInfo(services),

        // Saves the configured form on the server.
        persistConfiguredForm: getPersistConfiguredForm(services),

        // Deletes a configured form from the server.
        deleteConfiguredForm: getDeleteConfiguredForm(services)

    };

});

// Returns the function that gets information about a configured form.
function getGetConfiguredFormInfo(services) {
    return function (id) {

        // Variables.
        var url = services.formulateVars.GetConfiguredFormInfo;
        var params = {
            ConFormId: id
        };

        // Get configured form info from server.
        return services.formulateServer.get(url, params, function (data) {
            return {
                conFormId: data.ConFormId,
                path: data.Path,
                name: data.Name,
                layoutId: data.LayoutId,
                templateId: data.TemplateId
            };
        });

    };
}

// Returns the function that persists a configured form on the server.
function getPersistConfiguredForm(services) {
    return function (conFormInfo) {

        // Variables.
        var url = services.formulateVars.PersistConfiguredForm;
        var data = {
            ParentId: conFormInfo.parentId,
            ConFormId: conFormInfo.conFormId,
            Name: conFormInfo.name,
            LayoutId: conFormInfo.layoutId,
            TemplateId: conFormInfo.templateId
        };

        // Send request to create the configured form.
        return services.formulateServer.post(url, data, function (data) {

            // Return configured form information.
            return {
                id: data.Id,
                path: data.Path
            };

        });

    };
}

// Returns the function that deletes a configured form from the server.
function getDeleteConfiguredForm(services) {
    return function(conFormId) {

        // Variables.
        var url = services.formulateVars.DeleteConfiguredForm;
        var data = {
            ConFormId: conFormId
        };

        // Send request to delete the configured form.
        return services.formulateServer.post(url, data, function (data) {

            // Return empty data.
            return {};

        });

    };
}