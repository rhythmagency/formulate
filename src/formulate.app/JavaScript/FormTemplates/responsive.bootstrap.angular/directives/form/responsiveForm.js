'use strict';

var angular = require('angular');

var app = angular.module('formulate');

/**
 * @description Generate unique form names
 */
var genFormName = (function () {
    var counter = 0;

    return function () {
        counter += 1;

        return 'form' + counter;
    };
}());

function FormulateController($scope, $element, $http, $q) {
    var self = this;

    // Set reference to injected object to be used in prototype functions
    this.injected = {
        $q: $q,
        $http: $http,
        $scope: $scope,
        $element: $element
    };

    // set unique form name
    this.formName = genFormName();

    // Map Fields for faster access
    this.fieldMap = {};
    this.formData.fields.forEach(function (field) {
        self.fieldMap[field.id] = field;
    });

    // ng-model of fields
    this.fieldModels = {};
}

FormulateController.prototype.getFieldById = function (id) {
    return this.fieldMap[id];
};
FormulateController.prototype.submit = function () {
    var self = this;
    var formCtrl = this
        .injected
        .$element
        .find('form')
        .controller('form');

    function parseResponse(response) {
        var deferred = self.injected.$q.defer();

        if (response.data && response.data.Success === true) {
            deferred.resolve(response.data);
        } else {
            deferred.reject(response.message);
        }

        return deferred.promise;
    }

    function submitPost() {
        var data = angular.extend({}, self.formData.payload, self.fieldModels);

        return self
            .injected
            .$http({
                method: 'POST',
                url: self.formData.url,
                data: data,
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                },
                transformRequest: function (obj) {
                    return Object
                        .keys(obj)
                        .map(function (key) {
                            return encodeURIComponent(key) + "=" + encodeURIComponent(obj[key]);
                        })
                        .join('&');
                }
            })
            .then(parseResponse);
    }

    function postSuccess(data) {
        self.injected.$scope.$emit('Formulate.formSubmit.OK', self.fieldModels, self.formName, data);
    }

    function postFailed(message) {
        self.injected.$scope.$emit('Formulate.formSubmit.Failed', self.fieldModels, self.formName, message);
    }

    if (formCtrl.$valid) {
        submitPost().then(postSuccess, postFailed);
    }
};

function formulate() {
    return {
        restrict: "E",
        replace: true,
        template: '<div class="formulate-container">' +
            '<form data-ng-submit="ctrl.submit(ctrl.formName)" class="form" name="{{ctrl.formName}}">' +
            '<formulate-rows rows="ctrl.formData.rows"></formulate-rows>' +
            '</form>' +
            '</div>',

        controller: FormulateController,
        controllerAs: "ctrl",
        bindToController: true,

        scope: {
            formData: '='
        }
    };
}
app.directive('formulateResponsiveForm', formulate);