﻿(function () {
    function formulateMoveDialogDirective() {
        var controller = function ($scope, formulateEntityResource, treeService, navigationService, notificationsService, appState, eventsService) {

            $scope.dialogTreeApi = {};
            $scope.source = _.clone($scope.currentNode);

            function nodeSelectHandler(args) {
                args.event.preventDefault();
                args.event.stopPropagation();

                if ($scope.target) {
                    //un-select if there's a current one selected
                    $scope.target.selected = false;
                }

                $scope.target = args.node;
                $scope.target.selected = true;
            }

            $scope.move = function () {
                var options = { parentId: $scope.target.id, entityId: $scope.source.id, treeType: $scope.treeType };

                $scope.busy = true;
                $scope.error = false;

                formulateEntityResource.move(options)
                    .then(function (path) {
                        $scope.error = false;
                        $scope.success = true;
                        $scope.busy = false;

                        //first we need to remove the node that launched the dialog
                        treeService.removeNode($scope.currentNode);

                        //get the currently edited node (if any)
                        var activeNode = appState.getTreeState("selectedNode");

                        //we need to do a double sync here: first sync to the moved content - but don't activate the node,
                        //then sync to the currently edited content (note: this might not be the content that was moved!!)

                        navigationService.syncTree({ tree: $scope.treeType, path: path, forceReload: true, activate: false }).then(function (args) {
                            if (activeNode) {
                                var activeNodePath = treeService.getPath(activeNode).join();

                                //sync to this node now - depending on what was copied this might already be synced but might not be
                                navigationService.syncTree({ tree: $scope.treeType, path: activeNodePath, forceReload: false, activate: true });
                            }
                        });
                        
                        eventsService.emit("app.refreshEditor");

                    }, function (err) {
                        $scope.success = false;
                        $scope.error = err;
                        $scope.busy = false;
                    });
            };

            $scope.onTreeInit = function () {
                $scope.dialogTreeApi.callbacks.treeNodeSelect(nodeSelectHandler);
            };

            $scope.close = function () {
                navigationService.hideDialog();
            };


        };

        var directive = {
            replace: true,
            templateUrl: "/app_plugins/formulatebackoffice/directives/dialogs/move.dialog.html",
            scope: {
                currentNode: "=",
                treeType: "="
            },
            controller: controller
        };

        return directive;
    }

    angular.module("umbraco.directives").directive("formulateMoveDialog", formulateMoveDialogDirective);
})();