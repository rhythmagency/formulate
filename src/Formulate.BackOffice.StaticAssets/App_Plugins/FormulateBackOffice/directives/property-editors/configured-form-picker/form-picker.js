//TODO: Implement.
class ConfiguredFormPicker {
    $scope;

    constructor() {
    }

    controller = ($scope, overlayService, editorService) => {
        $scope.events = this;
        this.retainProperties({
            $scope,
            overlayService,
            editorService,
        });
        this.getFormName();
    };

    getFormName = () => {
        //TODO: Get name of configured form.
        this.$scope.formName = null;
    };

    retainProperties = (properties) => {
        for (const [key, value] of Object.entries(properties)) {
            this[key] = value;
        }
    };

    registerController = () => {
        angular
            .module('umbraco')
            .controller('Formulate.ConfiguredFormPicker', this.controller);
    };

    /**
     * Opens the form chooser dialog.
     */
    pickConfiguredForm = () => {

        // This is called when the dialog is closed.
        const closer = () => {
            this.overlayService.close();
        };

        // This is called when a form is chosen.
        const chosen = ({ id, name }) => {
            if (!id) {
                return;
            }
            this.overlayService.close();
            this.$scope.model.value = {
                id,
            };
            this.$scope.formName = name;
            this.editorService.close();
        };

        // The configuration for the tree that allows the configured
        // form to be chosen.
        const config = {
            section: 'formulate',
            treeAlias: 'forms',
            multiPicker: false,
            entityType: 'ConfiguredForm',
            filter: (node) => {
                return node.nodeType !== 'ConfiguredForm';
            },
            filterCssClass: 'not-allowed',
            select: chosen,
            submit: closer,
            close: closer,
        };

        // Open the overlay that displays the forms.
        this.editorService.treePicker(config);

    };

}

// Initialize.
const picker = new ConfiguredFormPicker();
picker.registerController();