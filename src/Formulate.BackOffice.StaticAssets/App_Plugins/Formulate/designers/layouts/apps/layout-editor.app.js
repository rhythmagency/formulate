(function () {
    "use strict";
    function LayoutEditorAppController(
        $scope,
        formulateEntityResource,
        formulateLayouts,
        formulateTypeDefinitionResource) {

        const services = {
            $scope,
            formulateEntityResource,
            formulateLayouts,
            formulateTypeDefinitionResource
        };

        initializeLayout(services);
    };


    /**
     * Initializes the templates on the scope.
     * @param formulateTypeDefinitionResource The resource that can be used to
     *  fetch templates.
     * @param scope The scope to set the templates on.
     */
    function initializeTemplates(services) {

        const { $scope, formulateTypeDefinitionResource } = services;

        $scope.templates = [];
        return formulateTypeDefinitionResource.getTemplateDefinitions()
            .then((data) => {
                $scope.templates = data;
            });
    }

    /**
     * Initializes the layout.
     */
    function initializeLayout(services) {
        const { $scope, formulateEntityResource, formulateTypeDefinitionResource } = services;

        // Disable layout saving until the data is populated.
        $scope.loading = true;
        $scope.saveButtonState = 'init';

        // Get the layout info.


        if (Utilities.isArray($scope.content.path)) {
            const formId = $scope.content.path.at(-2);

            const getFormOptions = {
                entityType: 'form',
                treeType: "Forms",
                id: formId
            };

            formulateEntityResource.getOrScaffold(getFormOptions).then(
                function (formEntity) {
                    $scope.form = formEntity;
                });
        }

        initializeTemplates(services).then(() => {
            $scope.loading = false;
        });
    };

    angular.module("umbraco").controller("Formulate.BackOffice.LayoutEditorAppController", LayoutEditorAppController);

})();