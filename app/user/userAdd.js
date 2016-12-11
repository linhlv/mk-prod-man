(function () {
    'use strict';

    window.app.directive('userAdd', buildDirective);

    function buildDirective() {
        return {
            scope: {
                users: "="
            },
            templateUrl: '/user/templates/userAdd.tmpl.cshtml',
            controller: controller,
            controllerAs: 'vm'
        };
    }

    controller.$inject = ['$scope', '$http', 'userSvc'];

    function controller($scope, $http, userSvc) {
        var vm = this;

        vm.saving = false;
        vm.user = {

        };

        vm.users = $scope.users;

        vm.add = add;

        function add() {
            vm.saving = true;

            userSvc.createUser(vm.user).then(function (data) {
                vm.users.unshift(data);
                alerts.success("User is created!");
                vm.saving = false;
                $scope.$parent.$close();
            }, function (error) {
                vm.errorMessage = error.errorMessage;
                vm.saving = false;
            });
        }
    }
})();