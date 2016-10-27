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

/**
 *
 * Returns a callback which in turn returns a bool that indicates render state of a given field (referenced by fieldId)
 *
 * Sample conditional data structure:
 * Field A controls display of fields B, C, D;
 * "conditionalControls": [
 *     {
 *         "fieldId": "A",
 *         "show": "Yes",
 *         "fields": ["B", "C", "D"]
 *     }
 * ]
 *
 * When field(Id: A) === "Yes" then callback for B, C, D will return true; otherwise false
 *
 *
 *
 * @param conditionalControls
 * @param fieldModels
 * @returns {show}
 */
function showConditionalFields(conditionalControls, fieldModels) {
    var condMap = {};
    var fieldMap = {};

    function show(id) {
        return function () {
            return condMap[id].show === fieldModels[id];
        };
    }

    function execCallback(fn) {
        return fn();
    }

    function addConditionalControl(item) {
        condMap[item.fieldId] = item;

        function pushFieldCallback(fieldId) {
            if (!fieldMap.hasOwnProperty(fieldId)) {
                fieldMap[fieldId] = [];
            }

            fieldMap[fieldId].push(show(item.fieldId));
        }

        item.fields.forEach(pushFieldCallback);
    }

    if (angular.isArray(conditionalControls)) {
        conditionalControls.forEach(addConditionalControl);
    }

    /**
     * Returns bool whether to show input control (field)
     */
    return function (fieldId) {
        if (fieldMap.hasOwnProperty(fieldId)) {
            return fieldMap[fieldId].every(execCallback);
        }

        return true;
    };
}

function FormulateController($rootScope, $scope, $element, $window, FormulateSubmitService) {
    var self = this;

    // Set reference to injected object to be used in prototype functions
    this.injected = {
        $rootScope: $rootScope,
        $scope: $scope,
        $element: $element,
        $window: $window,
        FormulateSubmitService: FormulateSubmitService
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

    // Show conditional fields
    this.showField = showConditionalFields(this.formData.conditionalControls, this.fieldModels);

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
        self.showField = null;
    }

    $scope.$on('$destroy', onDestroy);
}

FormulateController.prototype.getFieldById = function (id) {
    return this.fieldMap[id];
};

FormulateController.prototype.submit = function () {
    var invalidEl;
    var formEl = this.injected.$element.find('form');
    var formCtrl = formEl.controller('form');

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

    } else {
        invalidEl = formEl.find(':input.ng-invalid');

        // Scroll to first invalid field
        if (invalidEl.length > 0) {
            invalidEl[0].focus();
        }
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
                    '<formulate-rows></formulate-rows>' +
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
