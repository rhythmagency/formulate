(function () {

    // Variables.
    var app = angular.module("umbraco");

    // Directive.
    function directive() {
        return {
            restrict: "E",
            replace: true,
            templateUrl: "/app_plugins/formulate/editors/form-fields/radio-button-list/radio-button-list-field.editor.html",
            controller: Controller,
            scope: {
                configuration: "="
            }
        };
    }
    app.directive("formulateRadioButtonListField", directive);

    // Controller.
    function Controller($scope) {

        // Set scope variables.
        $scope.orientations = {
            values: [
                {
                    value: "Horizontal",
                    label: "Horizontal"
                },
                {
                    value: "Vertical",
                    label: "Vertical"
                }
            ]
        };
    }
})();
