// Variables.
var app = angular.module("umbraco");

// Associate directive/controller.
app.directive("formulateEntityPickerTree", entityPickerDirective);
app.controller("formulate.entityPickerTree", entityPickerController);

// Directive.
function entityPickerDirective(formulateDirectives, formulateRecursion) {
    return {
        restrict: "E",
        replace: true,
        template: formulateDirectives.get("entityPicker/entityPickerTree.html"),
        controller: "formulate.entityPickerTree",
        controllerAs: "ctrl",
        scope: {
            nodes: "=",
            isRoot: "="
        },
        compile: function(element) {
            return formulateRecursion.getCompiler(element);
        }
    };
}

// Controller.
function entityPickerController($scope, formulateEntities) {

    $scope.toggleNode = function(node) {
        node.expanded = !node.expanded;
        var childCount = (node.children || []).length;
        if (node.expanded && node.hasChildren && childCount === 0) {
            formulateEntities.getEntityChildren(node.id).then(function(result) {
                node.children = result.children.map(function(item) {
                    return {
                        id: item.id,
                        name: item.name,
                        icon: item.icon,
                        children: item.children || [],
                        hasChildren: item.hasChildren,
                        expanded: false
                    };
                });
            });
        }
    };

}