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
    $http, navigationService) {

    // Variable containing the common services (easier to pass around).
    var services = {
        $scope: $scope,
        $location: $location,
        notificationsService: notificationsService,
        $q: $q,
        $http: $http,
        navigationService: navigationService
    };

    // Assign variables to scope.
    $scope.layout = {
        id: null,
        layouts: [
            //TODO: These should come from the server.
            { id: "0", name: "Layout One" },
            { id: "1", name: "Layout Two" },
            { id: "2", name: "Layout Three" }
        ]
    };

    // Assign functions on scope.
    $scope.createLayout = getCreateLayout(services);

}

// Returns a function that creates a new layout.
function getCreateLayout(services) {
    return function() {
        addNodeToTree(services.$scope.layout.id, services)
            .then(function (node) {
                navigateToNode(node, services);
                UmbClientMgr.closeModalWindow();
            });
    };
}

// Adds a new node to the layout tree.
function addNodeToTree(layoutId, services) {
    return createNodeOnServer(layoutId, services)
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

// Creates the layout node on the server.
function createNodeOnServer(layoutId, services) {

    // Variables.
    //TODO: Use server variables to get this URL.
    var url = "/umbraco/backoffice/formulate/Layouts/CreateLayout";
    var data = {
        LayoutId: layoutId
    };
    var strData = JSON.stringify(data);
    var options = {
        headers: {
            "Content-Type": "application/json"
        }
    };

    // Send request to create the node.
    return services.$http.post(url, strData, options)
        .then(function (response) {

            // Variables.
            var data = response.data;

            // Was the request successful?
            if (data.Success) {
                return {
                    id: data.Id,
                    path: data.Path
                };
            } else {
                var title = "Unexpected Error";
                var message = data.Reason;
                services.notificationsService.error(title, message);
                return services.$q.reject();
            }

        }, function () {

            // An error occurred.
            var title = "Server Error";
            var message = "There was an issue communicating with the server.";
            services.notificationsService.error(title, message);
            services.$q.reject();

        });

}