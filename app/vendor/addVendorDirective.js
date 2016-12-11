(function() {
	'use strict';

	window.app.directive('addVendor', buildDirective);

	function buildDirective() {
		return {
			scope: {
			    vendors: "="
			},
			templateUrl: '/vendor/templates/addVendor.tmpl.cshtml',
			controller: controller,
			controllerAs: 'vm'
		}
	}

	controller.$inject = ['$scope', '$http'];

	function controller($scope, $http) {
		var vm = this;

		vm.saving = false;
		vm.vendor = {
			//customerId: $scope.customer.id
		}

		vm.add = add;

		function add() {
			vm.saving = true;

			$http.post('/Vendor/Add', vm.vendor)
				.success(function (data) {
				    $scope.vendors.unshift(data);
					//Close the modal
					$scope.$parent.$close();
				})
				.error(function (data) {
					vm.errorMessage = 'There was a problem adding the vendor: ' + data;
				})
				.finally(function () {
					vm.saving = false;
				});
		}
	}
})();