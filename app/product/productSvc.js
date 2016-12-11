(function () {
    window.app.factory('productSvc', productSvc);

    productSvc.$inject = ['$http', '$q', 'akFileUploaderService'];
    function productSvc($http, $q, akFileUploaderService) {
        var svc = {
            add: add,
            update: update,
            deleteProduct:  deleteProduct,
            getProduct: getProduct,
            search: search,
            addProductImage: addProductImage,
            removeProductImage: removeProductImage,
            updatePrice: updatePrice,
            getSizesByProductId: getSizesByProductId
        };

        return svc;

        function getSizesByProductId(id) {
            var deferred = $q.defer();
            $http.get('/Product/GetProductSizes?productId=' + id)
                .success(function (data) {
                    deferred.resolve(data);
                })
                .error(function (data) {
                    deferred.reject(data);
                });
            return deferred.promise;
        }

        function search(filter) {
            var deferred = $q.defer();
            $http.post('/Product/Search', filter)
                .success(function (data) {
                    deferred.resolve(data);
                })
                .error(function (data) {
                    deferred.reject(data);
                });
            return deferred.promise;
        }

        function add(product) {
            return akFileUploaderService.saveModel(product, "/Product/Add");
        }

        function addProductImage(productImage) {
            return akFileUploaderService.saveModel(productImage, "/Product/AddProductImage");
        }

        function update(updatedProduct) {
            var deferred = $q.defer();
            $http.post('/Product/Update', updatedProduct)
				 .success(function (data) {
				     deferred.resolve(data);
				 })
                .error(function (data) {
                    deferred.reject(data);
                });
            return deferred.promise;
        }

        function deleteProduct(deletingProduct) {
            var deferred = $q.defer();
            $http.post('/Product/Delete', deletingProduct)
				 .success(function (data) {
				     deferred.resolve(data);
				 })
                .error(function (data) {
                    deferred.reject(data);
                });
            return deferred.promise;
        }

        function updatePrice(updatingPriceProduct) {
            var deferred = $q.defer();
            $http.post('/Product/UpdatePrice', updatingPriceProduct)
                .success(function (data) {
                    deferred.resolve(data);
                })
                .error(function (data) {
                    deferred.reject(data);
                });
            return deferred.promise;
        }

        function removeProductImage(id) {
            var deferred = $q.defer();
            $http.post('/Product/DeleteProductImage', id)
                .success(function (data) {
                    deferred.resolve(data);
                })
                .error(function (data) {
                    deferred.reject(data);
                });
            return deferred.promise;
        }

        function getProduct(id) {
            for (var i = 0; i < products.length; i++) {
                if (products[i].Id == id) return products[i];
            }

            return null;
        }
    }
})();