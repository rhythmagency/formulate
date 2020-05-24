---
layout: page
title: Installing Formulate
---

# Installing Locally

There are a number of ways to install Formulate. This will cover the local installation method:

* Download the ZIP file from the latest release here: [Umbraco.com](https://our.umbraco.org/projects/backoffice-extensions/formulate/)
* Do NOT unzip this zip file. Instead, you'll install it into Umbraco:
  * Log into Umbraco.
  * Navigate to the developer section.
  * Expand the "Packages" node.
  * Click the "Install local package" node.
  * Select the ZIP file you downloaded and follow the instructions to complete the installation.

# Installing with NuGet

If you have setup your Umbraco website with NuGet in Visual Studio, you can also install
Formulate using NuGet. There are two packages available:

* [Formulate](https://www.nuget.org/packages/Formulate/)
* [Formulate Binaries](https://www.nuget.org/packages/Formulate.Binaries/)

The main Formulate NuGet package is what you'd typically install into your website.

The binaries package is what you'd install into a class library if you need to reference
the Formulate DLL's (e.g., if you are creating a custom field).

Note that once you install with NuGet, you'll have to run the website and wait a minute or two.
The first time the website is run with Formulate installed, Formulate will modify some files
(e.g., it will change the web.config to point to the Formulate configuration files).