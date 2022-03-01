(function () {
    function directive() {
        return {
            restrict: "E",
            replace: true,
            templateUrl: "/App_Plugins/FormulateBackOffice/directives/editors/validations/regex/regex-validation.editor.html",
            scope: {
                config: "=",
                alias: "=",
            }
        };
    }

    // Associate directive.
    angular.module("umbraco").directive("formulateRegexValidation", directive);
})();