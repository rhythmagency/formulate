'use strict';

var angular = require('angular');

var app = angular.module('formulate');

/**
 * @description regular expressing validator for angularjs ngModel $validator object
 *
 *
 * @param validation
 * @returns {Function} accepts value to validate
 *
 */
function regexValidation(validation) {
    var regex = new RegExp(validation.configuration.pattern);

    return function (value) {
        return regex.test(value || "");
    };
}

function mandatoryValidation() {
    return function (value) {
        return !!value;
    };
}

function validationFactory(validation) {
    switch (validation.validationType) {
    case 'regex':
        return regexValidation(validation);
    default:
        return mandatoryValidation();
    }
}

function formulateValidation() {
    /*jslint unparam: true */
    function link(scope, el, attrs, ctrls) {
        var ngModelCtrl = ctrls[0];
        var formCtrl = ctrls[1];
        var fieldCtrl = scope.fieldCtrl;

        function setValidation(validation) {
            ngModelCtrl.$validators[validation.id] = validationFactory(validation);
        }

        // Setup validations
        if (angular.isArray(fieldCtrl.validations)) {
            fieldCtrl.validations.forEach(setValidation);
        }

        // Register form control
        formCtrl.$addControl(ngModelCtrl);
    }

    return {
        restrict: 'A',
        require: ['ngModel', '^^form'],
        link: link
    };
}
app.directive('formulateValidation', formulateValidation);