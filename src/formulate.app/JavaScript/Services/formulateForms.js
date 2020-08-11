// Variables.
var app = angular.module("umbraco");

// Service to help with Formulate forms.
app.factory("formulateForms", function (formulateVars,
    formulateServer) {

    // Variables.
    var services = {
        formulateVars: formulateVars,
        formulateServer: formulateServer
    };

    // Return service.
    return {

        // Gets the form info for the form with the specified ID.
        getFormInfo: getGetFormInfo(services),

        // Saves the form on the server.
        persistForm: getPersistForm(services),

        // Deletes a form from the server.
        deleteForm: getDeleteForm(services),

        // Moves a form to a new parent on the server.
        moveForm: getMoveForm(services),

        // Duplicates a form.
        duplicateForm: getDuplicateForm(services)

    };

});

// Returns the function that gets information about a form.
function getGetFormInfo(services) {
    return function (id) {

        // Variables.
        var url = services.formulateVars.GetFormInfo;
        var params = {
            FormId: id
        };

        // Get form info from server.
        return services.formulateServer.get(url, params, function (data) {

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
                        category: field.Category,
                        directive: field.Directive,
                        icon: field.Icon,
                        isServerSideOnly: field.IsServerSideOnly,
                        typeLabel: field.TypeLabel,
                        typeFullName: field.TypeFullName,
                        validations: field.Validations
                            .map(function(validation) {
                                return {
                                    id: validation.Id,
                                    name: validation.Name
                                };
                            }),
                        configuration: field.Configuration || {}
                    };
                }),
                handlers: data.Handlers.map(function (handler) {
                    return {
                        id: handler.Id,
                        name: handler.Name,
                        enabled: handler.Enabled,
                        alias: handler.Alias,
                        directive: handler.Directive,
                        icon: handler.Icon,
                        typeLabel: handler.TypeLabel,
                        typeFullName: handler.TypeFullName,
                        configuration: handler.Configuration || {}
                    };
                })
            };

        });

    };
}

// Returns the function that persists a form on the server.
function getPersistForm(services) {
    return function (formData, isNew) {

        // Variables.
        var url = services.formulateVars.PersistForm;
        var data = {
            ParentId: formData.parentId,
            Alias: formData.alias,
            Name: formData.name,
            Fields: formData.fields.map(function(field) {
                var result = {
                    Name: field.name,
                    Alias: field.alias,
                    Label: field.label,
                    Category: field.category,
                    TypeFullName: field.typeFullName,
                    Validations: (field.validations || [])
                        .map(function(validation) {
                            return validation.id;
                        }),
                    Configuration: field.configuration
                };
                if (field.id) {
                    result.Id = field.id;
                }
                return result;
            }),
            Handlers: formData.handlers.map(function(handler) {
                var result = {
                    Name: handler.name,
                    Alias: handler.alias,
                    Enabled: handler.enabled,
                    TypeFullName: handler.typeFullName,
                    Configuration: handler.configuration
                };
                if (handler.id) {
                    result.Id = handler.id;
                }
                return result;
            })
        };
        if (!isNew) {
            data.FormId = formData.formId
        }

        // Send request to persist the form.
        return services.formulateServer.post(url, data, function (data) {

            // Return form ID.
            return {
                formId: data.FormId
            };

        });

    };
}

// Returns the function that deletes a form from the server.
function getDeleteForm(services) {
    return function(formId) {

        // Variables.
        var url = services.formulateVars.DeleteForm;
        var data = {
            FormId: formId
        };

        // Send request to delete the form.
        return services.formulateServer.post(url, data, function () {

            // Return empty data.
            return {};

        });

    };
}

// Returns the function that moves a form.
function getMoveForm(services) {
    return function (formId, newParentId) {

        // Variables.
        var url = services.formulateVars.MoveForm;
        var data = {
            FormId: formId,
            NewParentId: newParentId
        };

        // Send request to persist the form.
        return services.formulateServer.post(url, data, function (data) {

            // Return form info.
            return {
                id: data.Id,
                path: data.Path,
                descendants: data.Descendants.map(function (item) {
                    return {
                        id: item.Id,
                        path: item.Path
                    };
                })
            };

        });

    };
}


// Returns the function that duplicates a form from the server.
function getDuplicateForm(services) {
    return function (formId, parentId) {

        // Variables.
        var url = services.formulateVars.DuplicateForm;
        var data = {
            FormId: formId,
            ParentId: parentId
        };

        // Send request to delete the form.
        return services.formulateServer.post(url, data, function (data) {

            // Return form ID and path.
            return {
                formId: data.FormId,
                path: data.Path
            };

        });

    };
}