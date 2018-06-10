/**
 * Adds CSS classes to a DOM element
 * @param element The DOM element to add classes to.
 * @param cssClasses The CSS classes to add to the element.
 */
function addClasses(element, cssClasses) {

    // Variables.
    let i;

    // Add each CSS class to the element.
    for (i = 0; i < cssClasses.length; i++) {
        element.classList.add(cssClasses[i]);
    }

}

// Export the function that adds CSS classes to an element.
module.exports = addClasses;