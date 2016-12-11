(function () {
    'use strict';

    window.app.directive('productEditPrice', buildDirective);

    function buildDirective() {
        return {
            scope: {
                product: "="
            },
            templateUrl: '/product/templates/productEditPrice.tmpl.cshtml',
            controller: controller,
            controllerAs: 'vm'
        }
    }

    controller.$inject = ['$scope', '$http', 'productSvc'];

    function controller($scope, $http, productSvc) {
        var vm = this;
        vm.product = $scope.product;
        vm.editPriceProduct = {
            id: vm.product.id,
            oldPrice:  vm.product.listPrice,
            listPrice: vm.product.listPrice,
            lastPriceDate: vm.product.lastPriceDate
        }
        vm.saving = false;
        vm.close = close;
        vm.update = update;

        function update() {
            vm.saving = true;
            vm.editPriceProduct.lastPriceDate = $scope.dt;
            productSvc.updatePrice(vm.editPriceProduct)
                .then(function (data) {
                    vm.product.listPrice = data.listPrice;
                    //Close the modal
                    $scope.$parent.$close();
                    vm.saving = false;
                    location.reload();
                });
        }

        function close() {
            //Close the modal
            $scope.$parent.$close();
        }
        
        $scope.dt = vm.product.lastPriceDate;
    }
})();