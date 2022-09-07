/**
 * The class for the form picker property editor, which allows
 * a user to pick a form layout.
 */
class FormLayoutPicker {

    // Properties.
    $scope;
    editorService;
    $http;
    formulateVars;

    /**
     * The controller function that gets called by Angular.
     */
    controller = (
        retainProperties,
        $scope,
        editorService,
        formulateVars,
        $http) => {

        // Retain the injected parameters on this object.
        retainProperties({
            $scope,
            editorService,
            formulateVars,
            $http,
        }, this);

        // Attach this object to the scope so it's accessible by the view.
        $scope.events = this;

        // Initialize the property editor.
        this.init();
    };

    /**
     * Initializes the property editor.
     */
    init = () => {
        this.$scope.loaded = false;
        this.$scope.vm = {};
        if (this.$scope.model.value && this.$scope.model.value.id) {
            const baseUrl = this.formulateVars.Forms.Get;
            const url = `${baseUrl}?id=${this.$scope.model.value.id}`;

            // Get the form layout name.
            this.$http.get(url).then(response => {
                const { name, id } = response.data;

                this.$scope.vm.name = name;
                this.$scope.vm.id = id;
                this.$scope.loaded = true;
            });

        }
        else {
            this.$scope.loaded = true;
        }
    };

    /**
     * Registers this controller with Angular.
     */
    registerController = () => {
        angular
            .module('umbraco')
            .controller('Formulate.PropertyEditors.FormLayoutPicker', this.controller);
    };

    /**
     * Deselects the currently selected form layout.
     */
    clearFormLayout = () => {
        this.$scope.vm = {};
        this.$scope.model.value = {};
    };

    /**
     * Opens the form layout chooser dialog.
     */
    pickFormLayout = () => {

        // This is called when the dialog is closed.
        const closer = () => {
            this.editorService.close();
        };

        // This is called when a form is chosen.
        const chosen = ({ id, name }) => {
            if (!id) {
                return;
            }
            this.$scope.model.value = {
                id,
            };
            this.$scope.vm.id = id;
            this.$scope.vm.name = name;
            this.editorService.close();
        };

        // The configuration for the tree that allows the
        // form layout to be chosen.
        const config = {
            section: 'formulate',
            treeAlias: 'forms',
            multiPicker: false,
            entityType: 'Layout',
            filter: (node) => {
                return node.nodeType !== 'Layout';
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
const picker = new FormLayoutPicker();
picker.registerController();