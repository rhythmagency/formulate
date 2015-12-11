//TODO: Disable buttons during layout save.
// Variables.
var app = angular.module("umbraco");

// Associate directive/controller.
app.directive("formulateLayoutDesigner", LayoutDesignerDirective);
app.controller("formulate.layoutDesigner", LayoutDesignerController);

// Directive.
function LayoutDesignerDirective(formulateDirectives) {
    return {
        restrict: "E",
        template: formulateDirectives.get("layoutDesigner/designer.html")
    };
}

// Controller.
function LayoutDesignerController($scope, $routeParams, navigationService, formulateLayouts) {

    // Variables.
    var id = $routeParams.id;
    var services = {
        $routeParams: $routeParams,
        navigationService: navigationService,
        formulateLayouts: formulateLayouts
    };

    // Initialize.
    activateInTree(id, services);

    // Set scope variables.
    $scope.layoutId = id;

    // Set scope functions.
    $scope.save = getSaveLayout();

}

// Shows/highlights the node in the Formulate tree.
function activateInTree(id, services) {

    // Get path from server.
    services.formulateLayouts.getPath(id)
        .then(function(path) {
            var options = {
                tree: "formulate",
                path: path,
                forceReload: false,
                activate: true
            };
            services.navigationService.syncTree(options);
        });

}

// Saves the layout.
function getSaveLayout() {
    return function () {
        //TODO: ...
        alert("Saving...");
    };
}