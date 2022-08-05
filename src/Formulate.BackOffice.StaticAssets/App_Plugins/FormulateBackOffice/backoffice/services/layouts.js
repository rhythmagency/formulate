/**
 * The service for working with layouts (e.g., fetching layout data from the
 * server).
 */
class FormulateLayoutsService {

    // Properties.
    $http;
    formulateVars;

    /**
     * Registers the service with Angular.
     */
    registerService = () => {
        angular
            .module('umbraco')
            .factory('formulateLayouts', (
                retainProperties,
                $http,
                formulateVars) => {

                // Retain the injected parameters on this object.
                retainProperties({
                    $http,
                    formulateVars,
                }, this);

                // Use this object as the service.
                return this;

            });
    };

    /**
     * Returns information about the layout with the specified ID.
     * @param {any} id The layout ID.
     */
    getLayoutInfo = (id) => {

        // Variables.
        const baseUrl = this.formulateVars.GetLayoutInfo;
        const url = `${baseUrl}?id=${id}`

        // Get layout info from server.
        return this.$http.get(url).then(({ data }) => data);

    };

    // Returns the function that persists a layout on the server.
    persistLayout(data) {

        // Variables.
        const url = this.formulateVars['layouts.Save'];

        // Send request to create the layout.
        return this.$http.post(url, data, function (data) {

            // Return layout information.
            return {
                id: data.Id,
                path: data.Path
            };

        });
    };
}

// Initialize.
(new FormulateLayoutsService()).registerService();