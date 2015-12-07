// Variables.
var app = angular.module("umbraco");

// Directive.
app.directive("formulateFormDesigner", function (formulateDirectives) {
    return {
        restrict: "E",
        template: formulateDirectives.get("formDesigner/designer.html")
    };
});

// Controller.
app.controller("formulate.formDesigner", function($scope) {
    $scope.save = function() {
        //TODO: ...
        alert("Saving...");
    };
});