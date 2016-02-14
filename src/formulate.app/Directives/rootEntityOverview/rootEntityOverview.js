// Variables.
var app = angular.module("umbraco");

// Associate directive/controller.
app.directive("formulateRootEntityOverview", directive);
app.controller("formulate.rootEntityOverview", controller);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        transclude: true,
        template: formulateDirectives.get("rootEntityOverview/rootEntityOverview.html"),
        controller: "formulate.rootEntityOverview"
    };
}

// Controller.
function controller($routeParams, formulateEntities, formulateTrees) {

    // Variables.
    var id = $routeParams.id;
    var services = {
        formulateEntities: formulateEntities,
        formulateTrees: formulateTrees
    };

    // Initialize the entity.
    initializeEntity({
        id: id
    }, services);

}

// Initializes the entity.
function initializeEntity(options, services) {

    // Variables.
    var id = options.id;

    // Get the entity info.
    services.formulateEntities.getEntity(id)
        .then(function(entity) {

            // Update tree.
            services.formulateTrees.activateEntity(entity);

        });

}