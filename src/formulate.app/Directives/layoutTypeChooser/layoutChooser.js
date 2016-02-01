// Variables.
var app = angular.module("umbraco");

// Register directive/controller.
app.controller("formulate.layoutTypeChooser", controller);
app.directive("formulateLayoutTypeChooser", directive);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        template: formulateDirectives.get("layoutTypeChooser/layoutChooser.html"),
        controller: "formulate.layoutTypeChooser"
    };
}

// Controller.
function controller($scope, $location, $q, $http,
    navigationService, formulateLayouts, formulateVars) {

    // Variable containing the common services (easier to pass around).
    var services = {
        $scope: $scope,
        $location: $location,
        $q: $q,
        $http: $http,
        navigationService: navigationService,
        formulateLayouts: formulateLayouts,
        formulateVars: formulateVars
    };

    // Assign variables to scope.
    $scope.layoutName = null;
    $scope.layoutAlias = null;
    $scope.kind = {
        id: null,
        kinds: []
    };
    setLayoutKinds(services);

    // Assign functions on scope.
    $scope.createLayout = getCreateLayout(services);
    $scope.canCreate = getCanCreate(services);

}

// Sets the layout kinds on the scope.
function setLayoutKinds(services) {
    services.formulateLayouts.getKinds().then(function (data) {
        services.$scope.kind.kinds = data.map(function (item) {
            return {
                id: item.id,
                name: item.name
            };
        });
    });
}

// Returns a function that creates a new layout.
function getCreateLayout(services) {
    var $scope = services.$scope;
    return function() {
        var parentId = $scope.currentNode.id;
        var layoutInfo = {
            parentId: parentId,
            kindId: $scope.kind.id,
            name: $scope.layoutName,
            alias: $scope.layoutAlias,
            data: {}
        };
        addNodeToTree(layoutInfo, services)
            .then(function (node) {
                navigateToNode(node, services);
                services.navigationService.hideDialog();
            });
    };
}

// Adds a new node to the layout tree.
function addNodeToTree(layoutInfo, services) {
    return services.formulateLayouts.persistLayout(layoutInfo)
        .then(function (node) {

            // Refresh tree.
            var options = {
                tree: "formulate",
                path: node.path,
                forceReload: false,
                activate: true
            };
            services.navigationService.syncTree(options);

            // Return node.
            return node;

        },function () {
            services.$q.reject();
        });
}

// Navigates to a node.
function navigateToNode(node, services) {
    var nodeId = node.id;
    var url = services.formulateVars.EditLayoutBase + nodeId;
    services.$location.url(url);
}

// Returns a function that indicates whether or not the user can create.
function getCanCreate(services) {
    return function() {
        var layoutName = services.$scope.layoutName;
        var kindId = services.$scope.kind.id;
        //TODO: Check for whitespace and other edge cases that are invalid.
        if (layoutName && kindId && layoutName.length > 0) {
            return true;
        } else {
            return false;
        }
    };
}