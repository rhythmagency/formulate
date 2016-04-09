---
layout: page
title: Getting Started with Formulate
---

# Form Rendering
You can render your picked form like this (this assumes the form picker property is called "myFormPicker"):

```csharp
@using formulate.app.Types
@inherits Umbraco.Web.Mvc.UmbracoTemplatePage
@{
    Layout = null;
    var pickedForm = Model.Content.GetPropertyValue<ConfiguredFormInfo>("myFormPicker");
    var vm = formulate.api.Rendering.GetFormViewModel(pickedForm.FormId, pickedForm.LayoutId,
        pickedForm.TemplateId);
}<!doctype html>

<html>
<head>
    <title>Formulate Example</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/lodash.js/4.5.1/lodash.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.4/jquery.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.4.8/angular.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.4.8/angular-messages.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js" integrity="sha384-0mSbJDEHialfmuBBQP6A4Qrprq5OVfW37PRR3j5ELqxss1yVqOtnepnHVP9aJ7xS" crossorigin="anonymous"></script>
    <script src="/App_Plugins/formulate/responsive.bootstrap.angular.js"></script>
    <script>
        var app = angular.module('app', ['formulate']);
    </script>
</head>
<body ng-app="app">
    @Html.Partial("~/Views/Partials/Formulate/RenderForm.cshtml", vm)
</body>
</html>
```