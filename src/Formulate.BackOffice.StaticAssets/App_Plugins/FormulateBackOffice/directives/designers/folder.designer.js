(function () {
    function formulateFolderDesignerDirective($http, formulateDesignerResource, formHelper) {
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

                                const options = {
                                    entity,
                                    treeAlias: scope.treeType,
                                    newEntityText: 'Folder created',
                                    existingEntityText: 'Folder saved'
                                };

                                formulateDesignerResource.handleSuccessfulSave(options);
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