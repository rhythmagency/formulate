(function () {

    function FormFieldEditor($scope, editorService, formulateServer, formulateVars) {
        var vm = this;

        vm.model = {};
        vm.close = close;
        vm.submit = submit;
        vm.pickValidations = pickValidations;
        vm.removeValidation = removeValidation;

        function close() {
            if ($scope.model.close) {
                $scope.model.close();
            }
        }

        function removeValidation(validation) {
            if (!vm.model.validations) {
                return;
            }

            const index = vm.model.validations.findIndex(x => x.id === validation.id);

            if (index > -1) {
                vm.model.validations.splice(index, 1);
            }
        }

        function pickValidations() {
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

                    return vm.model.validations.some(x => formulateIds.compare(x.id, node.id));
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
                        vm.model.validations.push({
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


        function submit() {
            if ($scope.model.submit) {
                $scope.model.submit(vm.model);
            }
        }

        function init() {
            if ($scope.model.field) {
                vm.model = $scope.model.field;
                vm.supportsGeneralFields = getGeneralFieldSupport(vm.model);
                vm.loading = false;
            }

            else if ($scope.model.definition) {
                const definition = $scope.model.definition;
                const url = formulateVars['FormFields.GetScaffolding'] + '?id=' + definition.kindId;

                formulateServer.get(url).then(function (response) {
                    vm.model = response;
                    vm.supportsGeneralFields = getGeneralFieldSupport(vm.model);
                    vm.loading = false;
                });
            }
        };

        function getGeneralFieldSupport(model) {
            return model.supportsValidation || model.supportsFormField;
        }

        init();
    };

    angular.module('umbraco').controller('Formulate.Dialogs.EditFormField', FormFieldEditor);
})();