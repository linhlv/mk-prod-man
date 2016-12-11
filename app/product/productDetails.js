(function () {
    'use strict';

    window.app.directive('productDetails', buildDirective);

    function buildDirective() {
        return {
            scope: {
                product: '='
            },
            templateUrl: '/product/templates/productDetails.tmpl.cshtml',
            controller: controller,
            controllerAs: 'vm'
        }
    }

    controller.$inject = ['$scope', '$uibModal', 'quotationSvc', 'productSvc'];

    function controller($scope, $uibModal, quotationSvc, productSvc) {
        var vm = this;

        vm.product = $scope.product;
        vm.edit = edit;
        vm.delete = deleteProduct;
        vm.showImages = showImages;
        vm.setView = setView;
        vm.selectedView = 'details';
        vm.addToQuotation = addToQuotation;
        vm.editPrice = editPrice;
        vm.showDescription = showDescription;

        vm.addRemoveQuotationText = function () {
            return vm.product.quotationSelected ? 'Remove from Quotation' : 'Add to Quotation';
        };

        vm.productImageUrl = function () {
            var url = window.relativeUrl + 'Content/data/images/prod/' +
            ((vm.product.productImages && vm.product.productImages.length > 0) ? vm.product.productImages[0].imageFileUrl : 'no-image.png');

            return url;
        };

        function showDescription(product) {
            $uibModal.open({
                template: '<product-description product="product" />',
                scope: angular.extend($scope.$new(true), { product: product })
            });
        }

        function setView(view) {
            vm.selectedView = view;
        }

        function addToQuotation() {
            vm.product.quotationSelected = !vm.product.quotationSelected;

            if (vm.product.quotationSelected) {
                //add
                quotationSvc.addToQuotation(vm.product);
            } else {
                //remove
                quotationSvc.removeQuotationItem(vm.product);
            }
        }

        function showImages() {
            $uibModal.open({
                template: '<product-pictures product="product" />',
                scope: angular.extend($scope.$new(true), { product: vm.product })
            });
        }

        function editPrice() {
            $uibModal.open({
                template: '<product-edit-price product="product" />',
                scope: angular.extend($scope.$new(true), { product: vm.product })
            });
        }

        function edit() {
            $uibModal.open({
                template: '<product-edit product="product" />',
                scope: angular.extend($scope.$new(true), { product: vm.product })
            });
        }

        function deleteProduct() {
            var modal = $uibModal.open({
                template: '<confirm-message title="title" message="message" yes="yes()" />',
                scope: angular.extend($scope.$new(true), {
                    title: 'Delete ' + vm.product.code + ' - ' + vm.product.name + '?',
                    message: 'Are you sure you want to delete?',
                    yes: function () {
                        productSvc.deleteProduct(vm.product).then(function(data) {
                            $scope.$parent.$parent.vm.products = _.without(
                                $scope.$parent.$parent.vm.products,
                                _.findWhere($scope.$parent.$parent.vm.products, { id: data.id })
                            );

                            alerts.success("Product has been updated!");

                            modal.close();
                        });
                    }
                })
            });
        }
    }
})();