//TODO: Implement.
class ConfiguredFormPicker {
    $scope;

    constructor() {
    }

    controller = ($scope) => {
        this.retainProperties({
            $scope,
        });
    };

    retainProperties = (properties) => {
        for (const [key, value] of Object.entries(properties)) {
            this[key] = value;
            console.log('Stored', {
                key, value
            });
        }
    };

    registerController = () => {
        angular
            .module('umbraco')
            .controller('Formulate.ConfiguredFormPicker', this.controller);
    };
}

const picker = new ConfiguredFormPicker();
picker.registerController();