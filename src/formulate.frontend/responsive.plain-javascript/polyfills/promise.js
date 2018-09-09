// Variables.
let FormulatePromise;

// Get either the promise polyfill or the native promise.
if (typeof Promise === "undefined") {
    FormulatePromise = require("promiscuous/dist/promiscuous-browser-shim-full");
} else {
    FormulatePromise = Promise;
}

// Export the promise function (either native or the polyfill).
module.exports = FormulatePromise;