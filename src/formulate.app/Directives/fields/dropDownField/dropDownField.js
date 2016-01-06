// Variables.
var app = angular.module("umbraco");

// Associate directive/controller.
app.directive("formulateDropDownField", DropDownFieldDirective);

// Directive.
function DropDownFieldDirective(formulateDirectives) {
    return {
        restrict: "E",
        replace: true,
        template: formulateDirectives.get("fields/dropDownField/dropDownField.html")
    };
}