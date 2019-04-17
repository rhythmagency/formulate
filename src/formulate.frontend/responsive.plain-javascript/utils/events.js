/**
 * Dispatches the specified event.
 * @param element The element to dispatch the element on.
 * @param eventName The event to dispatch.
 * @param data The data to send with the event.
 */
function dispatchEvent(element, eventName, data) {
    let event;
    if (typeof window.CustomEvent === "function") {

        // Typical implementation for CustomEvent.
        event = new CustomEvent(eventName, {
            bubbles: true,
            detail: data
        });
        element.dispatchEvent(event);

    } else {

        // IE11 implementation for CustomEvent.
        event = document.createEvent("CustomEvent");
        event.initCustomEvent(eventName, true, false, data);
        element.dispatchEvent(event);

    }
}

// Export the function that dispatches an event.
module.exports = dispatchEvent;