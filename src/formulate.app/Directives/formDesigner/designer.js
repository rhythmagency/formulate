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
function FormDesignerController($scope, $routeParams, navigationService,
    formulateForms, $location, $route) {

    // Variables.
    var id = $routeParams.id;
    var isNew = id === "null";
    var parentId = $routeParams.under;
    var hasParent = !!parentId;
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
    $scope.formAlias = null;
    $scope.formName = null;
    $scope.fields = [];
    if (!isNew) {
        $scope.formId = id;
    }
    if (hasParent) {
        $scope.parentId = parentId;
    }
    $scope.fieldChooser = {
        show: false
    };

    // Set scope functions.
    $scope.save = getSaveForm(services);
    $scope.addField = getAddField(services);
    $scope.canSave = getCanSave(services);
    $scope.canAddField = getCanAddField(services);
    $scope.fieldChosen = getFieldChosen(services);
    $scope.toggleField = getToggleField();

    // Initializes form.
    initializeForm({
        id: id,
        isNew: isNew
    }, services);

}

// Saves the form.
function getSaveForm(services) {
    return function () {

        // Variables.
        var $scope = services.$scope;
        var fields = $scope.fields;
        var parentId = getParentId($scope);

        // Get form data.
        var formData = {
            parentId: parentId,
            formId: $scope.formId,
            alias: $scope.formAlias,
            name: $scope.formName,
            fields: fields
        };

        // Persist form on server.
        services.formulateForms.persistForm(formData)
            .then(function(responseData) {

                // Variables.
                var isNew = $scope.isNew;

                // Prevent "discard" notification.
                $scope.formulateFormDesigner.$dirty = false;

                // Redirect or reload page.
                if (isNew) {
                    var url = "/formulate/formulate/editForm/"
                        + responseData.formId;
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

        // Show field chooser.
        $scope.fieldChooser.show = true;

    };
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
        services.formulateForms.getFormInfo(id).then(function(form) {

            // Update tree.
            activateInTree(form, services);

            // Set the form info.
            $scope.formId = form.formId;
            $scope.formAlias = form.alias;
            $scope.formName = form.name;
            $scope.fields = form.fields;
            $scope.formPath = form.path;

            // Collapse fields.
            for (var field in $scope.fields) {
                field.expanded = false;
            }

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

// Gets the function that handles a chosen field.
function getFieldChosen(services) {
    return function(field) {
        var $scope = services.$scope;
        $scope.fieldChooser.show = false;
        if (field) {
            $scope.fields.push({
                name: null,
                alias: null,
                label: null,
                icon: field.icon,
                directive: field.directive,
                typeLabel: field.typeLabel,
                typeFullName: field.typeFullName,
                expanded: true
            });
        }
    };
}

// Gets the function that toggles the visibility of a field.
function getToggleField() {
    return function(field) {
        field.expanded = !field.expanded;
    };
}

// Gets the ID path to the form.
function getFormPath($scope) {
    var path = $scope.formPath;
    if (!path) {
        path = [];
    }
    return path;
}

// Gets the ID of the form's parent.
function getParentId($scope) {
    if ($scope.parentId) {
        return $scope.parentId;
    }
    var path = getFormPath($scope);
    var parentId = path.length > 0
        ? path[path.length - 2]
        : null;
    return parentId;
}