'use strict';

var angular = require('angular');

// Create app
var app = angular.module('formulate', ['ngMessages']);

// Include all directives.
require('./directives/**/*.js', { mode: 'expand' });