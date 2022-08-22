(function () {
    var resource = function ($http, umbRequestHelper) {
        var serverVars = Umbraco.Sys.ServerVariables.formulate;

        function performGetOrScaffold(options) {
            const treeServerVars = serverVars[options.treeType];
            var isNew = options.create === "true" && options.entityType;
            var hasId = options.id && options.id !== "-1";
            var hasKindId = options.kindId && options.kindId.length > 0;
            var url;

            // replace with resource calls
            if (isNew) {
                url = treeServerVars.GetScaffolding + "?entityType=" + options.entityType;

                if (hasId) {
                    url += `&parentId=${options.id}`;
                }

                if (hasKindId) {
                    url += `&kindId=${options.kindId}`;
                }
            } else {
                url = treeServerVars.Get;

                if (hasId) {
                    url += `?id=${options.id}`;
                }
            }

            return umbRequestHelper.resourcePromise($http.get(url), handleError("Unable to get content."));
        }

        function performDelete(options) {
            var url = serverVars[options.treeType].Delete + "?id=" + options.id;

            return umbRequestHelper.resourcePromise($http.get(url), handleError("Unable to delete item."));
        }

        function performMove(options) {
            var request = {
                entityId: options.entityId,
                treeType: options.treeType
            }

            if (options.parentId && options.parentId !== "-1") {
                request.parentId = options.parentId;
            }

            var url = serverVars[options.treeType].Move;

            return umbRequestHelper.resourcePromise($http.post(url, request), handleError("Failed to move item."));
        };


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
            getOrScaffold: performGetOrScaffold,
            delete: performDelete,
            move: performMove
        };
    };

    angular.module("umbraco").factory("formulateEntityResource", resource);
})();