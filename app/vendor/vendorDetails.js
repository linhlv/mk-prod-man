(function () {
    'use strict';

    window.app.directive('vendorDetails', vendorDetails);
    function vendorDetails() {
        return {
            scope: {
                vendor: '='
            },
            templateUrl: '/vendor/templates/vendorDetails.tmpl.cshtml',
            controller: controller,
            controllerAs: 'vm'
        }
    }

    controller.$inject = ['$scope', '$uibModal', 'vendorSvc'];
    function controller($scope, $uibModal, vendorSvc) {
        var vm = this;

        vm.vendor = $scope.vendor;
        vm.edit = edit;
        vm.vendorDelete = vendorDelete;
        vm.selectedView = 'details';

        function edit() {
            $uibModal.open({
                template: '<edit-vendor vendor="vendor" />',
                scope: angular.extend($scope.$new(true), { vendor: vm.vendor })
            });
        }

        function vendorDelete() {
            var modal = $uibModal.open({
                template: '<confirm-message title="title" message="message" yes="yes()" />',
                scope: angular.extend($scope.$new(true), {
                    title: 'Delete ' + vm.vendor.name + '?',
                    message: 'Are you sure you want to delete?',
                    yes: function () {
                        vendorSvc.vendorDelete(vm.vendor).then(function (data) {
                            $scope.$parent.$parent.vm.vendors = _.without(
                                $scope.$parent.$parent.vm.vendors,
                                _.findWhere($scope.$parent.$parent.vm.vendors, { id: data.id })
                            );

                            alerts.success("Vendor has been updated!");

                            modal.close();
                        });
                    }
                })
            });
        }
    }
})();