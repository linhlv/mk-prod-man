(function() {
	'use strict';

	window.app.directive('categoryEdit', buildDirective);

	function buildDirective() {
		return {
			scope: {
			    category: "="
			},
			templateUrl: '/category/templates/editCategory.tmpl.cshtml',
			controller: controller,
			controllerAs: 'vm'
		}
	}

	controller.$inject = ['$scope', '$http', 'alerts'];

	function controller($scope, $http, alerts) {
		var vm = this;

		vm.saving = false;

		vm.editingCategory = {};

		vm.category = $scope.category;

		angular.copy($scope.category, vm.editingCategory);

		vm.save = save;

		function save() {
		    vm.saving = true;

		    $http.post('/Category/Update', vm.editingCategory)
				.success(function (data) {
				    vm.category.name = data.name;

				    vm.category.description = data.description;

				    alerts.success("Category has been updated!");

				    //Close the modal
				    $scope.$parent.$close();
				})
				.error(function (data) {
				    vm.errorMessage = 'There was a problem updating the category: ' + data;
				})
				.finally(function () {
				    vm.saving = false;
				});
		}

	}
})();