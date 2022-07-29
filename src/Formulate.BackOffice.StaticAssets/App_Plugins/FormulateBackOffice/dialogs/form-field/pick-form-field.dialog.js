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
                view: "/app_plugins/formulatebackoffice/dialogs/form-field/edit-form-field.dialog.html",
                size: 'small'
            };

            editorService.open(options);
        };


        function init() {
            vm.loading = true;

            formulateTypeDefinitionResource.getFieldDefinitions()
                .then((response) => {
                    vm.formFields = response;
                    vm.loading = false;
                });
        };

        init();
    };

    angular.module('umbraco').controller('Formulate.Dialogs.PickFormField', FormFieldPicker);
})();