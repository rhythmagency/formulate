'use strict';

// Dependencies.
var angular = require('angular');

// App.
var app = angular.module('formulate');

// Controller.
function FormulateFileUploadController($scope) {
    // This is a trick to force Angular to render the file input again (thereby clearing the selection).
    this.uploadResetList = [{}];

    // Clears the upload selection.
    this.clearSelection = function () {
        // This is a trick to force Angular to render the file input again (thereby clearing the selection).
        this.uploadResetList = [{}];

        if ($scope.ctrl && $scope.ctrl.fieldModels && $scope.fieldCtrl) {
            $scope.ctrl.fieldModels[$scope.fieldCtrl.id] = null;
        }
    };
}

// Directive.
function formulateFileUpload() {
    return {
        restrict: "E",
        replace: true,

        controller: FormulateFileUploadController,
        controllerAs: "uploadCtrl",
        bindToController: true,

        template: '<div class="formulate__file-upload">' +
                '<div ng-repeat="uploadItem in uploadCtrl.uploadResetList">' +
                '<!-- Can be styled with this approach: https://www.abeautifulsite.net/whipping-file-inputs-into-shape-with-bootstrap-3 -->' +
                '<!-- TLDR: Hide the file input, and make the label look like a button. -->' +
                    '<label class="formulate__file-upload-button">' +
                        '<input name="field_{{fieldCtrl.id}}" class="formulate__file-upload-input" type="file" formulate-file-change ng-model="ctrl.fieldModels[fieldCtrl.id]" formulate-validation />' +
                        '<span class="formulate__file-upload-button-text" ng-bind="fieldCtrl.label"></span>' +
                    '</label>' +

                    '<span class="formulate__file-upload-filename" ng-bind="ctrl.fieldModels[fieldCtrl.id].name"></span>' +

                    '<a href class="formulate__file-upload-clear" ng-if="ctrl.fieldModels[fieldCtrl.id].name" ng-click="uploadCtrl.clearSelection()">' +
                        '<span class="formulate__file-upload-clear--inner">Clear Selection</span>' +
                    '</a>' +
                '</div>' +
            '</div>'
    };
}
app.directive("formulateFileUpload", formulateFileUpload);