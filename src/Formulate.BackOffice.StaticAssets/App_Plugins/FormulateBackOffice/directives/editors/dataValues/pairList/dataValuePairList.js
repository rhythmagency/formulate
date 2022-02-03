// Variables.
var app = angular.module("umbraco");

// Associate directive/controller.
app.directive("formulateDataValuePairList", directive);
app.controller("formulate.dataValuePairList", controller);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        replace: true,
        template: formulateDirectives.get(
            "dataValueKinds/dataValuePairList/dataValuePairList.html"),
        scope: {
            data: "="
        },
        controller: "formulate.dataValuePairList"
    };
}

// Controller.
function controller($scope) {
    if (!$scope.data.items) {
        $scope.data.items = [];
    }
    $scope.addItem = function () {
        $scope.data.items.push({
            primary: null,
            secondary: null
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