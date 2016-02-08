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

//TODO: ...
function controller($scope) {
    $scope.editRows = false;
    $scope.allFields = [
        {
            id: "sdgjlefjl",
            name: "First Name"
        },
        {
            id: "rlll0oijf",
            name: "Last Name"
        },
        {
            id: "gljlj44eskll",
            name: "Email Address"
        }
    ];
    $scope.rows = [];
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

    checkFields($scope);
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
            for (var k = 0; k < cell.fields.length; k++) {
                field = cell.fields[k];
                if (fields.hasOwnProperty(field.id)) {
                    delete fields[field.id];
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
        }
        $scope.rows.push(row);
    }
}