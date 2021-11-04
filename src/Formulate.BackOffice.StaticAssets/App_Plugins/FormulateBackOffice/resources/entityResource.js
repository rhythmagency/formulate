(function() {
    var resource = function($http) {
        function getOrScaffold(options) {
            var isNew = options.create === "true" && options.entityType;
            var hasId = options.id && options.id !== "-1";
            var hasDefinitionId = options.definitionId && options.definitionId.length > 0;
            var serverVars = Umbraco.Sys.ServerVariables.formulate;
            var url;

            // replace with resource calls
            if (isNew) {
                url = serverVars[`${options.treeType}.GetScaffolding`] + "?entityType=" + options.entityType;

                if (hasId) {
                    url += `&parentId=${options.id}`;
                }

                if (hasDefinitionId) {
                    url += `&definitionId=${options.definitionId}`;
                }
            } else {
                url = serverVars[`${options.treeType}.Get`];

                if (hasId) {
                    url += `?id=${options.id}`;
                }
            }

            return $http.get(url);
        }

        return {
            getOrScaffold: getOrScaffold
        };
    };

    angular.module("umbraco").factory("formulateEntityResource", resource);
})();