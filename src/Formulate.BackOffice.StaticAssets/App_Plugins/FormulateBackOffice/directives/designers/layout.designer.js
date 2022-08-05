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
        $location,
        $routeParams,
        formHelper,
        notificationsService,
        formulateIds,
        formulateLayouts) => {

        const services = {
            $scope,
            $http,
            $location,
            $routeParams,
            formHelper,
            notificationsService,
            formulateIds,
            formulateLayouts
        }

        // Initializes layout.
        this.initializeLayout(services);

    };

    /**
     * Initializes the layout.
     */
    initializeLayout = (services) => {
        const { $location, $scope, formHelper, notificationsService, formulateLayouts, formulateIds } = services;
        // Disable layout saving until the data is populated.
        $scope.initialized = false;
        $scope.saveButtonState = 'init';

        // Get the layout info.

        $scope.canSave = function () {
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
                    Data: JSON.stringify(entity.data)
                };

                formulateLayouts.persistLayout(data).then((x) => {
                    $scope.saveButtonState = 'success';

                    const resetData = {
                        scope: $scope,
                        formCtrl: $scope.formulateLayoutDesigner,
                    };

                    formHelper.resetForm(resetData);

                    notificationsService.success("Layout saved.");

                    if (entity.isNew) {
                        const sanitizedEntityId = formulateIds.sanitize(entity.id);

                        $location.path("/formulate/layouts/edit/" + sanitizedEntityId).search({});
                    }
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