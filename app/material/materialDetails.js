(function () {
    'use strict';

    window.app.directive('materialDetails', buildDirective);

    function buildDirective() {
        return {
            scope: {
                material: "="
            },
            templateUrl: '/material/templates/materialDetails.tmpl.cshtml',
            controller: controller,
            controllerAs: 'vm'
        }
    }

    controller.$inject = ['$scope', '$http', '$uibModal', 'materialSvc'];

    function controller($scope, $http, $uibModal, materialSvc) {
        var vm = this;

        vm.saving = false;

        vm.material = $scope.material;
        
        vm.edit = edit;
       
        vm.deleteItem = deleteItem;

        function edit() {
            $uibModal.open({
                template: '<material-edit material="material" />',
                scope: angular.extend($scope.$new(true), { material: vm.material })
            });
        }

        function deleteItem() {
            var modal = $uibModal.open({
                template: '<confirm-message title="title" message="message" yes="yes()" />',
                scope: angular.extend($scope.$new(true), {
                    title: 'Delete ' + vm.material.name + ' ?',
                    message: 'Are you sure you want to delete?',
                    yes: function () {
                        materialSvc.itemDelete(vm.material).then(function (data) {
                            $scope.$parent.$parent.vm.materials = _.without(
                                $scope.$parent.$parent.vm.materials,
                                _.findWhere($scope.$parent.$parent.vm.materials, { id: data.id })
                            );

                            alerts.success("Material has been updated!");

                            modal.close();
                        });
                    }
                })
            });
        }
    }
})();