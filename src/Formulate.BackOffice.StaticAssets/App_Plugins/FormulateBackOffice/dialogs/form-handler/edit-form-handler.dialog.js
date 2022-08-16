(function () {

    function FormHandlerEditor($scope, formulateServer, formulateVars) {
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
                vm.loading = false;
            }
            else if ($scope.model.definition) {
                const definition = $scope.model.definition;
                const url = formulateVars['FormHandlers.GetScaffolding'] + '?id=' + definition.kindId;

                formulateServer.get(url).then(function (response) {
                    vm.model = response;
                    vm.loading = false;
                });
            }

            vm.fields = $scope.model.fields || [];
        };

        init();
    };

    angular.module('umbraco').controller('Formulate.Dialogs.EditFormHandler', FormHandlerEditor);
})();