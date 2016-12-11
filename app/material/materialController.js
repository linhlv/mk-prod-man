(function () {
    'use strict';

    var controllerId = 'materialController';

    window.app.controller(controllerId, materialController);

    materialController.$inject = ['$uibModal', '$scope', '$timeout', 'materialSvc'];

    function materialController($uibModal, $scope, $timeout, materialSvc) {
        var vm = this;
        vm.materials = [];
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
            materialSvc.search(vm.filter).then(function (data) {
                vm.materials = data.data;
                vm.isLoading = false;
            });
        }

        function add() {
            $uibModal.open({
                template: '<add-material materials="materials" />',
                scope: angular.extend($scope.$new(true), { materials: vm.materials })
            });
        }
    }
})();
