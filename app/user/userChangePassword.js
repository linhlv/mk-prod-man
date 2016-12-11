(function () {
    'use strict';

    var controllerId = 'changePasswordController';

    window.app.controller(controllerId, controller);

    controller.$inject = ['$scope', '$timeout', 'alerts', 'userSvc'];

    function controller($scope, $timeout, alerts, userSvc) {
        var vm = this;

        vm.saving = false;

        vm.changePassword = {
            currentPassword: null,
            password: null,
            confirmedPassword: null
        };

        vm.save = save;

        function save() {
            vm.saving = true;
            userSvc.changePassword(vm.changePassword).then(function (data) {
                if (data) {
                    alerts.success("Password has been changed!");
                    vm.saving = false;
                }
            }, function (error) {
                alerts.success(error.errorMessage);
                vm.saving = false;
            });
        }
    }
})();
