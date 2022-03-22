"use strict";

function formulateStoreDataHandlerDirective() {
    return {
        replace: true,
        templateUrl: "/app_plugins/formulatebackoffice/directives/editors/formHandlers/store-data/formulate-store-data-handler.html",
    };
}

angular
    .module("umbraco.directives")
    .directive("formulateStoreDataHandler", formulateStoreDataHandlerDirective);