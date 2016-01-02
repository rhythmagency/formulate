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
            isRoot: "=",
            entityKinds: "=",
            selection: "=",
            maxCount: "="
        },
        compile: function(element) {
            return formulateRecursion.getCompiler(element);
        }
    };
}

// Controller.
function entityPickerController($scope, formulateEntities) {

    // Variables.
    var services = {
        $scope: $scope,
        formulateEntities: formulateEntities
    };

    // Set scope functions.
    $scope.toggleChildren = getToggleChildren(services);
    $scope.toggleNode = getToggleNode(services);

}

// Gets the function that toggles selected nodes.
function getToggleNode(services) {
    var $scope = services.$scope;
    return function(node) {
        var allowedKinds = $scope.entityKinds || [];
        if (allowedKinds.length === 0 || allowedKinds.indexOf(node.kind) >= 0) {
            node.selected = !node.selected;
            if (node.selected) {

                // Select node.
                $scope.selection.push(node);

                // If max count is exceeded, deselect another node.
                if ($scope.maxCount && $scope.selection.length > $scope.maxCount) {
                    var oldNode = $scope.selection.splice(0, 1)[0];
                    oldNode.selected = false;
                }

            } else {

                // Deselect node.
                var index = $scope.selection.indexOf(node);
                if (index >= 0) {
                    $scope.selection.splice(index, 1);
                }

            }
        }
    };
}

// Gets the function that toggles node children.
function getToggleChildren(services) {

    // Variables.
    var formulateEntities = services.formulateEntities;

    // Return function.
    return function(node) {

        // Variables.
        var childCount = (node.children || []).length;

        // Toggle.
        node.expanded = !node.expanded;

        // If just expanded and has unloaded children, load the children.
        if (node.expanded && node.hasChildren && childCount === 0) {
            formulateEntities.getEntityChildren(node.id).then(function(result) {
                node.children = result.children.map(getViewModel);
            });
        }

    };

}

// Gets the view model from a node.
function getViewModel(item) {
    return {
        id: item.id,
        name: item.name,
        icon: item.icon,
        kind: item.kind,
        children: item.children || [],
        hasChildren: item.hasChildren,
        expanded: false,
        selected: false
    };
}