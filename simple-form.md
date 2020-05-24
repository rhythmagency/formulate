---
layout: page
title: Creating a Simple Form in Formulate
---

# Creating a Simple Form

To create a simple form, follow these steps:

* Create a form.
* Add a handler.
* Create a layout.
* Create a form configuration.
* Pick the form configuration.
* Render the form.
* Style the form.

Here are those steps, explained in detail:

* Create a form, adding any fields that are necessary (don't forget to add at least one button field).
![Create Formulate Form](/images/simple-form/form.png)
* Add a handler so you get an email when the form is submitted. Be sure you have configured SMTP in your web.config (instructions [here](/smtp-configuration)).
![Add Formulate Handler](/images/simple-form/handlers.png)
* Create a layout, picking the form you just created.
![Create Formulate Layout](/images/simple-form/layout.png)
* Create a form configuration, picking a template and the layout you just created.
![Create Formulate Form Configuration](/images/simple-form/config.png)
* Pick the form configuration by following these steps:
  * Create a form picker data type in the developer section.
  * Add a form picker property to one of your document types in the settings section.
  * Pick your form from the content node.
![Pick Formulate Form](/images/simple-form/picker.png)
* Render your picked form in the CSHTML file for your page. See [here](/plain-javascript/render-form) for instructions on doing that.
![Rendered Formulate Form](/images/simple-form/done.png)
* Style your form to match the design of your site. Here is an example of an actual styled Formulate form:
![Styled Formulate Form](/images/simple-form/styled.png)