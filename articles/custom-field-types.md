---
layout: page
title: Creating Custom Formulate Field Types
---

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

# Render Field in Responsive Bootstrap Angular (RBA) Template

While Formulate allows for custom templates to render forms,
it is likely you will want to render your field in the Responsive Bootstrap Angular (RBA) template.
When you install Formulate, you will find the JavaScript for this template in `~/App_Plugins/formulate/responsive.bootstrap.angular.js`.
However, this JavaScript file is actually split across multiple files in the Formulate source code
(part of the Formulate compilation process is to combine them all into one file).
You can view them here:

* [index.js](https://github.com/rhythmagency/formulate/blob/af76b07d6e31755f32105ff502022060db31ae8e/src/formulate.app/JavaScript/FormTemplates/responsive.bootstrap.angular/index.js)
* [field.js](https://github.com/rhythmagency/formulate/blob/af76b07d6e31755f32105ff502022060db31ae8e/src/formulate.app/JavaScript/FormTemplates/responsive.bootstrap.angular/directives/form/field.js)
* [fileChange.js](https://github.com/rhythmagency/formulate/blob/af76b07d6e31755f32105ff502022060db31ae8e/src/formulate.app/JavaScript/FormTemplates/responsive.bootstrap.angular/directives/form/fileChange.js)
* [fileUpload.js](https://github.com/rhythmagency/formulate/blob/af76b07d6e31755f32105ff502022060db31ae8e/src/formulate.app/JavaScript/FormTemplates/responsive.bootstrap.angular/directives/form/fileUpload.js)
* [responsiveForm.js](https://github.com/rhythmagency/formulate/blob/af76b07d6e31755f32105ff502022060db31ae8e/src/formulate.app/JavaScript/FormTemplates/responsive.bootstrap.angular/directives/form/responsiveForm.js)
* [rows.js](https://github.com/rhythmagency/formulate/blob/af76b07d6e31755f32105ff502022060db31ae8e/src/formulate.app/JavaScript/FormTemplates/responsive.bootstrap.angular/directives/form/rows.js)
* [validation.js](https://github.com/rhythmagency/formulate/blob/af76b07d6e31755f32105ff502022060db31ae8e/src/formulate.app/JavaScript/FormTemplates/responsive.bootstrap.angular/directives/form/validation.js)
* [json-source.js](https://github.com/rhythmagency/formulate/blob/af76b07d6e31755f32105ff502022060db31ae8e/src/formulate.app/JavaScript/FormTemplates/responsive.bootstrap.angular/directives/json-source/json-source.js)

It is within these files that the RBA template creates the form from the form definition.
When creating a new type of field, you will mostly likely want to modify the `field.js` file.
For example, here are the code snippets from that file that render the text field:

```
function createTextField(field) {
    var el = angular.element('<input type="text" />');
    el.attr('placeholder', field.label);
    return setGlobalInputAttributes(field, el);
}
```

```
switch (field.fieldType) {
case 'select':
    addLabel(elWrap, field);
    elWrap.addClass('formulate__group--select');
    elWrap.append(createSelectField(field));
    break;

//...other fields omitted for brevity..

 default:
    addLabel(elWrap, field);
    elWrap.append(createTextField(field));
}
```

You can see that the `createTextField` function creates the `<input>` element and attaches some attributes to it.
There is also a `switch` statement that creates a field based on its type.
You can see from the above code snippet that if the type is `select` (which is the field type used for drop downs),
a drop down will be created. And the default case is to create a text field.
It is within this switch statement that you would add your own field type to generate
the DOM element for it when rendering the form.

# Copying the RBA Template

If you customize the RBA template, anytime you upgrade Formulate you risk overwriting your customizations.
For this reason, it is recommended that you first copy the template and reply upon that copy rather than
using the original RBA template. You can do this with a few minor changes.

First, copy the `responsive.bootstrap.angular.js` file and name it something different.
Be sure to reference this new file from whatever template used to reference the old JavaScript file.

Next, copy the `Responsive.Bootstrap.Angular.cshtml` file (in the `~/Views/Partials/Formulate` folder) and name it something different.

Finally, modify your `templates.config` file (in the `~/Config/Formulate` folder) to add the path to your new template
(be sure to also assign it a new GUID):

```
<?xml version="1.0" encoding="utf-8" ?>
<templates>
    <template id="0088536F694C4C4E949EBF6B91D32B59" name="Responsive" path="~/Views/Partials/Formulate/Responsive.Bootstrap.Angular.cshtml" />
    <template id="some-other-guid" name="My Custom Template" path="~/Views/Partials/Formulate/My Custom Template.cshtml" />
</templates>
```

If you have already used the original template when creating your forms, you will want to visit all of their form configurations
(remember, form configurations are created under each form) to select the new template.

By doing this, you will ensure that your custom changes won't be overwritten when you upgrade to a new version of Formulate.

# Review

You should now have a working custom field type. Here were the steps you took:

* Create an AngularJS directive, including the JavaScript that refers to the markup used by the directive.
* Create a class that implements the `IFormFieldType` interface.
* Render the field in the Responsive Bootstrap Angular (RBA) template.
* Copy the RBA template to avoid erasing your customizations when you upgrade Formulate.

If you have created a custom field type, be sure to [let me know](https://github.com/rhythmagency/formulate/issues) so it can be
considered for incorporation into the core of Formulate.
If you have any questions, feel free to post a message in the [forum](https://our.umbraco.org/projects/backoffice-extensions/formulate/formulate-questions/).