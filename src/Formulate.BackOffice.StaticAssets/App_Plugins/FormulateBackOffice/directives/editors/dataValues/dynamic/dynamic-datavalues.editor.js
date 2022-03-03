function directive() {
    return {
        restrict: "E",
        replace: true,
        templateUrl: "/App_Plugins/FormulateBackOffice/directives/editors/dataValues/dynamic/dynamic-datavalues.editor.html",
        scope: {
            config: "=",
            alias: "="
        },
    };
}

angular.module("umbraco.directives").directive("formulateDynamicDataValues", directive);