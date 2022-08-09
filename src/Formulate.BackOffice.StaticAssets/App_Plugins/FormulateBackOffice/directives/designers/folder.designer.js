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

                    const entity = scope.entity;
                    const payload = {
                        Id: entity.id,
                        Name: entity.name,
                        Alias: entity.alias,
                        Path: entity.path,
                    };

                    if (formHelper.submitForm({ scope: scope, formCtrl: scope.formCtrl })) {
                        $http.post(Umbraco.Sys.ServerVariables.formulate["Folders.Save"], payload).then(
                            function (response) {
                                scope.saveButtonState = "success";

                                formHelper.resetForm({ scope: scope, formCtrl: scope.formCtrl });

                                if (entity.isNew) {
                                    notificationsService.success("Folder created.");

                                    $location.path("/formulate/" + scope.treeType + "/edit/" + entity.id).search({});
                                } else {
                                    notificationsService.success("Folder saved.");

                                    var options = {
                                        tree: scope.treeType,
                                        path: entity.treePath,
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