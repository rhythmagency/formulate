﻿// Variables.
var app = angular.module("umbraco");

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        replace: true,
        template: formulateDirectives.get(
            "fields/radioButtonListField/radioButtonListField.html"),
        controller: Controller,
        scope: {
            configuration: "="
        }
    };
}
app.directive("formulateRadioButtonListField", directive);

// Controller.
function Controller($scope, dialogService, formulateDataValues) {

    // Variables.
    var services = {
        $scope: $scope,
        dialogService: dialogService,
        formulateDataValues: formulateDataValues
    };

    // Set scope variables.
    $scope.pickDataValue = getPickDataValue(services);
    $scope.orientations = {
        values: [
            {
                value: "Horizontal",
                label: "Horizontal"
            },
            {
                value: "Vertical",
                label: "Vertical"
            }
        ]
    };

    // Refresh the data value info.
    refreshDataValue(services);

}

// Allows the user to pick their data value.
function getPickDataValue(services) {
    var $scope = services.$scope;
    var dialogService = services.dialogService;
    return function() {
        dialogService.open({
            template: "../App_Plugins/formulate/dialogs/pickDataValue.html",
            show: true,
            callback: function(data) {

                // If no data value was picked, set data value to null.
                if (!data.length) {
                    $scope.dataValue = null;
                    $scope.configuration.dataValue = null;
                    return;
                }

                // Store data value to configuration.
                $scope.configuration.dataValue = data[0];
                refreshDataValue(services);

            }
        });
    };
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
    formulateDataValues.getDataValuesInfo([$scope.configuration.dataValue])
        .then(function (dataValues) {
            if (dataValues.length) {
                $scope.dataValue = {
                    id: dataValues[0].dataValueId,
                    name: dataValues[0].name
                };
            }
        });

}