'use strict';

var angular = require('angular');

var app = angular.module('formulate');

function FormulateRowController() {
    return this;
}

FormulateRowController.prototype.getColClass = function (cell) {
    return 'col-md-' + (cell.columns || '12');
};

function formulateRows() {
    return {
        restrict: "E",
        replace: true,
        template: '<div>' +
                '<div class="row ib top" ng-repeat="row in ctrl.rows">' +
                    '<div class="col-xs-12" ng-class="ctrl.getColClass(cell)" ng-repeat="cell in row.cells">' +
                        '<div ng-repeat="field in cell.fields track by filed.id">' +
                            '<formulate-field class="formulate__field" field-id="{{field.id}}"></formulate-field>' +
                        '</div>' +
                    '</div>' +
                '</div>' +
            '</div>',

        controller: FormulateRowController,
        controllerAs: "ctrl",
        bindToController: true,

        scope: {
            rows: '='
        }
    };
}
app.directive('formulateRows', formulateRows);