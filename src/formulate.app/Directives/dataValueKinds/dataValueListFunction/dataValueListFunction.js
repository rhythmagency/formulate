// Variables.
var app = angular.module("umbraco");

// Associate directive/controller.
app.directive("formulateDataValueListFunction", directive);
app.controller("formulate.dataValueListFunction", controller);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        replace: true,
        template: formulateDirectives.get(
            "dataValueKinds/dataValueListFunction/dataValueListFunction.html"),
        scope: {
            data: "="
        },
        controller: "formulate.dataValueListFunction"
    };
}

// Controller.
function controller($scope, formulateDataValues) {
    if (!$scope.data.hasOwnProperty("supplier")) {
        $scope.data.supplier = null;
    }
    formulateDataValues.getSuppliers().then(function (suppliers) {
        $scope.suppliers = suppliers;
    });
}