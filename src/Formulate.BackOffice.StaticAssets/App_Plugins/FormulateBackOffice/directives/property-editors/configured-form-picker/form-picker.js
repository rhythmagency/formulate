//TODO: Implement.
class ConfiguredFormPicker {
    $scope;

    constructor() {
    }

    controller = ($scope, overlayService) => {
        this.retainProperties({
            $scope,
            overlayService,
        });
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
            //TODO: Need to implement this view. Refer to the layout chooser.
            view: "/app_plugins/formulatebackoffice/directives/overlays/configured-form-chooser/form-chooser-overlay.html",
            hideSubmitButton: true,
            close: closer,
            chosen: chosen,
        };

        // Open the overlay that displays the forms.
        this.overlayService.open(data);

    };
}

const picker = new ConfiguredFormPicker();
picker.registerController();