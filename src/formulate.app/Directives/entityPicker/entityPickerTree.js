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

    initSelectedNodesIntoSelection(services);

    // Set scope functions.
    $scope.toggleChildren = getToggleChildren(services);
    $scope.toggleNode = getToggleNode(services);

    // Translations.
    localizationService.localize("formulate-errors_Node Selection Invalid").then(function (value) {
        $scope.defaultWrongKindError = value;
    });
    localizationService.localize("formulate-headers_Selection Failed").then(function (value) {
        $scope.selectionFailedHeader = value;
    });
}

// Loads the selected nodes into the selection variable. This should only be used to initialize the $scope.selection.
function initSelectedNodesIntoSelection(services) {
    var $scope = services.$scope;
    var nodes = $scope.nodes;

    for (var i = 0; i < nodes.length; i++) {
        var node = nodes[i];

        if (node.selected) {
            $scope.selection.push(node);
        }
    }
};

// Gets the function that toggles selected nodes.
function getToggleNode(services) {
    var $scope = services.$scope;
    var notificationsService = services.notificationsService;
    return function (node) {
        var allowedKinds = $scope.entityKinds || [],
            allowAny = allowedKinds.length === 0;
        if (allowAny || allowedKinds.indexOf(node.kind) >= 0) {
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
        expanded: false,
        selected: false
    };
}