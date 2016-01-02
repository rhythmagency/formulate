// See: http://stackoverflow.com/a/18609594/2052963

// Variables.
var app = angular.module("umbraco");

// Service to help with recursion.
app.factory('formulateRecursion', function($compile) {
    return {
        getCompiler: function(element) {

            // Break the recursion loop by removing the contents.
            var contents = element.contents().remove();
            var compiledContents;
            return {
                pre: null,

                // Compiles and re-adds the contents.
                post: function(scope, element) {

                    // Compile the contents.
                    if(!compiledContents) {
                        compiledContents = $compile(contents);
                    }

                    // Re-add the compiled contents to the element.
                    compiledContents(scope, function(clone) {
                        element.append(clone);
                    });

                }

            };
        }
    };
});