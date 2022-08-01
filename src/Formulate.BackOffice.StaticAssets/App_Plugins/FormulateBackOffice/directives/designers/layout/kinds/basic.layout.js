/**
 * The basic and most common layout. This one allows for multiple columns and rows
 * and multiple steps. It also allows for 
 */
class FormulateBasicLayout {

    // Properties.
    $scope;
    formulateVars;
    editorService;
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
        editorService,
        $http) => {

        // Keep the properties for later use.
        retainProperties({
            $scope,
            formulateVars,
            editorService,
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
     * Sets the cells used as a template to add a new row.
     */
    setSampleCells = () => {
        const cells = [];
        let i;
        for (i = 0; i < 12; i++) {
            cells.push({
                active: false
            });
        }
        this.$scope.sampleCells = cells;
    };

    /**
     * Allows the user to pick a form.
     */
    pickForm = () => {
        const formId = this.$scope.dataObject.formId;

        // This is called when the dialog is closed.
        const closer = () => {
            this.editorService.close();
        };

        // This is called when a form is chosen.
        const chosen = ({ id }) => {

            // Validate selection.
            if (!id) {
                return;
            }

            // Update fields.
            this.$scope.dataObject.formId = id;
            this.refreshFields();
            this.editorService.close();

        };

        // The configuration for the tree that allows the form to be chosen.
        const config = {
            section: 'formulate',
            treeAlias: 'forms',
            multiPicker: false,
            entityType: 'Form',
            filter: (node) => {
                return node.nodeType !== 'Form';
            },
            filterCssClass: 'not-allowed',
            select: chosen,
            submit: closer,
            close: closer,
        };

        // Open the overlay that displays the forms.
        this.editorService.treePicker(config);

    };

    /**
     * Toggles the "edit rows" / "edit fields" option.
     */
    toggleEditRows = () => {
        this.$scope.editRows = ! this.$scope.editRows;
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
        this.$scope.context = this;
        this.setSampleCells();

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
            opacity: 0.5,
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
            disabled: true,
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

    /**
     * Updates data on the fields to update based on the data in all
     * the fields. Any found fields will be removed from the collection
     * of all fields, and any fields not present in all fields will be
     * removed from the specified fields.
     * @param allFieldsById All fields.
     * @param fieldsToUpdate The fields to update.
     */
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

    /**
     * Moves a field from the unused collection to a cell in the layout.
     * @param fieldIndex The index of the field to move.
     */
    useField = (fieldIndex) => {
        const rows = this.$scope.dataObject.rows;
        const field = this.$scope.unusedFields.splice(fieldIndex, 1)[0];
        rows[rows.length -1].cells[0].fields.push(field);
    };

    /**
     * Returns the CSS classes to add to the sample cell with the specified index.
     * @param index The index of the sample cell.
     * @returns {string} The CSS classes.
     */
    getSampleCellClasses = (index) => {
        const cells = this.$scope.sampleCells;
        const cell = cells[index];
        const activeClass = cell.active
            ? "formulate-sample-cell--active"
            : "formulate-sample-cell--inactive";
        const nextCell = index + 1 < cells.length
            ? cells[index + 1]
            : null;
        const adjacentClass = nextCell && nextCell.active
            ? "formulate-sample-cell--adjacent"
            : null;
        const firstClass = index === 0
            ? "formulate-sample-cell--first"
            : null;
        const lastClass = index === cells.length - 1
            ? "formulate-sample-cell--last"
            : null;
        const classes = [activeClass, adjacentClass, firstClass, lastClass];
        const validClasses = [];
        for (let i = 0; i < classes.length; i++) {
            if (classes[i]) {
                validClasses.push(classes[i]);
            }
        }
        return validClasses.join(' ');
    };

    /**
     * Toggles the specified sample cell so that a split is added or
     * is removed from the left of it.
     * @param index The index of the cell to toggle the left of.
     */
    toggleCell = (index) => {
        const cells = this.$scope.sampleCells;
        const cell = cells[index];
        if (index > 0) {
            cell.active = !cell.active;
        }
    };

    /**
     * Adds a row based on the sample cells.
     */
    addRow = () => {

        // Variables.
        const sampleCells = this.$scope.sampleCells;
        const cells = [];
        let columnSpan = 0, sampleCell;

        // Add a new cell for each active sample cell.
        for (let i = 0; i < sampleCells.length; i++) {
            sampleCell = sampleCells[i];
            if (sampleCell.active) {
                cells.push({
                    columnSpan: columnSpan,
                    fields: [],
                });
                columnSpan = 0;
            }
            columnSpan = columnSpan + 1;
        }

        // Add a final cell to complete the row.
        cells.push({
            columnSpan: columnSpan,
            fields: [],
        });

        // Add the new row.
        this.$scope.dataObject.rows.push({
            cells: cells,
        });

    };

    /**
     * Returns the CSS class for the specified cell in the specified row.
     * @param row The cell row.
     * @param cell The cell.
     * @returns {string} The CSS class to attach to the cell.
     */
    getCellClass = (row, cell) => {
        return 'span' + cell.columnSpan.toString();
    };

}

// Initialize.
(new FormulateBasicLayout()).registerDirective();