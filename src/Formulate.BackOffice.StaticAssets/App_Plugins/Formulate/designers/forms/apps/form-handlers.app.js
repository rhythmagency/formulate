(function () {
    "use script";

    function FormHandlersAppController($scope, editorService) {
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
            addHandler: addHandler(services),
            deleteHandler: deleteHandler(services),
            editHandler: editHandler(services),
            toggleHandlerEnabled: toggleHandlerEnabled
        };
    };

    /**
     * Shows the dialog that allows the user to choose a form field to add.
     */
    function addHandler(services) {
        const { $scope, editorService } = services;

        return function () {
            // This is called when the dialog is closed.
            let closer = () => {
                editorService.close();
            };

            // This is called when a handler is chosen.
            let submit = (handler) => {
                editorService.close();

                if (handler) {
                    $scope.content.handlers.push(handler);
                }
            };

            const fieldsCopy = [];

            Utilities.copy($scope.content.fields, fieldsCopy);

            // The data sent to the form handler chooser.
            var options = {
                title: "Add Handler",
                subtitle: "Choose one of the following form handlers to add to your form.",
                view: "/app_plugins/formulate/dialogs/form-handler/pick-form-handler.dialog.html",
                fields: fieldsCopy,
                close: closer,
                submit: submit,
                size: 'medium'
            };

            // Open the overlay that displays the fields.
            editorService.open(options);
        };
    };

    /**
     * Deletes the specified handler (after confirming with the user).
     * @param handler The handler to delete.
     */
    function deleteHandler(services) {
        const { $scope } = services;

        return function (handler) {
            // Confirm deletion.
            const name = handler.name === null || !handler.name.length
                ? "unnamed handler"
                : `handler, "${handler.name}"`;
            const message = `Are you sure you wanted to delete the ${name}?`;
            const response = confirm(message);

            // Delete handler?
            if (response) {
                const index = $scope.content.handlers.indexOf(handler);
                $scope.content.handlers.splice(index, 1);
            }
        };
    };

    /**
      * Edit the handler.
      * @param handler The handler to edit.
      */
    function editHandler(services) {
        const { $scope, editorService } = services;

        return (handler) => {
            const fieldsCopy = [];
            let cloneHandler = {};
            Utilities.copy(handler, cloneHandler);
            Utilities.copy($scope.content.fields, fieldsCopy);

            var options = {
                handler: cloneHandler,
                fields: fieldsCopy,
                submit: (model) => {
                    if (model) {
                        Utilities.copy(model, handler);
                    }

                    editorService.close();
                },
                close: () => {
                    editorService.close();
                },
                view: "/app_plugins/formulate/dialogs/form-handler/edit-form-handler.dialog.html",
                size: 'medium'
            };

            editorService.open(options);
        };
    };

    /**
     * Toggles the enabled state of the handler.
     * @param handler The handler to toggle.
     */
    toggleHandlerEnabled = (handler) => {
        handler.enabled = !handler.enabled;
    };

    angular.module("umbraco").controller("Formulate.BackOffice.FormHandlersAppController", FormHandlersAppController);

})();