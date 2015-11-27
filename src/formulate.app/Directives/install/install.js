// Variables.
var app = angular.module("umbraco");

// Directive.
app.directive("formulateInstall", function (formulateDirectives) {
    return {
        restrict: "E",
        template: formulateDirectives.get("install/install.html")
    };
});

// Controller.
app.controller("formulate.install", function($scope, formulatePermissions) {
    $scope.givePermission = function() {
        formulatePermissions.permitFormulateAccess();
    };
});