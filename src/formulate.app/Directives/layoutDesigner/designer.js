//TODO: Disable buttons during layout save.
// Variables.
var app = angular.module("umbraco");

// Associate directive/controller.
app.directive("formulateLayoutDesigner", LayoutDesignerDirective);
app.controller("formulate.layoutDesigner", LayoutDesignerController);

// Directive.
function LayoutDesignerDirective(formulateDirectives) {
    return {
        restrict: "E",
        template: formulateDirectives.get("layoutDesigner/designer.html")
    };
}

// Controller.
function LayoutDesignerController($scope, $routeParams, navigationService,
    formulateLayouts, $route) {

    // Variables.
    var id = $routeParams.id;
    var services = {
        $routeParams: $routeParams,
        navigationService: navigationService,
        formulateLayouts: formulateLayouts,
        $scope: $scope,
        $route: $route
    };

    // Initialize.
    activateInTree(id, services);

    // Set scope variables.
    $scope.layoutId = id;

    // Set scope functions.
    $scope.save = getSaveLayout(services);

}

// Shows/highlights the node in the Formulate tree.
function activateInTree(id, services) {

    // Get path from server.
    services.formulateLayouts.getLayoutInfo(id)
        .then(function(layout) {
            var options = {
                tree: "formulate",
                path: layout.path,
                forceReload: true,
                activate: true
            };
            services.navigationService.syncTree(options);
        });

}

// Saves the layout.
function getSaveLayout(services) {
    return function () {

        // Variables.
        var $scope = services.$scope;
        var parentId = getParentId($scope);

        // Get layout data.
        var layoutData = {
            parentId: parentId,
            layoutId: $scope.layoutId,
            alias: $scope.layoutAlias,
            name: $scope.layoutName
        };

        // Persist layout on server.
        services.formulateLayouts.persistLayout(layoutData)
            .then(function(responseData) {

                // Layout is no longer new.
                var isNew = $scope.isNew;
                $scope.isNew = false;

                // Redirect or reload page.
                if (isNew) {
                    var url = "/formulate/formulate/editLayout/"
                        + responseData.layoutId;
                    services.$location.url(url);
                } else {

                    // Even existing layouts reload (e.g., to get new data).
                    services.$route.reload();

                }

            });

    };
}

// Gets the ID path to the layout.
function getLayoutPath($scope) {
    var path = $scope.layoutPath;
    if (!path) {
        path = [];
    }
    return path;
}

// Gets the ID of the layout's parent.
function getParentId($scope) {
    if ($scope.parentId) {
        return $scope.parentId;
    }
    var path = getLayoutPath($scope);
    var parentId = path.length > 0
        ? path[path.length - 2]
        : null;
    return parentId;
}