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
            '<div class="formulate__file-upload">' +
                '<!-- Can be styled with this approach: https://www.abeautifulsite.net/whipping-file-inputs-into-shape-with-bootstrap-3 -->' +
                '<!-- TLDR: Hide the file input, and make the label look like a button. -->' +
                '<label class="formulate__file-upload-button">' +
                    '<span class="formulate__file-upload-button-text">{{buttonLabel}}</span>'+
                    '<input class="formulate__file-upload-input" type="file" formulate-file-change ng-model="fieldModel" />' +
                '</label>' +
                '<span class="formulate__file-upload-filename">' +
                    '{{fieldModel.name}}' +
                '</span>'+
            '</div>'
    };
}
app.directive("formulateFileUpload", directive);