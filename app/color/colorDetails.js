(function () {
    'use strict';

    window.app.directive('colorDetails', buildDirective);

    function buildDirective() {
        return {
            scope: {
                color: "="
            },
            templateUrl: '/color/templates/colorDetails.tmpl.cshtml',
            controller: controller,
            controllerAs: 'vm'
        }
    }

    controller.$inject = ['$scope', '$http', '$uibModal', 'colorSvc'];

    function controller($scope, $http, $uibModal, colorSvc) {
        var vm = this;

        vm.saving = false;

        vm.color = $scope.color;
        
        vm.edit = edit;
       
        vm.deleteItem = deleteItem;

        function edit() {
            $uibModal.open({
                template: '<color-edit color="color" />',
                scope: angular.extend($scope.$new(true), { color: vm.color })
            });
        }

        function deleteItem() {
            var modal = $uibModal.open({
                template: '<confirm-message title="title" message="message" yes="yes()" />',
                scope: angular.extend($scope.$new(true), {
                    title: 'Delete ' + vm.color.name + ' ?',
                    message: 'Are you sure you want to delete?',
                    yes: function () {
                        colorSvc.itemDelete(vm.color).then(function (data) {
                            $scope.$parent.$parent.vm.colors = _.without(
                                $scope.$parent.$parent.vm.colors,
                                _.findWhere($scope.$parent.$parent.vm.colors, { id: data.id })
                            );

                            alerts.success("Color has been updated!");

                            modal.close();
                        });
                    }
                })
            });
        }
    }
})();