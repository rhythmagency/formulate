﻿// Variables.
var app = angular.module("umbraco");

// Register directive/controller.
app.controller("formulate.validationTypeChooser", controller);
app.directive("formulateValidationTypeChooser", directive);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        template: formulateDirectives.get(
            "validationTypeChooser/validationChooser.html"),
        controller: "formulate.validationTypeChooser"
    };
}

// Controller.
function controller($scope, $location, $q, $http, navigationService,
    formulateValidations, formulateVars) {

    // Variable containing the common services (easier to pass around).
    var services = {
        $scope: $scope,
        $location: $location,
        $q: $q,
        $http: $http,
        navigationService: navigationService,
        formulateValidations: formulateValidations,
        formulateVars: formulateVars
    };

    // Assign variables to scope.
    $scope.validationName = null;
    $scope.validationAlias = null;
    $scope.kind = {
        id: null,
        kinds: []
    };
    setValidationKinds(services);

    // Assign functions on scope.
    $scope.createValidation = getCreateValidation(services);
    $scope.canCreate = getCanCreate(services);

}

// Sets the validation kinds on the scope.
function setValidationKinds(services) {
    services.formulateValidations.getKinds().then(function (data) {
        services.$scope.kind.kinds = data.map(function (item) {
            return {
                id: item.id,
                name: item.name
            };
        });
    });
}

// Returns a function that creates a new validation.
function getCreateValidation(services) {
    var $scope = services.$scope;
    return function() {
        var parentId = $scope.currentNode.id;
        var validationInfo = {
            parentId: parentId,
            kindId: $scope.kind.id,
            name: $scope.validationName,
            alias: $scope.validationAlias,
            data: {}
        };
        addNodeToTree(validationInfo, services)
            .then(function (node) {
                navigateToNode(node, services);
                services.navigationService.hideDialog();
            });
    };
}

// Adds a new node to the validation tree.
function addNodeToTree(validationInfo, services) {
    return services.formulateValidations.persistValidation(validationInfo)
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
    var url = services.formulateVars.EditValidationBase + nodeId;
    services.$location.url(url);
}

// Returns a function that indicates whether or not the user can create.
function getCanCreate(services) {
    return function() {
        var validationName = services.$scope.validationName;
        var kindId = services.$scope.kind.id;
        //TODO: Check for whitespace and other edge cases that are invalid.
        if (validationName && kindId && validationName.length > 0) {
            return true;
        } else {
            return false;
        }
    };
}