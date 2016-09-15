'use strict';

var angular = require('angular');
var lodash = require('lodash');

var app = angular.module('formulate');
var jsonData = require('./mock.json');

function JsonMockController() {
    lodash.assign(this, jsonData);
}

function jsonMock() {
    /*jslint unparam: true */
    function link(scope, el, attr, ctrl, transclude) {
        transclude(scope, el.replaceWith.bind(el));
    }

    return {
        restrict: 'E',
        replace: true,
        template: '<div></div>',

        controller: JsonMockController,
        controllerAs: "ctrl",
        bindToController: true,

        transclude: true,
        link: link,
        scope: {}
    };
}
app.directive('jsonMock', jsonMock);