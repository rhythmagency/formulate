---
layout: page
title: Formulate Extensibility
---

# Extensibility
Most everything in Formulate can be extended or replaced. Here are some examples:
* Fields. You can create your own types of fields. If you want to create a field that allows users to choose their favorite cat pictures, you absolutely can do so. No judgement.
* Layout Designers. If you would like to create your own layout designer that supports grouping fields into wizard steps, this muggle isn't going to stand in your way. More power to you.
* Form Templates. By default, Formulate comes with a form template that allows for responsive, Bootstrap, AngularJS forms. You can easily create your own jQuery version, if that's your thing. Or you could copy the existing AngularJS version and modify it slightly.
* Form Persistence. Formulate stores all information about forms (fields, layouts, data values, validations, and so on) in JSON format on the file system, but it doesn't have to. The entire persistence layer can be swapped out. You could, for example, create a persistence layer that stores to SQL Server. Or XML, if you prefer inequality over curly braces. Heck, you could even store everything in a file format composed entirely of emoticons :-)