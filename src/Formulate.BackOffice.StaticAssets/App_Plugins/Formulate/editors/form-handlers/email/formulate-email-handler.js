"use strict";

function formulateEmailHandlerDirective() {
    return {
        replace: true,
        templateUrl: "/app_plugins/formulate/editors/form-handlers/email/formulate-email-handler.html",
    };
}

angular
    .module("umbraco.directives")
    .directive("formulateEmailHandler", formulateEmailHandlerDirective);