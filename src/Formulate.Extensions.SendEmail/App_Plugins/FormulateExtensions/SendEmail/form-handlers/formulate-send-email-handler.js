(function () {
    "use strict";

    function formulateSendEmailHandlerDirective() {
        return {
            replace: true,
            templateUrl: "/app_plugins/formulateextensions/sendemail/form-handlers/formulate-send-email-handler.html"
        };
    }

    angular
        .module("umbraco.directives")
        .directive("formulateSendEmailHandler", formulateSendEmailHandlerDirective);
})();
