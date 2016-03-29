'use strict';

var angular = require('angular');

var app = angular.module('formulate');

////////////////////////////////////////
// Functions that create html el
function addNgMessages(field) {
    var elMessages = null;
    var fieldName = 'formCtrl.field_' + field.id;

    if (angular.isArray(field.validations) && field.validations.length > 0) {
        elMessages = angular.element('<div></div>');
        elMessages.attr('ng-messages', fieldName + '.$error');

        // Show only when the following states apply
        elMessages.attr('ng-show', 'formCtrl.$submitted || ' + fieldName + '.$touched');

        elMessages.attr('role', 'alert');

        field.validations.forEach(function (validation) {
            var elMessage = angular.element('<div></div>');

            elMessage.attr('ng-message', validation.alias);
            elMessage.text(validation.configuration.message);
            elMessage.addClass('formulate__error-msg');

            elMessages.append(elMessage);
        });
    }

    return elMessages;
}

function setGlobalInputAttributes(field, el) {
    el.attr('id', field.id);
    el.attr('name', 'field_' + field.id);
    el.attr('aria-label', field.label);
    el.attr('ng-model', 'ctrl.fieldModels[\'' + field.id + '\']');
    el.attr('formulate-validation', true);

    el.addClass('formulate__control');

    return el;
}

function createSelectField(field) {
    var el = angular.element('<select></select>');

    el.attr('ng-options', "item.value as item.label for item in fieldCtrl.configuration.items");

    // Create empty option that serves as placeholder
    el.append('<option value="">' + field.label + '</option>');

    return setGlobalInputAttributes(field, el);
}

function createTextField(field) {
    var el = angular.element('<input type="text" />');

    el.attr('placeholder', field.label);

    return setGlobalInputAttributes(field, el);
}

function createTextAreaField(field) {
    var el = angular.element('<textarea></textarea>');

    el.attr('placeholder', field.label);

    return setGlobalInputAttributes(field, el);
}

function createSubmitField(field) {
    var el = angular.element('<button></button>');

    el.attr('type', 'Submit');
    el.text(field.label);

    el.addClass('formulate__btn formulate__btn--submit');

    return el;
}

function createField(field) {
    var elWrap = angular.element('<div></div>');

    elWrap.addClass('formulate__group');

    switch (field.fieldType) {
    case 'select':
        elWrap.addClass('formulate__group--select');
        elWrap.append(createSelectField(field));
        break;

    case 'submit':
        elWrap.append(createSubmitField(field));
        break;

    case 'textarea':
        elWrap.append(createTextAreaField(field));
        break;

    default:
        elWrap.append(createTextField(field));
    }

    // append error messages below the element
    elWrap.append(addNgMessages(field));

    return elWrap;
}

////////////////////////////////////////
// Angular Section
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
function formulateField($compile, $controller) {
    function link(scope, el, attr, ctrls) {
        var ctrl     = ctrls[0];
        var formCtrl = ctrls[1];

        var field = ctrl.getFieldById(attr.fieldId);
        var elField = createField(field);

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