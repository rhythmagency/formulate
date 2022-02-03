// Variables.
(function () {
    function directive(formulateDirectives) {
        return {
            restrict: "E",
            replace: true,
            templateUrl: "/App_Plugins/FormulateBackOffice/directives/editors/dataValues/dynamic/dynamic-datavalues.editor.html",
            scope: {
                config: "="
            }
        };
    };

    angular.module("umbraco.directives").directive("formulateDynamicDataValues", directive);
})();