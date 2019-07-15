// Associate controller.
app.controller("formulate.editors.configuredFormPicker", controller);

// Controller.
function controller($scope, formulateConfiguredForms, editorService) {

    // Variables.
    var services = {
        $scope: $scope,
        formulateConfiguredForms: formulateConfiguredForms,
        editorService: editorService
    };

    // Scope functions.
    $scope.pickForm = getPickForm(services);

    // Initialize.
    initialize(services);

}

// Initializes the scope variables.
function initialize(services) {
    var $scope = services.$scope;
    if (!$scope.model.value || !$scope.model.value.id) {
        $scope.model.value = {
            id: null
        };
    }
    var id = $scope.model.value.id
    if (id) {
        refreshForm(id, services);
    }
}

// Returns the function that opens a dialog to allow the user to pick a form.
function getPickForm(services) {
    var editorService = services.editorService;
    var $scope = services.$scope;
    return function () {
        var configureForms  = $scope.model.value.id ? [$scope.model.value.id] : [];

        editorService.open({
            configureForms: configureForms,
            view: "../App_Plugins/formulate/dialogs/pickConfiguredForm.html",
            close: function () {
                editorService.close();
            },
            submit: function (data) {

                // If no form was chosen, deselect form.
                if (!data.length) {
                    $scope.model.value.id = null;
                    editorService.close();
                    return;
                }

                // Store form ID.
                var conFormId = data[0];
                $scope.model.value.id = conFormId;

                // Update form info.
                refreshForm(conFormId, services);
                editorService.close();
            }
        });
    };
}

// Gets info about the form based on its ID, then updates the info on the scope.
function refreshForm(conFormId, services) {
    var $scope = services.$scope;
    $scope.formName = null;
    var formulateConfiguredForms = services.formulateConfiguredForms;
    formulateConfiguredForms.getConfiguredFormInfo(conFormId)
        .then(function (data) {
            $scope.formName = data.name;
        });
}