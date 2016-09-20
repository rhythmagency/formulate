'use strict';

var angular = require('angular');

////////////////////////////////////////
// Functions that create html el
function fieldId(field) {
    return 'field-' + field.randomId;
}
function fieldName(field, prefix) {
    return (prefix || 'field-name-') + field.randomId;
}

function setGlobalInputAttributes(field, el, options) {
    options = angular.extend({
        // When set to true, this element will get the "form-control" class.
        formControl: true,
        disableAutocomplete: true,
        bindToFieldModel: true
    }, options);

    el.attr('id', fieldId(field));
    el.attr('name', 'field_' + field.id);
    el.attr('aria-label', field.label);
    el.addClass('formulate__control');

    if (options.bindToFieldModel) {
        el.attr('ng-model', 'ctrl.fieldModels[\'' + field.id + '\']');
        el.attr('formulate-validation', true);
    }

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
module.exports.createSelectField = createSelectField;


function createRadioButtonListField(field) {
    var el = angular.element('<div></div>');
    var widgetLabel = angular.element('<label class="formulate__field-label" ng-bind="fieldCtrl.label"></label>');

    var wrapper = angular.element('<div class="radio" ng-repeat="item in fieldCtrl.configuration.items"></div>');
    var label = angular.element('<label></label>');
    var input = angular.element('<input type="radio" ng-value="item.value" />');
    var span = angular.element('<span ng-bind="item.label"></span>');

    // Append input/text span.
    label.append(input);
    label.append(span);
    wrapper.append(label);

    input.attr('name', 'field_' + field.id);
    input.attr('ng-model', 'ctrl.fieldModels[\'' + field.id + '\']');
    input.attr('formulate-validation', true);

    el.append(widgetLabel);
    el.append(wrapper);

    return el;
}
module.exports.createRadioButtonListField = createRadioButtonListField;


function createUploadField(field) {
    var el = angular.element('<formulate-file-upload></formulate-file-upload>');

    el.attr('button-label', JSON.stringify(field.label));
    var fieldModelValue = 'ctrl.fieldModels[' + JSON.stringify(field.id) + ']';
    el.attr('field-model', fieldModelValue);

    return el;
}
module.exports.createUploadField = createUploadField;


function createTextField(field) {
    var el = angular.element('<input type="text" />');

    el.attr('placeholder', field.label);

    return setGlobalInputAttributes(field, el);
}
module.exports.createTextField = createTextField;


function createTextAreaField(field) {
    var el = angular.element('<textarea></textarea>');

    el.attr('placeholder', field.label);

    return setGlobalInputAttributes(field, el);
}
module.exports.createTextAreaField = createTextAreaField;


function createRichTextField(field) {
    return field.configuration.text;
}
module.exports.createRichTextField = createRichTextField;


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
module.exports.createButtonField = createButtonField;


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
module.exports.createCheckboxField = createCheckboxField;

function createNullField() {
    return null;
}
module.exports.createNullField = createNullField;

