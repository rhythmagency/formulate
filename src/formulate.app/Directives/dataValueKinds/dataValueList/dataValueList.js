// Variables.
var app = angular.module("umbraco");

// Associate directive/controller.
app.directive("formulateDataValueList", directive);
app.controller("formulate.dataValueList", controller);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        replace: true,
        template: formulateDirectives.get(
            "dataValueKinds/dataValueList/dataValueList.html"),
        scope: {
            data: "="
        },
        controller: "formulate.dataValueList"
    };
}

// Controller.
function controller($scope) {
    if (!$scope.data.items) {
        $scope.data.items = [];
    }
    $scope.addItem = function () {
        $scope.data.items.push({
            value: null
        });
    };
    $scope.deleteItem = function (index) {
        $scope.data.items.splice(index, 1);
    };
    $scope.sortableOptions = {
        axis: "y",
        cursor: "move",
        delay: 100,
        opacity: 0.5
    };
}