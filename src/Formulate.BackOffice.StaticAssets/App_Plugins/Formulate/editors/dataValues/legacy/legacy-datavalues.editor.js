// Directive.
function directive() {

    // Controller.
    function controller() {
    }

    // Directive definition.
    return {
        restrict: "E",
        replace: true,
        templateUrl: "/app_plugins/formulate/editors/dataValues/legacy/legacy-datavalues.editor.html",
        scope: {
            config: "="
        },
        controller: controller,
    };

}

// Register directive.
angular.module("umbraco.directives").directive("formulateLegacyDataValue", directive);