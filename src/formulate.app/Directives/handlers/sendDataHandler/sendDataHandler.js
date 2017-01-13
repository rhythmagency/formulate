/*
 This directive handles the list of fields in a somewhat complicated way, so it could use some
 explaining. In short, the fields chosen by the users are stored both in the
 "$scope.configuration.fields" variable and the "$scope.tempData.chosenFields" variable. Both
 of those variables are arrays. The elements in the "chosenFields" array contain objects that
 have a "storedField" property. The values of the "storedField" properties are the same objects
 that are in the "fields" array. This facilitates a few things. For one, the objects in
 "fields" contain only the data necessary for persistence. The objects in "chosenFields" contain
 some additional information for temporary use (e.g., to show the Formulate field name, which is
 not persisted). By storing the "fields" objects as properties on the "chosenFields" objects,
 Angular is still able to perform binding (e.g., so when a user changes the value of the mapped
 field name, it will update the persisted version of the mapped field name). Phrased another way,
 you can think of "chosenFields" as the view model and "fields" as the data model, with a bit of
 extra magic to keep them synchronized.
  */

// Variables.
var app = angular.module("umbraco");

// Associate directive.
app.directive("formulateSendDataHandler", directive);

/**
 * Defines the "Send Data Handler" directive.
 * @param formulateDirectives The service used to get information about Formulate directives.
 * @returns {{restrict: string, replace: boolean, template: (*|string), scope: {configuration: string, fields: string}, controller: Controller}}
 */
function directive(formulateDirectives) {
    return {
        restrict: "E",
        replace: true,
        template: formulateDirectives.get(
            "handlers/sendDataHandler/sendDataHandler.html"),
        scope: {
            configuration: "=",
            fields: "="
        },
        controller: Controller
    };
}

/**
 * Controller for the "Send Data" handler directive.
 * @param $scope The injected Angular scope.
 * @param notificationsService The injected Umbraco notifications service.
 * @constructor
 */
function Controller($scope, notificationsService) {

    // Variables.
    this.injected = {
        $scope: $scope,
        notificationsService: notificationsService
    };

    // Initialize scope variables.
    $scope.tempData = {
        chosenField: null,
        chosenFields: []
    };
    if (!$scope.configuration.method) {
        $scope.configuration.method = "GET";
    }
    if (!$scope.configuration.transmissionFormat) {
        $scope.configuration.transmissionFormat = "Query String";
    }
    $scope.sortableFieldOptions = {
        cursor: "move",
        delay: 100,
        opacity: 0.5
    };
    this.initializeFields();

    // Add scope functions.
    $scope.addField = this.addField.bind(this);
    $scope.deleteField = this.deleteField.bind(this);

    // Start watching changes to the fields.
    this.watchFieldChanges();

}

/**
 * Adds the currently selected field to the list of fields.
 */
Controller.prototype.addField = function () {

    // Variables.
    var $scope = this.injected.$scope;
    var notificationsService = this.injected.notificationsService;
    var id = $scope.tempData.chosenField;

    // Ensure the field to add wasn't already added (don't want duplicates).
    for (var i = 0; i < $scope.tempData.chosenFields.length; i++) {
        var field = $scope.tempData.chosenFields[i];
        if (field.storedField.id === id) {
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
    $scope.tempData.chosenFields.push({
        fieldName: foundField.name,
        storedField: {
            id: id,
            name: foundField.alias
        }
    });

};

/**
 * Deletes the field at the specified index.
 * @param index The index of the field to delete.
 */
Controller.prototype.deleteField = function (index) {
    var $scope = this.injected.$scope;
    $scope.tempData.chosenFields.splice(index, 1);
};

/**
 * Initializes the fields based on the stored configuration of field ID's.
 */
Controller.prototype.initializeFields = function () {

    // Variables.
    var $scope = this.injected.$scope;
    var fields = [];

    // Ensure the configuration contains a fields array.
    if (!$scope.configuration.fields) {
        $scope.configuration.fields = [];
    }

    // Based on the stored field ID's, create an array containing the full
    // information about the fields with those ID's.
    for (var i = 0; i < $scope.configuration.fields.length; i++) {
        var storedField = $scope.configuration.fields[i];
        var field = this.findFieldWithId(storedField.id);
        var fieldName = field
            ? field.name
            : "Unknown Field";
        fields.push({
            fieldName: fieldName,
            storedField: storedField
        });
    }

    // Store the full field information to a temporary array used for display.
    $scope.tempData.chosenFields = fields;

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
 * Sets up a scope watcher to update the stored fields whenever the user changes the
 * temporary fields.
 */
Controller.prototype.watchFieldChanges = function () {
    var $scope = this.injected.$scope;
    $scope.$watchCollection("tempData.chosenFields", function (chosenFields) {
        var storedFields = [];
        for (var i = 0; i < chosenFields.length; i++) {
            var storedField = chosenFields[i].storedField;
            storedFields.push(storedField);
        }
        $scope.configuration.fields = storedFields;
    });
};