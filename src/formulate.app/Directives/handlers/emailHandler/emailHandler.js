// Variables.
var app = angular.module("umbraco");

// Associate directive/controller.
app.directive("formulateEmailHandler", directive);

// Directive.
function directive(formulateDirectives) {
    return {
        restrict: "E",
        replace: true,
        template: formulateDirectives.get(
            "handlers/emailHandler/emailHandler.html"),
        scope: {
            configuration: "=",
            fields: "="
        },
        controller: Controller
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
        recipientEmailField: null,
        recipientFields: []
    };
    if (!$scope.configuration.recipients) {
        $scope.configuration.recipients = [];
    }
    this.initializeRecipientFields();

    // Add scope functions.
    $scope.addRecipient = this.addRecipient.bind(this);
    $scope.deleteRecipient = this.deleteRecipient.bind(this);
    $scope.addRecipientField = this.addRecipientField.bind(this);
    $scope.deleteRecipientField = this.deleteRecipientField.bind(this);
    $scope.deleteRecipientField = this.deleteRecipientField.bind(this);

    // Start watching changes to the recipient email fields.
    this.watchRecipientFieldChanges();

}

/**
 * Adds a blank email address to the list of recipients of emails.
 */
Controller.prototype.addRecipient = function () {
    var $scope = this.injected.$scope;
    $scope.configuration.recipients.push({
        email: ""
    });
};

/**
 * Deletes the email recipient at the specified index.
 * @param index The index of the email recipient to delete.
 */
Controller.prototype.deleteRecipient = function (index) {
    var $scope = this.injected.$scope;
    $scope.configuration.recipients.splice(index, 1);
};

/**
 * Adds the currently selected field to the list of email recipient fields.
 */
Controller.prototype.addRecipientField = function () {

    // Variables.
    var $scope = this.injected.$scope;
    var notificationsService = this.injected.notificationsService;
    var id = $scope.tempData.recipientEmailField;

    // Ensure the field to add wasn't already added (don't want duplicates).
    for (var i = 0; i < $scope.tempData.recipientFields.length; i++) {
        var field = $scope.tempData.recipientFields[i];
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

    // Add the field to the list of recipient fields.
    $scope.tempData.recipientFields.push(foundField);

};

/**
 * Deletes the email recipient field at the specified index.
 * @param index The index of the email recipient field to delete.
 */
Controller.prototype.deleteRecipientField = function (index) {
    var $scope = this.injected.$scope;
    $scope.tempData.recipientFields.splice(index, 1);
};

/**
 * Initializes the recipient fields based on the stored configuration of field ID's.
 */
Controller.prototype.initializeRecipientFields = function () {

    // Variables.
    var $scope = this.injected.$scope;
    var fields = [];

    // Ensure the configuration contains a recipient fields array.
    if (!$scope.configuration.recipientFields) {
        $scope.configuration.recipientFields = [];
    }

    // Based on the stored recipient field ID's, create an array containing
    // the full information about the fields with those ID's.
    for (var i = 0; i < $scope.configuration.recipientFields.length; i++) {
        var fieldId = $scope.configuration.recipientFields[i].id;
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
    $scope.tempData.recipientFields = fields;

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
    for (var i = 0; i < $scope.fields.length; i++) {
        var field = $scope.fields[i];
        if (field.id === id) {
            foundField = field;
            break;
        }
    }
    return foundField;
};

/**
 * Sets up a scope watcher to update the stored field ID's whenever the user changes the
 * recipient email fields.
 */
Controller.prototype.watchRecipientFieldChanges = function () {
    var $scope = this.injected.$scope;
    $scope.$watchCollection("tempData.recipientFields", function (fields) {
        var ids = [];
        for (var i = 0; i < fields.length; i++) {
            ids.push({
                id: fields[i].id
            });
        }
        $scope.configuration.recipientFields = ids;
    });
};