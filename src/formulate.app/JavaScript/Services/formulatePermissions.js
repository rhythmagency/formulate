// Variables.
var app = angular.module("umbraco");

// Service to help with permissions.
app.factory("formulatePermissions", function ($http, notificationsService,
    formulateVars) {
    var services = {
        $http: $http,
        notificationsService: notificationsService,
        formulateVars: formulateVars
    };
    return {
        permitFormulateAccess:
            getPermitFormulateAccess(services)
    };
});

// Returns a function that will request that the current
// user be given access to the "Formulate" section in Umbraco.
function getPermitFormulateAccess(services) {

    // Variables.
    var $http = services.$http,
        notificationsService = services.notificationsService,
        formulateVars = services.formulateVars;

    // Return function.
    return function (directive) {

        // Variables.
        var url = formulateVars.PermitAccess;
        var options = getReqestOptions();

        // Send request to set permissions.
        $http.post(url, null, options).success(function (data) {
            if (data.Success) {

                // Success notification.
                showSuccess(notificationsService);

                // Reload page.
                reloadPage();

            } else {

                // Application error.
                handleApplicationError(notificationsService, data.Reason);

            }
        }).error(function () {

            // Server error.
            handleServerError(notificationsService);

        });

    };

}

// Reloads the page after 3 seconds.
function reloadPage() {
    setTimeout(function () {
        window.location.reload(true);
    }, 3000);
}

// Handles an application error with a notification message.
function handleApplicationError(notificationsService, message) {
    var title = "Unexpected Error";
    notificationsService.error(title, message);
}

// Handles a server error with a notification message.
function handleServerError(notificationsService) {
    var title = "Server Error";
    var message = "There was an issue communicating with the server.";
    notificationsService.error(title, message);
}

// Shows a success notification.
function showSuccess(notificationsService) {
    var title = "Success";
    var message = "You have been granted access to the \"Formulate\" section. The page will automatically refresh in a few seconds.";
    notificationsService.success(title, message);
}

// Gets the options variable for the HTTP request.
function getReqestOptions() {
    return {
        headers: {
            "Content-Type": "application/json"
        }
    };
}