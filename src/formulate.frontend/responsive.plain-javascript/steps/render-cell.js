/**
 * Renders a cell within a row of a Formulate form.
 * @param columnCount The number of columns this cell spans.
 * @returns {HTMLDivElement} The DOM element for the cell.
 */
function renderCell(columnCount) {

    // Variables.
    let cellElement;

    // Create the element.
    cellElement = document.createElement("div");

    // Add CSS classes to element.
    cellElement.classList.add("formulate__cell");
    cellElement.classList.add("formulate__cell--" + columnCount.toString() + "-columns");

    // Return the DOM element for the cell.
    return cellElement;

}

// Export the function that renders a cell.
module.exports = renderCell;