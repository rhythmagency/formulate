// Variables.
var app = angular.module("umbraco");

// Service to help with server variables.
app.factory("formulateVars", function() {
    return Umbraco.Sys.ServerVariables.formulate;
});