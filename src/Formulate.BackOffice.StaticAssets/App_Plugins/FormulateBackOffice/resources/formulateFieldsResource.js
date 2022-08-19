(function () {
    // Variables.
    var app = angular.module("umbraco");

    // Service to help with Formulate fields.
    app.factory("formulateFields", ["formulateVars", "formulateServer", function (formulateVars,
        formulateServer) {

        // Variables.
        var services = {
            formulateVars: formulateVars,
            formulateServer: formulateServer
        };

        // Return service.
        return {

            // Gets the field types.
            getFieldTypes: getGetFieldTypes(services),


            // Gets the button kinds.
            getButtonKinds: getButtonKinds(services),


            // Gets the field categories.
            getFieldCategories: getGetFieldCategories(services)

        };

    }]);

    // Returns the function that gets field types.
    function getGetFieldTypes(services) {
        return function () {

            // Variables.
            var url = services.formulateVars.GetFieldTypes;

            // Get field types from server.
            return services.formulateServer.get(url, {}, function (data) {

                // Return field types.
                return data.FieldTypes.map(function (field) {
                    return {
                        icon: field.Icon,
                        typeLabel: field.TypeLabel,
                        directive: field.Directive,
                        typeFullName: field.TypeFullName
                    };
                });

            });

        };
    }


    // Returns the function that gets button kinds.
    function getButtonKinds(services) {
        return function () {

            // Variables.
            const url = services.formulateVars['ButtonKinds.GetAll'];

            var callbackOptions = {
                success: (data) => {
                    return data;
                },
                errorMsg: 'Unable to get button kinds'
            };

            // Get button kinds from server.
            return services.formulateServer.get(url, {}, callbackOptions);
        };
    }


    // Returns the function that gets field categories.
    function getGetFieldCategories(services) {
        return function () {

            // Variables.
            var url = services.formulateVars.GetFieldCategories;

            // Get field categories from server.
            return services.formulateServer.get(url, {}, function (data) {

                // Return field categories.
                return data.FieldCategories.map(function (category) {
                    return {
                        kind: category.Kind,
                        group: category.Group
                    };
                });

            });

        };
    }
})();