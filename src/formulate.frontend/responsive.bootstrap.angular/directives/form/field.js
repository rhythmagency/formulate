'use strict';

// Dependencies.
var angular = require('angular');

// Angular app.
var app = angular.module('formulate');

// Variables.
var snakeCaseUpperRegex = /[A-Z]+/g,
    snakeCaseInvalidCharRegex = /([^a-zA-Z0-9])+/g;

/**
 * Converts the specified text to a snake case version.
 * @param text The text (e.g., "helloWorld" or "hello world").
 * @param separator The separator, which will be a hyphen if unspecified.
 * @returns {string} The snake case version of the text.
 */
function snakeCase(text, separator) {
    if (!text) {
        return text;
    }
    separator = separator || '-';
    text = text.replace(snakeCaseUpperRegex, function(letter) {
        return separator + letter;
    }).toLowerCase();
    text = text.replace(snakeCaseInvalidCharRegex, function () {
        return separator;
    });
    if (text.substring(0, 1) === separator) {
        text = text.substring(1);
    }
    if (text.substring(text.length -1, text.length) === separator) {
        text = text.substring(0, text.length - 1);
    }
    return text;
}

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
 * Returns the class to use for the specified field.
 * @param alias The field alias (e.g., "firstName" or "first name").
 * @returns {string} The field class (e.g., "formulate__field-alias--first-name").
 */
FormulateFieldController.prototype.getFieldClass =  function (alias) {
    alias = snakeCase(alias, '-') || 'unspecified_alias';
    return 'formulate__field-alias--' + alias;
};

/**
 * @description Angular directive that is responsible for rendering input field
 * @param $compile
 * @param $controller
 *
 * @returns DDO (Directive Definition Object)
 */
function formulateField($compile, $controller, FormulateFieldTypes) {
    function link(scope, el, attr, ctrls) {
        var ctrl     = ctrls[0];
        var formCtrl = ctrls[1];

        var field = ctrl.getFieldById(attr.fieldId);
        var elField = FormulateFieldTypes.createField(field);

        // Exclude null/hidden fields
        if (elField !== null) {
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
    }

    return {
        restrict: "E",
        replace: true,
        template: '<div ng-class="[\'formulate__field--\' + fieldCtrl.fieldType, fieldCtrl.getFieldClass(fieldCtrl.alias)]"></div>',
        require: ['^^formulateResponsiveForm', '^^form'],

        link: link,
        scope: {}
    };
}
app.directive('formulateField', formulateField);