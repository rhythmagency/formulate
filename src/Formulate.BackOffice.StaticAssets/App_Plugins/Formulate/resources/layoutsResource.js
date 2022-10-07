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
                $http,
                formulateVars) => {

                // Retain the injected parameters on this object.
                const services = {
                    $http,
                    formulateVars,
                };

                // Use this object as the service.
                return {
                    persistLayout: this.performPersistLayout(services)
                };

            });
    };

    // Returns the function that persists a layout on the server.
    performPersistLayout(services) {
        const { formulateVars, $http } = services;

        return function (data) {
            // Variables.
            const url = formulateVars.Layouts.Save;

            // Send request to create the layout.
            return $http.post(url, data, function (data) {

                // Return layout information.
                return {
                    id: data.Id,
                    path: data.Path
                };

            });
        }
    };
}

// Initialize.
(new FormulateLayoutsService()).registerService();