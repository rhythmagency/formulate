---
layout: page
title: Creating Custom Formulate Field Types with Plain JavaScript
---

# Plain JavaScript or AngularJS?

You are currently reading the documentation for adding custom fields with AngularJS.
If you are working with the Plain JavaScript template, you will instead want to read [Custom Field Types (AngularJS)](/articles/custom-field-types-angularjs).

# Custom Formulate Field Types

Formulate comes with a few common field types, such as text, checkbox, drop down, and upload fields.
However, you aren't limited to just these. You can create your own custom field types.
Read the sections below for instructions on how to do this.

When you are done, your new field type should appear in the dialog to add a field:

![Field Picker Dialog](/images/field-picker.png)

# The Back Office Directive

In order to allow for your custom field type to be used when constructing a form in the back office form designer,
you'll need to create an AngularJS directive. There are typically two components to this, as shown with this drop down example:

* [Directive Markup](https://github.com/rhythmagency/formulate/blob/af76b07d6e31755f32105ff502022060db31ae8e/src/formulate.app/Directives/fields/dropDownField/dropDownField.html)
* [Directive JavaScript](https://github.com/rhythmagency/formulate/blob/af76b07d6e31755f32105ff502022060db31ae8e/src/formulate.app/Directives/fields/dropDownField/dropDownField.js)

The markup is what is displayed below the main components (name, alias, label) of each field.
In this example of the drop down, it is just displaying a data value picker
(so the user can choose the items to appear in the drop down).

The JavaScript is what defines the behavior of the directive.
In the case of the drop down, it is defining the directive (including the markup to use for the template),
and it has some functions to manage the picked data value (e.g., to store it to the configuration for the field).

Note that when creating a custom field type, you will not be able to rely on the `formulateDirectives` service
to get the markup for your directive. Instead, you can embed the markup directly in the JavaScript as a string (not recommended),
specify a URL to your directive markup, or create your own service that can load up the markup for your directive.
Of those options, the simplist is probably to just specify the URL, which would look something like this:

```
function directive() {
    return {
        restrict: "E",
        replace: true,
        // Note that it's "templateUrl" rather than "template".
        templateUrl: "/some-path/myCustomField.html",
        controller: "formulate.myCustomField",
        scope: {
            configuration: "="
        }
    };
}
```

# The Field Type

To let Formulate know about your new type of field, you'll need to create a class that implements the `IFormFieldType` interface.
Formulate uses reflection to find any classes that implement this interface, and these implementations give Formulate
all it needs to know about those field types. Since the drop down example is a bit complex, I'll start with a simple example,
the text field:

* [TextField.cs](https://github.com/rhythmagency/formulate/blob/af76b07d6e31755f32105ff502022060db31ae8e/src/formulate.app/Forms/Fields/Text/TextField.cs)

It has just a few properties and a function. Here is the purpose of each of them:

* **Directive.** This property indicates the AngularJS directive to render in the back office in the form designer.
* **TypeLabel.** This property indicates the text to display to the user in the dialog that allows them to add a new field to their form in the form designer.
* **Icon.** This property indicates the icon to display to the user in the dialog that allows them to add a new field to their form in the form designer.
* **TypeId.** This property is a GUID that uniquely identifies the field type (e.g., when serializing the form definition to the file system).
* **DeserializeConfiguration.** This function deserializes the string version of the field's configuration. Since the text field doesn't have any configuration, it just returns null.

For a more extensive example of a field type class, refer to [DropDownField.cs](https://github.com/rhythmagency/formulate/blob/af76b07d6e31755f32105ff502022060db31ae8e/src/formulate.app/Forms/Fields/DropDown/DropDownField.cs).

# Render Field for Plain JavaScript Template

Once you have created the C# file and back office AngularJS, you will need to render your field on the frontend
using plain JavaScript. Suppose you want your field to be a simple text field that also displays the field
data below the text field. Here is what that would look like:

![Formulate](/images/custom-field-plain-js.png)

Below is an example of the JavaScript for a field called "CustomField" (you will need to add a script tag in one of
your Razor views to be sure you include this JavaScript). Note that the name of your C# class must match the value
of the key you use in the below JavaScript when registering the field on the window object:

```javascript
// Class that manages your custom field.
class CustomField {

    // Renders the field.
    constructor(fieldData, fieldValidators, cssClasses) {

        // Set instance properties.
        this.id = fieldData.id;
        this.alias = fieldData.alias;

        // Generate the markup for the field, including a wrapper element and a label.
        let autoId = "field_" + Math.random().toString().replace(".", "");
        let markup = `
            <div>
                <input type="text" id="${autoId}" />
                <label class="formulate__field__label" for="${autoId}"></label>
                <pre></pre>
            </div>
        `.trim();
        let docFragment = document.createRange().createContextualFragment(markup);

        // Add some debugging information.
        docFragment.querySelector('pre')
            .appendChild(document.createTextNode(JSON.stringify(fieldData, null, '  ')));

        // Extract the elements from the document fragment.
        this.wrapper = docFragment.querySelector("div");
        this.element = docFragment.querySelector("input");

        // Add CSS classes to the wrapper element.
        (cssClasses || []).forEach(x => this.wrapper.classList.add(x));

        // Set the field label text.
        docFragment.querySelector("label").appendChild(document.createTextNode(fieldData.label));

        // Configure the field validators.
        this.validators = prepareValidators(fieldData.validations, fieldValidators);

    }

    // Returns the DOM element Formulate will inject into the form.
    getElement() {
        return this.wrapper;
    }

    // Adds the data for this field on the specified FormData instance.
    setData(data, options) {
        setData(data, this.element.value, options, this.alias, this.id);
    }

    // Checks the validity of the value in this field (adding inline validation messages if necessary).
    checkValidity() {
        return checkValidityCommon(this, this.validators, this.element.value, this.wrapper, "validateText");
    }

}

// Store the field renderer configuration on the window so Formulate can access it.
let key = "formulate-plain-js-fields";
window[key] = window[key] || [];
window[key] = {
    key: "CustomField",
    renderer: CustomField
};



/************************************************************************
 * The below functions are necessary to copy if you are not using modular
 * JavaScript to import these from Formulate's built-in helper functions.
 ************************************************************************/



// Copied from responsive.plain-javascript/utils/validation.js.
let checkValidityCommon = function (fieldRenderer, validators, value, containerElement, validityFnName) {

    // Variables.
    let i, validator, validationResults;

    // Check each validator for the validity of the value in this field.
    validationResults = [];
    for (i = 0; i < validators.length; i++) {
        validator = validators[i];
        validationResults.push(checkValidity(validator, value, validityFnName));
    }

    // Add inline validation messages.
    aggregateValidations(validationResults)
        .then(function (result) {

            // Add inline validation messages.
            fieldRenderer.validationListElement = addValidationMessages(
                containerElement, result.messages, fieldRenderer.validationListElement);

            // Add or remove validation error CSS class.
            if (result.success) {
                containerElement.classList.remove("formulate__field--validation-error");
            } else {
                containerElement.classList.add("formulate__field--validation-error");
            }

        });

    // Return the validation results.
    return validationResults;

};

// Copied from responsive.plain-javascript/utils/validation.js.
function checkValidity(validator, value, validityFnName) {
    return validator.validator[validityFnName](value)
        .then(function (result) {
            if (result) {

                // Success.
                return {
                    success: true
                };

            } else {

                // Failure. Return validation message.
                return {
                    success: false,
                    message: validator.data.configuration.message
                };

            }
        });
}

// Copied from responsive.plain-javascript/utils/validation.js.
let aggregateValidations = function (validationPromises) {

    // Return a promise that resolves to the result of all of the validations.
    return Promise.all(validationPromises)
        .then(function (results) {

            // Variables.
            let i, result, success, failures;

            // Extract all the failures from the validation results.
            failures = [];
            for (i = 0; i < results.length; i++) {
                result = results[i];
                if (!result.success) {
                    failures.push(result);
                }
            }

            // Success if there are no failures.
            success = failures.length === 0;
            if (success) {

                // Success.
                return {
                    success: true
                };

            } else {

                // Failure. Return validation messages for the failures.
                return {
                    success: false,
                    messages: failures.map(function (x) {
                        return x.message;
                    })
                };

            }

        });

};

// Copied from responsive.plain-javascript/utils/validation.js.
let addValidationMessages = function (containerElement, messages, priorListElement) {

    // If there are no messages, remove the prior list element and return early.
    if (!messages || messages.length === 0) {
        if (priorListElement) {
            priorListElement.parentNode.removeChild(priorListElement);
        }
        return null;
    }

    // Variables.
    let i, listElement, message, itemElement;

    // Create the list element that contains the messages.
    listElement = document.createElement("ul");
    listElement.classList.add("formulate__inline-validation-summary");

    // Add the messages to the list element.
    for (i = 0; i < messages.length; i++) {
        message = messages[i];
        itemElement = document.createElement("li");
        itemElement.classList.add("formulate__inline-validation-summary__error");
        itemElement.appendChild(document.createTextNode(message));
        listElement.appendChild(itemElement);
    }

    // Remove the prior list element.
    if (priorListElement) {
        priorListElement.parentNode.removeChild(priorListElement);
    }

    // Add the new list element to the container.
    containerElement.appendChild(listElement);

    // Return the new list element (expected to be passed in on the subsequent call as the
    // prior list element).
    return listElement;

};

// Copied from responsive.plain-javascript/utils/validation.js.
let prepareValidators = function (validationData, fieldValidators) {

    // Validate input.
    if (!validationData || !fieldValidators) {
        return [];
    }

    // Variables.
    let i, validationOptions, validator, key, preparedValidators;

    // Process the validation data to prepare the validators.
    preparedValidators = [];
    for (i = 0; i < validationData.length; i++) {
        validationOptions = validationData[i];
        key = validationOptions.validationType;
        validator = fieldValidators[key];
        preparedValidators.push({
            validator: new validator(validationOptions.configuration),
            data: validationOptions
        });
    }

    // Return the prepared validators.
    return preparedValidators;

};

// Copied from responsive.plain-javascript/utils/field.js.
let setData = function (data, value, options, alias, id) {

    // Adjust options.
    options = options || {};
    options.rawDataByAlias = options.rawDataByAlias || false;

    // Set data.
    if (options.rawDataByAlias) {
        if (alias) {
            data[alias] = value;
        }
    } else {
        data.append(id, value);
    }

};
```

Notice the comment near the top that says most of the JavaScript near the bottom is copied from the Formulate
core JavaScript. If you are familiar with modular JavaScript (the kind with `require` statements) and you would
like to avoid copying all of that extra JavaScript, you can follow an example from the main Formulate code base.
For example, you could refer to the JavaScript for the text field: [plain-text.js](https://github.com/rhythmagency/formulate/blob/v3/master/src/formulate.frontend/responsive.plain-javascript/fields/plain-text.js)

These core JavaScript files are not available on NPM, so if you take the modular approach you will need to either
[download them from GitHub](https://github.com/rhythmagency/formulate/tree/v3/master/src/formulate.frontend/responsive.plain-javascript)
or refer to the ones that are installed into your website when Formulate is installed.

If you need to support IE11 and you are using the non-modular approach, be sure to use Babel to transpile to ES5
and be sure to include a promise polyfill. The Formulate core includes a promise polyfill already, but you will not be
able to use it unless you use the modular approach. Here is one promise polyfill you might consider using:
[promise-polyfill](https://cdnjs.com/libraries/promise-polyfill)

# Review

You should now have a working custom field type. Here were the steps you took:

* Create an AngularJS directive (in the Umbraco back office), including the JavaScript that refers to the markup used by the directive.
* Create a C# class that implements the `IFormFieldType` interface.
* Render the field renderer used by the plain JavaScript template (on the frontend of the website).

If you have created a custom field type, be sure to [let me know](https://github.com/rhythmagency/formulate/issues) so it can be
considered for incorporation into the core of Formulate.
If you have any questions, feel free to post a message in the [forum](https://our.umbraco.org/projects/backoffice-extensions/formulate/formulate-questions/).