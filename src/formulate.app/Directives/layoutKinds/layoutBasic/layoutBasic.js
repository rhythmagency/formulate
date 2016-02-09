//TODO: Need to refactor and comment this file.

// Variables.
var app = angular.module("umbraco");

// Associate directive/controller.
app.controller("formulate.layoutBasic", controller);
app.directive("formulateLayoutBasic", directive);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        replace: true,
        template: formulateDirectives.get(
            "layoutKinds/layoutBasic/layoutBasic.html"),
        scope: {
            data: "="
        },
        controller: "formulate.layoutBasic"
    };
}

function controller($scope, formulateForms, dialogService) {

    var services = {
        $scope: $scope,
        formulateForms: formulateForms,
        dialogService: dialogService
    };
    $scope.editRows = false;
    $scope.allFields = [];
    $scope.rows = angular.copy($scope.data.rows || []);
    $scope.getCellClass = function (row) {
        return "span" + (12 / row.cells.length).toString();
    };

    $scope.sortableOptions = {
        cursor: "move",
        connectWith: ".formulate-cell",
        tolerance: "pointer",
        items: ".formulate-cell-field",
        opacity: 0.5
    };

    $scope.rowsSortableOptions = {
        cursor: "move",
        tolerance: "pointer",
        axis: "y",
        opacity: 0.5,
        disabled: true
    };

    $scope.$watch("editRows", function (newValue, oldValue) {
        $scope.rowsSortableOptions.disabled = !newValue;
    });

    $scope.deleteRow = function (index) {
        $scope.rows.splice(index, 1);
        checkFields($scope);
    };

    $scope.addRow = function(columnCount) {
        var columns = [];
        for (var i = 0; i < columnCount; i++) {
            columns.push({
                fields: []
            });
        }
        $scope.rows.push({
            cells: columns
        });
    };

    $scope.pickForm = getPickForm(services);

    $scope.$watch("rows", function (newValue, oldValue) {
        updateDataRows(newValue, services);
    }, true);

    if ($scope.data.formId) {
        refreshFields($scope.data.formId, services);
    } else {
        checkFields($scope);
    }

}

function updateDataRows(rows, services) {
    var dataRows = [];
    for (var i = 0; i < rows.length; i++) {
        var row = rows[i];
        var dataRow = {
            cells: []
        };
        dataRows.push(dataRow);
        for (var j = 0; j < row.cells.length; j++) {
            var cell = row.cells[j];
            var dataCell = {
                fields: []
            };
            dataRow.cells.push(dataCell);
            for (var k = 0; k < cell.fields.length; k++) {
                var field = cell.fields[k];
                var dataField = {
                    id: field.id
                };
                dataCell.fields.push(dataField);
            }
        }
    }
    services.$scope.data.rows = dataRows;
}

function checkFields($scope) {
    var i, field, row, cell;
    var fields = {};
    for (i = 0; i < $scope.allFields.length; i++) {
        field = $scope.allFields[i];
        fields[field.id] = field;
    }
    for (i = 0; i < $scope.rows.length; i++) {
        row = $scope.rows[i];
        for (var j = 0; j < row.cells.length; j++) {
            cell = row.cells[j];
            for (var k = cell.fields.length - 1; k >= 0; k--) {
                field = cell.fields[k];
                if (fields.hasOwnProperty(field.id)) {
                    // Copy fresh data over.
                    field.name = fields[field.id].name;

                    // Remove field as it's accounted for.
                    delete fields[field.id];
                } else {
                    cell.fields.splice(k, 1);
                }
            }
        }
    }
    var unassignedFields = [];
    for (var key in fields) {
        if (fields.hasOwnProperty(key)) {
            unassignedFields.push(fields[key]);
        }
    }
    if (unassignedFields.length > 0) {
        row = {
            cells: [
                {
                    fields: unassignedFields
                }
            ]
        };
        $scope.rows.push(row);
    }
}

function getPickForm(services) {
    var dialogService = services.dialogService;
    var formulateForms = services.formulateForms;
    var $scope = services.$scope;
    return function() {
        dialogService.open({
            template: "../App_Plugins/formulate/dialogs/pickForm.html",
            show: true,
            callback: function(data) {

                if (!data.length) {
                    $scope.data.formId = null;
                    $scope.allFields = [];
                    checkFields($scope);
                    return;
                }

                $scope.data.formId = data[0];

                refreshFields(data[0], services);

            }
        });
    };
}

// Get info about form based its ID, then update the fields.
function refreshFields(formId, services) {
    var formulateForms = services.formulateForms;
    var $scope = services.$scope;
    formulateForms.getFormInfo(formId)
        .then(function (formData) {
            $scope.allFields = formData.fields
                .map(function (item) {
                    return {
                        id: item.id,
                        name: item.name
                    };
                });
            checkFields($scope);
        });
}