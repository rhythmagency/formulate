module.exports = function(grunt) {

    // Config variables.
    var projectName = "formulate";
    var appProject = projectName + ".app";

    // Extracts Formulate's version from the constants file.
    function getVersion() {
        var contents = grunt.file.read("formulate.meta/Constants.cs");
        var versionRegex = new RegExp("Version = \"([0-9.]+)\";", "gim");
        return versionRegex.exec(contents)[1];
    }

    // Initialize Grunt tasks.
    grunt.initConfig({
        "pkg": grunt.file.readJSON('package.json'),
        paths: {
            in: {
                js: appProject + "/JavaScript/formulate.js"
            },
            out: {
                js: appProject + "/App_Plugins/formulate/formulate.js"
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
                        src: [
                            appProject + ".dll",
                            appProject + ".pdb"
                        ],
                        dest: 'Website/bin/',
                        cwd: appProject + "/bin/Debug/"
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
                        src: [
                            appProject + ".dll",
                            appProject + ".pdb"
                        ],
                        dest: './FormulateTemp/package/bin/',
                        cwd: appProject + "/bin/Debug/"
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
            main: {
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
        msbuild: {
            main: {
                src: ["Formulate.sln"],
                options: {
                    projectConfiguration: "Debug",
                    targets: ["Rebuild"],
                    stdout: true
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
    grunt.loadNpmTasks("grunt-msbuild");

    // Register Grunt tasks.
    grunt.registerTask("default",
        // The "default" task is for general development of Formulate.
        ["clean:main", "htmlConvert", "browserify:default",
        "copy:main", "clean:main"]);
    grunt.registerTask("package",
        // The "package" task is used to create an installer package
        // for Formulate.
        ["clean:main", "htmlConvert", "browserify:default",
        "copy:package", "umbracoPackage:main", "clean:main"]);
    grunt.registerTask("package-full",
        // The "package-full" task is used to build the Visual Studio
        // solution and then create the installer package for Formulate.
        ["clean:main", "htmlConvert", "browserify:default",
        "copy:package", "umbracoPackage:main", "clean:main"]);

};