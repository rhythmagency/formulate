(function () {
    function formulateValidationDesignerDirective($http, formulateDesignerResource, formHelper) {
        var directive = {
            replace: true,
            templateUrl: "/app_plugins/formulate/designers/dataValues/dataValues.designer.html",
            scope: {
                entity: "="
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
                        $http.post(Umbraco.Sys.ServerVariables.formulate.DataValues.Save, payload).then(
                            function (response) {
                                scope.saveButtonState = "success";
                                                               
                                formHelper.resetForm({ scope: scope, formCtrl: scope.formCtrl });

                                const options = {
                                    entity,
                                    treeAlias: 'dataValues',
                                    newEntityText: 'Data Values created',
                                    existingEntityText: 'Data Values saved'
                                };

                                formulateDesignerResource.handleSuccessfulSave(options);
                            });
                    }
                };

                scope.canSave = function () {
                    if (scope.entity.isLegacy) {
                        return false;
                    }

                    if (scope.saveButtonState === "busy") {
                        return false;
                    }

                    return scope.entity.name && scope.entity.name.length > 0;
                }
            }
        };

        return directive;
    }

    angular.module("umbraco.directives").directive("formulateDataValuesDesigner", formulateValidationDesignerDirective);
})();