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
        formulateEntityResource,
        formulateLayouts,
        formulateTypeDefinitionResource) => {

        const services = {
            $scope,
            $http,
            formHelper,
            formulateDesignerResource,
            formulateEntityResource,
            formulateLayouts,
            formulateTypeDefinitionResource
        }

        // Initializes layout.
        this.initializeLayout(services);

    };

    /**
     * Initializes the templates on the scope.
     * @param formulateTypeDefinitionResource The resource that can be used to
     *  fetch templates.
     * @param scope The scope to set the templates on.
     */
    initializeTemplates = (formulateTypeDefinitionResource, scope) => {
        scope.templates = [];
        return formulateTypeDefinitionResource.getTemplateDefinitions()
            .then((data) => {
                scope.templates = data;
            });
    }

    /**
     * Initializes the layout.
     */
    initializeLayout = (services) => {
        const { $scope, formHelper, formulateLayouts, formulateDesignerResource, formulateEntityResource, formulateTypeDefinitionResource } = services;
        // Disable layout saving until the data is populated.
        $scope.initialized = false;
        $scope.saveButtonState = 'init';

        // Get the layout info.

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

        if (Utilities.isArray($scope.entity.path)) {
            const formId = $scope.entity.path.at(-2);

            const getFormOptions = {
                entityType: 'form',
                treeType: "Forms",
                id: formId
            };

            formulateEntityResource.getOrScaffold(getFormOptions).then(
                function (formEntity) {
                    $scope.form = formEntity;
                });
        }

        this.initializeTemplates(formulateTypeDefinitionResource, $scope).then(() => {
            $scope.loading = false;
        });
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