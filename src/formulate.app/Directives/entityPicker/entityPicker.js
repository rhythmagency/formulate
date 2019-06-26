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
            previouslySelectedIds: "=?",
            wrongKindError: "=?"//TODO: Is the question mark correct?
        }
    };
}

// Controller.
function entityPickerController($scope, formulateEntities) {

    // Push any previously selected IDs into the current selection
    var ids = $scope.previouslySelectedIds || [];

    for (var i = 0; i < ids.length; i++) {
        $scope.selection.push(ids[i]);
    }

    // Include the root or skip to the children?
    if ($scope.includeRoot) {

        // Get root.
        formulateEntities.getEntity($scope.rootId).then(function (result) {
            $scope.rootNodes = [
                getViewModel(result)
            ];
        });

    } else {

        // Get children of root.
        formulateEntities.getEntityChildren($scope.rootId).then(function (result) {
            $scope.rootNodes = result.children.map(getViewModel);
        });

    }
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
        expanded: false
    };
}