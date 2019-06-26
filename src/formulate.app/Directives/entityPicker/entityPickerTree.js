// Variables.
var app = angular.module("umbraco");

// Associate directive/controller.
app.directive("formulateEntityPickerTree", entityPickerTreeDirective);
app.controller("formulate.entityPickerTree", entityPickerTreeController);

// Directive.
function entityPickerTreeDirective(formulateDirectives, formulateRecursion) {
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
            maxCount: "=",
            wrongKindError: "=?"
        },
        compile: function (element) {
            return formulateRecursion.getCompiler(element);
        }
    };
}

// Controller.
function entityPickerTreeController($scope, formulateEntities, notificationsService, localizationService) {

    // Variables.
    var services = {
        $scope: $scope,
        formulateEntities: formulateEntities,
        notificationsService: notificationsService
    };

    // Set scope functions.
    $scope.toggleChildren = getToggleChildren(services);
    $scope.toggleNode = getToggleNode(services);
    $scope.isSelected = isSelected(services);

    // Translations.
    localizationService.localize("formulate-errors_Node Selection Invalid").then(function (value) {
        $scope.defaultWrongKindError = value;
    });
    localizationService.localize("formulate-headers_Selection Failed").then(function (value) {
        $scope.selectionFailedHeader = value;
    });
}

// Gets the function that toggles selected nodes.
function getToggleNode(services) {
    var $scope = services.$scope;
    var notificationsService = services.notificationsService;
    return function (node) {
        var allowedKinds = $scope.entityKinds || [],
            allowAny = allowedKinds.length === 0;
        if (allowAny || allowedKinds.indexOf(node.kind) >= 0) {
            node.selected = !$scope.isSelected(node);
            if (node.selected) {
                // Select node.
                $scope.selection.push(node.id);

                // If max count is exceeded, deselect another node.
                if ($scope.maxCount && $scope.selection.length > $scope.maxCount) {
                    $scope.selection.shift();
                }

            } else {

                // Deselect node.
                var index = $scope.selection.indexOf(node.id);
                if (index >= 0) {
                    $scope.selection.splice(index, 1);
                }

            }
        } else if (!allowAny) {

            // Show notification to indicate that the user has selected the wrong kind of node.
            var errorMessage = $scope.wrongKindError || $scope.defaultWrongKindError;
            notificationsService.error($scope.selectionFailedHeader, errorMessage);

        }
    };
}

// Gets the function that toggles node children.
function getToggleChildren(services) {

    // Variables.
    var formulateEntities = services.formulateEntities;

    // Return function.
    return function (node) {

        // Variables.
        var childCount = (node.children || []).length;

        // Toggle.
        node.expanded = !node.expanded;

        // If just expanded and has unloaded children, load the children.
        if (node.expanded && node.hasChildren && childCount === 0) {
            formulateEntities.getEntityChildren(node.id).then(function (result) {
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
        expanded: false
    };
}

// is the node already selected
function isSelected(services) {
    var $scope = services.$scope;

    return function (item) {
        return $scope.selection.indexOf(item.id) > -1;
    }
}