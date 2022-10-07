(function () {
    function formulateDeleteDialogDirective(formulateEntityResource, navigationService, $routeParams, $location) {
        var controller = function ($scope) {
            $scope.success = false;

            $scope.performDelete = function () {
                var options = {
                    id: $scope.currentNode.id,
                    treeType: $scope.treeType
                };

                formulateEntityResource.delete(options).then(function (data) {
                    var parent = $scope.currentNode.parent();
                    var isCurrentEditorDeleted = data.deletedEntityIds.indexOf($routeParams.id) > -1;

                    navigationService.reloadNode(parent);

                    if (isCurrentEditorDeleted) {
                        var url = `/${$routeParams.section}`;

                        if (parent.id && parent.id !== "-1") {
                            url += `/${$routeParams.tree}/edit/${parent.id}`;
                        }

                        $location.path(url);
                    }

                    $scope.success = true;
                });

            };

            $scope.close = function () {
                close();
            };

            $scope.cancel = function () {
                navigationService.hideDialog(false);
            };

            function close() {
                navigationService.hideMenu();
            }

        }

        var directive = {
            replace: true,
            templateUrl: "/app_plugins/formulatebackoffice/directives/dialogs/delete.dialog.html",
            scope: {
                currentNode: "=",
                treeType: "=",
                onDelete: "&"
            },
            controller: controller
        };

        return directive;
    }

    angular.module("umbraco.directives").directive("formulateDeleteDialog", formulateDeleteDialogDirective);
})();