/* This file listens for events dispatched by Formulate and responds to them appropriately. */

// Wait until the markup has rendered
setTimeout(function () {

    // Variables.
    let i, wrapper, wrappers = document.querySelectorAll(".formulate-wrapper");

    // Process each element that is wrapping a Formulate form.
    for (i = 0; i < wrappers.length; i++) {
        wrapper = wrappers[i];
        (function (wrapper) {

            // Variables.
            let validationListElement;

            // On form submit, remove the form and display a success message. You can do what you like here
            // (e.g., show a hidden element).
            wrapper.addEventListener("formulate form: submit: success", function (e) {
                let form = e.target;
                form.parentNode.replaceChild(document.createTextNode("Form submitted!"), form);
                if (validationListElement) {
                    validationListElement.parentNode.removeChild(validationListElement);
                }
                validationListElement = null;
            });

            // When there is an error, show an alert dialog. Feel free to change this to something
            // that makes more sense for your project.
            wrapper.addEventListener("formulate form: submit: failure", function () {
                alert("Unknown error. Please try again.");
            });

            // When there are validation errors, add a list of error messages to the bottom of the
            // form. If you remove this, the error messages will still be shown inline below each field.
            wrapper.addEventListener("formulate: submit: validation errors", function (e) {
                let i, message, messages = e.detail.messages, form = e.target, listElement, itemElement;
                listElement = document.createElement("ul");
                listElement.classList.add("formulate__validation-summary");
                for (i = 0; i < messages.length; i++) {
                    message = messages[i];
                    itemElement = document.createElement("li");
                    itemElement.classList.add("formulate__validation-summary__error");
                    itemElement.appendChild(document.createTextNode(message));
                    listElement.appendChild(itemElement);
                }
                if (validationListElement) {
                    validationListElement.parentNode.replaceChild(listElement, validationListElement);
                }
                validationListElement = listElement;
                form.parentNode.appendChild(listElement);
            });

        })(wrapper);
    }

}, 0);