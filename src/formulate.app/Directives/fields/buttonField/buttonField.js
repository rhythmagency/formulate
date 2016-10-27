// Variables.
var app = angular.module("umbraco");

// Associate directive.
app.directive("formulateButtonField", directive);

// Controller.
function Controller(formulateFields, $scope) {

    // Variables.
    $scope.kinds = {
        values: []
    };

    // Get the button kinds.
    formulateFields.getButtonKinds().then(function (kinds) {
        $scope.kinds.values = kinds.map(function (kind) {
            return {
                label: kind,
                value: kind
            };
        });
    });

}

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        replace: true,
        controller: Controller,
        template: formulateDirectives.get("fields/buttonField/buttonField.html")
    };
}