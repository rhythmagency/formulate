// Variables.
var app = angular.module("umbraco");

// Service to help with Formulate forms.
app.factory("formulateForms", function ($http, $q, notificationsService) {
    return {

        // This gets the form info for the form with the specified ID.
        getFormInfo: function (id) {

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
            return $http.get(url, options).then(function (response) {

                // Variables.
                var data = response.data;

                // Was the request successful?
                if (data.Success) {
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
                                typeLabel: field.TypeLabel
                            };
                        })
                    };
                } else {
                    return $q.reject();
                }

            }, function () {
                return $q.reject();
            });

        },

        // Saves the form on the server.
        persistForm: function (formData, isNew) {

            // Format data so the server can consume it.
            var data = {
                Alias: formData.alias,
                Name: formData.name,
                Fields: formData.fields.map(function(field) {
                    var result = {
                        Name: field.name,
                        Alias: field.alias,
                        Label: field.label
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
            return $http.post(url, strData, options)
                .then(function (response) {

                    // Variables.
                    var data = response.data;

                    // Was the request successful?
                    if (data.Success) {
                        return {
                            formId: data.FormId
                        };
                    } else {
                        var title = "Unexpected Error";
                        var message = data.Reason;
                        notificationsService.error(title, message);
                        return $q.reject();
                    }

                });

        }

    };
});