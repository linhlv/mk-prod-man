(function () {
    'use strict';

    window.app.directive('categoryDetails', buildDirective);

    function buildDirective() {
        return {
            scope: {
                category: "="
            },
            templateUrl: '/category/templates/categoryDetails.tmpl.cshtml',
            controller: controller,
            controllerAs: 'vm'
        }
    }

    controller.$inject = ['$scope', '$http', '$uibModal', 'categorySvc', 'alerts'];

    function controller($scope, $http, $uibModal, categorySvc, alerts) {
        var vm = this;

        vm.saving = false;

        vm.category = $scope.category;

        vm.edit = edit;

        vm.itemDelete = itemDelete;

        function edit() {
            $uibModal.open({
                template: '<category-edit category="category" />',
                scope: angular.extend($scope.$new(true), { category: vm.category })
            });
        }

        function itemDelete() {
            var modal = $uibModal.open({
                template: '<confirm-message title="title" message="message" yes="yes()" />',
                scope: angular.extend($scope.$new(true), {
                    title: 'Delete ' + vm.category.name + '?',
                    message: 'Are you sure you want to delete?',
                    yes: function () {
                        categorySvc.itemDelete(vm.category).then(function (data) {
                            $scope.$parent.$parent.vm.categories = _.without(
                                $scope.$parent.$parent.vm.categories,
                                _.findWhere($scope.$parent.$parent.vm.categories, { id: data.id })
                            );

                            alerts.success("Category has been updated!");

                            modal.close();
                        });
                    }
                })
            });
        }
    }
})();