(function () {
    'use strict';

    window.app.directive('userSetPassword', buildDirective);

    function buildDirective() {
        return {
            scope: {
                user: "="
            },
            templateUrl: '/user/templates/userSetPassword.tmpl.cshtml',
            controller: controller,
            controllerAs: 'vm'
        };
    }

    controller.$inject = ['$scope', 'userSvc', 'alerts'];

    function controller($scope, userSvc, alerts) {
        var vm = this;

        vm.saving = false;
       
        vm.userPassword = {
            id: $scope.user.id
        };

        vm.save = save;

        function save() {
            vm.saving = true;
            userSvc.setPassword(vm.userPassword).then(function (data) {
                if (data) {
                    alerts.success("Password has been resetted!");
                    vm.saving = false;
                    $scope.$parent.$close();
                }
            });
        }
    }
})();