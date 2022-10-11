"use strict";

function formulateFormDesignerDirective(
    $timeout, formHelper, $http, formulateDesignerResource, editorService, notificationsService) {
    const directive = {
        replace: true,
        templateUrl: "/app_plugins/formulate/designers/forms/form.designer.html",
        scope: {
            entity: "<"
        },
        link: function (scope, element, attrs) {
            scope.saveButtonState = 'init';
            scope.initialized = false;

            scope.events = new FormDesignerEventHandlers({
                $scope: scope,
                $timeout,
                $http,
                formulateDesignerResource,
                editorService,
                formHelper,
                notificationsService,
            });

            scope.initialized = true;
        }
    };

    return directive;
}

/**
 * The event handlers for the form designer.
 */
class FormDesignerEventHandlers {

    // Service properties.
    $scope;
    $http;
    formulateDesignerResource;
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
     * Can the form be saved currently?
     * @returns {boolean} True, if the form can be saved; otherwise, false.
     */
    canSave = () => {
        return this.$scope.initialized && this.isReadyToSave() && this.hasValidName();
    };

    /**
     * Is the form currently in a state which is not busy?
     * @returns {boolean} True, if the form is in a state that is ready to save;
     *      otherwise, false.
     */
    isReadyToSave = () => {
        return this.$scope.saveButtonState !== 'busy';
    };

    /**
     * Does the form have a valid name?
     * @returns {boolean} True, if the form has a valid name; otherwise, false.
     */
    hasValidName = () => {
        return this.$scope.entity.name && this.$scope.entity.name.length > 0;
    };

    /**
     * Saves the form to the server.
     */
    save = () => {

        // Mark the save button as busy, so it can't double submit.
        this.$scope.saveButtonState = 'busy';

        // Indicate that the form is submitting (e.g., performs field validation).
        const submitFormData = {
            scope: this.$scope,
            formCtrl: this.$scope.formulateFormDesigner,
        };
        if (this.formHelper.submitForm(submitFormData)) {

            // Prepare the data to save.
            const url = Umbraco.Sys.ServerVariables.formulate.Forms.Save;
            const entity = this.$scope.entity;

            const payload = {
                alias: entity.alias,
                id: entity.id,
                name: entity.name,
                path: entity.path,
                kindid: entity.kindId,
                isNew: entity.isNew,
                fields: entity.fields.map(x => {
                    return {
                        alias: x.alias,
                        category: x.category,
                        id: x.id,
                        kindId: x.kindId,
                        name: x.name,
                        label: x.label,
                        configuration: x.configuration,
                        validations: x.validations.map(v => {
                            return {
                                id: v.id
                            };
                        }),
                    };
                }),
                handlers: entity.handlers.map(x => {
                    return {
                        alias: x.alias,
                        enabled: x.enabled,
                        id: x.id,
                        kindId: x.kindId,
                        name: x.name,
                        //TODO: Double check naming (not sure if configuration is correct).
                        configuration: x.configuration,
                    };
                }),
            };

            // Save the data to the server.
            this.$http.post(url, payload).then(() => {
                this.$scope.saveButtonState = 'success';
                const resetData = {
                    scope: this.$scope,
                    formCtrl: this.$scope.formulateFormDesigner,
                };
                this.formHelper.resetForm(resetData);

                const options = {
                    entity,
                    treeAlias: 'forms',
                    newEntityText: 'Form created',
                    existingEntityText: 'Form saved'
                };

                this.formulateDesignerResource.handleSuccessfulSave(options);
            }, () => {
                this.$scope.saveButtonState = 'error';
                this.notificationsService.error(entity.isNew ? "Unknown error while creating form." : "Unknown error while saving form.");
            });

        } else {

            // Form couldn't be saved (probably a validation issue). Reset the button/form.
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

angular.module("umbraco.directives").directive("formulateFormDesigner", formulateFormDesignerDirective);