(function() {
	'use strict';

	window.app.directive('colorEdit', buildDirective);

	function buildDirective() {
		return {
			scope: {
			    color: "="
			},
			templateUrl: '/color/templates/colorEdit.tmpl.cshtml',
			controller: controller,
			controllerAs: 'vm'
		}
	}

	controller.$inject = ['$scope', '$http', 'alerts'];

	function controller($scope, $http, alerts) {
		var vm = this;

		vm.saving = false;

		vm.editColor = {};

		vm.color = $scope.color;

		angular.copy($scope.color, vm.editColor);

		vm.save = save;

		function save() {
		    vm.saving = true;

		    $http.post('/Color/Update', vm.editColor)
				.success(function (data) {
				    vm.color.name = data.name;

				    vm.color.description = data.description;

				    alerts.success("Color has been updated!");

				    //Close the modal
				    $scope.$parent.$close();
				})
				.error(function (data) {
				    vm.errorMessage = 'There was a problem updating the color: ' + data;
				})
				.finally(function () {
				    vm.saving = false;
				});
		}

	}
})();