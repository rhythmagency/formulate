// Variables.
var app = angular.module("umbraco");

// Service to help with Formulate form submissions.
app.factory("formulateSubmissions", function (formulateVars,
   formulateServer) {

    // Variables.
    var services = {
        formulateVars: formulateVars,
        formulateServer: formulateServer
    };

    // Return service.
    return {

        // Gets the form submissions based on the specified criteria.
        getSubmissions: getGetSubmissions(services),

        // Deletes the form submission with the specified ID.
        deleteSubmission: getDeleteSubmission(services)

    };

});

// Returns the function that gets the form submissions.
function getGetSubmissions(services) {
    return function (formId, page, itemsPerPage) {

        // Variables.
        var url = services.formulateVars.GetSubmissions;
        var downloadUrlBase = services.formulateVars.DownloadFile + "?PathSegment=";
        var params = {
            FormId: formId,
            Page: page,
            ItemsPerPage: itemsPerPage,
            TimezoneOffset: (new Date()).getTimezoneOffset()
        };

        // Get submissions from server.
        return services.formulateServer.get(url, params, function (data) {

            // Return information about submissions.
            return {
                total: data.Total,
                submissions: data.Submissions.map(function (item) {
                    return {
                        generatedId: item.GeneratedId,
                        pageId: item.PageId,
                        url: item.Url,
                        creationDate: item.CreationDate,
                        fields: item.Fields.map(function (field) {
                            return {
                                name: field.Name,
                                value: field.Value
                            };
                        }),
                        files: item.Files.map(function (field) {
                            var path = encodeURIComponent(field.PathSegment);
                            var filename = encodeURIComponent(field.Filename);
                            return {
                                name: field.Name,
                                filename: field.Filename,
                                path: downloadUrlBase + path + "&Filename=" + filename
                            };
                        })
                    };
                })
            };

        });

    };
}

// Returns the function that deletes the specified form submission.
function getDeleteSubmission(services) {
    return function (generatedId) {

        // Variables.
        var url = services.formulateVars.DeleteSubmission;
        var params = {
            GeneratedId: generatedId
        };

        // Delete submission from server.
        return services.formulateServer.post(url, params, function () {

            // Indicate success.
            return {
                success: true,
            };

        });

    };
}