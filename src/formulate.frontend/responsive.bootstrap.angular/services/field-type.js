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
        elMessages.attr('aria-atomic', 'true');

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

    var options = {
        prependLabel: true,
        placeholderLabel: true,
        firstSelectOptionLabel: true
    };

    var fieldOptions = {
        optionalLabel: true
    };

    function fieldFactory(tmpl) {
        return function () {
            return angular.element(tmpl);
        };
    }

    /**
     *
     * Register field
     *
     * @param fieldType
     * @param fieldTemplate
     * @param fieldOption configuration object; Properties => optionalLabel: true (allow for label to be inserted before the field)
     *
     * @returns {FormulateFieldTypesProvider}
     */
    this.register = function (fieldType, fieldTemplate, pOptions) {
        var opt = angular.extend({}, fieldOptions, pOptions);

        fieldTypes[fieldType] = angular.isString(fieldTemplate) ? fieldFactory(fieldTemplate) : fieldTemplate;

        prependLabel[fieldType] = opt.optionalLabel;

        return this;
    };

    /**
     * Register default field
     *
     * @param fieldTemplate
     * @param addLabel bool add label before the field
     * @returns {FormulateFieldTypesProvider}
     */
    this.setDefault = function (fieldTemplate, pOptions) {
        var opt = angular.extend({}, fieldOptions, pOptions);

        fieldTypes.defaultField = angular.isString(fieldTemplate) ? fieldFactory(fieldTemplate) : fieldTemplate;

        prependLabel.defaultField = opt.optionalLabel;

        return this;
    };

    /**
     * Set generic options
     *
     * Possible Options:
     *  prependLabel: true - Prepend Label Before the field
     *  placeholderLabel: true - Place Label into Placeholder
     *  firstSelectOptionLabel: true - Set Label as the very first option to select(dropdown) with an empty value
     *
     * @param opt
     */
    this.setOptions = function (pOptions) {
        angular.extend(options, pOptions);

        return this;
    };

    this.$get = function () {
        return {
            createField: function (field) {
                var elWrap = angular.element('<div></div>');

                elWrap.addClass('formulate__group form-group');

                // Append label
                if (fieldTypes.hasOwnProperty(field.fieldType)) {
                    if (options.prependLabel && prependLabel[field.fieldType]) {
                        elWrap.append(addLabel(field));
                    }
                } else {
                    if (options.prependLabel && prependLabel.defaultField) {
                        elWrap.append(addLabel(field));
                    }
                }

                if (fieldTypes.hasOwnProperty(field.fieldType)) {
                    elWrap.append(fieldTypes[field.fieldType](field, options));
                } else {
                    elWrap.append(fieldTypes.defaultField(field, options));
                }

                elWrap.append(addNgMessages(field));

                return elWrap;
            }
        };
    };
}
app.provider('FormulateFieldTypes', FormulateFieldTypesProvider);


