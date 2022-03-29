"use strict";

function formulateFormDesignerDirective(overlayService, $timeout) {
    const directive = {
        replace: true,
        templateUrl: "/app_plugins/formulatebackoffice/directives/designers/form.designer.html",
        scope: {
            entity: "<"
        },
        link: function (scope, element, attrs) {
            scope.initialized = false;
            // TODO: Replace placeholder value for apps view to use separate subviews
            // For now a placeholder value is set for view to ensure
            // Umbraco EditorNavigationDirective setItemToActive function
            // will toggle app navigation
            scope.apps = [
                {
                    active: true,
                    name: "Form",
                    alias: "Form",
                    icon: "icon-formulate-form",
                    view: "#"
                },
                {
                    name: "Handlers",
                    alias: "Handlers",
                    icon: "icon-handshake",
                    view: "#"
                }
            ];

            //TODO: Delete this (also from view)? Not sure if it actually gets used for anything.
            scope.appChanged = function (app) {
                scope.app = app;

                scope.$broadcast("editors.apps.appChanged", { app: app });
            }

            scope.fields = [];
            scope.handlers = [];

            Utilities.copy(scope.entity.fields, scope.fields);
            Utilities.copy(scope.entity.handlers, scope.handlers);

            //if (hasParent) {
            //    scope.parentId = parentId;
            //}
            scope.fieldChooser = {
                show: false
            };
            scope.handlerChooser = {
                show: false
            };
            scope.sortableOptions = {
                axis: "y",
                cursor: "move",
                delay: 100,
                opacity: 0.5
            };
            scope.sortableHandlerOptions = {
                axis: "y",
                cursor: "move",
                delay: 100,
                opacity: 0.5
            };

            scope.initialized = true;

            // we need to check whether an app is present in the current data, if not we will present the default app.
            var isAppPresent = false;

            // on first init, we dont have any apps. but if we are re-initializing, we do, but ...
            if (scope.app) {

                // lets check if it still exists as part of our apps array. (if not we have made a change to our docType, even just a re-save of the docType it will turn into new Apps.)
                Utilities.forEach(scope.apps, function (app) {
                    if (app === scope.app) {
                        isAppPresent = true;
                    }
                });

                // if we did reload our DocType, but still have the same app we will try to find it by the alias.
                if (isAppPresent === false) {
                    Utilities.forEach(content.apps, function (app) {
                        if (app.alias === scope.app.alias) {
                            isAppPresent = true;
                            app.active = true;
                            scope.appChanged(app);
                        }
                    });
                }

            }

            // if we still dont have a app, lets show the first one:
            if (isAppPresent === false) {
                scope.apps[0].active = true;
                scope.appChanged(scope.apps[0]);
            }

            scope.events = new FormDesignerEventHandlers({
                $scope: scope,
                overlayService: overlayService,
                $timeout: $timeout,
            });
        }
    };

    return directive;
}

/**
 * The event handlers for the form designer.
 */
class FormDesignerEventHandlers {

    // Properties.
    $scope;
    overlayService;
    handlerAccordion;
    fieldAccordion;

    /**
     * Constructor.
     * @param services The services the event handlers will need.
     */
    constructor(services) {
        Object.keys(services).forEach((x) => this[x] = services[x]);
        this.handlerAccordion = new window.FormulateAccordion(services);
        this.fieldAccordion = new window.FormulateAccordion(services);
    }

    /**
     * Toggles the field to either show or hide it.
     * @param field The field to show or hide.
     * @param target The clicked element.
     */
    toggleField = (field, target) => {
        let fieldsetEl = target.closest('fieldset');
        this.fieldAccordion.handleClick(field, fieldsetEl, '.formulate-field-details');
    };

    /**
     * Deletes the specified field (after confirming with the user).
     * @param field The field to delete.
     */
    deleteField = (field) => {

        // Confirm deletion.
        const name = field.name === null || !field.name.length
            ? "unnamed field"
            : `field, "${field.name}"`;
        const message = `Are you sure you wanted to delete the ${name}?`;
        const response = confirm(message);

        // Delete field?
        if (response) {
            const index = this.$scope.fields.indexOf(field);
            this.$scope.fields.splice(index, 1);
        }

    };

    /**
     * Toggles the handler to either show or hide it.
     * @param handler The handler to toggle.
     * @param target The clicked element.
     */
    toggleHandler = (handler, target) => {
        let fieldsetEl = target.closest('fieldset');
        this.handlerAccordion.handleClick(handler, fieldsetEl, '.formulate-handler-details');
    };

    /**
     * Deletes the specified handler (after confirming with the user).
     * @param handler The handler to delete.
     */
    deleteHandler = (handler) => {

        // Confirm deletion.
        const name = handler.name === null || !handler.name.length
            ? "unnamed handler"
            : `handler, "${handler.name}"`;
        const message = `Are you sure you wanted to delete the ${name}?`;
        const response = confirm(message);

        // Delete handler?
        if (response) {
            const index = this.$scope.handlers.indexOf(handler);
            this.$scope.handlers.splice(index, 1);
        }

    };

    /**
     * Toggles the enabled state of the handler.
     * @param handler The handler to toggle.
     */
    toggleHandlerEnabled = (handler) => {
        handler.enabled = !handler.enabled;
    };

    /**
     * Shows the dialog that allows the user to choose a form handler to add.
     */
    addHandler = () => {

        // This is called when the dialog is closed.
        let closer = () => {
            this.overlayService.close();
        };

        // This is called when a handler is chosen.
        let chosen = (item) => {
            this.overlayService.close();
            let handler = item.handler;
            this.$scope.handlers.push({
                directive: handler.directive,
                enabled: true,
                icon: handler.icon,
                id: crypto.randomUUID(),
                kindId: handler.kindId,
                name: null,
                alias: null,
            });
        };

        // The data sent to the form handler chooser.
        let data = {
            title: "Add Handler",
            subtitle: "Choose one of the following form handlers to add to your form.",
            view: "/app_plugins/formulatebackoffice/directives/overlays/formhandlerchooser/form-handler-chooser-overlay.html",
            hideSubmitButton: true,
            close: closer,
            chosen: chosen,
        };

        // Open the overlay that displays the handlers.
        this.overlayService.open(data);

    };

    /**
     * Can the user add a handler to the page yet?
     * @returns {boolean} True, if the user can add a handler; otherwise, false.
     */
    canAddHandler = () => {
        return this.$scope.initialized;
    }

    /**
     * Shows the dialog that allows the user to choose a form field to add.
     */
    addField = () => {

        // This is called when the dialog is closed.
        let closer = () => {
            this.overlayService.close();
        };

        // This is called when a field is chosen.
        let chosen = (item) => {
            this.overlayService.close();
            let field = item.field;
            this.$scope.fields.push({
                directive: field.directive,
                icon: field.icon,
                id: crypto.randomUUID(),
                kindId: field.kindId,
                name: null,
                alias: null,
            });
        };

        // The data sent to the form field chooser.
        let data = {
            title: "Add Handler",
            subtitle: "Choose one of the following form fields to add to your form.",
            view: "/app_plugins/formulatebackoffice/directives/overlays/formfieldchooser/form-field-chooser-overlay.html",
            hideSubmitButton: true,
            close: closer,
            chosen: chosen,
        };

        // Open the overlay that displays the fields.
        this.overlayService.open(data);

    };

    /**
     * Can the user add a field to the page yet?
     * @returns {boolean} True, if the user can add a field; otherwise, false.
     */
    canAddField = () => {
        return this.$scope.initialized;
    }

    /**
     * Opens the validation chooser dialog.
     * @param field The field to choose validations for.
     */
    pickValidations = (field) => {

        // This is called when the dialog is closed.
        let closer = () => {
            this.overlayService.close();
        };

        // The data sent to the validation chooser.
        let data = {
            title: "Choose Validations",
            subtitle: "Choose any of these validations to add to your field. They will be evaluated in the order you choose them.",
            view: "/app_plugins/formulatebackoffice/directives/overlays/validationchooser/validation-chooser-overlay.html",
            hideSubmitButton: true,
            close: closer,
            validations: field.validations,
        };

        // Open the overlay that displays the validations.
        this.overlayService.open(data);

    };

}

angular.module("umbraco.directives").directive("formulateFormDesignerV2", formulateFormDesignerDirective);