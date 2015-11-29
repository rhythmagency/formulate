module.exports = function(grunt) {

    // Config variables.
    var projectName = "formulate";
    var appProject = projectName + ".app";

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
                    "./formulate.app/App_Plugins/formulate/formulate.js"
                ]
            }
        }
    });

    // Load NPM tasks.
    grunt.loadNpmTasks("grunt-contrib-copy");
    grunt.loadNpmTasks("grunt-html-convert");
    grunt.loadNpmTasks("grunt-browserify");
    grunt.loadNpmTasks("grunt-contrib-clean");

    // Register Grunt tasks.
    grunt.registerTask("default", ["htmlConvert", "browserify:default",
        "copy:main", "clean:main"]);

};