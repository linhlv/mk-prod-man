(function () {
    'use strict';

    window.app.directive('userDetails', buildDirective);

    function buildDirective() {
        return {
            scope: {
                user: "="
            },
            templateUrl: '/user/templates/userDetails.tmpl.cshtml',
            controller: controller,
            controllerAs: 'vm'
        };
    }

    controller.$inject = ['$scope', '$http', '$uibModal', 'userSvc', 'alerts'];

    function controller($scope, $http, $uibModal, userSvc, alerts) {
        var vm = this;

        vm.saving = false;
        vm.user = $scope.user;

        vm.setPassword = setPassword;
        vm.deleteUser = deleteUser;

        function setPassword() {
            $uibModal.open({
                template: '<user-set-password user="user" />',
                scope: angular.extend($scope.$new(true), { user: vm.user })
            });
        }

        function deleteUser() {
            var modal = $uibModal.open({
                template: '<confirm-message title="title" message="message" yes="yes()" />',
                scope: angular.extend($scope.$new(true), {
                    title: 'Delete ' + vm.user.userName + ' ?',
                    message: 'Are you sure you want to delete?',
                    yes: function () {
                        userSvc.deleteUser(vm.user).then(function (data) {
                            if (data) {
                                $scope.$parent.$parent.vm.users = _.without(
                                   $scope.$parent.$parent.vm.users,
                                   _.findWhere($scope.$parent.$parent.vm.users, { id: data.id })
                                );

                                alerts.success("User is deleted!");
                                modal.close();
                            }
                        }, function (error) {
                            alerts.error("There is some error when deleting user!");
                            modal.close();
                        });
                    }
                })
            });
        }
    }
})();