(function() {
	'use strict';

	window.app.directive('addColor', buildDirective);

	function buildDirective() {
		return {
			scope: {
			    colors: "="
			},
			templateUrl: '/color/templates/colorAdd.tmpl.cshtml',
			controller: controller,
			controllerAs: 'vm'
		}
	}

	controller.$inject = ['$scope', '$http'];

	function controller($scope, $http) {
		var vm = this;

		vm.saving = false;
		vm.color = {}

		vm.add = add;

		function add() {
			vm.saving = true;

		    $http.post('/Color/Add', vm.color)
				.success(function (data) {
				    $scope.colors.unshift(data);

				    alerts.success("Color has been created!");

					//Close the modal
					$scope.$parent.$close();
				})
				.error(function (data) {
					vm.errorMessage = 'There was a problem adding the color: ' + data;
				})
				.finally(function () {
					vm.saving = false;
				});
		}
	}
})();