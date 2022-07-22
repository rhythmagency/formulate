"use strict";

(function () {

    // Variables.
    const app = angular.module("umbraco");

    // Associate directive.
    app.directive("formulateConfiguredFormChooser", directive);

    // Directive.
    function directive() {
        return {
            restrict: "E",
            templateUrl: "/app_plugins/formulatebackoffice/directives/overlays/configured-form-chooser/form-chooser.html",
            controller: controller,
            scope: {
                model: "=",
            }
        };
    }

    // Controller.
    function controller($scope) {
        $scope.events = new ConfiguredFormChooserEventHandlers({
            $scope,
        });
    }

    /**
     * The event handlers for the form chooser.
     */
    class ConfiguredFormChooserEventHandlers {

        // Properties.
        $scope;

        /**
         * Constructor.
         * @param services The services the event handlers will need.
         */
        constructor(services) {
            Object.keys(services).forEach((x) => this[x] = services[x]);
            this.$scope.formTreeApi = {};
        }

        /**
         * Initializes the event handlers once the tree is ready.
         */
        formTreeInit = () => {
            this.$scope.formTreeApi.callbacks.treeNodeSelect(this.handleFormClickedInTree);
        };

        /**
         * Hands a click of a configured form node.
         * @param args The configured form node details.
         */
        handleFormClickedInTree = (args) => {

            // Boilerplate.
            args.event.preventDefault();
            args.event.stopPropagation();

            // Variables.
            const node = args.node;
            const meta = node.metaData;

            // If this is not a configured form node (e.g., if it's a folder), exit early.
            if (node.nodeType !== 'ConfiguredForm') {
                return;
            }

            // Send notification of selected form.
            this.$scope.model.chosen({
                id: meta.NodeId,
                name: meta.NodeName,
            });

        };

    }

})();