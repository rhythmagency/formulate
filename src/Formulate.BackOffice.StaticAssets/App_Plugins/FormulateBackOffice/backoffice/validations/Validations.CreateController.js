(function () {
    var controller = function ($location) {
        var vm = this;

        vm.treeType = "Validations";

        vm.create = function (option, parentId) {
            var path = "/formulate/validations/edit/" + parentId;

            if (option.kindId) {
                $location
                    .path(path)
                    .search("entityType", option.entityType)
                    .search("kindId", option.kindId)
                    .search("create", "true");
            } else {
                $location
                    .path(path)
                    .search("entityType", option.entityType)
                    .search("kindId", null)
                    .search("create", "true");
            }

        };
    };

    angular.module("umbraco").controller("FormulateBackOffice.Validations.CreateController", controller);
})();

