"use strict";

(function () {
    // Variables.
    var app = angular.module("umbraco");

    // Service to help with server communication.
    app.factory("formulateServer", ["$http", "$q", "umbRequestHelper", function ($http, $q, umbRequestHelper) {

        // Variables.
        var services = {
            $http: $http,
            $q: $q,
            umbRequestHelper: umbRequestHelper
        };

        // Return service.
        return {

            // Performs an HTTP GET.
            get: performGet(services),

            // Performs an HTTP POST.
            post: performPost(services)

        };

    }]);

    // Returns the function that makes a GET request to the server.
    function performGet(services) {
        return function (url, params, callbackOptions) {
            const options = {
                cache: false,
                params: {
                    // Cache buster ensures requests aren't cached.
                    CacheBuster: Math.random()
                }
            };
            angular.extend(options.params, params || {});

            const promise = services.$http.get(url, options);
            return services.umbRequestHelper.resourcePromise(promise, callbackOptions);
        };

    }

    // Returns the function that makes a POST request to the server.
    function performPost(services) {
        return function (url, data, callbackOptions) {

            // Variables.
            const strData = JSON.stringify(data);
            const options = {
                headers: {
                    "Content-Type": "application/json"
                }
            };

            // Send request to server.
            const promise = services.$http.get(url, strData, options);
            return services.umbRequestHelper.resourcePromise(promise, callbackOptions);
        };
    }
})();
