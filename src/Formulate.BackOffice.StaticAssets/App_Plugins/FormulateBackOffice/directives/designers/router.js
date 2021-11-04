(function () {
    function formulateDesignerRouterDirective() {
        var directive = {
            replace: true,
            templateUrl: "/app_plugins/formulatebackoffice/directives/designers/router.html",
            scope: {
                entity: "=",
                entityType: "=",
                treeType: "="
            },
            link: function (scope, element, attrs) {
                console.log(scope.treeType);
            }
        };

        return directive;
    }

    angular.module("umbraco.directives").directive("formulateDesignerRouter", formulateDesignerRouterDirective);
})();