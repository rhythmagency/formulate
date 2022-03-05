"use strict";

function formulateFormDesignerDirective() {
    var directive = {
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

    /**
     * Constructor.
     * @param services The services the event handlers will need.
     */
    constructor(services) {
        Object.keys(services).forEach((x) => this[x] = services[x]);
    }

    /**
     * Toggles the field to either show or hide it.
     * @param field The field to show or hide.
     */
    toggleField = (field) => {
        field.expanded = !field.expanded;
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
     */
    toggleHandler = (handler) => {
        handler.expanded = !handler.expanded;
        //TODO: I think we need a corresponding CSS rule for this to work fully.
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

}


angular.module("umbraco.directives").directive("formulateFormDesignerV2", formulateFormDesignerDirective);