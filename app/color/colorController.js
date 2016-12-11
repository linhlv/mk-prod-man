(function () {
    'use strict';

    var controllerId = 'colorController';

    window.app.controller(controllerId, controller);

    controller.$inject = ['$uibModal', '$scope', '$timeout', 'colorSvc'];

    function controller($uibModal, $scope, $timeout, colorSvc) {
        var vm = this;
        vm.colors = [];
        vm.add = add;

        vm.filter = {
            keyword: '',
            page: 1,
            itemsPerPage: 30,
            totalItems: 0
        };

        vm.isLoading = false;

        vm.typeSearch = typeSearch;

        search();

        function typeSearch() {
            $timeout(function () {
                search();
            }, 200);
        }

        function search() {
            vm.isLoading = true;
            colorSvc.search(vm.filter).then(function (data) {
                vm.colors = data.data;
                vm.isLoading = false;
            });
        }

        function add() {
            $uibModal.open({
                template: '<add-color colors="colors" />',
                scope: angular.extend($scope.$new(true), { colors: vm.colors })
            });
        }
    }
})();
