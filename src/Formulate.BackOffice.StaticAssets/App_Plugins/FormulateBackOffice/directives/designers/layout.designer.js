(function () {
    function formulateLayoutDesignerDirective(
        $http,
        $location,
        $routeParams,
        formulateDefinitionDirectiveResource,
        navigationService,
        notificationsService,
        formHelper) {
        var directive = {
            replace: true,
            templateUrl: "/app_plugins/formulatebackoffice/directives/designers/layout.designer.html",
            scope: {
                entity: "=",
            },
            link: (scope) => {
                scope.test = JSON.parse(scope.entity.data);
            },
        };

        return directive;
    }

    angular.module("umbraco.directives")
        .directive("formulateLayoutDesigner",
            formulateLayoutDesignerDirective);
})();