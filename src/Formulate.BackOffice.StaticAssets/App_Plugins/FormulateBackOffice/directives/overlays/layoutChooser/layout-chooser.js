"use strict";

(function () {

    // Variables.
    const app = angular.module("umbraco");

    // Associate directive.
    app.directive("formulateLayoutChooser", directive);

    // Directive.
    function directive() {
        return {
            restrict: "E",
            templateUrl: "/app_plugins/formulatebackoffice/directives/overlays/layoutchooser/layout-chooser.html",
            controller: controller,
            scope: {
                model: "=",
            }
        };
    }

    // Controller.
    function controller($scope) {
        $scope.events = new LayoutChooserEventHandlers({
            $scope,
        });
    }

    /**
     * The event handlers for the layout chooser.
     */
    class LayoutChooserEventHandlers {

        // Properties.
        $scope;

        /**
         * Constructor.
         * @param services The services the event handlers will need.
         */
        constructor(services) {
            Object.keys(services).forEach((x) => this[x] = services[x]);
            this.$scope.layoutTreeApi = {};
        }

        /**
         * Initializes the event handlers once the tree is ready.
         */
        layoutTreeInit = () => {
            this.$scope.layoutTreeApi.callbacks.treeNodeSelect(this.handleLayoutClickedInTree);
        };

        /**
         * Hands a click of a layout node.
         * @param args The layout node details.
         */
        handleLayoutClickedInTree = (args) => {

            // Boilerplate.
            args.event.preventDefault();
            args.event.stopPropagation();

            // Variables.
            const node = args.node;
            const meta = node.metaData;

            // If this is not a layout node (e.g., if it's a folder), exit early.
            if (node.nodeType !== 'Layout') {
                return;
            }

            // Send notification of selected layout.
            this.$scope.model.chosen({
                id: meta.NodeId,
                name: meta.NodeName,
            });

        };

    }

})();