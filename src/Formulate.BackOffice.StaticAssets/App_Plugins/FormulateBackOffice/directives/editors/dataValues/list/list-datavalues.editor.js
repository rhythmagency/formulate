(function () {
    // Directive.
    function directive() {
        // Controller.
        function controller($scope) {
            if (!$scope.config.items) {
                $scope.config.items = [];
            }
            $scope.addItem = function () {
                $scope.config.items.push({
                    value: null
                });
            };
            $scope.deleteItem = function (index) {
                $scope.config.items.splice(index, 1);
            };
            $scope.sortableOptions = {
                axis: "y",
                cursor: "move",
                delay: 100,
                opacity: 0.5
            };
        }

        return {
            restrict: "E",
            replace: true,
            templateUrl: "/App_Plugins/FormulateBackOffice/directives/editors/dataValues/list/list-datavalues.editor.html",
            scope: {
                config: "="
            },
            controller: controller
        };
    };

    angular.module("umbraco.directives").directive("formulateListDataValues", directive);
})();