'use strict';

// Dependencies.
var angular = require('angular');

// App.
var app = angular.module('formulate');

// Directive: http://plnkr.co/edit/JYX3Pcq18gH3ol5XSizw?p=preview
function directive() {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function link(scope, element, attrs, ctrl) {
            /*jslint unparam:true */

            // Handle change event.
            function onChange() {
                // Set the model to the file.
                ctrl.$setViewValue(element[0].files[0]);
            }

            // Listen for change events.
            element.on('change', onChange);

            // When the directive is destroyed, stop listening to changes.
            scope.$on('destroy', function () {
                element.off('change', onChange);
            });
        }
    };
}
app.directive('formulateFileChange', directive);