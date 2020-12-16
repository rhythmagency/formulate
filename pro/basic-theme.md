---
layout: page
title: Formulate Pro Basic Theme
---

# Formulate Pro Basic Theme

[Formulate Pro](/pro/) comes with a basic theme, saving you the work of having to style your forms.

Additionally, the theme is written with the SCSS version of Sass, and it offers a number of variables that allow you to easily customize the look of the forms on your website.

By default, Formulate forms look like this:

![Plain Formulate Form with No Styles](/images/basic-theme/no-styles.png)

With the basic theme, Formulate forms look like this:

![Formulate Form with Basic Theme Styles](/images/basic-theme/basic-styles.png)

# How to Incorporate the Basic Theme

Once you've installed the Formulate Pro NuGet package, it will place a CSS file and a number of Scss (Sass) files in the following folder:

`/App_Plugins/Formulate.Pro/Themes/Basic/`

To ues the compiled CSS file, add the following to your HTML (e.g., in your `<head>` section):

`<link rel="stylesheet" href="/App_Plugins/Formulate.Pro/Themes/Basic/basic.css" />`

That's all you need to do to have great-looking forms!

# Customizing the Basic Theme

If you are familiar with Sass, you can also copy the Sass files from `/App_Plugins/Formulate.Pro/Themes/Basic/` to another folder and incorporate them into the rest of your Sass project.

This may change over time, but here is a snapshot of what the Sass files that are part of the basic theme:

![Formulate Pro Basic Theme Sass Files](/images/basic-theme/basic-theme-sass-files.png)

The `app.scss` file is what imports the rest of the file files, so you'll want to import that one into your main Sass file.

The `settings` folder contains all the variables you might want to modify. For example, here is what the `colors.scss` file looks like:

```scss
// Named colors.
$formulate-color--black: #000;
$formulate-color--black-transparent: #0000;
$formulate-color--white: #fff;
$formulate-color--red: #f00;
$formulate-color--red-transparent: #f000;
$formulate-color--grey: #888;
$formulate-color--grey-light: #eee;
$formulate-color--blue-super-light: #f6f6ff;

// Theme colors.
$formulate-color--border: $formulate-color--black;
$formulate-color--border-transparent: $formulate-color--black-transparent;
$formulate-color--field: $formulate-color--white;
$formulate-color--error: $formulate-color--red;
$formulate-color--error-transparent: $formulate-color--red-transparent;
$formulate-color--shadow: $formulate-color--grey;
$formulate-color--field-focused: $formulate-color--grey-light;
```

If you were to change the value of `$formulate-color--border` (e.g., to `#48a`), you would change the color of all the borders on the form, as shown in this example:

![Formulate Form with Altered Border Color](/images/basic-theme/basic-styles-different-border.png)

Thanks to a well-structured set of variables, you can easily change any form with just a few slight changes.