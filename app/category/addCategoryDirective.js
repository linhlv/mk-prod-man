(function() {
	'use strict';

	window.app.directive('addCategory', buildDirective);

	function buildDirective() {
		return {
			scope: {
			    categories: "="
			},
			templateUrl: '/category/templates/addCategory.tmpl.cshtml',
			controller: controller,
			controllerAs: 'vm'
		}
	}

	controller.$inject = ['$scope', '$http', 'alerts'];

	function controller($scope, $http, alerts) {
		var vm = this;

		vm.saving = false;
		vm.category = {
			//customerId: $scope.customer.id
		}

		vm.add = add;

		function add() {
			vm.saving = true;

		    $http.post('/Category/Add', vm.category)
				.success(function (data) {
				    $scope.categories.unshift(data);

				    alerts.success("Category has been created!");

					//Close the modal
					$scope.$parent.$close();
				})
				.error(function (data) {
					vm.errorMessage = 'There was a problem adding the category: ' + data;
				})
				.finally(function () {
					vm.saving = false;
				});
		}
	}
})();