(function () {

    function FormHandlerPicker($scope, editorService, formulateTypeDefinitionResource) {
        var vm = this;
        vm.close = close;
        vm.buildFormHandler = buildFormHandler;

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

        function buildFormHandler(formHandler) {
            var options = {
                definition: formHandler,
                submit: function (model) {
                    submit(model);
                    editorService.close();
                },
                close: function () {
                    editorService.close();
                },
                fields: $scope.model.fields,
                view: "/app_plugins/formulatebackoffice/dialogs/form-handler/edit-form-handler.dialog.html",
                size: 'small'
            };

            editorService.open(options);
        };


        function init() {
            vm.loading = true;

            formulateTypeDefinitionResource.getHandlerDefinitions()
                .then((response) => {
                    vm.formHandlers = response;
                    vm.loading = false;
                });
        };

        init();
    };

    angular.module('umbraco').controller('Formulate.Dialogs.PickFormHandler', FormHandlerPicker);
})();