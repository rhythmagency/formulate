// Service.
var app = angular.module("umbraco");
app.factory("formulatePermissions", function() {
    return {
        permitFormulateAccess: function(directive) {
            //TODO: ...
            alert("Permitting...");
        }
    };
});