'use strict';

// Dependencies.
var angular = require('angular');

// App.
var app = angular.module('formulate');

// Directive.
function directive() {
    return {
        restrict: "E",
        replace: true,
        scope: {
            fieldId: "=",
            fieldModel: "=",
            buttonLabel: "="
        },
        template:
            '<div class="file-upload">' +
                '<!-- Can be styled with this approach: https://www.abeautifulsite.net/whipping-file-inputs-into-shape-with-bootstrap-3 -->' +
                '<!-- TLDR: Hide the file input, and make the label look like a button. -->' +
                '<label class="file-upload__button">' +
                    '<span class="file-upload__button-text">{{buttonLabel}}</span>'+
                    '<input class="file-upload__input" type="file" formulate-file-change ng-model="fieldModel" />' +
                '</label>' +
                '<span class="file-upload__filename">' +
                    '{{fieldModel.name}}' +
                '</span>'+
            '</div>'
    };
}
app.directive("formulateFileUpload", directive);