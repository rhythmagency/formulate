---
layout: page
title: Including Formulate JavaScript for the RBA Template
---

# Include JavaScript
If you've followed the instructions to [render a form](/render-form), you will have a line that includes the JavaScript for the Responsive Bootstrap Angular template:

```html
<script src="/App_Plugins/formulate/responsive.bootstrap.angular.js"></script>
```

However, depending on your needs, there are alternatives, which you can read about below.

# Alternative #1: Minified JavaScript

Also included with Formulate is a minified version of the RBA template. You can include it like so:

```html
<script src="/App_Plugins/formulate/responsive.bootstrap.angular.min.js"></script>
```

Because this JavaScript is minified, you will not be able to read it as clearly as you would the unminified version.

# Alternative #2: Modular JavaScript

If you are using a task runner like Grunt, you can use Browserify to include Formulate's modular JavaScript into your compiled JavaScript.
Assuming you are using Grunt, you can start by using [grunt-browserify](https://www.npmjs.com/package/grunt-browserify).
You may also want to use [browserify-shim](https://www.npmjs.com/package/browserify-shim) in order to skip Angular and Lodash during the browserification process.

The browserification process should point to the file, `~/App_Plugins/formulate/templates/responsive.bootstrap.angular/index.js`.
This file references the remainder of the JavaScript files necessary for the RBA template to work.

Once the modular JavaScript has been combined into a single file with Browserify, you can further use
[grunt-ng-annotate](https://www.npmjs.com/package/grunt-ng-annotate) and [grunt-contrib-uglify](https://www.npmjs.com/package/grunt-contrib-uglify)
to minify it.