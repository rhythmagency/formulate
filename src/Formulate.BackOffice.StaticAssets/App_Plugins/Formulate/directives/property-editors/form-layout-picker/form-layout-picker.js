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
        $scope,
        editorService,
        formulateVars,
        $http) => {

        // Retain the injected parameters on this object.
        const services = {
            $scope,
            editorService,
            formulateVars,
            $http,
        };

        // Attach this object to the scope so it's accessible by the view.
        $scope.actions = {
            clearFormLayout: this.clearFormLayout(services),
            pickFormLayout: this.pickFormLayout(services)
        };

        // Initialize the property editor.
        this.init(services);
    };

    /**
     * Initializes the property editor.
     */
    init = (services) => {
        const { formulateVars, $http, $scope } = services;

        $scope.loaded = false;
        $scope.vm = {};
        if ($scope.model.value && $scope.model.value.id) {
            const baseUrl = formulateVars.Forms.Get;
            const url = `${baseUrl}?id=${$scope.model.value.id}`;

            // Get the form layout name.
            $http.get(url).then((response) => {
                const { name, id } = response.data;

                $scope.vm.name = name;
                $scope.vm.id = id;
                $scope.loaded = true;
            },
            (response) => {
                $scope.loaded = true;
            });

        }
        else {
            $scope.loaded = true;
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
    clearFormLayout = (services) => {
        const { $scope } = services;

        return function () {
            $scope.vm = {};
            $scope.model.value = {};
        };

    };

    /**
     * Opens the form layout chooser dialog.
     */
    pickFormLayout = (services) => {
        const { editorService, $scope } = services;

        return function () {

            // This is called when the dialog is closed.
            const closer = () => {
                editorService.close();
            };

            // This is called when a form is chosen.
            const chosen = ({ id, name }) => {
                if (!id) {
                    return;
                }
                $scope.model.value = {
                    id,
                };
                $scope.vm.id = id;
                $scope.vm.name = name;
                editorService.close();
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
            editorService.treePicker(config);
        };
    };

}

// Initialize.
const picker = new FormLayoutPicker();
picker.registerController();