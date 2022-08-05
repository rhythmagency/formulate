/**
 * The editor that chooses the appropriate layout designer dynamically.
 */
class FormulateLayoutEditor {

    /**
     * Registers this directive so it's discoverable in an AngularJS view.
     */
    registerDirective = () => {
        angular
            .module("umbraco")
            .directive("formulateLayoutEditor", ($compile) => {
                this.$compile = $compile;
                return {
                    restrict: 'E',
                    template: '<div></div>',
                    replace: true,
                    scope: {
                        directive: "=",
                        data: "=",
                    },
                    link: this.linkDirective,
                };
            });
    };

    /**
     * The link function does most of the work in that it figures out which
     * layout designer to render.
     */
    linkDirective = (scope, element) => {

        // Create directive.
        const markup = "<" + scope.directive + " data=\"data\"></" + scope.directive + ">";
        const directive = this.$compile(markup)(scope);
        element.replaceWith(directive);

    };

}

// Initialize.
(new FormulateLayoutEditor()).registerDirective();