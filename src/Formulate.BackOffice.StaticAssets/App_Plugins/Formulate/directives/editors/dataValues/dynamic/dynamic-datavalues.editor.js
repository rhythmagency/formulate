function directive() {
    return {
        restrict: "E",
        replace: true,
        templateUrl: "/app_plugins/formulate/directives/editors/dataValues/dynamic/dynamic-datavalues.editor.html",
        scope: {
            config: "=",
            alias: "="
        },
    };
}

angular.module("umbraco.directives").directive("formulateDynamicDataValues", directive);