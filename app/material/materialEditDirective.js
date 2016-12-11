(function() {
	'use strict';

	window.app.directive('materialEdit', buildDirective);

	function buildDirective() {
		return {
			scope: {
			    material: "="
			},
			templateUrl: '/material/templates/editMaterial.tmpl.cshtml',
			controller: controller,
			controllerAs: 'vm'
		}
	}

	controller.$inject = ['$scope', '$http', 'alerts'];

	function controller($scope, $http, alerts) {
		var vm = this;

		vm.saving = false;

		vm.editMaterial = {};

		vm.material = $scope.material;

		angular.copy($scope.material, vm.editMaterial);

		vm.save = save;

		function save() {
		    vm.saving = true;

		    $http.post('/Material/Update', vm.editMaterial)
				.success(function (data) {
				    vm.material.name = data.name;

				    vm.material.description = data.description;

				    alerts.success("Material has been updated!");

				    //Close the modal
				    $scope.$parent.$close();
				})
				.error(function (data) {
				    vm.errorMessage = 'There was a problem updating the material: ' + data;
				})
				.finally(function () {
				    vm.saving = false;
				});
		}

	}
})();