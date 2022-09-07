(function () {
    var controller = function ($location) {
        var vm = this;

        vm.treeType = "Forms";

        vm.create = function (option, parentId) {
            const path = "/formulate/forms/edit/" + parentId;

            if (option.kindId) {
                $location
                    .path(path)
                    .search("entityType", option.entityType)
                    .search("kindId", option.kindId)
                    .search("create", "true");
            }
            else {
                $location
                    .path(path)
                    .search("entityType", option.entityType)
                    .search("create", "true");
            }


        };
    };

    angular.module("umbraco").controller("FormulateBackOffice.Forms.CreateController", controller);
})();

