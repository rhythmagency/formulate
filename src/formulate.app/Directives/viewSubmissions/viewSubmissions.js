// Variables.
var app = angular.module("umbraco");
var itemsPerPage = 10;

// Directive.
app.directive("formulateViewSubmissions", directive);
function directive(formulateDirectives) {
    return {
        restrict: "E",
        replace: true,
        template: formulateDirectives.get(
            "viewSubmissions/viewSubmissions.html"),
        controller: controller
    };
}

// Controller.
function controller(formulateSubmissions, editorService, $scope, formulateForms, formulateVars) {

    // Variables.
    var injected = {
        $scope: $scope,
        editorService: editorService,
        formulateForms: formulateForms,
        formulateSubmissions: formulateSubmissions,
        formulateVars: formulateVars
    };

    // Scope functions.
    $scope.pickForm = getPickForm(injected);
    $scope.getPage = getGetPage(injected);
    $scope.deleteSubmission = getDeleteSubmission(injected);
    $scope.getPagerItems = getGetPagerItems(injected);
    $scope.getRowClass = getRowClass;
    $scope.getNodeUrl = getNodeUrl;
    $scope.getPagerItemClass = getPagerItemClass;
    $scope.getExportUrl = getGetExportUrl(injected);

    // Initialize scope variables.
    $scope.totalSubmissions = 0;
    $scope.submissions = [];
    $scope.currentPage = 0;
    $scope.pagerItems = [];
    $scope.info = {
        header: getFormHeader()
    };

}

// Returns the header to show for the given form name.
function getFormHeader(formName) {
    if (formName) {
        return "Form Submisions (" + formName + ")";
    } else {
        return "Form Submissions";
    }
}

// Returns the URL that downloads an export to a CSV.
function getGetExportUrl(injected) {
    var formulateVars = injected.formulateVars;
    return function (formId) {
        var downloadUrlBase = formulateVars.DownloadCsvExport + "?FormId=";
        var url = downloadUrlBase + encodeURIComponent(formId);
        return url;
    };
}

// Returns the function that allows the user to pick a form.
function getPickForm(injected) {
    var editorService = injected.editorService;
    var $scope = injected.$scope;
    return function () {
        var forms = $scope.formId ? [$scope.formId] : [];

        editorService.open({
            forms: forms,
            view: "../App_Plugins/formulate/dialogs/pickForm.html",
            close: function () {
                editorService.close();
            },
            submit: function (data) {

                // If no form was chosen, unset values.
                if (!data.length) {
                    $scope.formId = null;
                    $scope.info.header = getFormHeader();
                    $scope.totalSubmissions = 0;
                    $scope.submissions = [];
                    $scope.currentPage = 0;

                    editorService.close();
                    return;
                }

                // Update values.
                var formId = data[0];
                $scope.formId = formId;
                $scope.currentPage = 0;
                refreshForm(formId, injected);
                updateSubmissions(injected);

                editorService.close();
            }
        });
    };
}

// Gets info about the form based on its ID, then updates the info on the scope.
function refreshForm(formId, injected) {
    var $scope = injected.$scope;
    $scope.info.header = getFormHeader();
    var formulateForms = injected.formulateForms;
    formulateForms.getFormInfo(formId)
        .then(function (data) {
            $scope.info.header = getFormHeader(data.name);
        });
}

// Updates the form submissions table.
function updateSubmissions(injected) {
    var $scope = injected.$scope;
    var formulateSubmissions = injected.formulateSubmissions;
    var formId = $scope.formId;
    formulateSubmissions.getSubmissions(formId, $scope.currentPage + 1, itemsPerPage)
        .then(function (data) {
            $scope.totalSubmissions = data.total;
            $scope.submissions = data.submissions;
        });
}

// Returns the function that gets the specified page of form submissions.
function getGetPage(injected) {
    var $scope = injected.$scope;
    return function (pageNumber) {
        $scope.currentPage = pageNumber - 1;
        updateSubmissions(injected);
    };
}

// Returns the function that deletes the form submissions.
function getDeleteSubmission(injected) {
    var formulateSubmissions = injected.formulateSubmissions;
    return function (submission) {
        var shouldDelete = confirm("Are you sure you want to delete this entry? This cannot be undone.");
        if (shouldDelete) {
            formulateSubmissions.deleteSubmission(submission.generatedId)
                .then(function (data) {
                    submission.deleted = true;
                });
        }
    };
}

// Returns a class to be used on a form submission row.
function getRowClass(index, submission) {
    var isEven = (index % 2) === 0;
    var evenClass = isEven
        ? "formulate-submission-row-even"
        : "formulate-submission-row-odd";
    var deletedClass = submission.deleted
        ? "formulate-submission__row--deleted"
        : "formulate-submission__row";
    return evenClass + " " + deletedClass;
}

// Returns a class to be used on a pager item.
function getPagerItemClass(active) {
    return active
        ? "formulate-pager-item-active"
        : "formulate-pager-item-inactive";
}

// Gets the URL to edit a node with the specified ID.
function getNodeUrl(nodeId) {
    return "/umbraco/#/content/content/edit/" + nodeId.toString();
}

// Gets the function that returns items to be used when generating the pager.
function getGetPagerItems(injected) {
    var $scope = injected.$scope;
    return function () {
        adjustPagerItems(injected);
        return $scope.pagerItems;
    }
}

// Adjusts the pager items to contain the correct values.
function adjustPagerItems(injected) {

    // Variables.
    var $scope = injected.$scope;

    // If there are no submissions, set the pager items to an empty array.
    if ($scope.totalSubmissions === 0) {
        $scope.pagerItems = [];
        return;
    }

    // Variables.
    var pageCount = Math.ceil($scope.totalSubmissions / itemsPerPage);
    var minPage = Math.max(0, $scope.currentPage - 2);
    var maxPage = Math.min(minPage + 4, pageCount - 1);
    minPage = Math.max(0, maxPage - 4);
    var pages = $scope.pagerItems;
    var pagerItemCount = maxPage - minPage + 1;

    // Remove extra items from the pager.
    while (pages.length > pagerItemCount) {
        pages.pop();
    }

    // Add enough items to the pager.
    while (pages.length < pagerItemCount) {
        pages.push({
            pageNumber: 0,
            active: false
        });
    }

    // Adjust pager items to have correct values.
    for (var i = minPage; i <= maxPage; i++) {
        var indexIntoPager = i - minPage;
        pages[indexIntoPager].pageNumber = i + 1;
        pages[indexIntoPager].active = i === $scope.currentPage;
    }

}