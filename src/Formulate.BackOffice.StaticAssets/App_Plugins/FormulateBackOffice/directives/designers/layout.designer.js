/**
 * The layout designer, which facilitates editing the form layout (rows, columns,
 * field positions, etc.).
 */
class FormulateLayoutDesigner {

    // Properties.
    $scope;
    editorService;
    $http;
    $routeParams;
    formulateLayouts;

    /**
     * The controller function that gets called by Angular.
     */
    controller = (
        retainProperties,
        $scope,
        editorService,
        $http,
        $routeParams,
        formulateLayouts) => {

        // Retain the injected parameters on this object.
        retainProperties({
            $scope,
            editorService,
            $http,
            $routeParams,
            formulateLayouts,
        }, this);

        // Attach this object to the scope so it's accessible by the view.
        $scope.events = this;

        // Initializes layout.
        this.initializeLayout();

    };

    /**
     * Initializes the layout.
     */
    initializeLayout = () => {

        // Disable layout saving until the data is populated.
        this.$scope.initialized = false;

        // Get the layout info.
        const id = this.$routeParams.id;
        this.formulateLayouts.getLayoutInfo(id)
            .then((layout) => {

                // Update tree.
                //TODO: ...
                //this.formulateTrees.activateEntity(layout);

                //TODO: Confirm if this is necessary.
                // Set the layout info.
                this.$scope.kindId = layout.kindId;
                this.$scope.layoutId = layout.id;
                this.$scope.info = {};
                this.$scope.info.layoutAlias = layout.alias;
                this.$scope.info.layoutName = layout.name;
                this.$scope.layoutPath = layout.path;
                this.$scope.directive = layout.directive;
                this.$scope.data = layout.data;

                // The layout can be saved now.
                this.$scope.initialized = true;

            });

    };

    /**
     * Registers the directive with Angular.
     */
    registerDirective = () => {
        angular
            .module('umbraco.directives')
            .directive('formulateLayoutDesigner', () => {
            return {
                restrict: 'E',
                replace: true,
                templateUrl: "/app_plugins/formulatebackoffice/directives/designers/layout.designer.html",
                scope: {
                    entity: '=',
                },
                controller: this.controller,
            };
        });
    };
}

// Initialize.
(new FormulateLayoutDesigner()).registerDirective();