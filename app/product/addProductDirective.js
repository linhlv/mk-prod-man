(function() {
	'use strict';

	window.app.directive('addProduct', buildDirective);

	function buildDirective() {
		return {
			scope: {
			    products: "=",
			    reload: "&"
			},
			templateUrl: '/product/templates/addProduct.tmpl.cshtml',
			controller: controller,
			controllerAs: 'vm'
		}
	}

	controller.$inject = ['$scope', '$http', 'productSvc'];

	function controller($scope, $http, productSvc) {
		var vm = this;

		vm.saving = false;
		vm.product = {
			//customerId: $scope.customer.id
		}

		vm.product.sizes = [];
        
		vm.addSize = function () {
		    vm.product.sizes.push(
                {
                    id: guid(),
                    value: ''
                }
            );
		}

        vm.recalculate = function() {
            console.log('sdfsdf');
        }

	    $scope.dt = new Date();

		function guid() {
		    function s4() {
		        return Math.floor((1 + Math.random()) * 0x10000)
                  .toString(16)
                  .substring(1);
		    }
		    return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
              s4() + '-' + s4() + s4() + s4();
		}

		vm.add = add;

		function add() {
			vm.saving = true;

            if (vm.product.sizes && vm.product.sizes.length) {
                for (var i = 0; i < vm.product.sizes.length; i++) {
                    vm.product['size_' + i] = vm.product.sizes[i].value;
                }
            }

            vm.product.lastPriceDate = $scope.dt.toUTCString();

		    productSvc.add(vm.product)
		    .then(function(data) {
		        $scope.products.unshift(data);
		        //Close the modal
		        $scope.$parent.$close();
		    }, function(error) {
		        vm.errorMessage = 'There was a problem adding the product: ' + data;
		    });
		}
	}
})();