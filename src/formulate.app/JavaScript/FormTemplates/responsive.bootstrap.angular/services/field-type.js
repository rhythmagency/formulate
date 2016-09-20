'use strict';

var angular = require('angular');

var app = angular.module('formulate');

function fieldId(field) {
    return 'field-' + field.randomId;
}

function addLabel(field) {
    /*global document */
    var contents = angular.element(document.createTextNode(field.label));
    var label = angular.element('<label class="formulate__field-label"></label>');

    label.append(contents);
    label.attr('for', fieldId(field));

    return label;
}

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

            elMessage.attr('ng-message', validation.id);
            elMessage.text(validation.configuration.message);
            elMessage.addClass('formulate__error-msg');

            elMessages.append(elMessage);
        });
    }

    return elMessages;
}

/**
 *
 * Registers field rendering factories - used by formulateField to render form field
 *
 * Please Note: fields are registered globally; Hence, you cannot have different field types per different
 *              instances of formulate.
 *
 * @constructor
 */
function FormulateFieldTypesProvider() {
    // Pre-register builtin types
    var fieldTypes = {};
    var prependLabel = {};

    function fieldFactory(tmpl) {
        return function () {
            return angular.element(tmpl);
        };
    }

    this.register = function (fieldType, fieldTemplate, pLabel) {
        fieldTypes[fieldType] = angular.isString(fieldTemplate) ? fieldFactory(fieldTemplate) : fieldTemplate;

        prependLabel[fieldType] = pLabel;

        return this;
    };

    this.setDefault = function (fieldTemplate, pLabel) {
        fieldTypes.defaultField = angular.isString(fieldTemplate) ? fieldFactory(fieldTemplate) : fieldTemplate;

        prependLabel.defaultField = pLabel;

        return this;
    };

    this.$get = function () {
        return {
            createField: function (field) {
                var elWrap = angular.element('<div></div>');

                elWrap.addClass('formulate__group form-group');

                // Append label
                if (fieldTypes.hasOwnProperty(field.fieldType)) {
                    if (prependLabel[field.fieldType]) {
                        elWrap.append(addLabel(field));
                    }
                } else {
                    if (prependLabel.defaultField) {
                        elWrap.append(addLabel(field));
                    }
                }

                if (fieldTypes.hasOwnProperty(field.fieldType)) {
                    elWrap.append(fieldTypes[field.fieldType](field));
                } else {
                    elWrap.append(fieldTypes.defaultField(field));
                }

                elWrap.append(addNgMessages(field));

                return elWrap;
            }
        };
    };
}
app.provider('FormulateFieldTypes', FormulateFieldTypesProvider);


