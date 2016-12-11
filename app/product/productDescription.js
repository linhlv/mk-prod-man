(function () {
    'use strict';

    window.app.directive('productDescription', buildDirective);

    function buildDirective() {
        return {
            scope: {
                product: "="
            },
            templateUrl: '/product/templates/productDescription.tmpl.cshtml',
            controller: controller,
            controllerAs: 'vm'
        }
    }

    controller.$inject = ['$scope'];

    function controller($scope) {
        var vm = this;
        vm.product = $scope.product;
        vm.close = close;

        function close() {
            //Close the modal
            $scope.$parent.$close();
        }
    }
})();