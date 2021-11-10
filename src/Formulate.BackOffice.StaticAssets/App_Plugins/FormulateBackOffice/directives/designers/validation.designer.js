(function () {
    function formulateValidationDesignerDirective($http, $location, $routeParams, formulateDefinitionDirectiveResource, navigationService, notificationsService, formHelper) {
        var directive = {
            replace: true,
            templateUrl: "/app_plugins/formulatebackoffice/directives/designers/validation.designer.html",
            scope: {
                entity: "="
            },
            link: function (scope, element, attrs) {
                scope.saveButtonState = "init";

                if (scope.entity.configuration !== null) {
                    scope.deserializedConfig = JSON.parse(scope.entity.configuration);
                } else {
                    scope.deserializedConfig = {};
                }

                formulateDefinitionDirectiveResource.getValidationDirective(scope.entity.definitionId).then(
                    function (directive) {
                        scope.directive = directive;
                    });

                // Set scope functions.
                scope.save = function () {
                    scope.saveButtonState = "busy";

                    if (typeof (scope.deserializedConfig) !== "undefined") {
                        scope.entity.configuration = JSON.stringify(scope.deserializedConfig);
                    }

                    var payload = {
                        entity: scope.entity,
                        parentId: !$routeParams.isNew && $routeParams.id && $routeParams.id !== "-1" ? $routeParams.id : "",
                        treeType: scope.treeType
                    };

                    if (formHelper.submitForm({ scope: scope, formCtrl: scope.formCtrl })) {
                        $http.post(Umbraco.Sys.ServerVariables.formulate["validations.Save"], payload).then(
                            function (response) {
                                var entityId = response.data.entityId;
                                scope.saveButtonState = "success";

                                formHelper.resetForm({ scope: scope, formCtrl: scope.formCtrl });

                                if (entityId !== $routeParams.id) {
                                    notificationsService.success("Validation created.");

                                    $location.path("/formulate/validation/edit/" + entityId).search({});
                                } else {
                                    notificationsService.success("Validation saved.");

                                    var options = {
                                        tree: "validations",
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

    angular.module("umbraco.directives").directive("formulateValidationDesignerV2", formulateValidationDesignerDirective);
})();