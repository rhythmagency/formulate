(function () {
    function directive() {
        return {
            restrict: "E",
            replace: true,
            templateUrl: "/app_plugins/formulate/directives/editors/validations/mandatory/mandatory-validation.editor.html",
            scope: {
                config: "=",
                alias: "=",
            }
        };
    }

    // Associate directive.
    angular.module("umbraco").directive("formulateMandatoryValidation", directive);
})();