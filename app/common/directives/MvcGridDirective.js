(function () {
    'use strict';

    window.app.directive('mvcGrid', mvcGrid);

    function mvcGrid() {
        return {
            scope: {
                gridDataUrl: '@',
                gridOptions: '=',
                title: '@',
                columns: '@?'
            },
            template:
				'<div>{{vm.gridOptions.data.length}}' +
					'<h4><i class="fa fa-pie-chart fa-fw"></i> {{vm.title}}</h4>' +
					'<div>' +
						'<p ng-if="vm.loading">Loading...</p>' +
						'<div ng-if="!vm.loading" ui-grid="gridOptions"></div>' +
					'</div>' +
				'</div>',
            controllerAs: 'vm',
            controller: controller
        }
    }

    controller.$inject = ['$scope', '$http'];

    function controller($scope, $http) {
        $scope.gridOptions = {
            enableHorizontalScrollbar: 0
        };

        var vm = this;

        vm.loading = true;

        vm.title = $scope.title;

        if ($scope.columns)
            $scope.gridOptions.columnDefs = angular.fromJson($scope.columns);

        $http.post($scope.gridDataUrl)
			.success(function (data) {
			    $scope.gridOptions.data = data;
			    vm.loading = false;
			});
    }

})();