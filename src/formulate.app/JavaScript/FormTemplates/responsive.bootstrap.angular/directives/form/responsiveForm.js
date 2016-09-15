'use strict';

var angular = require('angular');

var app = angular.module('formulate');

function FormulateController($scope, $element, $http, $q, $window) {
    var self = this;

    // Set reference to injected object to be used in prototype functions
    this.injected = {
        $q: $q,
        $http: $http,
        $scope: $scope,
        $element: $element,
        $window: $window
    };

    // Map Fields for faster access
    this.fieldMap = {};
    this.formData.fields.forEach(function (field) {
        self.fieldMap[field.id] = field;
    });
}

FormulateController.prototype.getFieldById = function (id) {
    return this.fieldMap[id];
};
FormulateController.prototype.submit = function () {
    var self = this;

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
                    'Content-Type': undefined
                },
                transformRequest: function (obj) {

                    // Convert to FormData: http://stackoverflow.com/a/25264008/2052963
                    // This is necessary for file uploads to submit properly via AJAX.
                    var formData = new self
                        .injected
                        .$window
                        .FormData();

                    angular.forEach(obj, function (value, key) {

                        // Skip over null/undefined so they don't get sent as serialized version.
                        if (value !== undefined && value !== null) {
                            formData.append(key, value);
                        }

                    });
                    return formData;

                }
            })
            .then(parseResponse);
    }

    function postSuccess(data) {
        self.injected.$scope.$emit('Formulate.formSubmit.OK', {
            fields: self.fieldModels,
            name: self.formData.name,
            response: data
        });
    }

    function postFailed(message) {
        self.injected.$scope.$emit('Formulate.formSubmit.Failed', {
            fields: self.fieldModels,
            name: self.formData.name,
            message: message
        });
    }
};

function formulate() {
    return {
        restrict: "E",
        replace: true,
        template: '<div class="formulate">' +
                '<formulate-layout layout="ctrl.formData.layout"></formulate-layout>' +
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