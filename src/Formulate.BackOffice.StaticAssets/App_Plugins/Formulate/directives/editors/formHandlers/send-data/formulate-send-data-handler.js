"use strict";

function formulateSendDataHandlerDirective() {
    return {
        replace: true,
        templateUrl: "/app_plugins/formulate/directives/editors/formHandlers/send-data/formulate-send-data-handler.html",
    };
}

angular
    .module("umbraco.directives")
    .directive("formulateSendDataHandler", formulateSendDataHandlerDirective);