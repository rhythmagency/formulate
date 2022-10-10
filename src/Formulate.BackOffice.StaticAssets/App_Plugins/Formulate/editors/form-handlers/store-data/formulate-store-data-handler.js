"use strict";

function formulateStoreDataHandlerDirective() {
    return {
        replace: true,
        templateUrl: "/app_plugins/formulate/editors/form-handlers/store-data/formulate-store-data-handler.html",
    };
}

angular
    .module("umbraco.directives")
    .directive("formulateStoreDataHandler", formulateStoreDataHandlerDirective);