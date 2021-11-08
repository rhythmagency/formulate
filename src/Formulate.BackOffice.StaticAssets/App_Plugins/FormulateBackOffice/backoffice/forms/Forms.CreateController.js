(function () {
    var controller = function ($location) {
        var vm = this;

        vm.treeType = "forms";

        vm.create = function (option, parentId) {
            $location
                .path("/formulate/forms/edit/" + parentId)
                .search("entityType", option.entityType)
                .search("create", "true");
        };
    };

    angular.module("umbraco").controller("FormulateBackOffice.Forms.CreateController", controller);
})();

