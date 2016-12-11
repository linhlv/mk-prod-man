(function () {
    'use strict';

    var controllerId = 'editProfileController';

    window.app.controller(controllerId,
        ['$scope', '$http', 'alerts', 'editProfileConfig', 'model', editProfileController]);

    function editProfileController($scope, $http, alerts, editProfileConfig, model) {
        var vm = this;

        vm.profile = model;
        vm.save = save;

        function save() {
            vm.saving = true;
            vm.errorMessage = null;
            vm.success = false;

            $http.post(editProfileConfig.saveUrl, vm.profile)
				.success(function () {
				    vm.success = true;
				})
				.error(function (msg) {
				    vm.errorMessage = msg;
				})
				.finally(function () {
				    vm.saving = false;
				});
        }
    }
})();