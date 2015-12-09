// Variables.
var app = angular.module("umbraco");

// Service to help with Formulate layouts.
app.factory("formulateLayouts", function ($http, $q) {
    return {

        // This gets the paths to the layouts with the specified IDs.
        getPath: function (id) {

            // Variables.
            //TODO: Use server variables to get this URL.
            var url = "/umbraco/backoffice/formulate/Layouts/GetPath";
            var options = {
                cache: false,
                params: {
                    LayoutId: id,
                    // Cache buster ensures requests aren't cached.
                    CacheBuster: Math.random()
                }
            };

            // Get path from server.
            return $http.get(url, options).then(function (response) {

                // Variables.
                var data = response.data;

                // Was the request successful?
                if (data.Success) {
                    return data.Path;
                } else {
                    return $q.reject();
                }

            }, function () {
                return $q.reject();
            });

        }

    };
});