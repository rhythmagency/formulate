'use strict';

var angular = require('angular');

var app = angular.module('formulate');

/**
 * Service responsible for ajax-post
 *
 * @constructor
 */
function FormulateSubmitService($rootScope, $http, $q, $window) {
    ////////////////////////////////////////
    // Handle Post
    function submitPost(data) {
        return $http({
            method: 'POST',
            url: data.url,
            data: data.postData,
            headers: {
                'Content-Type': undefined
            },
            transformRequest: function (obj) {
                // Convert to FormData: http://stackoverflow.com/a/25264008/2052963
                // This is necessary for file uploads to submit properly via AJAX.
                var formData = new $window.FormData();

                angular.forEach(obj, function (value, key) {
                    if (angular.isArray(value)) {
                        value.forEach(function (itemVal) {
                            formData.append(key, itemVal);
                        });

                        // Skip over null/undefined so they don't get sent as serialized version.
                    } else if (value !== undefined && value !== null) {
                        formData.append(key, value);
                    }
                });

                return formData;
            }
        });
    }

    function parseResponse(response) {
        var deferred = $q.defer();

        if (response.data && response.data.Success === true) {
            deferred.resolve(response);
        } else {
            deferred.reject(response.message);
        }

        return deferred.promise;
    }

    this.post = function (data) {
        function postSuccess(response) {
            $rootScope.$broadcast('Formulate.formSubmit.OK', {
                fields: data.postData,
                name: data.name,
                response: response.data
            });
        }

        function postFailed(message) {
            $rootScope.$broadcast('Formulate.formSubmit.Failed', {
                fields: data.postData,
                name: data.name,
                message: message
            });
        }

        submitPost(data)
            .then(parseResponse)
            .then(postSuccess, postFailed);
    };
}
app.service('FormulateSubmitService', FormulateSubmitService);

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

function FormulateController($rootScope, $scope, $element, $window, FormulateSubmitService) {
    var self = this;

    // Set reference to injected object to be used in prototype functions
    this.injected = {
        $rootScope: $rootScope,
        $scope: $scope,
        $element: $element,
        $window: $window
    };

    // set unique form name
    this.generatedName = genFormName();

    function handleSubmitEvent($event, data) {
        if ($event.targetScope === $scope && !$event.defaultPrevented) {
            FormulateSubmitService.post(data);
        }
    }

    // Map Fields for faster access
    this.fieldMap = {};
    this.formData.fields.forEach(function (field) {
        self.fieldMap[field.id] = field;
    });

    // ng-model of fields
    this.fieldModels = {};

    // Set initial values.
    this.formData.fields.forEach(function (field) {
        var initialValue = field.initialValue;

        if (field.initialValue !== undefined && field.initialValue !== null) {
            self.fieldModels[field.id] = initialValue;
        }
    });

    ////////////////////////////////////////
    // Bind later - this is to allow consumers to intercept event on $rootScope
    var removeSubmitHandler = null;
    var timeout = $window.setTimeout(function () {
        removeSubmitHandler = $rootScope.$on('Formulate.submit', handleSubmitEvent);
    }, 40);

    // Clean up pending requests
    function onDestroy() {
        if (removeSubmitHandler) {
            removeSubmitHandler();
        }
        $window.clearTimeout(timeout);

        self.injected = null;
        self.fieldMap = null;
        self.formData = null;
        self.fieldModels = null;
    }

    $scope.$on('$destroy', onDestroy);
}

FormulateController.prototype.getFieldById = function (id) {
    return this.fieldMap[id];
};

FormulateController.prototype.submit = function () {
    var formCtrl = this
        .injected
        .$element
        .find('form')
        .controller('form');

    if (formCtrl.$valid) {
        var data = {
            name: this.formData.name,
            url: this.formData.url,
            postData: angular.extend({}, this.formData.payload, this.fieldModels)
        };

        this
            .injected
            .$scope
            .$emit('Formulate.submit', data);
    }
};

/**
 *
 * @param $event
 * @param buttonKind
 */
FormulateController.prototype.buttonClicked = function (buttonKind) {
    if (buttonKind !== null) {
        this.injected.$scope.$emit('Formulate.buttonClicked', buttonKind);
    }
};

function formulate() {
    return {
        restrict: "E",
        replace: true,
        template: '<div class="formulate-container">' +
            '<form data-ng-submit="ctrl.submit(ctrl.generatedName)" class="form" name="{{ctrl.generatedName}}">' +
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
