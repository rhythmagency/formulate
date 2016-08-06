/**
 * The "formulateLayoutBasic" directive allows the user to create
 * a form layout that uses multiple rows and multiple columns. It
 * only supports a few row configurations (i.e., 1-4 columns).
 */

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

// Controller.
function controller($scope, formulateForms, dialogService) {

    // Variables.
    var services = {
        $scope: $scope,
        formulateForms: formulateForms,
        dialogService: dialogService
    };

    // Scope variables.
    $scope.editRows = false;
    $scope.allFields = [];
    $scope.rows = angular.copy($scope.data.rows || []);
    $scope.fieldSortableOptions = getFieldSortableOptions();
    $scope.rowsSortableOptions = getRowSortableOptions();
    $scope.getCellClass = getGetCellClass();
    $scope.deleteRow = getDeleteRow(services);
    $scope.addRow = getAddRow(services);
    $scope.pickForm = getPickForm(services);
    $scope.getSampleCellClasses = getGetSampleCellClasses();
    $scope.toggleCell = getToggleCell();
    $scope.sampleCells = getSampleCells();

    // Initialize watchers.
    watchEditRowsSetting(services);
    watchRowChanges(services);

    // Initialize.
    if ($scope.data.formId) {
        refreshFields($scope.data.formId, services);
    } else {
        replenishFields($scope);
    }

}

// Returns the function that returns a class for a cell in the
// specified row.
function getGetCellClass() {
    return function (row) {
        return "span" + (12 / row.cells.length).toString();
    };
}

// Returns the 12 sample cells.
function getSampleCells() {
    var cells = [];
    for (var i = 0; i < 12; i++) {
        cells.push({
            active: false
        });
    }
    return cells;
}

// Returns the function that returns a classes for the specified cell.
function getGetSampleCellClasses() {
    return function (cells, index) {
        var cell = cells[index];
        var activeClass = (cell.active ? "formulate-sample-cell--active" : "formulate-sample-cell--inactive");
        var nextCell = index + 1 < cells.length ? cells[index + 1] : null;
        var adjacentClass = nextCell && nextCell.active ? "formulate-sample-cell--adjacent" : null;
        var firstClass = index == 0 ? "formulate-sample-cell--first" : null;
        var lastClass = index == cells.length - 1 ? "formulate-sample-cell--last" : null;
        var classes = [activeClass, adjacentClass, firstClass, lastClass];
        var validClasses = [];
        for (var i = 0; i < classes.length; i++) {
            if (classes[i]) {
                validClasses.push(classes[i]);
            }
        }
        return validClasses.join(" ");
    };
}

// Returns the function that toggles the specified cell's active state.
function getToggleCell() {
    return function (cells, index) {
        var cell = cells[index];
        if (index > 0) {
            cell.active = !cell.active;
        }
    };
}

// Returns the options to use when sorting fields.
function getFieldSortableOptions() {
    return {
        cursor: "move",
        connectWith: ".formulate-cell",
        tolerance: "pointer",
        items: ".formulate-cell-field",
        opacity: 0.5
    };
}

// Returns the options to use when sorting rows.
function getRowSortableOptions() {
    return {
        cursor: "move",
        tolerance: "pointer",
        axis: "y",
        opacity: 0.5,
        disabled: true
    };
}

// Returns a function that deletes the row at the specified index.
function getDeleteRow(services) {
    var $scope = services.$scope;
    return function (index) {
        $scope.rows.splice(index, 1);
        replenishFields($scope);
    };
}

// Returns a function that adds a row with the specified number
// of columns.
function getAddRow(services) {
    var $scope = services.$scope;
    return function(columnCount) {
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
}

// Returns the function that allows the user to pick a form.
function getPickForm(services) {
    var dialogService = services.dialogService;
    var $scope = services.$scope;
    return function() {
        dialogService.open({
            template: "../App_Plugins/formulate/dialogs/pickForm.html",
            show: true,
            callback: function(data) {

                // If no form was chosen, empty all fields.
                if (!data.length) {
                    $scope.data.formId = null;
                    $scope.allFields = [];
                    replenishFields($scope);
                    return;
                }

                // Update fields.
                var formId = data[0];
                $scope.data.formId = formId;
                refreshFields(formId, services);

            }
        });
    };
}

// Watches the "Edit Rows" setting to enable/disable sorting.
function watchEditRowsSetting(services) {
    var $scope = services.$scope;
    $scope.$watch("editRows", function (newValue) {
        $scope.rowsSortableOptions.disabled = !newValue;
    });
}

// Watches for changes to the rows and updates the saved
// data correspondingly.
function watchRowChanges(services) {
    var $scope = services.$scope;
    $scope.$watch("rows", function (newValue) {
        refreshDataRows(newValue, services);
    }, true);
}

// Copies from "rows" to create "data.rows"
// (the latter is stored to the server).
function refreshDataRows(rows, services) {
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

// Ensures that no old fields exist in the rows and that all
// new fields exist in a row. This is called whenever the
// rows or the fields change.
function replenishFields($scope) {

    // Variables.
    var i, field, row, cell;
    var fields = {};

    // Place all fields in an associative array keyed by ID.
    for (i = 0; i < $scope.allFields.length; i++) {
        field = $scope.allFields[i];
        fields[field.id] = field;
    }

    // Remove or replenish the fields in the rows.
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

                    // This field no longer exists in the layout,
                    // so remove it from the rows.
                    cell.fields.splice(k, 1);

                }
            }
        }
    }

    // Aggregate all fields which aren't yet assigned to a row.
    var unassignedFields = [];
    for (var key in fields) {
        if (fields.hasOwnProperty(key)) {
            unassignedFields.push(fields[key]);
        }
    }

    // Construct a new row containing all of the unassigned fields.
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

// Gets info about the form based on its ID,
// then updates the fields.
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
            replenishFields($scope);
        });
}