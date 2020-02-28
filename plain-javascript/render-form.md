---
layout: page
title: Rendering a Form Using Formulate's Plain JavaScript Template
---

# Plain JavaScript Template

Note that this page only applies to the plain JavaScript template (i.e., it does not apply to the AngularJS template).

Also, this applies to Umbraco 8. For the Umbraco 7 version, go here: [github.com/.../plain-javascript/render-form.md](https://github.com/rhythmagency/formulate/blob/a0b705a71a6a9034e1041d7c8d08ee4a748a6359/plain-javascript/render-form.md)

# Form Rendering

Once you've gone through the trouble of installing Formulate, creating a form, and modifying a document type to pick a form,
the final step is to render that form to HTML on your website.

You can render your picked form like this (this assumes the document type is called "Contact" and the form picker property is called "My Form"):

```csharp
@inherits UmbracoViewPage<Contact>
<!doctype html>

<html>
<head>
    <title>Formulate Plain JavaScript Example</title>
</head>
<body>

    @* Wrap the form with this element so the custom JavaScript can find it. *@
    <div class="formulate-wrapper">

        @* Render the form. *@
        @Html.Action("Render", "FormulateRendering", new { form = Model.MyForm })

    </div>

    @* The JavaScript file for Formulate's plain JavaScript template. *@
    <script src="/App_Plugins/formulate/responsive.plain-javascript.min.js" async></script>

    @* This is your JavaScript that handles events dispatched by Formulate. *@
    <script src="/scripts/custom-formulate-script.js" async></script>

</body>
</html>
```

That goes in a CSHTML file.

You will also need some custom JavaScript to respond to the events dispatched by Formulate (this is the `custom-formulate-script.js` referenced above):

```js
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

            // When proceeding to the next step in a multi-step form, clear any validation errors.
            wrapper.addEventListener("formulate: validation: next: success", function () {
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
```

Next up, you'll want to style the form: [Styles](/plain-javascript/styles)
