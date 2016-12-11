(function () {
    'use strict';

    var controllerId = 'categoryController';

    window.app.controller(controllerId, controller);

    controller.$inject = ['$uibModal', '$scope', '$timeout', 'categorySvc'];

    function controller($uibModal, $scope, $timeout, categorySvc) {
        var vm = this;
        vm.categories = [];
        vm.add = add;
        vm.isLoading = false;

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

            categorySvc.search(vm.filter).then(function (data) {
                vm.categories = data.data;
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
                template: '<add-category categories="categories" />',
                scope: angular.extend($scope.$new(true), { categories: vm.categories })
            });
        }

        vm.search = search;
        vm.pageChanged = pageChanged;
        vm.typeSearch = typeSearch;
    }
})();
