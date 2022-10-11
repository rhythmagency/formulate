(function () {

    function Controller($scope) {
        $scope.toggleEnabled = function() {
            $scope.content.model.enabled = !$scope.content.model.enabled;
        }
    };

    angular.module('umbraco').controller('Formulate.BackOffice.FormHandlerEditorAppController', Controller);
})();