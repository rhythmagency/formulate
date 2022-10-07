(function () {

    function FormFieldPicker($scope, editorService, formulateTypeDefinitionResource) {
        var vm = this;
        vm.close = close;
        vm.buildFormField = buildFormField;

        function close() {
            if ($scope.model.close) {
                $scope.model.close();
            }
        }

        function submit(model) {
            if ($scope.model.submit) {
                $scope.model.submit(model);
            }
        }

        function buildFormField(formField) {
            var options = {
                definition: formField,
                submit: function (model) {
                    submit(model);
                    editorService.close();
                },
                close: function () {
                    editorService.close();
                },
                view: "/app_plugins/formulate/dialogs/form-field/edit-form-field.dialog.html",
                size: 'medium'
            };

            editorService.open(options);
        };


        function init() {
            vm.loading = true;

            formulateTypeDefinitionResource.getFieldDefinitions()
                .then((response) => {
                    const groupedFormFields = {};
                    vm.formFields = [];

                    response.map(x => {
                        if (!groupedFormFields[x.category]) {
                            groupedFormFields[x.category] = [];
                        }

                        groupedFormFields[x.category].push(x);
                    });

                    for (const group in groupedFormFields) {
                        vm.formFields.push({
                            name: group,
                            items: groupedFormFields[group]
                        });
                    }

                    vm.loading = false;
                });
        };

        init();
    };

    angular.module('umbraco').controller('Formulate.Dialogs.PickFormField', FormFieldPicker);
})();