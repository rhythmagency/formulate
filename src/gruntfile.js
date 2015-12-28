module.exports = function(grunt) {

    // Config variables.
    var projectName = "formulate";
    var binaries = [].concat.apply([], [
        "app",
        "core",
        "meta"
    ].map(function(base) {
        return [".dll", ".pdb"].map(function (ext) {
            return projectName + "." + base + ext;
        });
    })).concat([
        "Microsoft.Web.XmlTransform.dll"
    ]);
    var appProject = projectName + ".app";
    var buildConfig = grunt.option("buildConfiguration");

    // Extracts Formulate's version from the constants file.
    function getVersion() {
        var contents = grunt.file.read("formulate.meta/Constants.cs");
        var versionRegex = new RegExp("Version = \"([0-9.]+)\";", "gim");
        return versionRegex.exec(contents)[1];
    }

    // Gets the configuration that should be used when building
    // Visual Studio source code. The result will be stored and
    // returned the next time this function is called.
    function getConfiguration(preference) {

        // If an option or override is specified, use that version.
        if (buildConfig) {
            getConfiguration.configuration = buildConfig;
        } else if (preference) {
            getConfiguration.configuration = preference;
        }

        // If a previous configuration was already used, keep
        // using it.
        if (getConfiguration.configuration) {
            return getConfiguration.configuration;
        }

        // Variables.
        var debugDate = tryGetModifiedDate(
            appProject + "/bin/Debug/" + appProject + ".dll");
        var releaseDate = tryGetModifiedDate(
            appProject + "/bin/Release/" + appProject + ".dll");

        // If the bin folder does not contain either configuration,
        // use the "Release" configuration.
        if (!debugDate && !releaseDate) {
            getConfiguration.configuration = "Release";
            return getConfiguration.configuration;
        }

        // If both configurations exist, use the one with the
        // newer modification date.
        if (debugDate && releaseDate) {
            if (releaseDate > debugDate) {
                getConfiguration.configuration = "Release";
            } else {
                getConfiguration.configuration = "Debug";
            }
            return getConfiguration.configuration;
        }

        // If one exists, prefer it over the other.
        // Otherwise, default to "Release".
        if (debugDate) {
            getConfiguration.configuration = "Debug";
        } else if (releaseDate) {
            getConfiguration.configuration = "Release";
        } else {
            getConfiguration.configuration = "Release";
        }
        return getConfiguration.configuration;

    }

    // Tries to get the last modified date of a file.
    // If the file does not exist, null will be returned.
    function tryGetModifiedDate(path) {
        var fs = require("fs");
        try {
            var info = fs.statSync(path);
            return info.mtime;
        } catch (ex) {
            return null;
        }
    }

    // Initialize Grunt tasks.
    grunt.initConfig({
        "pkg": grunt.file.readJSON('package.json'),
        paths: {
            in: {
                js: appProject + "/JavaScript/formulate.js"
            },
            out: {
                js: appProject + "/App_Plugins/formulate/formulate.js",
                jsdoc: "frontendDocs/"
            }
        },
        copy: {
            // Main is used to copy files to the sample website.
            main: {
                files: [
                    {
                        // Frontend files.
                        expand: true,
                        src: ["App_Plugins/**"],
                        dest: 'Website/',
                        cwd: appProject + "/"
                    }, {
                        // App binaries.
                        expand: true,
                        src: binaries,
                        dest: 'Website/bin/',
                        cwd: appProject + "/bin/" + getConfiguration() + "/"
                    }
                ]
            },
            frontend: {
                files: [
                    {
                        // Frontend files.
                        expand: true,
                        src: ["App_Plugins/**"],
                        dest: 'Website/',
                        cwd: appProject + "/"
                    }
                ]
            },
            // Package is used to copy files to create the Umbraco package.
            package: {
                files: [
                    {
                        // Frontend files.
                        expand: true,
                        src: ["App_Plugins/**"],
                        dest: './FormulateTemp/package/',
                        cwd: appProject + "/"
                    }, {
                        // App binaries.
                        expand: true,
                        src: binaries,
                        dest: './FormulateTemp/package/bin/',
                        cwd: appProject + "/bin/" + getConfiguration() + "/"
                    }, {
                        // Config and transform files.
                        expand: true,
                        src: [
                            "Config/Formulate/*.config",
                            "Transforms/Formulate/*.xdt"
                        ],
                        dest: './FormulateTemp/package/',
                        cwd: "Website/"
                    }
                ]
            }
        },
        htmlConvert: {
            options: {
                base: appProject + "/Directives/",
                target: "js",
                fileHeaderString: "module.exports = function () {return directives;};"
            },
            directives: {
                src: [
                    // Directive markup gets compiled into a temporary
                    // JavaScript file and then included by
                    // getDirective.js. That file then gets included
                    // in the main JavaScript file and Browserify
                    // combines the entire result. The end result is
                    // that directive markup is embedded in the compiled
                    // JavaScript file.
                    appProject + "/Directives/**/*.html"
                ],
                dest: "./FormulateTemp/directives.js"
            }
        },
        browserify: {
            default: {
                options: {
                    transform: ["require-globify"]
                },
                files: {
                    "<%= paths.out.js %>": "<%= paths.in.js %>"
                }
            }
        },
        clean: {
            before: {
                src: [
                    // Generated documentation.
                    "./frontendDocs",
                    // Temporary folder for intermediate build artifacts.
                    "./FormulateTemp",
                    // Temporary file (compiled version of JS).
                    "./formulate.app/App_Plugins/formulate/formulate.js"
                ]
            },
            after: {
                src: [
                    // Temporary folder for intermediate build artifacts.
                    "./FormulateTemp",
                    // Temporary file (compiled version of JS).
                    "./formulate.app/App_Plugins/formulate/formulate.js"
                ]
            }
        },
        umbracoPackage: {
            main: {
                src: "./FormulateTemp/package",
                dest: "../dist",
                options: {
                    name: "Formulate",
                    version: getVersion(),
                    url: "http://www.formulate.rocks/",
                    license: "MIT License",
                    licenseUrl: "http://opensource.org/licenses/MIT",
                    author: "Rhythm Agency",
                    authorUrl: "http://rhythmagency.com/",
                    readme: grunt.file.read("templates/inputs/readme.txt"),
                    manifest: "templates/package.template.xml"
                }
            }
        },
        nuget_install: {
            file: "Formulate.sln"
        },
        msbuild: {
            main: {
                src: ["Formulate.sln"],
                options: {
                    projectConfiguration: getConfiguration("Release"),
                    targets: ["Rebuild"],
                    stdout: true
                }
            }
        },
        ngAnnotate: {
            main: {
                files: {
                    "<%= paths.out.js %>": "<%= paths.out.js %>"
                }
            }
        },
        jsdoc: {
            main: {
                src: ["<%= paths.out.js %>"],
                options: {
                    destination: "<%= paths.out.jsdoc %>"
                }
            }
        }
    });

    // Load NPM tasks.
    grunt.loadNpmTasks("grunt-contrib-copy");
    grunt.loadNpmTasks("grunt-html-convert");
    grunt.loadNpmTasks("grunt-browserify");
    grunt.loadNpmTasks("grunt-contrib-clean");
    grunt.loadNpmTasks("grunt-umbraco-package");
    grunt.loadNpmTasks("grunt-nuget-install");
    grunt.loadNpmTasks("grunt-msbuild");
    grunt.loadNpmTasks("grunt-ng-annotate");
    grunt.loadNpmTasks("grunt-jsdoc");

    // Register Grunt tasks.
    grunt.registerTask("default",
        // The "default" task is for general development of Formulate.
        ["clean:before", "htmlConvert", "browserify:default", "ngAnnotate:main",
        "copy:main", "clean:after"]);
    grunt.registerTask("frontend",
        // The "frontend" task is for frontend development of Formulate. This
        // will skip copying the binaries.
        ["clean:before", "htmlConvert", "browserify:default", "ngAnnotate:main",
        "copy:frontend", "jsdoc:main", "clean:after"]);
    grunt.registerTask("package",
        // The "package" task is used to create an installer package
        // for Formulate.
        ["clean:before", "htmlConvert", "browserify:default", "ngAnnotate:main",
        "copy:package", "umbracoPackage:main", "clean:after"]);
    grunt.registerTask("package-full",
        // The "package-full" task is used to build the Visual Studio
        // solution and then create the installer package for Formulate.
        ["clean:before", "nuget_install", "msbuild:main", "htmlConvert",
        "browserify:default", "ngAnnotate:main", "copy:package",
        "umbracoPackage:main", "clean:after"]);

};