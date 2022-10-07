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
                view: "/app_plugins/formulate/dialogs/form-handler/edit-form-handler.dialog.html",
                size: 'medium'
            };

            editorService.open(options);
        };


        function init() {
            vm.loading = true;

            formulateTypeDefinitionResource.getHandlerDefinitions()
                .then((response) => {
                    const groupedFormHandlers = {};
                    vm.formHandlers = [];

                    response.map(x => {
                        if (!groupedFormHandlers[x.category]) {
                            groupedFormHandlers[x.category] = [];
                        }

                        groupedFormHandlers[x.category].push(x);
                    });

                    for (const group in groupedFormHandlers) {
                        vm.formHandlers.push({
                            name: group,
                            items: groupedFormHandlers[group]
                        });
                    }

                    vm.loading = false;
                });
        };

        init();
    };

    angular.module('umbraco').controller('Formulate.Dialogs.PickFormHandler', FormHandlerPicker);
})();