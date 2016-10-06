'use strict';

// Dependencies.
var angular = require('angular');

// App.
var app = angular.module('formulate');

// Controller.
function FormulateFileUploadController() {
    // This is a trick to force Angular to render the file input again (thereby clearing the selection).
    this.uploadResetList = [{}];
}

// Clears the upload selection.
FormulateFileUploadController.prototype.clearSelection = function () {
    // This is a trick to force Angular to render the file input again (thereby clearing the selection).
    this.uploadResetList = [{}];

    // Clear the model.
    this.fieldModel = undefined;
};

// Directive.
function formulateFileUpload() {
    return {
        restrict: "E",
        replace: true,

        controller: FormulateFileUploadController,
        controllerAs: "ctrl",
        bindToController: true,

        scope: {
            fieldId: "=",
            fieldModel: "=",
            buttonLabel: "="
        },

        template:
            '<div class="formulate__file-upload">' +
                '<div ng-repeat="uploadItem in ctrl.uploadResetList">' +
                    '<!-- Can be styled with this approach: https://www.abeautifulsite.net/whipping-file-inputs-into-shape-with-bootstrap-3 -->' +
                    '<!-- TLDR: Hide the file input, and make the label look like a button. -->' +
                    '<label class="formulate__file-upload-button">' +
                        '<input class="formulate__file-upload-input" type="file" formulate-file-change ng-model="ctrl.fieldModel" />' +
                        '<span class="formulate__file-upload-button-text" ng-bind="ctrl.buttonLabel"></span>' +
                    '</label>' +

                    '<span class="formulate__file-upload-filename" ng-bind="ctrl.fieldModel.name"></span>' +

                    '<a href class="formulate__file-upload-clear" ng-if="ctrl.fieldModel.name" ng-click="ctrl.clearSelection()">' +
                        '<span class="formulate__file-upload-clear--inner">Clear Selection</span>' +
                    '</a>' +
                '</div>' +
            '</div>'
    };
}
app.directive("formulateFileUpload", formulateFileUpload);