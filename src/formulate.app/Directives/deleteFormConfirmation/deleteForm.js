// Variables.
var app = angular.module("umbraco");

// Register directive/controller.
app.controller("formulate.deleteFormConfirmation", controller);
app.directive("formulateDeleteFormConfirmation", directive);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        template: formulateDirectives.get("deleteFormConfirmation/deleteForm.html"),
        controller: "formulate.deleteFormConfirmation"
    };
}

// Controller.
function controller($scope, $location, $q, $http, navigationService,
    formulateForms, treeService) {

    // Variables.
    var formId = $scope.currentNode.id;

    // Variable containing the common services (easier to pass around).
    var services = {
        $scope: $scope,
        $location: $location,
        $q: $q,
        $http: $http,
        navigationService: navigationService,
        formulateForms: formulateForms,
        treeService: treeService
    };

    // Assign scope variables.
    $scope.initialized = false;

    // Assign functions on scope.
    $scope.deleteForm = getDeleteForm(services);
    $scope.cancel = getCancel(services);

    // Load form information.
    loadFormInfo(formId, services);

}

// Loads information about the form.
function loadFormInfo(formId, services) {
    services.formulateForms.getFormInfo(formId).then(function(form) {
        services.$scope.formId = form.formId;
        services.$scope.formName = form.name;
        services.$scope.initialized = true;
    });
}

// Returns a function that deletes a form.
function getDeleteForm(services) {
    return function() {

        // Variables.
        var node = services.$scope.currentNode;
        var formId = services.$scope.formId;
        var formPromise = services.formulateForms.getFormInfo(formId);

        // Once we have the form information...
        formPromise.then(function (form) {

            // Variables.
            var path = form.path;
            var partialPath = path.slice(0, path.length - 1);

            // Delete form.
            services.formulateForms.deleteForm(formId)
                .then(function () {

                    // Remove the node from the tree.
                    services.treeService.removeNode(node);

                    // Update tree (down to the deleted form's parent).
                    var options = {
                        tree: "formulate",
                        path: partialPath,
                        forceReload: true,
                        activate: false
                    };
                    services.navigationService.syncTree(options);

                    // Close dialog.
                    services.navigationService.hideDialog();

                });

        });

    };
}

// Returns the function that cancels the deletion.
function getCancel(services) {
    return function () {
        services.navigationService.hideDialog();
    };
}