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
            function (response) {
                $scope.entity = response.data.entity;
                $scope.entityType = response.data.entityType;
                $scope.treeType = options.treeType;

                navigationService.syncTree({ tree: options.treeType, path: response.data.treePath, forceReload: true });

                $scope.loading = false;
            }, function () {
                notificationsService.error("Unable to get content");
            });
    };

    angular.module("umbraco").controller("FormulateBackOffice.Forms.EditController", controller);
})();