// Variables.
var app = angular.module("umbraco");

// Associate directive/controller.
app.directive("formulateEntityPicker", entityPickerDirective);
app.controller("formulate.entityPicker", entityPickerController);

// Directive.
function entityPickerDirective(formulateDirectives, $compile) {
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
            chosen: "&"
        }
    };
}

// Controller.
function entityPickerController($scope, $routeParams, navigationService,
    formulateForms, $location, $route, $element, formulateEntities) {

    if ($scope.includeRoot) {

        // Get root.
        formulateEntities.getEntity($scope.rootId).then(function(result) {
            $scope.rootNodes = [
                {
                    id: result.id,
                    name: result.name,
                    icon: result.icon,
                    children: result.children || [],
                    hasChildren: result.hasChildren,
                    expanded: false
                }
            ];
        });

    } else {

        // Get children of root.
        formulateEntities.getEntityChildren($scope.rootId).then(function(result) {
            $scope.rootNodes = result.children.map(function(item) {
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
}