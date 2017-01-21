// Variables.
var app = angular.module("umbraco");

// Directive.
app.directive("formulateFormFieldsChooser", formFieldsChooserDirective);
function formFieldsChooserDirective(formulateDirectives) {
    return {
        restrict: "E",
        template: formulateDirectives.get("formFieldsChooser/formFieldsChooser.html"),
        controller: Controller,
        scope: {
            allFields: "=",
            chosenFields: "="
        }
    };
}

// Controller.
function Controller($scope, notificationsService) {

    // Variables.
    this.injected = {
        $scope: $scope,
        notificationsService: notificationsService
    };

    // Initialize scope variables.
    $scope.tempData = {
        field: null,
        fields: []
    };
    this.initializeFields();

    // Add scope functions.
    $scope.addField = this.addField.bind(this);
    $scope.deleteField = this.deleteField.bind(this);

    // Start watching changes to the temporary fields.
    this.watchFieldChanges();

}

/**
 * Adds the currently selected field to the list of fields.
 */
Controller.prototype.addField = function () {

    // Variables.
    var $scope = this.injected.$scope;
    var notificationsService = this.injected.notificationsService;
    var id = $scope.tempData.field;

    // Ensure the field to add wasn't already added (don't want duplicates).
    for (var i = 0; i < $scope.tempData.fields.length; i++) {
        var field = $scope.tempData.fields[i];
        if (field.id === id) {
            var title = "Duplicate Field";
            var message = "That field was already added. You cannot add the same field twice.";
            notificationsService.error(title, message);
            return;
        }
    }

    // Get the field based on the ID selected in the field selection drop down.
    var foundField = this.findFieldWithId(id);
    if (!foundField) {
        return;
    }

    // Add the field to the list of fields.
    $scope.tempData.fields.push(foundField);

};

/**
 * Deletes the field at the specified index.
 * @param index The index of the field to delete.
 */
Controller.prototype.deleteField = function (index) {
    var $scope = this.injected.$scope;
    $scope.tempData.fields.splice(index, 1);
};

/**
 * Initializes the fields.
 */
Controller.prototype.initializeFields = function () {

    // Variables.
    var $scope = this.injected.$scope;
    var fields = [];

    // Ensure the fields array exists.
    if (!$scope.chosenFields) {
        $scope.chosenFields = [];
    }

    // Based on the stored field ID's, create an array containing the full information
    // about the fields with those ID's.
    for (var i = 0; i < $scope.chosenFields.length; i++) {
        var fieldId = $scope.chosenFields[i].id;
        var field = this.findFieldWithId(fieldId);
        if (field) {
            fields.push(field);
        } else {
            fields.push({
                id: fieldId,
                name: "Unknown Field"
            });
        }
    }

    // Store the full field information to a temporary array used for display.
    $scope.tempData.fields = fields;

};

/**
 * Attempts to get the field with the specified field ID.
 * @param id The field ID.
 * @returns {*} The field, if one matching the ID could be found; otherwise, null.
 */
Controller.prototype.findFieldWithId = function (id) {
    var $scope = this.injected.$scope;
    if (!id) {
        return null;
    }
    var foundField = null;
    for (var i = 0; i < $scope.allFields.length; i++) {
        var field = $scope.allFields[i];
        if (field.id === id) {
            foundField = field;
            break;
        }
    }
    return foundField;
};

/**
 * Sets up a scope watcher to update the stored field ID's whenever the user changes the fields.
 */
Controller.prototype.watchFieldChanges = function () {
    var $scope = this.injected.$scope;
    $scope.$watchCollection("tempData.fields", function (fields) {
        var ids = [];
        for (var i = 0; i < fields.length; i++) {
            ids.push({
                id: fields[i].id
            });
        }
        $scope.chosenFields = ids;
    });
};