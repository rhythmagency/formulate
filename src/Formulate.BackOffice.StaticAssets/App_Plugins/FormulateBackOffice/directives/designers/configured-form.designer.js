(function () {
    function formulateConfiguredFormDesignerDirective(
        $http,
        $location,
        editorService,
        formHelper,
        formulateIds,
        formulateTypeDefinitionResource,
        notificationsService) {

        const link = (scope) => {
            scope.loading = true;
            scope.saveButtonState = 'init';
            const services = {
                $scope: scope,
                $http,
                $location,
                editorService,
                formHelper,
                formulateIds,
                formulateTypeDefinitionResource,
                notificationsService
            };
            scope.events = new ConfiguredFormEvents(services);

            initializeTemplates(formulateTypeDefinitionResource, scope).then(() => {
                scope.loading = false;
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
        editorService;
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
                this.editorService.close();
            };

            // This is called when a layout is chosen.
            let chosen = ({id, name}) => {
                this.editorService.close();
                this.$scope.entity.layout = { id, name };
            };

            // The data sent to the layout chooser.
            let data = {
                section: 'formulate',
                treeAlias: 'layouts',
                multiPicker: false,
                entityType: 'Layout',
                filter: (node) => {
                    return node.nodeType !== 'Layout';
                },
                filterCssClass: 'not-allowed',
                title: "Choose Layout",
                subtitle: "Choose a layout to associate with your form configuration.",
                close: closer,
                select: chosen,
            };

            // Open the overlay that displays the layouts.
            this.editorService.treePicker(data);
        };

        removeLayout = () => {
            this.$scope.entity.layout = {};
        }

        /**
         * Can the configured form be saved currently?
         * @returns {boolean} True, if the configured form can be saved; otherwise, false.
         */
        canSave = () => {
            return !this.$scope.loading && this.isReadyToSave() && this.hasValidName();
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
                    layout: !entity.layout.id ? null : { id: entity.layout.id }
                };

                // Save the data to the server.
                this.$http.post(url, payload).then(({data: {success}}) => {
                    const resetData = {
                        scope: this.$scope,
                        formCtrl: this.$scope.formulateConfiguredFormDesigner,
                    };
                    this.formHelper.resetForm(resetData);
                    if (success) {
                        this.$scope.saveButtonState = 'success';

                        if (entity.isNew) {
                            const sanitizedEntityId = this.formulateIds.sanitize(entity.id);
                            this.notificationsService.success("Configured form created.");

                            this.$location.path("/formulate/forms/edit/" + sanitizedEntityId).search({});
                        }
                        else {
                            this.notificationsService.success("Configured form saved.");
                        }
                    } else {
                        this.$scope.saveButtonState = 'error';

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