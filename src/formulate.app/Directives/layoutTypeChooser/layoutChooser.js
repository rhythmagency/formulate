// Variables.
var app = angular.module("umbraco");

// Register directive/controller.
app.controller("formulate.layoutTypeChooser", controller);
app.directive("formulateLayoutTypeChooser", directive);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        template: formulateDirectives.get("layoutTypeChooser/layoutChooser.html")
    };
}

// Controller.
function controller($scope, $location, notificationsService, $q,
    $http, navigationService, formulateLayouts) {

    // Variable containing the common services (easier to pass around).
    var services = {
        $scope: $scope,
        $location: $location,
        notificationsService: notificationsService,
        $q: $q,
        $http: $http,
        navigationService: navigationService,
        formulateLayouts: formulateLayouts
    };

    // Assign variables to scope.
    $scope.layoutName = null;
    $scope.layout = {
        id: null,
        layouts: [
            //TODO: These should come from the server.
            { id: "19F0F9282CCE40229BA2610AC776A97E", name: "Layout One" },
            { id: "D6CC7AE0C9D94D51B3F3101FA27A0ACA", name: "Layout Two" },
            { id: "363614C2987545E19A515BF3A0F04CD4", name: "Layout Three" }
        ]
    };

    // Assign functions on scope.
    $scope.createLayout = getCreateLayout(services);
    $scope.canCreate = getCanCreate(services);

}

// Returns a function that creates a new layout.
function getCreateLayout(services) {
    var $scope = services.$scope;
    return function() {
        var parentId = $scope.currentNode.id;
        var layoutInfo = {
            layoutId: $scope.layout.id,
            layoutName: $scope.layoutName,
            layoutAlias: $scope.layoutAlias,
            parentId: parentId
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
    //TODO: Use server variables to get this URL.
    var url = "/formulate/formulate/editLayout/" + nodeId;
    services.$location.url(url);
}

//TODO: Comment.
function getCanCreate(services) {
    return function() {
        var layoutName = services.$scope.layoutName;
        var layoutId = services.$scope.layout.id;
        //TODO: Check for whitespace and other edge cases that are invalid.
        if (layoutName && layoutId && layoutName.length > 0) {
            return true;
        } else {
            return false;
        }
    };
}