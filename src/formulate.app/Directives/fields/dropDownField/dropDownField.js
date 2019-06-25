// Variables.
var app = angular.module("umbraco");

// Associate directive/controller.
app.directive("formulateDropDownField", directive);
app.controller("formulate.dropDownField", controller);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        replace: true,
        template: formulateDirectives.get(
            "fields/dropDownField/dropDownField.html"),
        controller: "formulate.dropDownField",
        scope: {
            configuration: "="
        }
    };
}

// Controller.
function controller($scope, editorService, formulateDataValues) {

    // Variables.
    var services = {
        $scope: $scope,
        editorService: editorService,
        formulateDataValues: formulateDataValues
    };

    // Set scope variables.
    $scope.pickDataValue = getPickDataValue(services);

    // Refresh the data value info.
    refreshDataValue(services);

}

// Allows the user to pick their data value.
function getPickDataValue(services) {
    var $scope = services.$scope;
    var editorService = services.editorService;
    return function() {
        var dataValues = $scope.configuration.dataValue ? [$scope.configuration.dataValue] : [];

        editorService.open({
            view: "../App_Plugins/formulate/dialogs/pickDataValue.html",
            dataValues: dataValues,
            close: function () {
                editorService.close();
            },
            submit: function (data) {
                // If no data value was picked, set data value to null.
                if (!data.length) {
                    $scope.dataValue = null;
                    $scope.configuration.dataValue = null;
                    editorService.close();
                    return;
                }

                // Store data value to configuration.
                $scope.configuration.dataValue = data[0];
                refreshDataValue(services);
                editorService.close();
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