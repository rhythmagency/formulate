// Variables.
var app = angular.module("umbraco");

// Associate directive.
app.directive("formulateRichTextField", directive);

// Controller.
function Controller($scope) {

    // Configure rich text editor model.
    $scope.textEditorModel = {
        editor: "Umbraco.TinyMCEv3",
        view: "rte",
        config: {
            editor: {
                toolbar: [
                    "code",
                    "removeformat",
                    "bold",
                    "italic",
                    "underline",
                    "strikethrough",
                    "alignleft",
                    "aligncenter",
                    "alignright",
                    "alignjustify",
                    "bullist",
                    "numlist",
                    "link",
                    "unlink",
                    "anchor",
                    "table",
                    "hr"
                ],
                dimensions: {
                    height: 250
                }
            }
        },
        value: $scope.configuration.text
    };

    // Update the text value when the rich text editor model changes.
    $scope.$watch("textEditorModel.value", function () {
        $scope.configuration.text = $scope.textEditorModel.value;
    });

}

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        controller: Controller,
        replace: true,
        template: formulateDirectives.get("fields/richTextField/richTextField.html")
    };
}