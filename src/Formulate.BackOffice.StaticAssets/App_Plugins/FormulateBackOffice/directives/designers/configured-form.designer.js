(function () {
    function formulateConfiguredFormDesignerDirective(
            notificationsService, formHelper, overlayService,
            formulateTypeDefinitionResource) {
        const directive = {
            replace: true,
            templateUrl: "/app_plugins/formulatebackoffice/directives/designers/configured-form.designer.html",
            scope: {
                entity: "=",
            },
            link: function (scope, element, attrs) {
                const services = {
                    $scope: scope,
                    overlayService,
                };
                scope.events = new ConfiguredFormEvents(services);

                initializeTemplates(formulateTypeDefinitionResource, scope);
            },
        };

        return directive;
    }

    /**
     * Initializes the templates on the scope.
     * @param formulateTypeDefinitionResource The resource that can be used to
     *  fetch templates.
     * @param scope The scope to set the templates on.
     */
    function initializeTemplates(formulateTypeDefinitionResource, scope) {
        scope.templates = [];
        formulateTypeDefinitionResource.getTemplateDefinitions()
            .then((data) => {
                scope.templates = data;
            });
    }

    /**
     * The event handlers for the configured form designer.
     */
    class ConfiguredFormEvents {

        // Service properties.
        $scope;
        overlayService;

        /**
         * Constructor.
         * @param services The services the event handlers will need.
         */
        constructor(services) {
            Object.keys(services).forEach((x) => this[x] = services[x]);
        }

        /**
         * Opens the layout chooser dialog.
         */
        pickLayout = () => {

            // This is called when the dialog is closed.
            let closer = () => {
                this.overlayService.close();
            };

            // This is called when a layout is chosen.
            let chosen = ({id, name}) => {
                this.overlayService.close();
                this.$scope.entity.layoutId = id;
                this.$scope.entity.layoutName = name;
            };

            // The data sent to the layout chooser.
            let data = {
                title: "Choose Layout",
                subtitle: "Choose a layout to associate with your form configuration.",
                view: "/app_plugins/formulatebackoffice/directives/overlays/layoutchooser/layout-chooser-overlay.html",
                hideSubmitButton: true,
                close: closer,
                chosen: chosen,
            };

            // Open the overlay that displays the layouts.
            this.overlayService.open(data);

        };

    }

    // Register the directive.
    angular
        .module("umbraco.directives")
        .directive("formulateConfiguredFormDesigner", formulateConfiguredFormDesignerDirective);

})();