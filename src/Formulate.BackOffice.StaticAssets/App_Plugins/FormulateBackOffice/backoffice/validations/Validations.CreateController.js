(function () {
    var controller = function ($location) {
        var vm = this;

        vm.treeType = "validations";

        vm.create = function (option, parentId) {
            var path = "/formulate/validations/edit/" + parentId;

            if (option.definitionId) {
                $location
                    .path(path)
                    .search("entityType", option.entityType)
                    .search("definitionId", option.definitionId)
                    .search("create", "true");
            } else {
                $location
                    .path(path)
                    .search("entityType", option.entityType)
                    .search("definitionId", null)
                    .search("create", "true");
            }

        };
    };

    angular.module("umbraco").controller("FormulateBackOffice.Validations.CreateController", controller);
})();

