// Variables.
var app = angular.module("umbraco");

// Register directive/controller.
app.controller("formulate.dataValueTypeChooser", Controller);
app.directive("formulateDataValueTypeChooser", Directive);

// Directive.
function Directive(formulateDirectives) {
    return {
        restrict: "E",
        template: formulateDirectives.get("dataValueTypeChooser/dataValueChooser.html"),
        controller: "formulate.dataValueTypeChooser"
    };
}

// Controller.
function Controller($scope, $location, notificationsService, $q,
    $http, navigationService, formulateDataValues, formulateVars) {

    // Variable containing the common services (easier to pass around).
    var services = {
        $scope: $scope,
        $location: $location,
        notificationsService: notificationsService,
        $q: $q,
        $http: $http,
        navigationService: navigationService,
        formulateDataValues: formulateDataValues,
        formulateVars: formulateVars
    };

    // Assign variables to scope.
    $scope.dataValueName = null;
    $scope.dataValueAlias = null;
    $scope.kind = {
        id: null,
        kinds: [
            //TODO: These should come from the server.
            { id: "C43A94A018A5418ABF8A6FA7DEE6D2DE", name: "Data Value One" },
            { id: "AB2DE75A582245F4903211A224E2905A", name: "Data Value Two" },
            { id: "30B3961F4FA44707A1D196C4D3CEE1C9", name: "Data Value Three" }
        ]
    };

    // Assign functions on scope.
    $scope.createDataValue = getCreateDataValue(services);
    $scope.canCreate = getCanCreate(services);

}

// Returns a function that creates a new data value.
function getCreateDataValue(services) {
    var $scope = services.$scope;
    return function() {
        var parentId = $scope.currentNode.id;
        var dataValueInfo = {
            parentId: parentId,
            kindId: $scope.kind.id,
            name: $scope.dataValueName,
            alias: $scope.dataValueAlias
        };
        addNodeToTree(dataValueInfo, services)
            .then(function (node) {
                navigateToNode(node, services);
                services.navigationService.hideDialog();
            });
    };
}

// Adds a new node to the data value tree.
function addNodeToTree(dataValueInfo, services) {
    return services.formulateDataValues.persistDataValue(dataValueInfo)
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
    var url = services.formulateVars.EditDataValueBase + nodeId;
    services.$location.url(url);
}

// Returns a function that indicates whether or not the user can create.
function getCanCreate(services) {
    return function() {
        var dataValueName = services.$scope.dataValueName;
        var kindId = services.$scope.kind.id;
        //TODO: Check for whitespace and other edge cases that are invalid.
        if (dataValueName && kindId && dataValueName.length > 0) {
            return true;
        } else {
            return false;
        }
    };
}