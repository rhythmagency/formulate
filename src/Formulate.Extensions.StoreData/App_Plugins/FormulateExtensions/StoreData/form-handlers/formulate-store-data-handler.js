(function () {
    "use strict";

    function formulateStoreDataHandlerDirective() {
        return {
            replace: true,
            templateUrl: "/app_plugins/formulateextensions/storedata/form-handlers/formulate-store-data-handler.html",
        };
    }

    angular
        .module("umbraco.directives")
        .directive("formulateStoreDataHandler", formulateStoreDataHandlerDirective);
})();
