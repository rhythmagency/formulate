(function () {
    function formulateConfiguredFormDesignerDirective(
            notificationsService, formHelper, overlayService, $http, $routeParams,
            formulateTypeDefinitionResource) {

        const link = (scope) => {
            scope.saveButtonState = 'init';
            const services = {
                $scope: scope,
                $http,
                overlayService,
                formHelper,
                notificationsService,
            };
            scope.events = new ConfiguredFormEvents(services);

            // Initialize the templates and ID/path.
            const promise1 = initializeTemplates(formulateTypeDefinitionResource, scope);
            const promise2 = initializeIdAndPath(scope, $routeParams);

            // Store initialization state once complete.
            Promise.all([promise1, promise2])
                .then(() => {
                    scope.initialied = true;
                });

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

        /**
         * Initializes the ID and path of the entity if it is new.
         * @param $scope The current AngularJS scope.
         * @param $routeParams The parameters from the route.
         */
        function initializeIdAndPath($scope, $routeParams) {

            // Variables.
            const entity = $scope.entity;
            const path = entity.path;
            const id = entity.id;
            const url = Umbraco.Sys.ServerVariables.formulate["Forms.GenerateNewPathAndId"];
            const parentId = !$routeParams.isNew && $routeParams.id && $routeParams.id !== "-1"
                ? $routeParams.id
                : null;

            // Return early if the path and ID are already set.
            if (path && path.length && id) {
                $scope.initialized = true;
                return new Promise(resolve => resolve());
            }

            // Get the path and ID from the server.
            const payload = {
                parentId,
            };
            return $http.post(url, payload).then(({data: response}) => {
                entity.id = response.id;
                entity.path = response.path;
            });

        }
    }

    /**
     * Initializes the templates on the scope.
     * @param formulateTypeDefinitionResource The resource that can be used to
     *  fetch templates.
     * @param scope The scope to set the templates on.
     */
    function initializeTemplates(formulateTypeDefinitionResource, scope) {
        scope.templates = [];
        return formulateTypeDefinitionResource.getTemplateDefinitions()
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
        $http;
        overlayService;
        formHelper;
        notificationsService;

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

        /**
         * Saves the configured form to the server.
         */
        save = () => {

            // Mark the save button as busy, so it can't double submit.
            this.$scope.saveButtonState = 'busy';

            // Indicate that the form is submitting (e.g., performs field validation).
            const submitFormData = {
                scope: this.$scope,
                formCtrl: this.$scope.formulateConfiguredFormDesigner,
            };
            if (this.formHelper.submitForm(submitFormData)) {

                // Prepare the data to save.
                const url = Umbraco.Sys.ServerVariables.formulate["configuredForms.Save"];
                const entity = this.$scope.entity;
                const payload = {
                    alias: entity.alias,
                    id: entity.id,
                    name: entity.name,
                    path: entity.path,
                    templateId: entity.templateId,
                    layoutId: entity.layoutId,
                };

                // Save the data to the server.
                this.$http.post(url, payload).then(({data: {success}}) => {
                    this.$scope.saveButtonState = 'init';
                    const resetData = {
                        scope: this.$scope,
                        formCtrl: this.$scope.formulateConfiguredFormDesigner,
                    };
                    this.formHelper.resetForm(resetData);
                    if (success) {
                        this.notificationsService.success("Configured form saved.");
                    } else {
                        this.notificationsService.error("Unknown error while saving configured form.");
                    }
                });

            } else {

                // Configured form couldn't be saved (probably a validation issue).
                // Reset the button/form.
                this.$scope.saveButtonState = 'init';
                const resetData = {
                    scope: this.$scope,
                    formCtrl: this.$scope.formulateFormDesigner,
                    hasErrors: true,
                };
                this.formHelper.resetForm(resetData);

            }
        };

    }

    // Register the directive.
    angular
        .module("umbraco.directives")
        .directive("formulateConfiguredFormDesigner", formulateConfiguredFormDesignerDirective);

})();