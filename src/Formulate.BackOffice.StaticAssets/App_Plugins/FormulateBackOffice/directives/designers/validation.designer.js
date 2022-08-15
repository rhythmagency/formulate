(function () {
    function formulateValidationDesignerDirective($http, $location, $routeParams, formulateDefinitionDirectiveResource, navigationService, notificationsService, formHelper) {
        var directive = {
            replace: true,
            templateUrl: "/app_plugins/formulatebackoffice/directives/designers/validation.designer.html",
            scope: {
                entity: "=",
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
                        KindId: entity.kindId,
                        Data: entity.data
                    };

                    if (formHelper.submitForm({ scope: scope, formCtrl: scope.formCtrl })) {
                        $http.post(Umbraco.Sys.ServerVariables.formulate["validations.Save"], payload).then(
                            function (response) {
                                scope.saveButtonState = "success";

                                formHelper.resetForm({ scope: scope, formCtrl: scope.formCtrl });

                                if (entity.isNew) {
                                    notificationsService.success("Validation created.");

                                    $location.path("/formulate/validations/edit/" + entity.id).search({});
                                } else {
                                    notificationsService.success("Validation saved.");

                                    var options = {
                                        tree: "validations",
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

    angular.module("umbraco.directives").directive("formulateValidationDesigner", formulateValidationDesignerDirective);
})();