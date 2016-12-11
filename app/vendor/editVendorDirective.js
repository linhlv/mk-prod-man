(function() {
	'use strict';

	window.app.directive('editVendor', buildDirective);

	function buildDirective() {
		return {
			scope: {
			    vendors: "=",
			    vendor: '='
			},
			templateUrl: '/vendor/templates/editVendor.tmpl.cshtml',
			controller: controller,
			controllerAs: 'vm'
		}
	}

	controller.$inject = ['$scope', '$http', 'alerts'];

	function controller($scope, $http) {
		var vm = this;

		vm.vendor = $scope.vendor;

	    vm.editingVendor = {

	    };

		vm.saving = false;

		angular.copy($scope.vendor, vm.editingVendor);

		vm.save = save;

		function save() {
			vm.saving = true;

		    $http.post('/Vendor/Update', vm.editingVendor)
				.success(function (data) {
				    vm.vendor.name = data.name;
				    vm.vendor.createDate = data.createDate;
				    vm.vendor.description = data.description;
				    vm.vendor.homeAddress = data.homeAddress;
				    vm.vendor.homeEmail = data.homeEmail;
				    vm.vendor.homePhone = data.homePhone;
				    vm.vendor.workAddress = data.workAddress;
				    vm.vendor.workEmail = data.workEmail;
				    vm.vendor.workPhone = data.workPhone;

				    alerts.success("Vendor has been updated!");

					//Close the modal
					$scope.$parent.$close();
				})
				.error(function (data) {
					vm.errorMessage = 'There was a problem updating the vendor: ' + data;
				})
				.finally(function () {
					vm.saving = false;
				});
		}
	}
})();