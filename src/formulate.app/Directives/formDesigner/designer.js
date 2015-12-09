// Variables.
var app = angular.module("umbraco");

// Associate directive/controller.
app.directive("formulateFormDesigner", FormDesignerDirective);
app.controller("formulate.formDesigner", FormDesignerController);

// Directive.
function FormDesignerDirective(formulateDirectives) {
    return {
        restrict: "E",
        template: formulateDirectives.get("formDesigner/designer.html")
    };
}

// Controller.
function FormDesignerController($scope, $routeParams, navigationService, formulateForms) {

    // Variables.
    var id = $routeParams.id;
    var isNew = id === "null";
    var services = {
        $scope: $scope,
        $routeParams: $routeParams,
        navigationService: navigationService,
        formulateForms: formulateForms
    };

    // Set scope variables.
    $scope.isNew = isNew;
    $scope.fields = [];

    // Set scope functions.
    $scope.save = getSaveForm({
        id: id
    }, services);
    $scope.canSave = getCanSave(services);

    // Initializes form.
    initializeForm({
        id: id,
        isNew: isNew
    }, services);

}

// Saves the form.
function getSaveForm(options, services) {
    return function () {
        //TODO: ...
        alert("Saving...");
    };
}

// Gets the fields from the server for this form.
function getFields(formId, services) {
    return services.formulateForms.getFields(formId)
        .then(function(form) {
            return form;
        });
}

// Initializes the form.
function initializeForm(options, services) {

    // Variables.
    var id = options.id;
    var isNew = options.isNew;

    // Is this a new form?
    if (isNew) {

        // Default to no fields.
        services.$scope.fields = [];

        // The form can be saved now.
        services.$scope.initialized = true;

    } else {

        // Disable form saving until the data is populated.
        services.$scope.initialized = false;

        // Get the fields.
        getFields(id, services).then(function(form) {

            // Set the form fields.
            services.$scope.fields = form.fields;

            // The form can be saved now.
            services.$scope.initialized = true;

        });

    }

}

// Returns the function that indicates whether or not the form can be saved.
function getCanSave(services) {
    return function() {
        return services.$scope.initialized;
    };
}