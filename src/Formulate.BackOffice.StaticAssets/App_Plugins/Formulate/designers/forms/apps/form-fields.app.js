(function () {
    "use script";

    function FormFieldsAppController($scope, editorService) {
        const services = {
            $scope,
            editorService
        };

        $scope.sortableOptions = {
            axis: "y",
            cursor: "move",
            delay: 100,
            opacity: 0.5
        };

        $scope.actions = {
            addField: addField(services),
            deleteField: deleteField(services),
            editField: editField(services)
        };
    };

    /**
     * Shows the dialog that allows the user to choose a form field to add.
     */
    function addField(services) {
        const { $scope, editorService } = services;

        return function () {

            // This is called when the dialog is closed.
            let closer = () => {
                editorService.close();
            };

            // This is called when a field is chosen.
            let submit = (field) => {
                editorService.close();

                if (field) {
                    $scope.content.fields.push(field);
                }
            };

            // The data sent to the form field chooser.
            var options = {
                title: "Add Field",
                subtitle: "Choose one of the following form fields to add to your form.",
                view: "/app_plugins/formulate/dialogs/form-field/pick-form-field.dialog.html",
                close: closer,
                submit: submit,
                size: 'medium'
            };

            // Open the overlay that displays the fields.
            editorService.open(options);
        };
    };

    /**
     * Deletes the specified field (after confirming with the user).
     * @param field The field to delete.
     */
    function deleteField(services) {
        const { $scope } = services;

        return function (field) {
            // Confirm deletion.
            const name = field.name === null || !field.name.length
                ? "unnamed field"
                : `field, "${field.name}"`;
            const message = `Are you sure you wanted to delete the ${name}?`;
            const response = confirm(message);

            // Delete field?
            if (response) {
                const index = $scope.content.fields.indexOf(field);
                $scope.content.fields.splice(index, 1);
            }
        };
    };



    /**
     * Edit the field.
     * @param field The field to edit.
     */
    function editField(services) {
        const { editorService } = services;

        return (field) => {
            let cloneField = {};
            Utilities.copy(field, cloneField);

            var options = {
                field: cloneField,
                submit: (model) => {
                    if (model) {
                        Utilities.copy(model, field);
                    }

                    editorService.close();
                },
                close: () => {
                    editorService.close();
                },
                view: "/app_plugins/formulate/dialogs/form-field/edit-form-field.dialog.html",
                size: 'medium'
            };

            editorService.open(options);
        };
    };

    angular.module("umbraco").controller("Formulate.BackOffice.FormFieldsAppController", FormFieldsAppController);

})();