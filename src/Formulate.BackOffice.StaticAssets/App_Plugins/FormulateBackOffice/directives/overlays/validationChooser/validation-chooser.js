"use strict";

(function () {

    // Variables.
    const app = angular.module("umbraco");

    // Associate directive.
    app.directive("formulateValidationChooser", directive);

    // Directive.
    function directive() {
        return {
            restrict: "E",
            templateUrl: "/app_plugins/formulatebackoffice/directives/overlays/validationchooser/validation-chooser.html",
            controller: controller,
            scope: {
                model: "=",
            }
        };
    }

    // Controller.
    function controller($scope) {
        $scope.events = new ValidationChooserEventHandlers({
            $scope,
        });
    }

    /**
     * The event handlers for the validation chooser.
     */
    class ValidationChooserEventHandlers {

        // Properties.
        $scope;

        /**
         * Constructor.
         * @param services The services the event handlers will need.
         */
        constructor(services) {
            Object.keys(services).forEach((x) => this[x] = services[x]);
            this.$scope.validationTreeApi = {};
        }

        /**
         * Initializes the event handlers once the tree is ready.
         */
        validationTreeInit = () => {
            this.$scope.validationTreeApi.callbacks.treeNodeSelect(this.handleValidationClickedInTree);
            this.$scope.validationTreeApi.callbacks.treeNodeExpanded(this.handleValidationTreeNodeExpanded);
        };

        /**
         * Process tree nodes when they are expanded.
         * @param args The tree node details.
         */
        handleValidationTreeNodeExpanded = (args) => {

            // Select any nodes that correspond to selected validations.
            args.children.forEach(x => {
                this.selectValidationNodeIfSelected(x);
            });

        };

        /**
         * Selects the node if it corresponds to a selected validation.
         * @param node The node to potentially select.
         */
        selectValidationNodeIfSelected = (node) => {
            const meta = node.metaData;
            if (node.id === '-1') {
                return;
            }
            if (this.$scope.model.validations.some(x => x.id === meta.NodeId)) {
                node.selected = true;
            }
        }

        /**
         * Handles a click of a validation node.
         * @param args The validation node details.
         */
        handleValidationClickedInTree = (args) => {

            // Boilerplate.
            args.event.preventDefault();
            args.event.stopPropagation();

            // Variables.
            const node = args.node;
            const meta = node.metaData;

            // If this is not a validation node (e.g., if it's a folder), exit early.
            if (node.nodeType !== 'Validation') {
                return;
            }

            // Toggle the node.
            node.selected = !node.selected;

            // Either add or remove the selected/deselected validation.
            const validations = this.$scope.model.validations;
            if (node.selected) {
                validations.push({
                    id: meta.NodeId,
                    kindId: meta.NodeKindId,
                    name: meta.NodeName,
                });
            } else {
                const existingIndex = validations.findIndex(x => x.id === meta.NodeId);
                if (existingIndex >= 0) {
                    validations.splice(existingIndex, 1);
                }
            }

        };

    }

})();