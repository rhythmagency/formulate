/**
 * The class for the configured form picker property editor, which allows
 * a user to pick a configured form.
 */
class ConfiguredFormPicker {

    // Properties.
    $scope;
    overlayService;
    editorService;
    formulateEntityResource;
    $http;

    /**
     * The controller function that gets called by Angular.
     */
    controller = (
        $scope,
        overlayService,
        editorService,
        formulateEntityResource,
        $http) => {

        // Retain the injected parameters on this object.
        this.retainProperties({
            $scope,
            overlayService,
            editorService,
            formulateEntityResource,
            $http,
        });

        // Attach this object to the scope so it's accessible by the view.
        $scope.events = this;

        // Set the form name.
        this.getFormName();

    };

    /**
     * Sets the name of the form.
     */
    getFormName = () => {

        // Validate the data (exit early if no ID present).
        const id = this.$scope.model.value.id;
        if (!id) {
            return;
        }

        // Prepare the request URL.
        const baseUrl = Umbraco.Sys.ServerVariables.formulate["configuredForms.Get"];
        const url = `${baseUrl}?id=${id}`;

        // Get the configured form name.
        this.$http.get(url).then(({ data: { entity: { name } } }) => {
            this.$scope.formName = name;
        });

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
            .controller('Formulate.ConfiguredFormPicker', this.controller);
    };

    /**
     * Deselects the currently selected configured form.
     */
    clearPickedForm = () => {
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