'use strict';

// Dependencies.
var angular = require('angular');

// App.
var app = angular.module('formulate');

// Controller.
function Controller($scope) {
    this.injected = {
        $scope: $scope
    };
    $scope.uploadList = [{}];
}

// Clears the upload selection.
Controller.prototype.clearSelection = function () {

    // This is a trick to force Angular to render the file input again (thereby clearing the selection).
    this.injected.$scope.uploadList = [{}];

    // Clear the model.
    this.injected.$scope.fieldModel = undefined;

};

// Directive.
function directive() {
    return {
        restrict: "E",
        replace: true,
        controller: Controller,
        controllerAs: "ctrl",
        scope: {
            fieldId: "=",
            fieldModel: "=",
            buttonLabel: "="
        },
        template:
            '<div class="formulate__file-upload">' +
                '<!-- Can be styled with this approach: https://www.abeautifulsite.net/whipping-file-inputs-into-shape-with-bootstrap-3 -->' +
                '<!-- TLDR: Hide the file input, and make the label look like a button. -->' +
                '<label class="formulate__file-upload-button" ng-repeat="uploadItem in uploadList">' +
                    '<span class="formulate__file-upload-button-text">{{$parent.buttonLabel}}</span>'+
                    '<input class="formulate__file-upload-input" type="file" formulate-file-change ng-model="$parent.fieldModel" />' +
                '</label>' +
                '<span class="formulate__file-upload-filename">' +
                    '{{fieldModel.name}}' +
                '</span>'+
                '<a class="formulate__file-upload-clear" ng-if="fieldModel.name" ng-click="ctrl.clearSelection()">' +
                    '<span class="formulate__file-upload-clear--inner">' +
                        'Clear Selection' +
                    '</span>' +
                '</a>'+
            '</div>'
    };
}
app.directive("formulateFileUpload", directive);