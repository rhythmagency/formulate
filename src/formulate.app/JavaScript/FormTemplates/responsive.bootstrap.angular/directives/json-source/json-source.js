'use strict';

var angular = require('angular');
var lodash = require('lodash');

var app = angular.module('formulate');

function JsonSourceCtrl($scope, $attrs) {
    var data = JSON.parse($attrs.source);

    // Pass found formData up to parent scopes
    $scope.$emit('Formulate.newForm', data);

    // Parse Json Source
    lodash.assign(this, data);
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