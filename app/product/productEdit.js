(function () {
    'use strict';

    window.app.directive('productEdit', buildDirective);

    function buildDirective() {
        return {
            scope: {
                product: "="
            },
            templateUrl: '/product/templates/productEdit.tmpl.cshtml',
            link: {
                post: function (scope, element, attrs) {
                    setTimeout(function () {
                        $("#ColorId").val(scope.product.colorId + '');
                        $("#VendorId").val(scope.product.vendorId + '');
                        $("#CategoryId").val(scope.product.categoryId + '');
                        $("#MaterialId").val(scope.product.materialId + '');
                    }, 200);
                }
            },
            controller: controller,
            controllerAs: 'vm'
        }
    }

    controller.$inject = ['$scope', '$http', '$element', 'productSvc'];

    function controller($scope, $http, $element, productSvc) {
        var vm = this;
        vm.product = $scope.product;
        vm.saving = false;
        vm.close = close;
        vm.update = update;

        vm.product.sizes = [];

        if (vm.product.productSizes && vm.product.productSizes.length) {

            for (var i = 0; i < vm.product.productSizes.length; i++) {
                 vm.product.sizes.push(
                    {
                        id: vm.product.productSizes[i].id,
                        value: vm.product.productSizes[i].value
                    }
                );
            }
        }

        vm.addSize = function () {
            vm.product.sizes.push(
                {
                    id: guid(),
                    value: ''
                }
            );
        }

        vm.remove = function (ps) {
            var idx = -1;
            for (var i = 0; i < vm.product.sizes.length; i++) {
                if (vm.product.sizes[i].id === ps.id) {
                    idx = i;
                    break;
                }
            }

            if (idx!==-1) {
                vm.product.sizes.splice(idx, 1);
            }
        };

        function guid() {
            function s4() {
                return Math.floor((1 + Math.random()) * 0x10000)
                  .toString(16)
                  .substring(1);
            }
            return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
              s4() + '-' + s4() + s4() + s4();
        }

        function update() {
            vm.saving = true;
            productSvc.update(vm.product)
                .then(function (data) {
                    
                    vm.product = data;

                   
                    
                    if (data.productSizes && data.productSizes.length) {
                        vm.product.sizes = [];
                        for (var i = 0; i < data.productSizes.length; i++) {
                            vm.product.sizes.push({
                                id: data.productSizes[i].id,
                                value: data.productSizes[i].value
                            });
                        }
                    }

                    

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

    }
})();