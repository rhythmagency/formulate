// Variables.
var app = angular.module("umbraco");

// Associate directive/controller.
app.directive("formulateEmailHandler", directive);
app.controller("formulate.emailHandler", controller);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        replace: true,
        template: formulateDirectives.get(
            "handlers/emailHandler/emailHandler.html"),
        scope: {
            configuration: "=",
            fields: "="
        },
        controller: "formulate.emailHandler"
    };
}

// Controller.
function controller($scope) {
    if (!$scope.configuration.recipients) {
        $scope.configuration.recipients = [];
    }
    $scope.addRecipient = function () {
        $scope.configuration.recipients.push({
            email: ""
        });
    };
    $scope.deleteRecipient = function (index) {
        $scope.configuration.recipients.splice(index, 1);
    };
}