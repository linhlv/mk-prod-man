(function () {
    'use strict';

    var controllerId = 'productController';

    window.app.controller(controllerId, productController);

    productController.$inject = ['$uibModal', '$scope', '$timeout', '$q', 'productSvc', 'categorySvc', 'vendorSvc', 'materialSvc'];

    function productController($uibModal, $scope, $timeout, $q, productSvc, categorySvc, vendorSvc, materialSvc) {
        var vm = this;
        vm.isLoading = true;
        vm.isLoadingMore = false;
        vm.isSearchFormInit = true;

        vm.filter = {
            keyword: '',
            page: 1,
            itemsPerPage: 30,
            totalItems: 0,
            factory: 0,
            material: 0,
            category: 0
        };

        vm.search = search;
        vm.typeSearch = typeSearch;
        vm.pageChanged = pageChanged;
        vm.loadMore = loadMore;
        vm.showLoadMore = showLoadMore;
        vm.add = add;
        vm.openQuotationEdit = openQuotationEdit;
        vm.nextCount = nextCount;

        vm.factories = [];
        vm.materials = [];
        vm.categories = [];

        search();

        init();

        function nextCount() {
            if ((vm.filter.totalItems - vm.filter.itemsPerPage * vm.filter.page) > vm.filter.itemsPerPage) {
                return vm.filter.itemsPerPage + '/' + (vm.filter.totalItems - vm.filter.itemsPerPage * vm.filter.page);
            }

            return (vm.filter.totalItems - vm.filter.itemsPerPage * vm.filter.page) + '/' + (vm.filter.totalItems - vm.filter.itemsPerPage * vm.filter.page);
        }

        function init() {
            vm.isSearchFormInit = true;
            $q.all([categorySvc.all(), vendorSvc.all(), materialSvc.all()]).then(function (data) {
                vm.categories.addRange(data[0]);

                vm.categories.insert(0, {
                    id: 0,
                    name: '-- All Category --'
                });

                vm.factories.addRange(data[1]);

                vm.factories.insert(0, {
                    id: 0,
                    name: '-- All Factory --'
                });

                vm.materials.addRange(data[2]);

                vm.materials.insert(0, {
                    id: 0,
                    name: '-- All Material --'
                });

                vm.filter.category = vm.categories[0].id;
                vm.filter.factory = vm.factories[0].id;
                vm.filter.material = vm.materials[0].id;

                vm.isSearchFormInit = false;
            });
        }

        function showLoadMore() {
            return (vm.filter.page * vm.filter.itemsPerPage) < vm.filter.totalItems;
        }

        function typeSearch() {
            $timeout(function () {
                search();
            }, 200);
        }

        function search() {
            vm.filter.page = 1;
            vm.isLoading = true;
            productSvc.search(vm.filter).then(function (data) {
                vm.products = data.data;
                vm.filter.totalItems = data.totalCount;
                vm.isLoading = false;
            }, function (error) {
                vm.isLoading = false;
            });
        }

        function pageChanged() {
            search();
        }

        function loadMore() {
            vm.filter.page++;
            vm.isLoadingMore = true;
            productSvc.search(vm.filter).then(function (data) {
                vm.products.addRange(data.data);
                vm.isLoadingMore = false;
            }, function (error) {
                vm.isLoadingMore = false;
            });
        }

        function add() {
            $uibModal.open({
                template: '<add-product products="products" />',
                scope: angular.extend($scope.$new(true), { products: vm.products })
            });
        }

        function openQuotationEdit() {
            $uibModal.open({
                template: '<edit-quotation />'
            });
        }
    }
})();

