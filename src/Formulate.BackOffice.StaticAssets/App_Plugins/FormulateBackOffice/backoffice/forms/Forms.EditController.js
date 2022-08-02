(function () {
    var controller = function ($scope, $routeParams, navigationService, notificationsService, formulateEntityResource) {
        var options = {
            create: $routeParams.create,
            entityType: $routeParams.entityType,
            treeType: "forms",
            id: $routeParams.id
        };

        $scope.loading = true;
        formulateEntityResource.getOrScaffold(options).then(
            function (entity) {
                $scope.entity = entity;
                $scope.treeType = options.treeType;

                if (!entity.isNew) {
                    navigationService.syncTree({ tree: options.treeType, path: entity.treePath, forceReload: true });
                }

                $scope.loading = false;
            }, function (err) {
                notificationsService.error(err.errorMsg);
            });
    };

    angular.module("umbraco").controller("FormulateBackOffice.Forms.EditController", controller);
})();