/**
 * Manages toggling items in an accordion (e.g., the form handlers accordion).
 */
class FormulateAccordion {

    // Service properties.
    $scope;
    $timeout;

    /**
     * Constructor (mainly used to pass in services).
     * @param services The services required by this class.
     */
    constructor(services) {
        Object.keys(services).forEach((x) => this[x] = services[x]);
    }

    /**
     * Handles the click that toggles an item in the accordion.
     * @param item The item to toggle.
     * @param itemEl The DOM element for the item.
     * @param contentsSelector The selector used to find the contents of the accordion item.
     */
    handleClick(item, itemEl, contentsSelector) {

        // Variables.
        const detailsEl = itemEl.querySelector(contentsSelector);

        // Toggle the expanded state.
        item.expanded = !item.expanded;

        // Is the item now expanded or collapsed?
        if (item.expanded) {
            this.handleExpand(item, detailsEl);
        } else {
            this.handleCollapse(item, detailsEl)
        }

    }

    /**
     * Evaluates the supplied function in an AngularJS context after
     * a timeout and an animation frame. This common pattern is used in situations
     * where we need to wait for some element to apply to the DOM, and then
     * make further changes that interact with AngularJS models.
     * @param fn The function to run.
     */
    timeoutAnimateEval(fn) {
        this.$timeout(() => {
            requestAnimationFrame(() => {
                this.$scope.$evalAsync(fn);
            });
        });
    }

    /**
     * Handles the expansion of the accordion item.
     * @param item The accordion item.
     * @param detailsEl The DOM element for the accordion item contents.
     */
    handleExpand(item, detailsEl) {

        // We'll momentarily expand the item, so we can figure out its height.
        item.quickExpand = true;
        item.detailsStyle = {
            maxHeight: null,
            visibility: 'hidden',
        };

        // Use timeout to allow a digest cycle to run so the changes will be reflected
        // in the DOM.
        this.timeoutAnimateEval(() => {

            // Get the height of the element now that it's been increased to its
            // max height.
            const height = detailsEl.offsetHeight;

            // Shrink the element back to no height.
            item.detailsStyle = {
                maxHeight: '0',
                visibility: null,
            };

            // Run in a timeout to give the above changes time to take effect in the DOM,
            // then wait for the height to be reset to zero before the next step.
            this.timeoutAnimateEval(() => {

                // Increase the size of the contents by setting the max height
                // (the CSS will then animate the height).
                item.quickExpand = false;
                item.detailsStyle = {
                    maxHeight: height + 'px',
                };

                // Do some cleanup once the contents finish expanding.
                this.handleFullyExpanded(item, detailsEl);

            });

        });

    }

    /**
     * When the contents have fully expanded, remove the max height
     * (that way, if the window resizes, the contents won't be
     * artificially shortened).
     * @param item The accordion item.
     * @param detailsEl The DOM element for the accordion item contents.
     */
    handleFullyExpanded(item, detailsEl) {
        const transitionEnd = () => {
            detailsEl.removeEventListener('transitionend', transitionEnd);
            this.$scope.$evalAsync(() => {
                item.detailsStyle = {
                    maxHeight: null,
                };
            });
        };
        detailsEl.addEventListener('transitionend', transitionEnd);
    }

    /**
     * Handles collapsing the accordion item contents.
     * @param item The accordion item.
     * @param detailsEl The DOM element for the accordion item contents.
     */
    handleCollapse(item, detailsEl) {

        // Set the max height based on the current expanded height of the element.
        const height = detailsEl.offsetHeight;
        item.detailsStyle = {
            maxHeight: height + 'px',
        };

        // Wait for the max height to be applied to the DOM.
        this.timeoutAnimateEval(() => {

            // Remove the max height so the CSS will set it, which will cause
            // the max height to animate by collapsing.
            item.detailsStyle = {
                maxHeight: null,
            };

        });

    }

}

// Make t his class accessible to other JavaScript files.
window.FormulateAccordion = FormulateAccordion;