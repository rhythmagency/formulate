(function () {
    function formulateConfiguredFormDesignerDirective(
            $http, $location, $routeParams, formulateDefinitionDirectiveResource,
            navigationService, notificationsService, formHelper) {
        const directive = {
            replace: true,
            templateUrl: "/app_plugins/formulatebackoffice/directives/designers/configured-form.designer.html",
            scope: {
                entity: "=",
            },
            link: function (scope, element, attrs) {
                //TODO: ...
            },
        };

        return directive;
    }

    angular
        .module("umbraco.directives")
        .directive("formulateConfiguredFormDesigner", formulateConfiguredFormDesignerDirective);
})();