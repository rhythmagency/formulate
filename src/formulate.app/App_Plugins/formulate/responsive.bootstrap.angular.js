(function e(t,n,r){function s(o,u){if(!n[o]){if(!t[o]){var a=typeof require=="function"&&require;if(!u&&a)return a(o,!0);if(i)return i(o,!0);var f=new Error("Cannot find module '"+o+"'");throw f.code="MODULE_NOT_FOUND",f}var l=n[o]={exports:{}};t[o][0].call(l.exports,function(e){var n=t[o][1][e];return s(n?n:e)},l,l.exports,e,t,n,r)}return n[o].exports}var i=typeof require=="function"&&require;for(var o=0;o<r.length;o++)s(r[o]);return s})({1:[function(require,module,exports){
(function (global){
'use strict';

var angular = (typeof window !== "undefined" ? window['angular'] : typeof global !== "undefined" ? global['angular'] : null);

var app = angular.module('formulate');

////////////////////////////////////////
// Functions that create html el
function addNgMessages(field) {
    var elMessages = null;
    var fieldName = 'formCtrl.field_' + field.id;

    if (angular.isArray(field.validations) && field.validations.length > 0) {
        elMessages = angular.element('<div></div>');
        elMessages.attr('ng-messages', fieldName + '.$error');

        // Show only when the following states apply
        elMessages.attr('ng-show', 'formCtrl.$submitted || ' + fieldName + '.$touched');

        elMessages.attr('role', 'alert');

        field.validations.forEach(function (validation) {
            var elMessage = angular.element('<div></div>');

            elMessage.attr('ng-message', validation.alias);
            elMessage.text(validation.configuration.message);
            elMessage.addClass('formulate__error-msg');

            elMessages.append(elMessage);
        });
    }

    return elMessages;
}

function setGlobalInputAttributes(field, el) {
    el.attr('id', field.id);
    el.attr('name', 'field_' + field.id);
    el.attr('aria-label', field.label);
    el.attr('ng-model', 'ctrl.fieldModels[\'' + field.id + '\']');
    el.attr('formulate-validation', true);

    el.addClass('formulate__control');

    return el;
}

function createSelectField(field) {
    var el = angular.element('<select></select>');

    el.attr('ng-options', "item.value as item.label for item in fieldCtrl.configuration.items");

    // Create empty option that serves as placeholder
    el.append('<option value="">' + field.label + '</option>');

    return setGlobalInputAttributes(field, el);
}

function createTextField(field) {
    var el = angular.element('<input type="text" />');

    el.attr('placeholder', field.label);

    return setGlobalInputAttributes(field, el);
}

function createTextAreaField(field) {
    var el = angular.element('<textarea></textarea>');

    el.attr('placeholder', field.label);

    return setGlobalInputAttributes(field, el);
}

function createSubmitField(field) {
    var el = angular.element('<button></button>');

    el.attr('type', 'Submit');
    el.text(field.label);

    el.addClass('formulate__btn formulate__btn--submit');

    return el;
}

function createField(field) {
    var elWrap = angular.element('<div></div>');

    elWrap.addClass('formulate__group');

    switch (field.fieldType) {
    case 'select':
        elWrap.addClass('formulate__group--select');
        elWrap.append(createSelectField(field));
        break;

    case 'submit':
        elWrap.append(createSubmitField(field));
        break;

    case 'textarea':
        elWrap.append(createTextAreaField(field));
        break;

    default:
        elWrap.append(createTextField(field));
    }

    // append error messages below the element
    elWrap.append(addNgMessages(field));

    return elWrap;
}

////////////////////////////////////////
// Angular Section
/**
 * @description Field controller
 * @param field
 * @constructor
 */
function FormulateFieldController(field) {
    angular.extend(this, field);
}
app.controller('FormulateFieldController', FormulateFieldController);

/**
 * @description Angular directive that is responsible for rendering input field
 * @param $compile
 * @param $controller
 * @returns {{restrict: string, replace: boolean, template: string, require: string, link: link, scope: {}}}
 */
function formulateField($compile, $controller) {
    function link(scope, el, attr, ctrls) {
        var ctrl     = ctrls[0];
        var formCtrl = ctrls[1];

        var field = ctrl.getFieldById(attr.fieldId);
        var elField = createField(field);

        var inputs = {
            $scope: scope,
            $element: elField,
            field: field
        };

        // Attach main controller
        scope.ctrl = ctrl;
        scope.formCtrl = formCtrl;

        $controller('FormulateFieldController as fieldCtrl', inputs);

        el.append(elField);

        $compile(elField)(scope);
    }

    return {
        restrict: "E",
        replace: true,
        template: '<div></div>',
        require: ['^^formulateResponsiveForm', '^^form'],

        link: link,
        scope: {}
    };
}
app.directive('formulateField', formulateField);
}).call(this,typeof global !== "undefined" ? global : typeof self !== "undefined" ? self : typeof window !== "undefined" ? window : {})
},{}],2:[function(require,module,exports){
(function (global){
'use strict';

var angular = (typeof window !== "undefined" ? window['angular'] : typeof global !== "undefined" ? global['angular'] : null);

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

function FormulateController($rootScope, $element) {
    var self = this;

    // Set reference to injected object to be used in prototype functions
    this.injected = {
        $rootScope: $rootScope,
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
    var formCtrl = this
        .injected
        .$element
        .find('form')
        .controller('form');

    if (formCtrl.$valid) {
        this.injected.$rootScope.$broadcast('Formulate.formSubmit', this.fieldModels, this.formName);
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
}).call(this,typeof global !== "undefined" ? global : typeof self !== "undefined" ? self : typeof window !== "undefined" ? window : {})
},{}],3:[function(require,module,exports){
(function (global){
'use strict';

var angular = (typeof window !== "undefined" ? window['angular'] : typeof global !== "undefined" ? global['angular'] : null);

var app = angular.module('formulate');

function FormulateRowController() {
    return this;
}

FormulateRowController.prototype.getColClass = function (cell) {
    return 'col-md-' + (cell.columns || '12');
};

function formulateRows() {
    return {
        restrict: "E",
        replace: true,
        template: '<div>' +
                '<div class="row ib top" ng-repeat="row in ctrl.rows">' +
                    '<div class="col-xs-12" ng-class="ctrl.getColClass(cell)" ng-repeat="cell in row.cells">' +
                        '<div ng-repeat="field in cell.fields track by filed.id">' +
                            '<formulate-field class="formulate__field" field-id="{{field.id}}"></formulate-field>' +
                        '</div>' +
                    '</div>' +
                '</div>' +
            '</div>',

        controller: FormulateRowController,
        controllerAs: "ctrl",
        bindToController: true,

        scope: {
            rows: '='
        }
    };
}
app.directive('formulateRows', formulateRows);
}).call(this,typeof global !== "undefined" ? global : typeof self !== "undefined" ? self : typeof window !== "undefined" ? window : {})
},{}],4:[function(require,module,exports){
(function (global){
'use strict';

var angular = (typeof window !== "undefined" ? window['angular'] : typeof global !== "undefined" ? global['angular'] : null);

var app = angular.module('formulate');

/**
 * @description regular expressing validator for angularjs ngModel $validator object
 *
 *
 * @param validation
 * @returns {Function} accepts value to validate
 *
 */
function regexValidation(validation) {
    var regex = new RegExp(validation.configuration.pattern);

    return function (value) {
        return regex.test(value || "");
    };
}

function validationFactory(validation) {
    // If we get other types of validation, then this
    // will need to go into a switch statement
    return regexValidation(validation);
}

function formulateValidation() {
    /*jslint unparam: true */
    function link(scope, el, attrs, ctrls) {
        var ngModelCtrl = ctrls[0];
        var formCtrl = ctrls[1];
        var fieldCtrl = scope.fieldCtrl;

        function setValidation(validation) {
            ngModelCtrl.$validators[validation.alias] = validationFactory(validation);
        }

        // Setup validations
        if (angular.isArray(fieldCtrl.validations)) {
            fieldCtrl.validations.forEach(setValidation);
        }

        // Register form control
        formCtrl.$addControl(ngModelCtrl);
    }

    return {
        restrict: 'A',
        require: ['ngModel', '^^form'],
        link: link
    };
}
app.directive('formulateValidation', formulateValidation);
}).call(this,typeof global !== "undefined" ? global : typeof self !== "undefined" ? self : typeof window !== "undefined" ? window : {})
},{}],5:[function(require,module,exports){
(function (global){
'use strict';

var angular = (typeof window !== "undefined" ? window['angular'] : typeof global !== "undefined" ? global['angular'] : null);
var lodash = (typeof window !== "undefined" ? window['_'] : typeof global !== "undefined" ? global['_'] : null);

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
}).call(this,typeof global !== "undefined" ? global : typeof self !== "undefined" ? self : typeof window !== "undefined" ? window : {})
},{}],6:[function(require,module,exports){
(function (global){
'use strict';

var angular = (typeof window !== "undefined" ? window['angular'] : typeof global !== "undefined" ? global['angular'] : null);

// Create app
var app = angular.module('formulate', ['ngMessages']);

// Include all directives.
require('./directives/form/field.js');require('./directives/form/responsiveForm.js');require('./directives/form/rows.js');require('./directives/form/validation.js');require('./directives/json-source/json-source.js');
}).call(this,typeof global !== "undefined" ? global : typeof self !== "undefined" ? self : typeof window !== "undefined" ? window : {})
},{"./directives/form/field.js":1,"./directives/form/responsiveForm.js":2,"./directives/form/rows.js":3,"./directives/form/validation.js":4,"./directives/json-source/json-source.js":5}]},{},[6]);
