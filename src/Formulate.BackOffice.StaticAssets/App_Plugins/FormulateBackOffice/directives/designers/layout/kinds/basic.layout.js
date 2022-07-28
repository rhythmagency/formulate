/**
 * The basic and most common layout. This one allows for multiple columns and rows
 * and multiple steps. It also allows for 
 */
class FormulateBasicLayout {

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
    controller = ($scope) => {
    };

}

// Initialize.
(new FormulateBasicLayout()).registerDirective();