/**
 * Renders a row in a Formulate form.
 * @returns {HTMLDivElement} The DOM element for the row.
 */
function renderRow() {

    // Variables.
    let rowElement;

    // Create the DOM element for the row.
    rowElement = document.createElement("div");

    // Add a CSS class to the DOM element.
    rowElement.classList.add("formulate__row");

    // Return the DOM element for the row.
    return rowElement;

}

// Export the function that renders a row.
module.exports = renderRow;