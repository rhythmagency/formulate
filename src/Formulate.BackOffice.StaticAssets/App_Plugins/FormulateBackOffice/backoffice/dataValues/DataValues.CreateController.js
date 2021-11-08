(function () {
    var controller = function ($location) {
        var vm = this;

        vm.treeType = "datavalues";

        vm.create = function (option, parentId) {
            var path = "/formulate/dataValues/edit/" + parentId;

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

    angular.module("umbraco").controller("FormulateBackOffice.DataValues.CreateController", controller);
})();

