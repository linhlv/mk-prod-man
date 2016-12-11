(function() {
	'use strict';

	window.app.directive('addMaterial', addMaterial);

	function addMaterial() {
		return {
			scope: {
			    materials: "="
			},
			templateUrl: '/material/templates/addMaterial.tmpl.cshtml',
			controller: controller,
			controllerAs: 'vm'
		}
	}

	controller.$inject = ['$scope', '$http'];

	function controller($scope, $http) {
		var vm = this;

		vm.saving = false;
		vm.material = {
			//customerId: $scope.customer.id
		}

		vm.add = add;

		function add() {
			vm.saving = true;

		    $http.post('/Material/Add', vm.material)
				.success(function (data) {
				    $scope.materials.unshift(data);

				    alerts.success("Material has been created!");

					//Close the modal
					$scope.$parent.$close();
				})
				.error(function (data) {
					vm.errorMessage = 'There was a problem adding the material: ' + data;
				})
				.finally(function () {
					vm.saving = false;
				});
		}
	}
})();