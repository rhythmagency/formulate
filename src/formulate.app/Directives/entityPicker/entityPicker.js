// Variables.
var app = angular.module("umbraco");

// Associate directive/controller.
app.directive("formulateEntityPicker", entityPickerDirective);
app.controller("formulate.entityPicker", entityPickerController);

// Directive.
function entityPickerDirective(formulateDirectives) {
    return {
        restrict: "E",
        template: formulateDirectives.get("entityPicker/entityPicker.html"),
        controller: "formulate.entityPicker",
        controllerAs: "ctrl",
        scope: {
            entityKinds: "=",
            rootId: "=",
            maxCount: "=",
            includeRoot: "=",
            selection: "=",
            previouslySelectedIds: "=",
            wrongKindError: "=?"//TODO: Is the question mark correct?
        }
    };
}

// Controller.
function entityPickerController($scope, formulateEntities) {

    // Set scope variables.
    $scope.selectedNodes = [];

    // Include the root or skip to the children?
    if ($scope.includeRoot) {

        // Get root.
        formulateEntities.getEntity($scope.rootId).then(function (result) {
            $scope.rootNodes = [
                getViewModel(result, $scope.previouslySelectedIds)
            ];
        });

    } else {

        // Get children of root.
        formulateEntities.getEntityChildren($scope.rootId).then(function (result) {
            $scope.rootNodes = result.children.map(function(c) { return getViewModel(c, $scope.previouslySelectedIds); });
        });

    }

    // Watch selected nodes and update selection.
    $scope.$watchCollection("selectedNodes", function (newValue) {
        $scope.selection.splice(0, $scope.selection.length);
        for (var i = 0; i < newValue.length; i++) {
            var id = newValue[i].id;
            $scope.selection.push(id);
        }
    });
}

// Gets the view model from a node.
function getViewModel(item, previouslySelectedIds) {
    return {
        id: item.id,
        name: item.name,
        icon: item.icon,
        kind: item.kind,
        children: item.children || [],
        hasChildren: item.hasChildren,
        expanded: false,
        selected: isSelected(item.id, previouslySelectedIds)
    };
}

// is the node already selected
function isSelected(id, previouslySelectedIds) {
    return previouslySelectedIds.indexOf(id) > -1;
}