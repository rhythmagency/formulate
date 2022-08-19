(function () {
    var controller = function ($location) {
        var vm = this;

        vm.treeType = "dataValues";

        vm.create = function (option, parentId) {
            var path = "/formulate/dataValues/edit/" + parentId;

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

    angular.module("umbraco").controller("FormulateBackOffice.DataValues.CreateController", controller);
})();

