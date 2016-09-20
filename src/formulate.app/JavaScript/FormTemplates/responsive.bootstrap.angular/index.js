'use strict';

var angular = require('angular');
var builtinTypes = require('./builtin-types');

// Create app
var app = angular.module('formulate', ['ngMessages']);

// Include all directives.
require('./services/**/*.js', { mode: 'expand' });
require('./directives/**/*.js', { mode: 'expand' });

// Register built-in types
app.config(['FormulateFieldTypesProvider', function (FormulateFieldTypesProvider) {
    FormulateFieldTypesProvider
        .register('select', builtinTypes.createSelectField, true)
        .register('radio-list', builtinTypes.createRadioButtonListField, true)
        .register('button', builtinTypes.createButtonField, false)
        .register('textarea', builtinTypes.createTextAreaField, true)
        .register('checkbox', builtinTypes.createCheckboxField, false)
        .register('upload', builtinTypes.createUploadField, false)
        .register('rich-text', builtinTypes.createRichTextField, false)
        .setDefault(builtinTypes.createTextField, true);
}]);

