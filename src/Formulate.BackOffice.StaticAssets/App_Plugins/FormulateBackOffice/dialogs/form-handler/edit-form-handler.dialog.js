(function () {

    function FormHandlerEditor($scope) {
        var vm = this;

        vm.model = {};
        vm.close = close;
        vm.submit = submit;
        vm.toggleEnabled = toggleEnabled;

        function close() {
            if ($scope.model.close) {
                $scope.model.close();
            }
        }

        function submit() {
            if ($scope.model.submit) {
                $scope.model.submit(vm.model);
            }
        }

        function toggleEnabled() {
            vm.model.enabled = !vm.model.enabled;
        }

        function init() {
            if ($scope.model.handler) {
                vm.model = $scope.model.handler;
            }
            else if ($scope.model.definition) {
                const definition = $scope.model.definition;

                vm.model = {
                    directive: definition.directive,
                    enabled: true,
                    icon: definition.icon,
                    id: crypto.randomUUID(),
                    kindId: definition.kindId,
                    name: null,
                    alias: null,
                    configuration: null
                };
            }

            vm.fields = $scope.model.fields || [];

            vm.loading = false;
        };

        init();
    };

    angular.module('umbraco').controller('Formulate.Dialogs.EditFormHandler', FormHandlerEditor);
})();