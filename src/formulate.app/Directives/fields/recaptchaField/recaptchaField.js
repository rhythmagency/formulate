// Variables.
var app = angular.module("umbraco");

// Associate directive.
app.directive("formulateRecaptchaField", directive);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        replace: true,
        template: formulateDirectives.get("fields/recaptchaField/recaptchaField.html"),
        controller: controller
    };
}

// Controller.
function controller(formulateServerConfig, $scope) {
    $scope.hasConfiguredRecaptcha = true;
    formulateServerConfig.hasConfiguredRecaptcha().then(function(hasConfigured) {
        $scope.hasConfiguredRecaptcha = hasConfigured;
    });
}