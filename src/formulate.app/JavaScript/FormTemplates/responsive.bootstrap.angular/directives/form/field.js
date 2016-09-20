'use strict';

var angular = require('angular');

var app = angular.module('formulate');

/**
 * @description Field controller
 * @param field
 * @constructor
 */
function FormulateFieldController(field) {
    angular.extend(this, field);
}
app.controller('FormulateFieldController', FormulateFieldController);

/**
 * @description Angular directive that is responsible for rendering input field
 * @param $compile
 * @param $controller
 * @returns {{restrict: string, replace: boolean, template: string, require: string, link: link, scope: {}}}
 */
function formulateField($compile, $controller, FormulateFieldTypes) {
    function link(scope, el, attr, ctrls) {
        var ctrl     = ctrls[0];
        var formCtrl = ctrls[1];

        var field = ctrl.getFieldById(attr.fieldId);
        var elField = FormulateFieldTypes.createField(field);

        var inputs = {
            $scope: scope,
            $element: elField,
            field: field
        };

        // Attach main controller
        scope.ctrl = ctrl;
        scope.formCtrl = formCtrl;

        $controller('FormulateFieldController as fieldCtrl', inputs);

        el.append(elField);

        $compile(elField)(scope);
    }

    return {
        restrict: "E",
        replace: true,
        template: '<div></div>',
        require: ['^^formulateResponsiveForm', '^^form'],

        link: link,
        scope: {}
    };
}
app.directive('formulateField', formulateField);