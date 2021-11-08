(function () {
    var controller = function ($scope, $routeParams, navigationService, notificationsService, formulateEntityResource) {
        var options = {
            create: $routeParams.create,
            entityType: $routeParams.entityType,
            treeType: "datavalues",
            id: $routeParams.id,
            definitionId: $routeParams.definitionId
        };

        $scope.loading = true;
        formulateEntityResource.getOrScaffold(options).then(
            function (response) {
                $scope.entity = response.data.entity;
                $scope.entityType = response.data.entityType;
                $scope.definitionId = response.data.definitionId;
                $scope.treeType = options.treeType;

                navigationService.syncTree({ tree: options.treeType, path: response.data.treePath, forceReload: true });

                $scope.loading = false;
            }, function () {
                notificationsService.error("Unable to get content");
            });
    };

    angular.module("umbraco").controller("FormulateBackOffice.DataValues.EditController", controller);
})();