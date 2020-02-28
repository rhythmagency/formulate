---
layout: page
title: Styling the Formulate Plain JavaScript Grid
---

# Plain JavaScript Template

Note that this page only applies to the plain JavaScript template (i.e., it does not apply to the AngularJS template).

# Styling the Grid

This CodePen shows how you can style the grid rendered by Formulate's plain JavaScript template: [https://codepen.io/anon/pen/oPOOYZ?editors=1100](https://codepen.io/anon/pen/oPOOYZ?editors=1100)

Note that it includes a style reset you may want to remove, as you likely already have it in your styles.
Also, it includes some decorative styles near the bottom that are purely for this demo, so you'll want to remove those as well.

This CodePen shows the SCSS flavor of Sass, but you can click the drop down to view the compiled CSS if you aren't using Sass.

# Styling Mult-Step Forms

You can use this CSS style to hide steps that are inactive in multi-step forms:

```css
.formulate__row--inactive {
    display: none;
}
```