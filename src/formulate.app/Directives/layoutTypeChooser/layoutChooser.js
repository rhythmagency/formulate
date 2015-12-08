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
    return function() {
        var layoutInfo = {
            layoutId: services.$scope.layout.id,
            layoutName: services.$scope.layoutName
        };
        addNodeToTree(layoutInfo, services)
            .then(function (node) {
                navigateToNode(node, services);
                UmbClientMgr.closeModalWindow();
            });
    };
}

// Adds a new node to the layout tree.
function addNodeToTree(layoutInfo, services) {
    return createNodeOnServer(layoutInfo, services)
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
function createNodeOnServer(layoutInfo, services) {

    // Variables.
    //TODO: Use server variables to get this URL.
    var url = "/umbraco/backoffice/formulate/Layouts/CreateLayout";
    var data = {
        LayoutId: layoutInfo.layoutId,
        LayoutName: layoutInfo.layoutName
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