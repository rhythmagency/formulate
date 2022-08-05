"use strict";

(function () {
    var controller = function ($scope, $http, formulateVars, editorService, formulateIds) {
        const services = {
            $scope,
            $http,
            formulateIds,
            formulateVars,
            editorService,
        };

        init(services);
    };

    const init = function (services) {
        const { $scope } = services;

        $scope.loading = true;

        // default VM setup.
        $scope.vm = {
            form: {},
            unusedFields: [],
            rows: [],
            autoPopulate: false,
            sampleCells : []
        };

        $scope.editor = {
            mode: 'fields',
        };
                
        for (let i = 0; i < 12; i++) {
            $scope.vm.sampleCells.push({
                active: false
            });
        }

        $scope.sortableOptions = {
            fields: {
                cursor: "move",
                connectWith: ".formulate-cell",
                tolerance: "pointer",
                items: ".formulate-cell-field",
                opacity: 0.5,
            },
            rows: {
                cursor: "move",
                tolerance: "pointer",
                axis: "y",
                opacity: 0.5,
                disabled: true,
            }
        };

        // configure actions.
        $scope.actions = {
            pickForm: performPickForm(services),
            removeForm: performRemoveForm(services),
            toggleAutoPopulate: () => {
                $scope.vm.autoPopulate = !$scope.vm.autoPopulate;
            },
            getSampleCellClasses: getSampleCellClasses(services),
            toggleCell: toggleCell(services),
            addRow: addRow(services),
            deleteRow: deleteRow(services),
            addStep: addStep(services),
            getCellClass: (row, cell) => {
                return 'span' + cell.columnSpan.toString();
            },
            useField: useField(services),
            toggleEditorMode: toggleEditorMode(services)
        };

        // if any data from parent configure vm.
        if ($scope.data) {
            if ($scope.data.autoPopulate) {
                $scope.vm.autoPopulate = $scope.data.autoPopulate;
            }

            if ($scope.data.formId) {
                $scope.vm.form.id = $scope.data.formId;

                requestFormData(services).then((formData) => {
                    const rows = Utilities.isArray($scope.data.rows) ? $scope.data.rows : [];
                    updateForm(services, formData);
                    updateFields(services, formData);
                    updateRows(services, rows);

                    finalizeInit(services);
                });
            }
            else {
                finalizeInit(services);
            }
        } else {
            finalizeInit(services);
        }
    };

    const requestFormData = function (services) {
        const { $scope, $http, formulateVars } = services;
        return new Promise((resolve, reject) => {
            const { form } = $scope.vm;

            if (!form || !form.id) {
                return resolve();
            }

            const baseUrl = formulateVars['forms.Get'];
            const url = `${baseUrl}?id=${form.id}`;
            $http.get(url)
                .then(x => x.data)
                .then((formData) => {
                    return resolve(formData);
                });
        });
    }

    const performPickForm = function (services) {
        const { editorService, $scope } = services;

        return function () {
            const chosen = (node) => {
                $scope.vm.form = {
                    id: node.id,
                    name: node.name
                };
                requestFormData(services).then((formData) => {
                    updateForm(services, formData);
                    updateFields(services, formData);
                    updateRows(services, []);
                });
                editorService.close();
            };

            const closer = () => {
                editorService.close();
            };

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

            //        // Open the overlay that displays the forms.
            editorService.treePicker(config);
        };
    };

    const performRemoveForm = function (services) {
        return function () {
            reset(services);
        };
    };

    const updateForm = function (services, formData) {
        const { $scope } = services;

        if (!formData) {
            $scope.vm.form = {};
            return;
        }

        $scope.vm.form = {
            id: formData.id,
            name: formData.name
        };
    }

    const updateFields = function (services, formData) {
        const { $scope } = services;

        $scope.vm.allFields = formData.fields
            .filter(function (item) {
                return !item.isServerSideOnly;
            })
            .map(function (item) {
                return {
                    id: item.id,
                    name: item.name
                };
            });

        $scope.vm.unusedFields = [];
        Utilities.copy($scope.vm.allFields, $scope.vm.unusedFields);
    }

    const updateRows = function (services, rows) {
        const { $scope, formulateIds } = services;
        const allFields = $scope.vm.allFields;

        // no rows? populate with a default row set.
        if (Utilities.isArray(rows) == false || rows.length == 0) {
            $scope.vm.rows = [
                {
                    isStep: false,
                    cells: [
                        {
                            columnSpan: 12,
                            fields: []
                        }
                    ]
                }
            ];
        }
        else {
            $scope.vm.rows = rows.map((row) => {
                const cells = Utilities.isArray(row.cells) ? row.cells : [];
                const mappedCells = cells.map((cell) => {
                    const fields = Utilities.isArray(cell.fields) ? cell.fields : [];

                    const mappedFields = fields.map((cellField) => {
                        const foundField = allFields.find((field) => {
                            return formulateIds.compare(field.id, cellField.id);
                        });

                        if (!foundField) {
                            return undefined;
                        }

                        useUnusedField(services, cellField.id);

                        return foundField;
                    }).filter((f) => typeof (f) !== 'undefined');

                    return {
                        columnSpan: cell.columnSpan,
                        fields: mappedFields
                    }
                });

                return {
                    isStep: row.isStep,
                    cells: mappedCells
                };
            });

            updateStepNumbers(services);
        }
    };

    const useUnusedField = function (services, fieldId) {
        const { $scope } = services;
        const unusedFields = $scope.vm.unusedFields;

        if (!unusedFields) {
            return;
        }

        const index = unusedFields.findIndex(x => x.id === fieldId);

        if (index > -1) {
            unusedFields.splice(index, 1);
        }
    }

    const reset = function (services) {
        const { $scope } = services;

        $scope.editor.mode = 'fields';
        $scope.vm.form = {};
        $scope.vm.allFields = [];
        $scope.vm.unusedFields = [];
        $scope.vm.rows = [];        
    };

    /* sample cells */

    const getSampleCellClasses = function (services) {
        const { $scope } = services;

        return function (index) {
            const cells = $scope.vm.sampleCells;
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
        }
    };

    const toggleCell = function (services) {
        const { $scope } = services;

        return function (index) {
            const cells = $scope.vm.sampleCells;
            const cell = cells[index];
            if (index > 0) {
                cell.active = !cell.active;
            }
        };
    };

    const addRow = function (services) {
        const { $scope } = services;

        return function () {
            // Variables.
            const sampleCells = $scope.vm.sampleCells;

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
            $scope.vm.rows.push({
                cells: cells,
                isStep: false
            });
        };
    };

    const deleteRow = function (services) {
        const { $scope } = services;

        return function (rowIndex) {
            const row = $scope.vm.rows.splice(rowIndex, 1)[0];

            if (row) {
                const orphanedFields = [];

                row.cells.forEach((cell) => {
                    orphanedFields.push(...cell.fields);
                });

                $scope.vm.unusedFields.push(...orphanedFields);
            }

            updateStepNumbers(services);
        };
    };

    const addStep = function (services) {
        const { $scope } = services;

        return function (index) {
            const newRow = {
                isStep: true,
                cells: [],
            };

            $scope.vm.rows.splice(index + 1, 0, newRow);

            updateStepNumbers(services);
        };
    };

    const useField = function (services) {
        const { $scope } = services;

        return function (field) {
            const rows = $scope.vm.rows;
            let offset = 1;
            while (offset <= rows.length) {
                const cells = rows[rows.length - offset].cells;
                if (cells.length) {
                    cells[0].fields.push(field);
                    useUnusedField(services, field.id);

                    break;
                }
                offset = offset + 1;
            }
        };
    };

    const toggleEditorMode = function (services) {
        const { $scope } = services;
        return function () {
            $scope.editor.mode = $scope.editor.mode === 'fields' ? 'rows' : 'fields';
        };
    };

    const updateStepNumbers = function (services) {
        const { $scope } = services;

        let stepNumber = 1;
        $scope.vm.rows.forEach((row) => {
            if (row.isStep) {
                row.stepNumber = stepNumber;
                stepNumber++;
            }
        });
    }

    const finalizeInit = function (services) {
        const { $scope } = services;

        $scope.loading = false;

        $scope.$watch('vm', function (newVal, oldVal) {
            const vm = newVal;
            const hasFormId = vm.form && vm.form.id;

            if (hasFormId) {
                $scope.data = {
                    formId: vm.form.id,
                    autoPopulate: vm.autoPopulate,
                    rows: vm.rows.map((row) => {
                        return {
                            cells: row.cells.map((cell) => {
                                return {
                                    columnSpan: cell.columnSpan,
                                    fields: cell.fields.map((field) => {
                                        return {
                                            id: field.id
                                        }
                                    })
                                };
                            }),
                            isStep: row.isStep
                        };
                    })
                };
            }
            else {
                $scope.data = {
                    formId: undefined,
                    autoPopulate: vm.autoPopulate,
                    rows: []
                };
            }
        }, true);
    }

    var directive = function () {
        return {
            restrict: "E",
            replace: true,
            templateUrl: "/app_plugins/formulatebackoffice/directives/designers/layout/kinds/basic.layout.html",
            controller: controller,
            scope: {
                data: "="
            }
        };
    };

    angular.module("umbraco").directive("formulateLayoutBasic", directive);
})();
