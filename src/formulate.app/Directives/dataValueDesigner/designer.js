//TODO: Disable buttons during data value save.
// Variables.
var app = angular.module("umbraco");

// Associate directive/controller.
app.directive("formulateDataValueDesigner", directive);
app.controller("formulate.dataValueDesigner", controller);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        template: formulateDirectives.get("dataValueDesigner/designer.html"),
        controller: "formulate.dataValueDesigner"
    };
}

// Controller.
function controller($scope, $routeParams, $route, formulateTrees,
    formulateDataValues) {

    // Variables.
    var id = $routeParams.id;
    var services = {
        $routeParams: $routeParams,
        formulateTrees: formulateTrees,
        formulateDataValues: formulateDataValues,
        $scope: $scope,
        $route: $route
    };

    // Set scope variables.
    $scope.dataValueId = id;
    $scope.info = {
        dataValueName: null,
        dataValueAlias: null
    };

    $scope.kindId = null;
    $scope.parentId = null;
    $scope.directive = null;
    $scope.data = null;
    
    // Set scope functions.
    $scope.save = getSaveDataValue(services);
    $scope.canSave = getCanSave(services);

    // Initializes data value.
    initializeDataValue({
        id: id
    }, services);

    // Handle events.
    handleDataValueMoves(services);

}

// Handles updating a data value when it's moved.
function handleDataValueMoves(services) {
    var $scope = services.$scope;
    $scope.$on("formulateEntityMoved", function(event, data) {
        var id = data.id;
        var newPath = data.path;
        if ($scope.dataValueId === id) {

            // Store new path.
            $scope.dataValuePath = newPath;

            // Activate in tree.
            services.formulateTrees.activateEntity(data);

        }
    });
}

// Saves the data value.
function getSaveDataValue(services) {
    return function () {

        // Variables.
        var $scope = services.$scope;
        var parentId = getParentId($scope);

        // Get data value data.
        var dataValueData = {
            parentId: parentId,
            kindId: $scope.kindId,
            dataValueId: $scope.dataValueId,
            alias: $scope.info.dataValueAlias,
            name: $scope.info.dataValueName,
            data: angular.fromJson(angular.toJson($scope.data))
        };

        // Persist data value on server.
        services.formulateDataValues.persistDataValue(dataValueData)
            .then(function(responseData) {

                // Data value is no longer new.
                var isNew = $scope.isNew;
                $scope.isNew = false;

                // Redirect or reload page.
                if (isNew) {
                    var url = "/formulate/formulate/editDataValue/"
                        + responseData.dataValueId;
                    services.$location.url(url);
                } else {

                    // Even existing data values reload (e.g., to get new data).
                    services.$route.reload();

                }

            });

    };
}

// Gets the ID path to the data value.
function getDataValuePath($scope) {
    var path = $scope.dataValuePath;
    if (!path) {
        path = [];
    }
    return path;
}

// Gets the ID of the data value's parent.
function getParentId($scope) {
    if ($scope.parentId) {
        return $scope.parentId;
    }
    var path = getDataValuePath($scope);
    var parentId = path.length > 0
        ? path[path.length - 2]
        : null;
    return parentId;
}

// Initializes the data value.
function initializeDataValue(options, services) {

    // Variables.
    var id = options.id;
    var $scope = services.$scope;

    // Disable data value saving until the data is populated.
    $scope.initialized = false;

    // Get the data value info.
    services.formulateDataValues.getDataValueInfo(id)
        .then(function(dataValue) {

            // Update tree.
            services.formulateTrees.activateEntity(dataValue);

            // Set the dataValue info.
            $scope.kindId = dataValue.kindId;
            $scope.dataValueId = dataValue.dataValueId;
            $scope.info.dataValueAlias = dataValue.alias;
            $scope.info.dataValueName = dataValue.name;
            $scope.dataValuePath = dataValue.path;
            $scope.directive = dataValue.directive;
            $scope.data = dataValue.data;

            // The data value can be saved now.
            $scope.initialized = true;

        });

}

// Returns the function that indicates whether or not the data value
// can be saved.
function getCanSave(services) {
    return function() {
        return services.$scope.initialized;
    };
}