(function () {

    function Controller($scope, editorService, formulateIds, formulateServer, formulateVars) {
        const services = {
            $scope,
            editorService,
            formulateIds,
            formulateServer,
            formulateVars
        };

        $scope.supportsGeneralSettings = getGeneralSettingsSupport($scope.content.model);

        $scope.actions = {
            pickValidations: pickValidations(services),
            removeValidation: removeValidation(services)
        };

        initCategories(services).then(() => {
            $scope.loading = false;
        });  
    };

    function initCategories(services) {
        const { $scope, formulateVars, formulateServer } = services;
        const model = $scope.content.model;

        return new Promise((resolve, reject) => {

            if (model.supportsCategory) {
                const url = formulateVars.FormFields.GetCategories;

                return formulateServer.get(url).then(function (response) {
                    $scope.categories = response;
                }).then(resolve());
            }
            else {
                return resolve();
            }
        });
    }

    function getGeneralSettingsSupport(model) {
        return model.supportsValidation || model.supportsLabel || model.supportsCategory;
    }

    function pickValidations(services) {
        const { $scope, editorService, formulateIds } = services;

        return function () {
            // The data sent to the validation chooser.
            var config = {
                section: 'formulate',
                treeAlias: 'validations',
                entityType: 'validation',
                multiPicker: false,
                subtitle: "Choose any of these validations to add to your field. They will be evaluated in the order you choose them.",
                filter: (node) => {
                    if (node.nodeType === 'Folder') {
                        return true;
                    }

                    return $scope.content.model.validations.some(x => formulateIds.compare(x.id, node.id));
                },
                filterCssClass: 'not-allowed',
                submit: () => {
                    editorService.close();
                },
                close: () => {
                    editorService.close();
                },
                select: (selection) => {
                    if (selection) {
                        $scope.content.model.validations.push({
                            id: selection.id,
                            name: selection.name
                        });
                    }

                    editorService.close();
                }
            };

            // Open the overlay that displays the validations.
            editorService.treePicker(config);
        };
    }

    function removeValidation(services) {
        const { $scope } = services;

        return function (validation) {
            if (!$scope.content.model.validations) {
                return;
            }

            const index = $scope.content.model.validations.findIndex(x => x.id === validation.id);

            if (index > -1) {
                $scope.content.model.validations.splice(index, 1);
            }
        }
    }

    angular.module("umbraco").controller("Formulate.BackOffice.FormFieldEditorAppController", Controller);
})();