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
    $scope.rows = [
        {
            cells: [
                {
                    fields: []
                },
                {
                    fields: []
                },
                {
                    fields: []
                },
                {
                    fields: []
                }
            ]
        },
        {
            cells: [
                {
                    fields: []
                },
                {
                    fields: []
                },
                {
                    fields: []
                }
            ]
        },
        {
            cells: [
                {
                    fields: []
                },
                {
                    fields: []
                }
            ]
        },
        {
            cells: [
                {
                    fields: [
                        {
                            name: "First Name"
                        },
                        {
                            name: "Last Name"
                        },
                        {
                            name: "Email Address"
                        }
                    ]
                }
            ]
        }
    ];
    $scope.getCellClass = function (row) {
        return "span" + (12 / row.cells.length).toString();
    };
    $scope.addField = function(cell) {
        //TODO: Allow user to select field to add.
        alert("This button doesn't do anything yet.");
    };

    $scope.sortableOptions = {
        cursor: "move",
        connectWith: ".formulate-cell",
        tolerance: "pointer"
    };
}