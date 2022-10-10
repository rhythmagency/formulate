"use strict";

(function () {
    // Directive.
    function directive() {
        return {
            restrict: "E",
            templateUrl: "/app_plugins/formulate/editors/common/dataValues-picker.html",
            controller: controller,
            scope: {
                model: "="
            }
        };
    }

    // Controller.
    function controller($scope, editorService, formulateIds, formulateDataValues) {
        const services = {
            $scope: $scope,
            editorService: editorService,
            formulateDataValues: formulateDataValues,
            formulateIds: formulateIds
        }

        $scope.pickDataValues = performPickDataValues(services);
        $scope.removeDataValues = performRemoveDataValues(services);

        // Refresh the data value info.
        refreshDataValues(services);
    }

    // Allows the user to pick their data value.
    function performPickDataValues(services) {
        const { editorService, $scope } = services;

        return function () {
            const config = {
                section: 'formulate',
                treeAlias: 'dataValues',
                multiPicker: false,
                entityType: 'dataValues',
                filter: (node) => {
                    return node.nodeType === 'Folder';
                },
                filterCssClass: 'not-allowed',
                select: (node) => {
                    if (node) {
                        $scope.model = node.id;
                    }

                    refreshDataValues(services);

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

    function performRemoveDataValues(services) {
        var $scope = services.$scope;

        return function () {
            $scope.model = undefined;
            $scope.dataValues = {};
        }
    }

    // Update the scope with info about the data value based on its ID.
    function refreshDataValues(services) {
        // Variables.
        const { formulateDataValues, formulateIds, $scope } = services;

        // Return early if there is no data value to get info about.
        if (formulateIds.isEmpty($scope.model)) {
            return;
        }

        // Get info about data value.
        formulateDataValues.getDataValueInfo($scope.model)
            .then(function (data) {

                if (data) {
                    $scope.dataValues = {
                        id: data.id,
                        name: data.name
                    };
                }
            });
    }

    // Associate directive/controller.
    angular.module("umbraco").directive("formulateDataValuesPicker", directive);

})();
