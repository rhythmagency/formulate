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
            if (node.id === '-1') {
                return;
            }
            if (this.$scope.model.validations.some(x => x === node.id)) {
                node.selected = true;
            }
        }

        /**
         * Hands a click of a validation node.
         * @param args The validation node details.
         */
        handleValidationClickedInTree = (args) => {

            // Boilerplate.
            args.event.preventDefault();
            args.event.stopPropagation();

            // If this is not a validation node (e.g., if it's a folder), exit early.
            if (args.node.nodeType !== 'Validation') {
                return;
            }

            // Toggle the node.
            args.node.selected = !args.node.selected;

            // Either add or remove the selected/deselected validation.
            const validations = this.$scope.model.validations;
            if (args.node.selected) {
                validations.push(args.node.id);
            } else {
                const existingIndex = validations.indexOf(args.node.id);
                if (existingIndex >= 0) {
                    validations.splice(existingIndex, 1);
                }
            }

        };

    }

})();