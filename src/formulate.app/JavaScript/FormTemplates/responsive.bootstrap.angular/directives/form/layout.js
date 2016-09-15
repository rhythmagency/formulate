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

function FormulateLayoutController($scope, $element) {
    // Set reference to injected object to be used in prototype functions
    this.injected = {
        $scope: $scope,
        $element: $element
    };

    // set unique form name
    this.generatedName = genFormName();

    this.currentLayoutIndex = 0;
}

FormulateLayoutController.prototype.getLayout = function () {
    return this.layout[this.currentLayoutIndex];
};

FormulateLayoutController.prototype.submit = function () {
    var formCtrl = this
        .injected
        .$element
        .find('form')
        .controller('form');

    if (formCtrl.$valid) {
        this.currentLayoutIndex += 1;
    }
};

function formulateLayout() {
    return {
        restrict: "E",
        replace: true,
        template: '<div class="formulate-container">' +
                '<form data-ng-submit="ctrl.submit(ctrl.generatedName)" class="form" name="{{ctrl.generatedName}}">' +
                    '<formulate-rows rows="ctrl.getLayout().rows"></formulate-rows>' +
                '</form>' +
            '</div>',

        controller: FormulateLayoutController,
        controllerAs: "ctrl",
        bindToController: true,

        scope: {
            layout: '='
        }
    };
}
app.directive('formulateLayout', formulateLayout);
