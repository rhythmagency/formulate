"use strict";

function formulateEmailHandlerDirective() {
    return {
        replace: true,
        templateUrl: "/app_plugins/formulate/directives/editors/formHandlers/email/formulate-email-handler.html",
    };
}

angular
    .module("umbraco.directives")
    .directive("formulateEmailHandler", formulateEmailHandlerDirective);