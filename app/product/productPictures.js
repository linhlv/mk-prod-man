(function () {
    'use strict';

    window.app.directive('productPictures', buildDirective);

    function buildDirective() {
        return {
            scope: {
                product: "="
            },
            templateUrl: '/product/templates/productPictures.tmpl.cshtml',
            controller: controller,
            controllerAs: 'vm'
        }
    }

    controller.$inject = ['$scope', '$http', 'productSvc'];

    function controller($scope, $http, productSvc) {
        var vm = this;

        vm.product = $scope.product;

        vm.saving = false;

        vm.deleting = false;

        vm.close = close;

        vm.deleteImage = deleteImage;

        vm.uploadingImage = {
            productId: vm.product.id,
            file: null
        };

        vm.uploadImage = uploadImage;

        function setActiveImage(index) {
            for (var i = 0; i < vm.product.productImages.length; i++) {
                if (index !== i) {
                    vm.product.productImages[i].active = false;
                }
            }

            vm.product.productImages[index].active = true;
        }

        function uploadImage() {
            vm.saving = true;
            productSvc.addProductImage(vm.uploadingImage)
		    .then(function (data) {
		        vm.product.productImages.push(data);
		        vm.saving = false;
		        angular.element(document).find('#productImageFile').val('');
		    }, function (error) {
		        vm.errorMessage = 'There was a problem adding the product image: ' + error;
		        vm.saving = false;
		    });
        }

        function close() {
            //Close the modal
            $scope.$parent.$close();
        }

        function deleteImage(id) {
            vm.deleting = true;
            productSvc.removeProductImage(id)
                .then(
                function (data) {
                    vm.product.productImages = _.without(
                        vm.product.productImages,
                        _.findWhere(vm.product.productImages, { id: data.id })
                    );
                    vm.deleting = false;
                }, function (error) {
                    vm.deleting = false;
                }
            );
        }
    }
})();