(function () {
    function formulateCreateDialogDirective($http, navigationService) {
        var directive = {
            replace: true,
            templateUrl: "/app_plugins/formulate/dialogs/common/create.dialog.html",
            scope: {
                currentNode: "=",
                treeType: "=",
                onCreate: "&"
            },
            link: function (scope, element, attrs) {

                function initialize() {
                    scope.loading = true;
                    scope.allowedOptions = [];

                    var url = Umbraco.Sys.ServerVariables.formulate[scope.treeType].GetCreateOptions;

                    if (scope.currentNode.id !== "-1") {
                        url += `?id=${scope.currentNode.id}`;
                    }

                    $http.get(url).then(
                            function (response) {
                            scope.allowedOptions = response.data;
                                scope.loading = false;
                            });
                }

                scope.create = function (option, $index, $event) {
                    if (scope.onCreate) {
                        scope.onCreate({ option: option, parentId: scope.currentNode.id, $index: $index, $event: $event });
                    }
                    close();
                };

                scope.close = function () {
                    close();
                };

                scope.closeDialog = function (showMenu) {
                    navigationService.hideDialog(showMenu);
                };

                function close() {
                    navigationService.hideMenu();
                }

                // watch for changes in current node.
                scope.$watch("currentNode", initialize);
            }
        };

        return directive;
    }

    angular.module("umbraco.directives").directive("formulateCreateDialog", formulateCreateDialogDirective);
})();