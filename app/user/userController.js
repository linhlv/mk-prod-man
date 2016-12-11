(function () {
    'use strict';

    var controllerId = 'userController';

    window.app.controller(controllerId, controller);

    controller.$inject = ['$uibModal', '$scope', '$timeout', 'userSvc'];

    function controller($uibModal, $scope, $timeout, userSvc) {
        var vm = this;
        vm.isLoading = true;
        vm.filter = {
            keyword: '',
            page: 1,
            itemsPerPage: 30,
            totalItems: 0
        };
        
        function typeSearch() {
            $timeout(function () {
                search();
            }, 200);
        }


        function search() {
            vm.isLoading = true;

            userSvc.search(vm.filter).then(function (data) {
                vm.users = data.data;
                vm.filter.totalItems = data.totalCount;
                vm.isLoading = false;
            }, function (error) {
                vm.isLoading = false;
            });
        }

        function pageChanged() {
            search();
        }

        function add() {
            $uibModal.open({
                template: '<user-add users="users" />',
                scope: angular.extend($scope.$new(true), { users: vm.users })
            });
        }

        vm.add = add;
        vm.search = search;
        vm.pageChanged = pageChanged;
        vm.typeSearch = typeSearch;

        search();
    }
})();
