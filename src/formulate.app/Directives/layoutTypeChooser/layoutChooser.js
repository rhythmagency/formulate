// Variables.
var app = angular.module("umbraco");

// Directive.
app.directive("formulateLayoutTypeChooser", function (formulateDirectives) {
    return {
        restrict: "E",
        template: formulateDirectives.get("layoutTypeChooser/layoutChooser.html")
    };
});

// Controller.
app.controller("formulate.layoutTypeChooser", function ($scope, formulatePermissions) {
    $scope.createLayout = function() {
        alert("Creating layout...");
    }
});