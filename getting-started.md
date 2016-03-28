---
layout: page
title: Formulate
---

# Integration
In order to submit forms, you'll have to do a bit of integration. The high-level steps are:

* Add one of the Formulate frontends.
* Create a C# controller so the frontend can submit data to Formulate.
* Modify the frontend to submit to the controller you created.

# Form Rendering
You can render your picked form like this (this assumes the form picker property is called "myFormPicker"):

```csharp
@using formulate.app.Types
@inherits Umbraco.Web.Mvc.UmbracoTemplatePage
@{
    Layout = null;
    var pickedForm = Model.Content.GetPropertyValue<ConfiguredFormInfo>("myFormPicker");
    var vm = formulate.api.Rendering.GetFormViewModel(pickedForm.FormId, pickedForm.LayoutId, pickedForm.TemplateId);
}
@Html.Partial("~/Views/Partials/Formulate/RenderForm.cshtml", vm)
```