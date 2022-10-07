(function () {
    // Variables.
    var app = angular.module("umbraco");

    app.factory("formulateDesignerResource", function ($location, formulateIds, navigationService, notificationsService) {

        // Variables.
        var services = {
            $location,
            formulateIds,
            navigationService,
            notificationsService
        };

        // Return service.
        return {
            handleSuccessfulSave: handleSuccessfulSave(services),
        };

    });

    function handleSuccessfulSave(services) {
        const { $location, formulateIds, navigationService, notificationsService } = services;

        return function ({entity, treeAlias, newEntityText, existingEntityText }) {

            if (entity.isNew) {
                const sanitizedEntityId = formulateIds.sanitize(entity.id);
                const path = `/formulate/${treeAlias}/edit/${sanitizedEntityId}`;

                notificationsService.success(newEntityText);
                $location.path(path).search({});
            }
            else {
                notificationsService.success(existingEntityText);
                const options = {
                    tree: treeAlias,
                    path: entity.treePath,
                    forceReload: true,
                    activate: false
                };

                navigationService.syncTree(options);
            }
        };
    }

})();
