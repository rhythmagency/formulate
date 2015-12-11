//TODO: Disable buttons during form save.
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
function FormDesignerController($scope, $routeParams, navigationService, formulateForms, $location, $route) {

    // Variables.
    var id = $routeParams.id;
    var isNew = id === "null";
    var services = {
        $scope: $scope,
        $routeParams: $routeParams,
        navigationService: navigationService,
        formulateForms: formulateForms,
        $location: $location,
        $route: $route
    };

    // Set scope variables.
    $scope.isNew = isNew;
    $scope.formAlias = "";
    $scope.formName = "";
    $scope.fields = [];
    if (!isNew) {
        $scope.formId = id;
    }

    // Set scope functions.
    $scope.save = getSaveForm({
        id: id
    }, services);
    $scope.addField = getAddField(services);
    $scope.canSave = getCanSave(services);
    $scope.canAddField = getCanAddField(services);

    // Initializes form.
    initializeForm({
        id: id,
        isNew: isNew
    }, services);

}

// Saves the form.
function getSaveForm(options, services) {
    return function () {

        // Variables.
        var $scope = services.$scope;
        var fields = $scope.fields;

        // Get form data.
        var formData = {
            formId: $scope.formId,
            alias: $scope.formAlias,
            name: $scope.formName,
            fields: fields
        };

        // Persist form on server.
        services.formulateForms.persistForm(formData).then(function(responseData) {

            // Form is no longer new.
            var isNew = $scope.isNew;
            $scope.isNew = false;

            // Redirect or reload page.
            if (isNew) {
                var url = "/formulate/formulate/editForm/" + responseData.formId;
                services.$location.url(url);
            } else {

                // Even existing forms reload (e.g., to get new field ID's).
                services.$route.reload();

            }

        });

    };
}

// Adds a field.
function getAddField(services) {
    return function () {

        // Variables.
        var $scope = services.$scope;

        //TODO: Testing.
        $scope.fields.push({
            alias: "newField",
            name: "New Field"
        });

    };
}

// Gets the form info from the server for this form.
function getFormInfo(formId, services) {
    return services.formulateForms.getFormInfo(formId)
        .then(function(form) {
            return form;
        });
}

// Initializes the form.
function initializeForm(options, services) {

    // Variables.
    var id = options.id;
    var isNew = options.isNew;
    var $scope = services.$scope;

    // Is this a new form?
    if (isNew) {

        // The form can be saved now.
        $scope.initialized = true;

    } else {

        // Disable form saving until the data is populated.
        $scope.initialized = false;

        // Get the form info.
        getFormInfo(id, services).then(function(form) {

            // Update tree.
            activateInTree(form, services);

            // Set the form info.
            $scope.formId = form.formId;
            $scope.formAlias = form.alias;
            $scope.formName = form.name;
            $scope.fields = form.fields;

            // The form can be saved now.
            $scope.initialized = true;

        });

    }

}

// Returns the function that indicates whether or not the form can be saved.
function getCanSave(services) {
    return function() {
        return services.$scope.initialized;
    };
}

// Returns the function that indicates whether or not fields can be added.
function getCanAddField(services) {
    return function() {
        return services.$scope.initialized;
    };
}

// Shows/highlights the node in the Formulate tree.
function activateInTree(form, services) {
    var options = {
        tree: "formulate",
        path: form.path,
        forceReload: true,
        activate: true
    };
    services.navigationService.syncTree(options);
}