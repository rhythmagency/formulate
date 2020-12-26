/**
 * Renders a row in a Formulate form.
 * @param stepIndex {number} The index of the step this row belongs to.
 * @param isActiveStep {boolean} Is this row in the currently active step?
 * @returns {HTMLDivElement} The DOM element for the row.
 */
function renderRow(stepIndex, isActiveStep) {

    // Variables.
    let rowElement;

    // Create the DOM element for the row.
    rowElement = document.createElement("div");

    // Add a CSS class to the DOM element.
    rowElement.classList.add("formulate__row");
    rowElement.classList.add("formulate__row--step-" + stepIndex.toString());
    rowElement.classList.add("formulate__row--" + (isActiveStep ? "active" : "inactive"));
    if (isActiveStep) {
        rowElement.classList.add("formulate__row--active-initial");
    }
    else
    {
        rowElement.classList.add("formulate__row--inactive-and-disabled");
    }

    // Return the DOM element for the row.
    return rowElement;

}

// Export the function that renders a row.
module.exports = renderRow;