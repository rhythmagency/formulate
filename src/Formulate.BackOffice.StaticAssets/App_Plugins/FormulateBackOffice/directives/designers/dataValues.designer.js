(function () {
    function formulateValidationDesignerDirective($http, $location, $routeParams, formulateDefinitionDirectiveResource, navigationService, notificationsService, formHelper) {
        var directive = {
            replace: true,
            templateUrl: "/app_plugins/formulatebackoffice/directives/designers/dataValues.designer.html",
            scope: {
                entity: "="
            },
            link: function (scope, element, attrs) {
                scope.saveButtonState = "init";

                if (scope.entity.configuration !== null) {
                    scope.deserializedConfiguration = JSON.parse(scope.entity.configuration);
                } else {
                    scope.deserializedConfiguration = {};
                }

                formulateDefinitionDirectiveResource.getDataValuesDirective(scope.entity.kindId).then(
                    function (directive) {
                        scope.directive = directive;
                    });

                // Set scope functions.
                scope.save = function () {
                    scope.saveButtonState = "busy";

                    if (typeof (scope.deserializedConfiguration) !== "undefined") {
                        scope.entity.configuration = JSON.stringify(scope.deserializedConfiguration);
                    }

                    var payload = {
                        entity: scope.entity,
                        parentId: !$routeParams.isNew && $routeParams.id && $routeParams.id !== "-1" ? $routeParams.id : ""
                    };

                    if (formHelper.submitForm({ scope: scope, formCtrl: scope.formCtrl })) {
                        $http.post(Umbraco.Sys.ServerVariables.formulate["datavalues.Save"], payload).then(
                            function (response) {
                                var entityId = response.data.entityId;
                                scope.saveButtonState = "success";

                                formHelper.resetForm({ scope: scope, formCtrl: scope.formCtrl });

                                if (entityId !== $routeParams.id) {
                                    notificationsService.success("Data Values created.");

                                    $location.path("/formulate/datavalues/edit/" + entityId).search({});
                                } else {
                                    notificationsService.success("Data Values saved.");

                                    var options = {
                                        tree: "datavalues",
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

    angular.module("umbraco.directives").directive("formulateDataValuesDesignerV2", formulateValidationDesignerDirective);
})();