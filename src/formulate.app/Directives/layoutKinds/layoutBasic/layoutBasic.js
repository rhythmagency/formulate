/**
 * The "formulateLayoutBasic" directive allows the user to create
 * a form layout that uses multiple rows and multiple columns.
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
function controller($scope, formulateForms, editorService, notificationsService) {

    // Variables.
    var services = {
        $scope: $scope,
        formulateForms: formulateForms,
        editorService: editorService,
        notificationsService: notificationsService
    };

    // Scope variables.
    $scope.editRows = false;
    $scope.allFields = [];
    $scope.rows = angular.copy($scope.data.rows || []);
    $scope.unusedFields = [];
    $scope.fieldSortableOptions = getFieldSortableOptions();
    $scope.rowsSortableOptions = getRowSortableOptions();
    $scope.getCellClass = getGetCellClass();
    $scope.deleteRow = getDeleteRow(services);
    $scope.addRow = getAddRow(services);
    $scope.pickForm = getPickForm(services);
    $scope.getSampleCellClasses = getGetSampleCellClasses();
    $scope.toggleCell = getToggleCell();
    $scope.sampleCells = getSampleCells();
    $scope.useField = getUseField(services);
    $scope.toggleEditRows = getToggleEditRows(services);

    // Initialize watchers.
    watchEditRowsSetting(services);
    watchRowChanges(services);

    // Initialize.
    ensureRowExists($scope);
    if ($scope.data.formId) {
        refreshFields($scope.data.formId, services);
    } else {
        replenishFields($scope);
    }

}

// Returns the function that moves a field from the unused collection to
// a cell in the layout.
function getUseField(services) {
    var $scope = services.$scope;
    return function (fieldIndex) {
        var field = $scope.unusedFields.splice(fieldIndex, 1)[0];
        $scope.rows[$scope.rows.length -1].cells[0].fields.push(field);
    };
}

// Returns the function that returns a class for the specified cell
// in the specified row.
function getGetCellClass() {
    return function (row, cell) {

        // If a column span is not present (e.g., for older versions of Formulate),
        // then fallback to the calculated column span.
        var columnSpan = cell.columnSpan  ?  cell.columnSpan : getFallbackColumnSpan(row, cell);
        return "span" + columnSpan.toString();

    };
}

// Deprecated. This will be deleted in a future version of Formulate.
// This calculates a fallback column span when one is not otherwise
// specified (useful for forms created in older versions of Formulate).
function getFallbackColumnSpan(row, cell) {
    return cell.columnSpan || (12 / row.cells.length);
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

// Returns the function that returns the classes for the specified cell.
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

// Returns the function that toggles the "edit rows" option.
function getToggleEditRows(services) {
    var $scope = services.$scope;
    return function () {
        $scope.editRows = !$scope.editRows;
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
    var notificationsService = services.notificationsService;
    return function (index) {
        if ($scope.rows.length > 1) {
            $scope.rows.splice(index, 1);
            replenishFields($scope);
        } else {
            var title = "Unable to Delete Final Row";
            var message = "The layout must contain at least one row.";
            notificationsService.error(title, message);
        }
    };
}

// Returns a function that adds a row based on the specified sample cells.
function getAddRow(services) {
    var $scope = services.$scope;
    return function(sampleCells) {

        // Variables.
        var cells = [];
        var columnSpan = 0;

        // Add a new cell for each active sample cell.
        for (var i = 0; i < sampleCells.length; i++) {
            var sampleCell = sampleCells[i];
            if (sampleCell.active) {
                cells.push({
                    columnSpan: columnSpan,
                    fields: []
                });
                columnSpan = 0;
            }
            columnSpan = columnSpan + 1;
        }

        // Add a final cell to complete the row.
        cells.push({
            columnSpan: columnSpan,
            fields: []
        });

        // Add the new row.
        $scope.rows.push({
            cells: cells
        });

    };
}

// Returns the function that allows the user to pick a form.
function getPickForm(services) {
    var editorService = services.editorService;
    var $scope = services.$scope;
    return function () {
        var forms = $scope.data.formId ? [$scope.data.formId] : [];

        editorService.open({
            forms: forms,
            view: "../App_Plugins/formulate/dialogs/pickForm.html",
            show: true,
            close: function() {
                editorService.close();
            },
            submit: function (data) {
                // If no form was chosen, empty all fields.
                if (!data.length) {
                    $scope.data.formId = null;
                    $scope.allFields = [];
                    replenishFields($scope);
                    editorService.close();
                    return;
                }

                // Update fields.
                var formId = data[0];
                $scope.data.formId = formId;
                refreshFields(formId, services);
                editorService.close();
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
                columnSpan: cell.columnSpan,
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
    var i, j, field, row, cell;
    var fields = {};

    // Place all fields in an associative array keyed by ID.
    for (i = 0; i < $scope.allFields.length; i++) {
        field = $scope.allFields[i];
        fields[field.id] = field;
    }

    // Remove or replenish the fields in the rows.
    for (i = 0; i < $scope.rows.length; i++) {
        row = $scope.rows[i];
        for (j = 0; j < row.cells.length; j++) {
            cell = row.cells[j];
            deleteAndUpdateFields(fields, cell.fields);
        }
    }

    // Remove or replenish the fields in the unused fields section.
    deleteAndUpdateFields(fields, $scope.unusedFields);

    // Aggregate all fields which aren't yet assigned to a row.
    var unassignedFields = [];
    for (var key in fields) {
        if (fields.hasOwnProperty(key)) {
            unassignedFields.push(fields[key]);
        }
    }

    // Add unassigned fields to the unused fields section.
    if (unassignedFields.length > 0) {
        $scope.unusedFields = $scope.unusedFields.concat(unassignedFields);
    }

    // Ensure each cell has a column span.
    ensureFallbackColumnSpans($scope.rows);

}

// Ensure at least one row exists (otherwise, there would be nothing to drop fields into).
function ensureRowExists($scope) {
    if ($scope.rows.length < 1) {
        $scope.rows.push({
            cells: [
                {
                    columnSpan: 12,
                    fields: []
                }
            ]
        });
    }
}

// Deletes and updates fields based on the information known about all fields.
function deleteAndUpdateFields(allFieldsById, fieldsToUpdate) {
    var i, field;
    for (i = fieldsToUpdate.length - 1; i >= 0; i--) {
        field = fieldsToUpdate[i];
        if (allFieldsById.hasOwnProperty(field.id)) {

            // Copy fresh data over.
            field.name = allFieldsById[field.id].name;

            // Remove field as it's accounted for.
            delete allFieldsById[field.id];

        } else {

            // This field no longer exists in the layout,
            // so remove it from the fields to update.
            fieldsToUpdate.splice(i, 1);

        }
    }
}

// Deprecated. This will be deleted in a future version of Formulate.
// Ensure each cell has a column span. This is done because
// older versions of Formulate did not have a column span
// specified for each cell.
function ensureFallbackColumnSpans(rows) {
    var i, row, cell;
    for (i = 0; i < rows.length; i++) {
        row = rows[i];
        for (j = 0; j < row.cells.length; j++) {
            cell = row.cells[j];
            if (!cell.columnSpan) {
                cell.columnSpan = 12 / row.cells.length;
            }
        }
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
                .filter(function (item) {
                    return !item.isServerSideOnly;
                })
                .map(function (item) {
                    return {
                        id: item.id,
                        name: item.name
                    };
                });
            replenishFields($scope);
        });
}