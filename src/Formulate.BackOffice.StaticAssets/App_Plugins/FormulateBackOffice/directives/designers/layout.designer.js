/**
 * The layout designer, which facilitates editing the form layout (rows, columns,
 * field positions, etc.).
 */
class FormulateLayoutDesigner {
    /**
     * The controller function that gets called by Angular.
     */
    controller = (
        $scope,
        $http,
        formHelper,
        formulateDesignerResource,
        formulateLayouts) => {

        const services = {
            $scope,
            $http,
            formHelper,
            formulateDesignerResource,            
            formulateLayouts
        }

        // Initializes layout.
        this.initializeLayout(services);

    };

    /**
     * Initializes the layout.
     */
    initializeLayout = (services) => {
        const { $scope, formHelper, formulateLayouts, formulateDesignerResource } = services;
        // Disable layout saving until the data is populated.
        $scope.initialized = false;
        $scope.saveButtonState = 'init';

        // Get the layout info.

        $scope.canSave = function () {
            if (scope.entity.isLegacy) {
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
                        treeAlias: 'layouts',
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

        $scope.initialized = true;
    };

    /**
     * Registers the directive with Angular.
     */
    registerDirective = () => {
        angular
            .module('umbraco.directives')
            .directive('formulateLayoutDesigner', () => {
            return {
                restrict: 'E',
                replace: true,
                templateUrl: "/app_plugins/formulatebackoffice/directives/designers/layout.designer.html",
                scope: {
                    entity: '=',
                },
                controller: this.controller,
            };
        });
    };
}

// Initialize.
(new FormulateLayoutDesigner()).registerDirective();