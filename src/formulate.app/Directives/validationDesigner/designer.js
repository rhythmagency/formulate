//TODO: Disable buttons during validation save.
// Variables.
var app = angular.module("umbraco");

// Associate directive/controller.
app.directive("formulateValidationDesigner", directive);
app.controller("formulate.validationDesigner", controller);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        template: formulateDirectives.get("validationDesigner/designer.html"),
        controller: "formulate.validationDesigner"
    };
}

// Controller.
function controller($scope, $routeParams, $route, formulateValidations,
    formulateTrees, formulateLocalization) {

    // Variables.
    var id = $routeParams.id;
    var services = {
        formulateValidations: formulateValidations,
        $scope: $scope,
        $route: $route,
        formulateTrees: formulateTrees
    };

    // Set scope variables.
    $scope.validationId = id;
    $scope.info = {
        validationName: null,
        validationAlias: null
    };
    $scope.kindId = null;
    $scope.parentId = null;
    $scope.directive = null;
    $scope.data = null;

    // Set scope functions.
    $scope.save = getSaveValidation(services);
    $scope.canSave = getCanSave(services);

    // Initializes validation.
    initializeValidation({
        id: id
    }, services);

    // Handle events.
    handleValidationMoves(services);

}

// Handles updating a validation when it's moved.
function handleValidationMoves(services) {
    var $scope = services.$scope;
    $scope.$on("formulateEntityMoved", function(event, data) {
        var id = data.id;
        var newPath = data.path;
        if ($scope.validationId === id) {

            // Store new path.
            $scope.validationPath = newPath;

            // Activate in tree.
            services.formulateTrees.activateEntity(data);

        }
    });
}

// Saves the validation.
function getSaveValidation(services) {
    return function () {

        // Variables.
        var $scope = services.$scope;
        var parentId = getParentId($scope);

        // Get validation data.
        var validationData = {
            parentId: parentId,
            kindId: $scope.kindId,
            validationId: $scope.validationId,
            alias: $scope.info.validationAlias,
            name: $scope.info.validationName,
            data: angular.fromJson(angular.toJson($scope.data))
        };

        // Persist validation on server.
        services.formulateValidations.persistValidation(validationData)
            .then(function(responseData) {

                // Validation is no longer new.
                var isNew = $scope.isNew;
                $scope.isNew = false;

                // Redirect or reload page.
                if (isNew) {
                    var url = "/formulate/formulate/editValidation/"
                        + responseData.validationId;
                    services.$location.url(url);
                } else {

                    // Even existing validations reload (e.g., to get new data).
                    services.$route.reload();

                }

            });

    };
}

// Gets the ID path to the validation.
function getValidationPath($scope) {
    var path = $scope.validationPath;
    if (!path) {
        path = [];
    }
    return path;
}

// Gets the ID of the validation's parent.
function getParentId($scope) {
    if ($scope.parentId) {
        return $scope.parentId;
    }
    var path = getValidationPath($scope);
    var parentId = path.length > 0
        ? path[path.length - 2]
        : null;
    return parentId;
}

// Initializes the validation.
function initializeValidation(options, services) {

    // Variables.
    var id = options.id;
    var $scope = services.$scope;

    // Disable validation saving until the data is populated.
    $scope.initialized = false;

    // Get the validation info.
    services.formulateValidations.getValidationInfo(id)
        .then(function(validation) {

            // Update tree.
            services.formulateTrees.activateEntity(validation);

            // Set the validation info.
            $scope.kindId = validation.kindId;
            $scope.validationId = validation.validationId;
            $scope.info.validationAlias = validation.alias;
            $scope.info.validationName = validation.name;
            $scope.validationPath = validation.path;
            $scope.directive = validation.directive;
            $scope.data = validation.data;

            // The validation can be saved now.
            $scope.initialized = true;

        });

}

// Returns the function that indicates whether or not the validation can be saved.
function getCanSave(services) {
    return function() {
        return services.$scope.initialized;
    };
}