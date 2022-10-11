(function () {
    /**
     * The controller function that gets called by Angular.
     */
    function Controller(
        $scope,
        formHelper,
        formulateDesignerResource,
        formulateLayouts) {

        $scope.canSave = function () {
            if ($scope.entity.isLegacy) {
                return false;
            }

            return $scope.entity.name && $scope.entity.name.length > 0;
        };


        $scope.save = () => {
            $scope.saveButtonState = 'busy';

            const submitFormData = {
                scope: $scope,
                formCtrl: $scope.formulateLayoutDesigner,
            };

            if (formHelper.submitForm(submitFormData)) {
                const entity = $scope.entity;
                const data = {
                    Id: entity.id,
                    Name: entity.name,
                    Alias: entity.alias,
                    Path: entity.path,
                    KindId: entity.kindId,
                    TemplateId: entity.templateId,
                    Data: entity.data
                };

                formulateLayouts.persistLayout(data).then(() => {
                    $scope.saveButtonState = 'success';

                    const resetData = {
                        scope: $scope,
                        formCtrl: $scope.formulateLayoutDesigner,
                    };

                    formHelper.resetForm(resetData);

                    const options = {
                        entity,
                        treeAlias: 'forms',
                        newEntityText: 'Layout created',
                        existingEntityText: 'Layout saved'
                    };

                    formulateDesignerResource.handleSuccessfulSave(options);
                });
            }
            else {

                const resetData = {
                    scope: $scope,
                    formCtrl: $scope.formulateLayoutDesigner,
                    hasErrors: true
                };

                formHelper.resetForm(resetData);
            }
        };
    };


    angular
        .module('umbraco.directives')
        .directive('formulateLayoutDesigner', () => {
            return {
                restrict: 'E',
                replace: true,
                templateUrl: "/app_plugins/formulate/designers/layouts/layout.designer.html",
                scope: {
                    entity: '=',
                },
                controller: Controller,
            };
        });
})();
