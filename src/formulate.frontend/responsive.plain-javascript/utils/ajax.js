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
 * @param {SendRequestCallback} callback - The function to call when the form has completed posting to the server.
 * @constructor
 */
function SendRequest(url, payload, callback) {

    // Variables.
    let self = this;

    // Instance variables.
    this.callback = callback;
    this.request = new XMLHttpRequest();

    // Initiate the AJAX request.
    this.request.onreadystatechange = function () {
        self.handleStateChange();
    };
    this.request.open(METHOD_POST, url, true);
    this.request.send(payload);

}

/**
 * Called whenever the state changes for the AJAX call.
 */
SendRequest.prototype.handleStateChange = function () {

    // Is the request done?
    if (this.request.readyState === STATE_DONE) {

        // Was the request successful?
        if (this.request.status === STATUS_SUCCESS){

            // Success.
            this.callback(STATUS_SUCCESS, this.request.responseText);

        } else {

            // Error.
            this.callback(this.request.status, null);

        }

    }

};

// Export the function that sends an AJAX request.
module.exports = SendRequest;