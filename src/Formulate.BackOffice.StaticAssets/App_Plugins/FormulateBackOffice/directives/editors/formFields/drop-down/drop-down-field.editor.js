"use strict";

(function () {
    // Directive.
    function directive() {
        return {
            restrict: "E",
            replace: true,
            templateUrl: "/App_Plugins/FormulateBackOffice/directives/editors/formFields/drop-down/drop-down-field.editor.html",
            controller: controller,
            scope: {
                configuration: "="
            }
        };
    }

    // Controller.
    function controller($scope, editorService, formulateDataValues) {
        $scope.configuration = $scope.configuration || {};

        // Variables.
        var services = {
            $scope: $scope,
            editorService: editorService,
            formulateDataValues: formulateDataValues
        };

        // Set scope variables.
        $scope.pickDataValue = performPickDataValue(services);
        $scope.removeDataValue = performRemoveDataValue(services);

        // Refresh the data value info.
        refreshDataValue(services);

    }

    // Allows the user to pick their data value.
    function performPickDataValue(services) {
        var $scope = services.$scope;
        var editorService = services.editorService;
        return function () {
            const config = {
                section: 'formulate',
                treeAlias: 'datavalues',
                multiPicker: false,
                entityType: 'datavalue',
                filter: (node) => {
                    return node.nodeType === 'Folder';
                },
                filterCssClass: 'not-allowed',
                select: (node) => {
                    if (node) {
                        $scope.configuration.dataValue = node.id;
                    }

                    refreshDataValue(services);

                    editorService.close();
                },
                submit: () => {
                    editorService.close();
                },
                close: () => {
                    editorService.close();
                }
            };

            editorService.treePicker(config);

        };
    }

    function performRemoveDataValue(services) {
        var $scope = services.$scope;

        return function () {
            $scope.configuration.dataValue = undefined;
            $scope.dataValue = {};
        }
    }

    // Update the scope with info about the data value based on its ID.
    function refreshDataValue(services) {

        // Variables.
        var $scope = services.$scope;
        var formulateDataValues = services.formulateDataValues;

        // Return early if there is no data value to get info about.
        if (!$scope.configuration.dataValue) {
            return;
        }

        // Get info about data value.
        formulateDataValues.getDataValueInfo($scope.configuration.dataValue)
            .then(function (data) {

                if (data) {
                    $scope.dataValue = {
                        id: data.id,
                        name: data.name
                    };
                }
            });

    }

    // Associate directive/controller.
    angular.module("umbraco").directive("formulateDropDownField", directive);

})();