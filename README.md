# Progress / Status
In development. This isn't ready for use yet.

# Formulate Overview
A form builder for Umbraco.

# Contributing
Requires:
* Visual Studio 2013
* Node.js
* npm
* grunt-cli (installed globally)

# Building
These are the steps you can take to build and test Formulate:
* Build the solution.
* Run `npm install` (this only needs to be done once).
* Run `grunt`.
* Run the sample website.
* Run `grunt package` to create an Umbraco installer package (in the "dist" folder).

There are a few nuances to building you may want to consider:
* Most grunt tasks will use whichever build configuration is most recent, but will otherwise default to "Release".
* The `grunt package` task always defaults to "Release".
* You can specify a particular build configuration like this: `grunt package --buildConfiguration=Debug`.