/**
 * The basic and most common layout. This one allows for multiple columns and rows
 * and multiple steps. It also allows for 
 */
class FormulateBasicLayout {

    // Properties.
    $scope;
    formulateVars;
    $http;

    /**
     * Registers this directive so it's discoverable with AngularJS.
     */
    registerDirective = () => {
        angular
            .module("umbraco.directives")
            .directive("formulateLayoutBasic", () => {
                return {
                    restrict: "E",
                    replace: true,
                    templateUrl: "/app_plugins/formulatebackoffice/directives/designers/layout/kinds/basic.layout.html",
                    scope: {
                        data: "="
                    },
                    controller: this.controller,
                };
            });
    };

    //TODO: Implement.
    controller = (
        retainProperties,
        $scope,
        formulateVars,
        $http) => {

        // Keep the properties for later use.
        retainProperties({
            $scope,
            formulateVars,
            $http
        }, this);

        // Initialize variables on the scope.
        this.initializeScopeVariables();

        // Parse/refresh field data.
        if ($scope.dataObject.formId) {
            this.refreshFields();
        } else {
            this.replenishFields();
        }

    };

    /**
     * Initializes the variables on the current AngularJS scope
     * for this directive.
     */
    initializeScopeVariables = () => {

        // Initialize variables.
        this.$scope.unusedFields = [];
        this.$scope.rowsSortableOptions = this.getRowSortableOptions();
        this.$scope.fieldSortableOptions = this.getFieldSortableOptions();

        // Parse/refresh field data.
        this.$scope.dataObject = JSON.parse(this.$scope.data.data);

    };

    /**
     * Returns the options to use when sorting fields.
     * @returns {any} The sort options.
     */
    getFieldSortableOptions = () => {
        return {
            cursor: "move",
            connectWith: ".formulate-cell",
            tolerance: "pointer",
            items: ".formulate-cell-field",
            opacity: 0.5
        };
    };

    /**
     * Returns the options to use when sorting rows.
     * @returns {any} The sort options.
     */
    getRowSortableOptions = () => {
        return {
            cursor: "move",
            tolerance: "pointer",
            axis: "y",
            opacity: 0.5,
            disabled: true
        };
    };

    /**
     * Gets info about the form based on its ID, then updates the fields.
     */
    refreshFields = () => {
        const baseUrl = this.formulateVars['Forms.GetFormInfo'];
        const url = `${baseUrl}?id=${this.$scope.dataObject.formId}`;
        this.$http.get(url)
            .then(x => x.data)
            .then((formData) => {
                this.$scope.allFields = formData.fields
                    .filter(function (item) {
                        return !item.isServerSideOnly;
                    })
                    .map(function (item) {
                        return {
                            id: item.id.replace(/-/g, ''),
                            name: item.name
                        };
                    });
                this.replenishFields();
            });
    };

    /**
     * Ensures that no old fields exist in the rows and that all
     * new fields exist in a row. This is called whenever the
     * rows or the fields change.
     */
    replenishFields = () => {

        // Variables.
        let i, j, field, row, cell, key;
        let fields = {}, $scope = this.$scope,
            rows = $scope.dataObject.rows;

        // Place all fields in an associative array keyed by ID.
        for (i = 0; i < $scope.allFields.length; i++) {
            field = $scope.allFields[i];
            fields[field.id] = field;
        }

        // Remove or replenish the fields in the rows.
        for (i = 0; i < rows.length; i++) {
            row = rows[i];
            for (j = 0; j < row.cells.length; j++) {
                cell = row.cells[j];
                this.deleteAndUpdateFields(fields, cell.fields);
            }
        }

        // Remove or replenish the fields in the unused fields section.
        this.deleteAndUpdateFields(fields, $scope.unusedFields);

        // Aggregate all fields which aren't yet assigned to a row.
        let unassignedFields = [];
        for (key in fields) {
            if (fields.hasOwnProperty(key)) {
                unassignedFields.push(fields[key]);
            }
        }

        // Add unassigned fields to the unused fields section.
        if (unassignedFields.length > 0) {
            $scope.unusedFields = $scope.unusedFields.concat(unassignedFields);
        }

    };

    //TODO: Update comment.
    // Deletes and updates fields based on the information known about all fields.
    deleteAndUpdateFields(allFieldsById, fieldsToUpdate) {
        let i, field;
        for (i = fieldsToUpdate.length - 1; i >= 0; i--) {
            field = fieldsToUpdate[i];
            if (allFieldsById.hasOwnProperty(field.id)) {

                // Copy fresh data over.
                field.name = allFieldsById[field.id].name;

                // Remove field as it's accounted for.
                delete allFieldsById[field.id];

            } else {

                // This field no longer exists in the layout,
                // so remove it from the fields to update.
                fieldsToUpdate.splice(i, 1);

            }
        }
    }

}

// Initialize.
(new FormulateBasicLayout()).registerDirective();