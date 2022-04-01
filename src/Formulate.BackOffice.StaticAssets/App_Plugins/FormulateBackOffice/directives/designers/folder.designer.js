(function () {
    function formulateFolderDesignerDirective($http, $location, $routeParams, navigationService, notificationsService, formHelper) {
        var directive = {
            replace: true,
            templateUrl: "/app_plugins/formulatebackoffice/directives/designers/folder.designer.html",
            scope: {
                entity: "=",
                treeType: "="
            },
            link: function (scope, element, attrs) {
                scope.saveButtonState = "init";

                // Set scope functions.
                scope.save = function () {
                    scope.saveButtonState = "busy";

                    var payload = {
                        entity: scope.entity,
                        parentId: !$routeParams.isNew && $routeParams.id && $routeParams.id !== "-1" ? $routeParams.id : "",
                        treeType: scope.treeType
                    };

                    if (formHelper.submitForm({ scope: scope, formCtrl: scope.formCtrl })) {
                        $http.post(Umbraco.Sys.ServerVariables.formulate["Folders.Save"], payload).then(
                            function (response) {
                                var entityId = response.data.entityId;
                                scope.saveButtonState = "success";

                                formHelper.resetForm({ scope: scope, formCtrl: scope.formCtrl });

                                if (entityId !== $routeParams.id) {
                                    notificationsService.success("Folder created.");

                                    $location.path("/formulate/" + scope.treeType + "/edit/" + entityId).search({});
                                } else {
                                    notificationsService.success("Folder saved.");

                                    var options = {
                                        tree: scope.treeType,
                                        path: response.data.entityPath,
                                        forceReload: true,
                                        activate: false
                                    };

                                    navigationService.syncTree(options);
                                }
                            });
                    }
                };

                scope.canSave = function () {
                    if (scope.saveButtonState === "busy") {
                        return false;
                    }

                    return scope.entity.name && scope.entity.name.length > 0;
                }
            }
        };

        return directive;
    }

    angular.module("umbraco.directives").directive("formulateFolderDesigner", formulateFolderDesignerDirective);
})();