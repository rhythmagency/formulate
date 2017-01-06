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
    $scope.tempData = {
        recipientEmailField: null
    };
    if (!$scope.configuration.recipients) {
        $scope.configuration.recipients = [];
    }
    if (!$scope.configuration.recipientFields) {
        $scope.configuration.recipientFields = [];
    }
    $scope.addRecipient = function () {
        $scope.configuration.recipients.push({
            email: ""
        });
    };
    $scope.deleteRecipient = function (index) {
        $scope.configuration.recipients.splice(index, 1);
    };
    $scope.addRecipientField = function () {
        var id = $scope.tempData.recipientEmailField;
        if (!id) {
            return;
        }
        var foundField = null;
        for (var i = 0; i < $scope.fields.length; i++) {
            var field = $scope.fields[i];
            if (field.id === id) {
                foundField = field;
                break;
            }
        }
        if (!foundField) {
            return;
        }
        $scope.configuration.recipientFields.push(foundField);
    };
    $scope.deleteRecipientField = function (index) {
        $scope.configuration.recipientFields.splice(index, 1);
    };
}