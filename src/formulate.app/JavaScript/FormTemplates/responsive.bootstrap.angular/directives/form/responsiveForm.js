'use strict';

var angular = require('angular');

var app = angular.module('formulate');

/**
 * @description Generate unique form names
 */
var genFormName = (function () {
    var counter = 0;

    return function () {
        counter += 1;

        return 'form' + counter;
    };
}());

function FormulateController($rootScope, $element) {
    var self = this;

    // Set reference to injected object to be used in prototype functions
    this.injected = {
        $rootScope: $rootScope,
        $element: $element
    };

    // set unique form name
    this.formName = genFormName();

    // Map Fields for faster access
    this.fieldMap = {};
    this.formData.fields.forEach(function (field) {
        self.fieldMap[field.id] = field;
    });

    // ng-model of fields
    this.fieldModels = {};
}

FormulateController.prototype.getFieldById = function (id) {
    return this.fieldMap[id];
};
FormulateController.prototype.submit = function () {
    var formCtrl = this
        .injected
        .$element
        .find('form')
        .controller('form');

    if (formCtrl.$valid) {
        this.injected.$rootScope.$broadcast('Formulate.formSubmit', this.fieldModels, this.formName);
    }
};

function formulate() {
    return {
        restrict: "E",
        replace: true,
        template: '<div class="formulate-container">' +
            '<form data-ng-submit="ctrl.submit(ctrl.formName)" class="form" name="{{ctrl.formName}}">' +
                '<formulate-rows rows="ctrl.formData.rows"></formulate-rows>' +
            '</form>' +
            '</div>',

        controller: FormulateController,
        controllerAs: "ctrl",
        bindToController: true,

        scope: {
            formData: '='
        }
    };
}
app.directive('formulateResponsiveForm', formulate);