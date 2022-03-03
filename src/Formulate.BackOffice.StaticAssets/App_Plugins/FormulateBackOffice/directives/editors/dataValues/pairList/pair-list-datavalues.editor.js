// Variables.
var app = angular.module("umbraco");

// Associate directive/controller.
app.directive("formulateDataValuePairList", directive);
app.controller("formulate.dataValuePairList", controller);

// Directive.
function directive() {
    return {
        restrict: "E",
        replace: true,
        templateUrl: "/App_Plugins/FormulateBackOffice/directives/editors/dataValues/pairList/pair-list-datavalues.editor.html",
        scope: {
            config: "="
        },
        controller: "formulate.dataValuePairList"
    };
}

// Controller.
function controller($scope) {
    if (!$scope.config.items) {
        $scope.config.items = [];
    }
    $scope.addItem = function () {
        $scope.config.items.push({
            primary: null,
            secondary: null
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