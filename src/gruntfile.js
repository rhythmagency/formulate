module.exports = function(grunt) {

    // Config variables.
    var projectName = "formulate";
    var appProject = projectName + ".app";

    // Initialize Grunt tasks.
    grunt.initConfig({
        "pkg": grunt.file.readJSON('package.json'),
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
        }
    });

    // Load NPM tasks.
    grunt.loadNpmTasks("grunt-contrib-copy");

    // Register Grunt tasks.
    grunt.registerTask("default", ["copy"]);

};