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
            this.overlayService.close();
            this.$scope.model.value = {
                id,
            };
            this.$scope.formName = name;
        };

        // The data sent to the layout chooser.
        const data = {
            title: "Choose Form",
            subtitle: "Choose a form.",
            view: "/app_plugins/formulatebackoffice/directives/overlays/configured-form-chooser/form-chooser-overlay.html",
            hideSubmitButton: true,
            close: closer,
            chosen: chosen,
        };

        // Open the overlay that displays the forms.
        this.overlayService.open(data);

    };

}

// Initialize.
const picker = new ConfiguredFormPicker();
picker.registerController();