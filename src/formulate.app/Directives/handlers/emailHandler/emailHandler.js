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
function controller($scope, formulateFields) {
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

    formulateFields.getFieldCategories().then(function (categories) {
        $scope.categories = categories;
    });

    if (!$scope.configuration.FieldCategoryFilter) {
        $scope.configuration.FieldCategoryFilter = [];
    }

    // toggle selection for a given category
    $scope.toggleSelection = function toggleSelection(category) {
        var idx = $scope.configuration.FieldCategoryFilter.indexOf(category);

        // is currently selected else is not selected
        if (idx > -1) {
            $scope.configuration.FieldCategoryFilter.splice(idx, 1);
        }
        else {
            $scope.configuration.FieldCategoryFilter.push(category);
        }
    };
}