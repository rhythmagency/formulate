//TODO: Disable buttons during layout save.
// Variables.
var app = angular.module("umbraco");

// Associate directive/controller.
app.directive("formulateLayoutDesigner", directive);
app.controller("formulate.layoutDesigner", controller);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        template: formulateDirectives.get("layoutDesigner/designer.html"),
        controller: "formulate.layoutDesigner"
    };
}

// Controller.
function controller($scope, $routeParams, $route, formulateTrees,
    formulateLayouts) {

    // Variables.
    var id = $routeParams.id;
    var services = {
        formulateTrees: formulateTrees,
        formulateLayouts: formulateLayouts,
        $scope: $scope,
        $route: $route
    };

    // Set scope variables.
    $scope.layoutId = id;
    $scope.info = {
        layoutName: null,
        layoutAlias: null
    };

    $scope.kindId = null;
    $scope.parentId = null;
    $scope.directive = null;
    $scope.data = null;

    // Set scope functions.
    $scope.save = getSaveLayout(services);
    $scope.canSave = getCanSave(services);

    // Initializes layout.
    initializeLayout({
        id: id
    }, services);

    // Handle events.
    handleLayoutMoves(services);

}

// Handles updating a layout when it's moved.
function handleLayoutMoves(services) {
    var $scope = services.$scope;
    $scope.$on("formulateEntityMoved", function(event, data) {
        var id = data.id;
        var newPath = data.path;
        if ($scope.layoutId === id) {

            // Store new path.
            $scope.layoutPath = newPath;

            // Activate in tree.
            services.formulateTrees.activateEntity(data);

        }
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
            kindId: $scope.kindId,
            layoutId: $scope.layoutId,
            alias: $scope.info.layoutAlias,
            name: $scope.info.layoutName,
            data: $scope.data
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

// Initializes the layout.
function initializeLayout(options, services) {

    // Variables.
    var id = options.id;
    var $scope = services.$scope;

    // Disable layout saving until the data is populated.
    $scope.initialized = false;

    // Get the layout info.
    services.formulateLayouts.getLayoutInfo(id)
        .then(function(layout) {

            // Update tree.
            services.formulateTrees.activateEntity(layout);

            // Set the layout info.
            $scope.kindId = layout.kindId;
            $scope.layoutId = layout.layoutId;
            $scope.info.layoutAlias = layout.alias;
            $scope.info.layoutName = layout.name;
            $scope.layoutPath = layout.path;
            $scope.directive = layout.directive;
            $scope.data = layout.data;

            // The layout can be saved now.
            $scope.initialized = true;

        });

}

// Returns the function that indicates whether or not the layout can be saved.
function getCanSave(services) {
    return function() {
        return services.$scope.initialized;
    };
}