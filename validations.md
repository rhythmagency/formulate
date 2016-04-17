---
layout: page
title: Formulate Validations
---

# Validations
Validations allow you to ensure users enter correct input values. When you create a validation in Formulate, you are able to indicate a message to be displayed when the validation fails (i.e., when the user types an incorrect value into a field). You can include special placeholders in this message and they will be dynamically replaced. These placeholders are:

* **{name}** - The name of the field.
* **{alias}** - The field alias.
* **{label}** - The field label.
* **{% raw %}{{}{% endraw %}** - This is an escape sequence in case you want to include a curly brace in your message.

As an example, suppose you had a validation with a message of "The field, {name}, is mandatory":

![Formulate Validation Message](images/validations/message.png?raw=true "Formulate Validation Message")

If you applied it to the field with a name of "First Name", the resulting message that would be displayed to the user would be "The field, First Name, is mandatory":

![Formulate Validation Error](images/validations/validation.png?raw=true "Formulate Validation Error")