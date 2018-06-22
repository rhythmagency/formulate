// A readyState of 4 indicates the request has completed: https://developer.mozilla.org/en-US/docs/Web/API/XMLHttpRequest/readyState
const STATE_DONE = 4;

// An HTTP response code of 200 indicates success.
const STATUS_SUCCESS = 200;

// HTTP request method to post data to the server: https://developer.mozilla.org/en-US/docs/Web/HTTP/Methods/POST
const METHOD_POST = "POST";

/**
 * The function called when a SendRequest response is ready.
 * @callback SendRequestCallback
 * @param statusCode {number} The HTTP response code.
 * @param result The response data from the request.
 */

/**
 * Posts a form to the server with AJAX.
 * @param url {string} The URL to post the form to.
 * @param payload {FormData} The form data to send with the request.
 * @returns {Promise<any>} A promise that resolves once the request is complete.
 * @constructor
 */
function SendRequest(url, payload) {

    // Variables.
    let self = this, Promise = require("../polyfills/promise"), state;

    // Instance variables.
    this.request = new XMLHttpRequest();

    // Return a promise that will resolve once the AJAX request has returned from the server.
    return new Promise(function (resolve, reject) {

        // Initiate the AJAX request.
        self.request.onreadystatechange = function () {
            state = self.handleStateChange();
            if (state && state.status === STATUS_SUCCESS) {
                resolve(state);
            } else if (state && state.status !== STATUS_SUCCESS) {
                reject(state);
            }
        };
        self.request.open(METHOD_POST, url, true);
        self.request.send(payload);

    });

}

/**
 * Called whenever the state changes for the AJAX call.
 * @returns {{} | null} The result of the state change, or null if it's an irrelevant state change.
 */
SendRequest.prototype.handleStateChange = function () {

    // Is the request done?
    if (this.request.readyState === STATE_DONE) {

        // Was the request successful?
        if (this.request.status === STATUS_SUCCESS){

            // Success.
            return {
                status: STATUS_SUCCESS,
                text: this.request.responseText
            };

        } else {

            // Error.
            return {
                status: this.request.status
            };

        }

    }

    // Not a state change we care about.
    return null;

};

// Export the function that sends an AJAX request.
module.exports = SendRequest;