# Progress / Status
Alpha. Formulate is fully functional and early adopters can begin using it now.

![Formulate](assets/images/formulate-icon-zoomed-out.png?raw=true "Formulate")

# Formulate Overview
A form builder for Umbraco. More info here: [www.formulate.rocks](http://www.formulate.rocks/)

![Formulate](assets/images/formulate.png?raw=true "Formulate")

# Contributors
These are some of the people who have made Formulate possible:
* Nicholas Westby - Rhythm Agency - [nicholaswestby.com](http://www.nicholaswestby.com/)
* Josef Kohout - Rhythm Agency - [rhythmagency.com](http://rhythmagency.com/leadership?idoeverything=Josef.Kohout)

# Contributing
Requires:
* Visual Studio 2015
* Node.js
* npm
* grunt-cli (installed globally)

# Building
To build the source code, you can use the simple building technique or the advanced building technique. Both versions are described below.

## Simple Building Technique
Double click the file "build/build.bat". The Umbraco package will be created in the "dist" folder. You can then install this Umbraco package into your website.

If you would like to use the built-in sample website, refer to the advanced building technique below.

## Advanced Building Technique
These are the steps you can take to build and test Formulate:
* Build the solution.
* Run `npm install` (this only needs to be done once).
* Run `grunt`.
  * Pro-tip: Running `grunt frontend` is faster
* Run the sample website.
* Run `grunt package` to create an Umbraco installer package (in the "dist" folder).

There are a few nuances to building you may want to consider:
* Most grunt tasks will use whichever build configuration is most recent, but will otherwise default to "Release".
* The `grunt package-full` task always defaults to "Release".
* You can specify a particular build configuration like this: `grunt package-full --buildConfiguration=Debug`.

# Assemblies / Projects
Each project is built into an assembly, and each assembly has a different purpose. Here is a description of each project's purpose:
* **formulate.api**: This contains the easy to use functionality that a developer rendering Formulate forms will need.
* **formulate.app**: This is the main core of Formulate. It contains all of the functionality necessary for the back office to work.
* **formulate.core**: This contains some basic functionality shared by all of the assemblies.
* **formulate.meta**: This contains information about Formulate (e.g., version number). Used primarily during the build process.
* **Website**: This is a sample website for developers who wish to contribute to the Formulate codebase. The binary is not part of the packaged releases, though a few of the files it contains are (e.g., some CSHTML, config, and XDT files).

# Thanks
Thanks to the creators of the following Umbraco packages, which served as excellent points of reference when building Formulate:
* [Archetype](https://github.com/imulus/Archetype)
* [Umbraco Bookshelf](https://github.com/kgiszewski/UmbracoBookshelf)
* [Merchello](https://github.com/Merchello/Merchello)