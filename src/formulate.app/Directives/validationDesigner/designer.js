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
function controller($scope, $routeParams, navigationService,
    formulateValidations, $route) {

    // Variables.
    var id = $routeParams.id;
    var services = {
        $routeParams: $routeParams,
        navigationService: navigationService,
        formulateValidations: formulateValidations,
        $scope: $scope,
        $route: $route
    };

    // Set scope variables.
    $scope.validationId = id;
    $scope.validationName = null;
    $scope.validationAlias = null;
    $scope.kindId = null;
    $scope.parentId = null;

    // Set scope functions.
    $scope.save = getSaveValidation(services);
    $scope.canSave = getCanSave(services);

    // Initializes validation.
    initializeValidation({
        id: id
    }, services);

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
            alias: $scope.validationAlias,
            name: $scope.validationName
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
            activateInTree(validation, services);

            // Set the validation info.
            $scope.kindId = validation.kindId;
            $scope.validationId = validation.validationId;
            $scope.validationAlias = validation.alias;
            $scope.validationName = validation.name;
            $scope.validationPath = validation.path;

            // The validation can be saved now.
            $scope.initialized = true;

        });

}

//TODO: Move this function to a service.
// Shows/highlights the node in the Formulate tree.
function activateInTree(entity, services) {
    var options = {
        tree: "formulate",
        path: entity.path,
        forceReload: true,
        activate: true
    };
    services.navigationService.syncTree(options);
}

// Returns the function that indicates whether or not the validation can be saved.
function getCanSave(services) {
    return function() {
        return services.$scope.initialized;
    };
}