(function () {
    function formulateConfiguredFormDesignerDirective(
            notificationsService, formHelper, overlayService,
            formulateTypeDefinitionResource) {

        const link = (scope) => {
            scope.saveButtonState = 'init';
            const services = {
                $scope: scope,
                overlayService,
            };
            scope.events = new ConfiguredFormEvents(services);

            initializeTemplates(formulateTypeDefinitionResource, scope);
        };

        const directive = {
            replace: true,
            templateUrl: "/app_plugins/formulatebackoffice/directives/designers/configured-form.designer.html",
            scope: {
                entity: "=",
            },
            link: link,
        };

        return (directive);
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
                scope.initialized = true;
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

        /**
         * Can the configured form be saved currently?
         * @returns {boolean} True, if the configured form can be saved; otherwise, false.
         */
        canSave = () => {
            return this.$scope.initialized && this.isReadyToSave() && this.hasValidName();
        };

        /**
         * Is the configured form currently in a state which is not busy?
         * @returns {boolean} True, if the configured form is in a state that is ready to save;
         *      otherwise, false.
         */
        isReadyToSave = () => {
            return this.$scope.saveButtonState !== 'busy';
        };

        /**
         * Does the configured form have a valid name?
         * @returns {boolean} True, if the configured form has a valid name; otherwise, false.
         */
        hasValidName = () => {
            return this.$scope.entity.name && this.$scope.entity.name.length > 0;
        };

    }

    // Register the directive.
    angular
        .module("umbraco.directives")
        .directive("formulateConfiguredFormDesigner", formulateConfiguredFormDesignerDirective);

})();