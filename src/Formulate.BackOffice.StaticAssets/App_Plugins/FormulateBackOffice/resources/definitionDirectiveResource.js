(function () {
    var resource = function ($http, umbRequestHelper) {
        var serverVars = Umbraco.Sys.ServerVariables.formulate;

        function performGetDataValuesDirective(definitionId) {
            var options = {
                id: definitionId,
                type: "datavalues"
            };

            return performGet(options);
        }

        function performGetValidationDirective(definitionId) {
            var options = {
                id: definitionId,
                type: "validations"
            };

            return performGet(options);
        }

        function performGet(options) {
            var url = serverVars[`${options.type}.GetDirective`] + "?id=" + options.id;

            return umbRequestHelper.resourcePromise($http.get(url), handleError("Unable to get directive."));
        }

        function handleError(errorMessage) {
            return {
                error: function(data) {
                    var errorMsg = errorMessage;

                    if (typeof (data.notifications) !== "undefined") {
                        if (data.notifications.length > 0) {
                            if (data.notifications[0].header.length > 0) {
                                errorMsg = data.notifications[0].header;
                            }
                            if (data.notifications[0].message.length > 0) {
                                errorMsg = errorMsg + ": " + data.notifications[0].message;
                            }
                        }
                    }

                    return {
                        errorMsg: errorMsg
                    };
                }
            };
        }

        return {
            getDataValuesDirective: performGetDataValuesDirective,
            getValidationDirective: performGetValidationDirective
        };
    };

    angular.module("umbraco").factory("formulateDefinitionDirectiveResource", resource);
})();