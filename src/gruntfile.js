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
                        // Binaries.
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
        }
    });

    // Load NPM tasks.
    grunt.loadNpmTasks("grunt-contrib-copy");
    grunt.loadNpmTasks("grunt-html-convert");
    grunt.loadNpmTasks("grunt-browserify");

    // Register Grunt tasks.
    grunt.registerTask("default", ["htmlConvert", "browserify:default", "copy"]);

};