(function () {
    function formulateDesignerRouterDirective() {
        var directive = {
            replace: true,
            templateUrl: "/app_plugins/formulate/designers/router.html",
            scope: {
                entity: "=",
                treeType: "="
            },
            link: function (scope, element, attrs) {
            }
        };

        return directive;
    }

    angular.module("umbraco.directives").directive("formulateDesignerRouter", formulateDesignerRouterDirective);
})();