'use strict';

var angular = require('angular');
var builtinTypes = require('./builtin-types');

// Create app
var app = angular.module('formulate', ['ngMessages']);

// Include all services and directives.
require('./services/**/*.js', { mode: 'expand' });
require('./directives/**/*.js', { mode: 'expand' });

// Register built-in types
app.config(['FormulateFieldTypesProvider', function (FormulateFieldTypesProvider) {
    FormulateFieldTypesProvider
        .register('select', builtinTypes.createSelectField, true)
        .register('radio-list', builtinTypes.createRadioListField, false)
        .register('extended-radio-list', builtinTypes.createExtendedRadioListField, false)
        .register('checkbox-list', builtinTypes.createCheckboxListField, false)
        .register('button', builtinTypes.createButtonField, false)
        .register('textarea', builtinTypes.createTextAreaField, true)
        .register('checkbox', builtinTypes.createCheckboxField, false)
        .register('upload', builtinTypes.createUploadField, false)
        .register('header', builtinTypes.createHeaderField, false)
        .register('rich-text', builtinTypes.createRichTextField, false)
        .register('hidden', builtinTypes.createNullField, false)
        .register('DateField', builtinTypes.createDateField, true)
        .setDefault(builtinTypes.createTextField, true);
}]);

