// Variables.
var app = angular.module("umbraco");

// Service to help with server communication.
app.factory("formulateServer", function ($http, $q, notificationsService) {

    // Variables.
    var services = {
        $http: $http,
        $q: $q,
        notificationsService: notificationsService
    };

    // Return service.
    return {

        // Performs an HTTP GET.
        get: getGet(services),

        // Performs an HTTP POST.
        post: getPost(services)

    };

});

// Returns the function that makes a GET request to the server.
function getGet(services) {
    return function (url, params, successCallback) {

        // Set options.
        var options = {
            cache: false,
            params: {
                // Cache buster ensures requests aren't cached.
                CacheBuster: Math.random()
            }
        };
        angular.extend(options.params, params || {});

        // Make request.
        return services.$http.get(url, options)
            .then(
                getHandleResponse(services, successCallback),
                getHandleServerError(services));

    };

}

// Returns the function that makes a POST request to the server.
function getPost(services) {
    return function (url, data, successCallback) {

        // Variables.
        var strData = JSON.stringify(data);
        var options = {
            headers: {
                "Content-Type": "application/json"
            }
        };

        // Send request to server.
        return services.$http.post(url, strData, options)
            .then(
                getHandleResponse(services, successCallback),
                getHandleServerError(services));

    };
}

// Returns the function that handles a server error.
function getHandleServerError(services) {
    return function() {

        // Indicate error with notification.
        var title = "Server Error";
        var message = "There was an issue communicating with the server.";
        services.notificationsService.error(title, message);
        return services.$q.reject();

    };
}

// Returns the function that handles a server response.
function getHandleResponse(services, successCallback) {
    return function (response) {

        // Variables.
        var data = response.data;

        // Was the request successful?
        if (data.Success) {
            return successCallback(data);
        } else {

            // Error notification.
            var title = "Unexpected Error";
            var message = data.Reason;
            services.notificationsService.error(title, message);
            return services.$q.reject();

        }

    };
}