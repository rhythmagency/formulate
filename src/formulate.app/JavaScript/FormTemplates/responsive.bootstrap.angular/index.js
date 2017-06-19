'use strict';

var angular = require('angular');
var ngRecaptcha = require('angular-recaptcha');
var builtinTypes = require('./builtin-types');

// Create app
var app = angular.module('formulate', ['ngMessages', 'vcRecaptcha']);

// Include all services and directives.
require('./services/**/*.js', { mode: 'expand' });
require('./directives/**/*.js', { mode: 'expand' });

// Register built-in types
app.config(['FormulateFieldTypesProvider', function (FormulateFieldTypesProvider) {
    FormulateFieldTypesProvider
        .register('select', builtinTypes.createSelectField)
        .register('radio-list', builtinTypes.createRadioListField, {optionalLabel: false})
        .register('extended-radio-list', builtinTypes.createExtendedRadioListField, {optionalLabel: false})
        .register('checkbox-list', builtinTypes.createCheckboxListField, {optionalLabel: false})
        .register('button', builtinTypes.createButtonField, {optionalLabel: false})
        .register('textarea', builtinTypes.createTextAreaField)
        .register('checkbox', builtinTypes.createCheckboxField, {optionalLabel: false})
        .register('upload', builtinTypes.createUploadField, {optionalLabel: false})
        .register('header', builtinTypes.createHeaderField, {optionalLabel: false})
        .register('rich-text', builtinTypes.createRichTextField, {optionalLabel: false})
        .register('hidden', builtinTypes.createNullField, {optionalLabel: false})
        .register('DateField', builtinTypes.createDateField)
        .register('recaptcha', builtinTypes.createRecaptchaField, {optionallabel: false})
        .setDefault(builtinTypes.createTextField);
}]);