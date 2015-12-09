// Variables.
var app = angular.module("umbraco");

// Service to help with Formulate forms.
app.factory("formulateForms", function ($http, $q) {
    return {

        // This gets the fields for the form with the specified ID.
        getFields: function (id) {

            // Variables.
            //TODO: Use server variables to get this URL.
            var url = "/umbraco/backoffice/formulate/Forms/GetFields";
            var options = {
                cache: false,
                params: {
                    FormId: id,
                    // Cache buster ensures requests aren't cached.
                    CacheBuster: Math.random()
                }
            };

            // Get fields from server.
            return $http.get(url, options).then(function (response) {

                // Variables.
                var data = response.data;

                // Was the request successful?
                if (data.Success) {
                    return {
                        fields: data.Fields.map(function (field) {
                            return {
                                id: field.Id,
                                name: field.Name,
                                alias: field.Alias
                            };
                        })
                    };
                } else {
                    return $q.reject();
                }

            }, function () {
                return $q.reject();
            });

        }

    };
});