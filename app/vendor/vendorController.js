(function () {
    'use strict';

    var controllerId = 'vendorController';

    window.app.controller(controllerId, controller);

    controller.$inject = ['$uibModal', '$scope', '$timeout', 'vendorSvc'];

    function controller($uibModal, $scope, $timeout, vendorSvc) {
        var vm = this;
        vm.isLoading = true;
        vm.filter = {
            keyword: '',
            page: 1,
            itemsPerPage: 30,
            totalItems: 0
        };

        search();
        
        function typeSearch() {
            $timeout(function () {
                search();
            }, 200);
        }


        function search() {
            vm.isLoading = true;

            vendorSvc.search(vm.filter).then(function (data) {
                vm.vendors = data.data;
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
                template: '<add-vendor vendors="vendors" />',
                scope: angular.extend($scope.$new(true), { vendors: vm.vendors })
            });
        }

        vm.add = add;
        vm.search = search;
        vm.pageChanged = pageChanged;
        vm.typeSearch = typeSearch;

        search();
    }
})();
