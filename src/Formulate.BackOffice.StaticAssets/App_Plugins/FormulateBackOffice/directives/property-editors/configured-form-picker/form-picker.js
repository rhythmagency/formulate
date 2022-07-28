/**
 * The class for the configured form picker property editor, which allows
 * a user to pick a configured form.
 */
class ConfiguredFormPicker {

    // Properties.
    $scope;
    editorService;
    $http;
    formulateVars;

    /**
     * The controller function that gets called by Angular.
     */
    controller = (
        $scope,
        editorService,
        formulateVars,
        $http) => {

        // Retain the injected parameters on this object.
        this.retainProperties({
            $scope,
            editorService,
            formulateVars,
            $http,
        });

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
        if (this.$scope.model.value?.id) {
            const baseUrl = this.formulateVars["configuredForms.Get"];
            const url = `${baseUrl}?id=${this.$scope.model.value.id}`;

            // Get the configured form name.
            this.$http.get(url).then(({ data: { entity: { name } } }) => {
                this.$scope.vm.name = name;
                this.$scope.loaded = true;
            });

        }
        else {
            this.$scope.loaded = true;
        }
    };

    /**
     * Stores the specified properties on this object.
     * @param {any} properties The object containing the properties to store.
     */
    retainProperties = (properties) => {
        for (const [key, value] of Object.entries(properties)) {
            this[key] = value;
        }
    };

    /**
     * Registers this controller with Angular.
     */
    registerController = () => {
        angular
            .module('umbraco')
            .controller('Formulate.PropertyEditors.ConfiguredFormPicker', this.controller);
    };

    /**
     * Deselects the currently selected configured form.
     */
    clearPickedForm = () => {
        this.$scope.vm = {};
        this.$scope.model.value = {};
    };

    /**
     * Opens the form chooser dialog.
     */
    pickConfiguredForm = () => {

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
            this.$scope.vm.name = name;
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