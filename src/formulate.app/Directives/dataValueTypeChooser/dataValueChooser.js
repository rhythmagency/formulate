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
function Controller($scope, $location, $q, $http,
    navigationService, formulateDataValues, formulateVars) {

    // Variable containing the common services (easier to pass around).
    var services = {
        $scope: $scope,
        $location: $location,
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
        kinds: []
    };
    setDataValueKinds(services);

    // Assign functions on scope.
    $scope.createDataValue = getCreateDataValue(services);
    $scope.canCreate = getCanCreate(services);

}

// Sets the data value kinds on the scope.
function setDataValueKinds(services) {
    services.formulateDataValues.getKinds().then(function (data) {
        services.$scope.kind.kinds = data.map(function (item) {
            return {
                id: item.id,
                name: item.name
            };
        });
    });
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
            alias: $scope.dataValueAlias,
            data: {}
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
        if (dataValueName && kindId && dataValueName.length > 0) {
            return true;
        } else {
            return false;
        }
    };
}