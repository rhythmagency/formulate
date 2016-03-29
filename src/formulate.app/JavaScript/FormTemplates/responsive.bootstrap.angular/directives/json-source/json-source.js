'use strict';

var angular = require('angular');
var lodash = require('lodash');

var app = angular.module('formulate');

function JsonSourceCtrl($attrs) {
    // Parse Json Source
    lodash.assign(this, JSON.parse($attrs.source));
}

function jsonSource() {
    /*jslint unparam: true */
    function link(scope, el, attr, ctrl, transclude) {
        transclude(scope, el.replaceWith.bind(el));
    }

    return {
        restrict: 'E',
        replace: true,
        template: '<div></div>',

        controller: JsonSourceCtrl,
        controllerAs: "ctrl",
        bindToController: true,

        transclude: true,
        link: link,
        scope: {}
    };
}
app.directive('formulateJsonSource', jsonSource);