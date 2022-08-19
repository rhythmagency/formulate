/**
 * Manages requests for type definitions (e.g., form handler type definitions).
 */
class FormulateTypeDefinitionResource {

    // Properties.
    serverVars;
    $http;
    umbRequestHelper;

    /**
     * Constructor.
     * @param $http The $http service.
     * @param umbRequestHelper The umbRequestHelper service.
     */
    constructor($http, umbRequestHelper) {
        this.serverVars = Umbraco.Sys.ServerVariables.formulate;
        this.$http = $http;
        this.umbRequestHelper = umbRequestHelper;
    }

    /**
     * Returns the form handler definitions.
     * @returns {*} The promise that resolves to the array of definitions.
     */
    getHandlerDefinitions() {
        const url = this.serverVars.FormHandlers.GetDefinitions;
        return this.umbRequestHelper
            .resourcePromise(this.$http.get(url));
    }

    /**
     * Returns the form field definitions.
     * @returns {*} The promise that resolves to the array of definitions.
     */
    getFieldDefinitions() {
        const url = this.serverVars.FormFields.GetDefinitions;
        return this.umbRequestHelper
            .resourcePromise(this.$http.get(url));
    }

    /**
     * Returns the form template definitions.
     * @returns {*} The promise that resolves to the array of definitions.
     */
    getTemplateDefinitions() {
        const url = this.serverVars.Templates.GetAll;
        return this.umbRequestHelper
            .resourcePromise(this.$http.get(url));
    }

}

// Register the resource service.
angular
    .module("umbraco")
    .factory("formulateTypeDefinitionResource", FormulateTypeDefinitionResource);