'use strict';

var angular = require('angular');

var app = angular.module('formulate');

////////////////////////////////////////
// Functions that create html el
function fieldId(field) {
    return 'field-' + field.randomId;
}

function addLabel(elWrap, field) {
    /*global document */
    var contents = angular.element(document.createTextNode(field.label));
    var label = angular.element('<label class="formulate__field-label"></label>');

    label.append(contents);
    label.attr('for', fieldId(field));

    elWrap.append(label);
}

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

            elMessage.attr('ng-message', validation.id);
            elMessage.text(validation.configuration.message);
            elMessage.addClass('formulate__error-msg');

            elMessages.append(elMessage);
        });
    }

    return elMessages;
}

function setGlobalInputAttributes(field, el, options) {
    options = angular.extend({
        // When set to true, this element will get the "form-control" class.
        formControl: true,
        disableAutocomplete: true
    }, options);

    el.attr('id', fieldId(field));
    el.attr('name', 'field_' + field.id);
    el.attr('aria-label', field.label);
    el.attr('ng-model', 'ctrl.fieldModels[\'' + field.id + '\']');
    el.attr('formulate-validation', true);

    el.addClass('formulate__control');

    if (options.formControl) {
        el.addClass('form-control');
    }

    if (options.disableAutocomplete) {
        el.attr('autocomplete', 'off');
    }

    return el;
}

function createSelectField(field) {
    var el = angular.element('<select></select>');

    el.attr('ng-options', "item.value as item.label for item in fieldCtrl.configuration.items");

    // Create empty option that serves as placeholder
    el.append('<option value="">' + field.label + '</option>');

    return setGlobalInputAttributes(field, el, {
        disableAutocomplete: false
    });
}

function createUploadField(field) {
    var el = angular.element('<formulate-file-upload></formulate-file-upload>');

    el.attr('button-label', JSON.stringify(field.label));
    var fieldModelValue = 'ctrl.fieldModels[' + JSON.stringify(field.id) + ']';
    el.attr('field-model', fieldModelValue);

    return el;
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

function createRichTextField(field) {
    var markup = $(field.configuration.text);
    return markup;
}

function createButtonField(field) {
    var el = angular.element('<button></button>');
    var span = angular.element('<span></span>');

    span.text(field.label);
    el.append(span);
    el.attr('type', 'submit');
    el.attr('button-kind', field.configuration.buttonKind);

    el.addClass('formulate__btn formulate__btn--submit btn btn-default');

    return el;
}

function createCheckboxField(field) {
    var container = angular.element('<div></div>');
    var label = angular.element('<label class="formulate__checkbox-label"></label>');
    var el = angular.element('<input type="checkbox" value="1" />');
    var span = angular.element('<span></span>');

    span.text(field.label);

    label.append(setGlobalInputAttributes(field, el, {
        formControl: false,
        disableAutocomplete: false
    }));
    label.append(span);

    container.addClass('formulate__checkbox');
    container.append(label);

    return container;
}

function createField(field) {
    var elWrap = angular.element('<div></div>');

    elWrap.addClass('formulate__group');
    elWrap.addClass('form-group');

    switch (field.fieldType) {
    case 'select':
        addLabel(elWrap, field);
        elWrap.addClass('formulate__group--select');
        elWrap.append(createSelectField(field));
        break;

    case 'button':
        elWrap.append(createButtonField(field));
        break;

    case 'textarea':
        addLabel(elWrap, field);
        elWrap.append(createTextAreaField(field));
        break;

    case 'checkbox':
        elWrap.append(createCheckboxField(field));
        break;

    case 'upload':
        elWrap.append(createUploadField(field));
        break;

    case 'rich-text':
        elWrap.append(createRichTextField(field));
        break;

    default:
        addLabel(elWrap, field);
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