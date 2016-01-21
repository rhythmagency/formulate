// Variables.
var app = angular.module("umbraco");

// Service to help with permissions.
app.factory("formulatePermissions", function (notificationsService,
    formulateVars, formulateServer) {
    var services = {
        notificationsService: notificationsService,
        formulateVars: formulateVars,
        formulateServer: formulateServer
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
    var notificationsService = services.notificationsService,
        formulateVars = services.formulateVars;

    // Return function.
    return function () {

        // Variables.
        var url = formulateVars.PermitAccess;

        // Send request to set permissions.
        services.formulateServer.post(url, {}, function (data) {

            // Success notification.
            showSuccess(notificationsService);

            // Reload page.
            reloadPage();

        });

    };

}

// Reloads the page after 3 seconds.
function reloadPage() {
    setTimeout(function () {
        window.location.reload(true);
    }, 3000);
}

// Shows a success notification.
function showSuccess(notificationsService) {
    var title = "Success";
    var message = "You have been granted access to the \"Formulate\" section. The page will automatically refresh in a few seconds.";
    notificationsService.success(title, message);
}