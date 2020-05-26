---
layout: page
title: Formulate Pro Designed Emails
---

# Formulate Pro Designed Emails

[Formulate Pro](/pro/) comes with the ability to send designed emails using Razor syntax in a CSHTML view file.

That means that you can add the "Designed Email" handler to your form and configure it to send an email that represents the full professionalism of your business.

When somebody submits a contact form on your website, they will receive a well-thought-out email rather than the built-in capability of Formulate to send an email that is just a list of field labels and values.

# How to Implement a Designed Email

First, add a handler and choose "Designed Email":

![Select Designed Email](/images/designed-email/designed-email-selection.png)

Then, fill in the values, including the paths to the CSHTML files:

![Configuration Screen in Back Office for Designed Email](/images/designed-email/configure-designed-email.png)

Only the "HTML Email Razor Path" field is required. This is the file that renders the HTML email.

You may also specify a razor view that renders a dynamic subject line, and another to render a plain text version of the email.

The paths shown are for the built-in CSHTML files that Formulate Pro installs.

# The Razor View Files

The Razor views inherit from `TemplateBase<EmailData>`, which means they can call the `@Raw()` function to render markup that is already HTML encoded, and they have access to all the properties on the `EmailData` model class.

You will usually want to access `Model.Values` (e.g., if you have a "First Name" field, you can access `Model.Values.FirstName`). The property names are case-insensitive (i.e., `FirstName` and `firstName` are equivalent), and they are based on the field alias (or the field name, if a field alias is unspecified).

Here is a list of each property on the `EmailData` class that you have access to from the CSHTML files:

* **MailMessage** The email object, in case you need to manipulate it manually.
* **SenderEmail** The email address of the sender.
* **RecipientEmails** The collection of email recipients.
* **Subject** The subject line, as configured in the back office.
* **Message** The message text, as configured in the back office.
* **Values** The dynamic object that gives you access to the field values.
* **CollectionValues** Similar to `Values`, except each property value is a collection. Useful for fields that have multiple values (e.g., a checkbox list).
* **Helper** Helper functions, such as `Helper.AttachFile` (allows you to attach a file to the email).

Refer to the sample files included with your Formulate Pro installation. You can also view them online here:

* [Formulate Pro Sample (HTML).cshtml](https://github.com/Formulate-Pro/Formulate-Pro/blob/master/src/Formulate.Pro/Views/Formulate/Email/Formulate%20Pro%20Sample%20(HTML).cshtml)
* [Formulate Pro Sample (Text).cshtml](https://github.com/Formulate-Pro/Formulate-Pro/blob/master/src/Formulate.Pro/Views/Formulate/Email/Formulate%20Pro%20Sample%20(Text).cshtml)
* [Formulate Pro Sample (Subject).cshtml](https://github.com/Formulate-Pro/Formulate-Pro/blob/master/src/Formulate.Pro/Views/Formulate/Email/Formulate%20Pro%20Sample%20(Subject).cshtml)

Here is an example of what one of them might look like:

![CSHTML for Designed Email](/images/designed-email/designed-email-cshtml.png)