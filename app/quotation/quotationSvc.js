(function () {
    window.app.factory('quotationSvc', quotationSvc);

    quotationSvc.$inject = ['$http', '$q'];
    function quotationSvc($http, $q) {

        function addToQuotation(product) {
            var deferred = $q.defer();
            $http.post('/Quotation/Add', product)
                .success(function (data) {
                    deferred.resolve(data);
                })
                .error(function (data) {
                    deferred.reject(data);
                });
            return deferred.promise;
        }

        function removeQuotationItem(product) {
            var deferred = $q.defer();
            $http.post('/Quotation/Remove', product)
                .success(function (data) {
                    deferred.resolve(data);
                })
                .error(function (data) {
                    deferred.reject(data);
                });
            return deferred.promise;
        }

        function removeQuotationItemByProductId(id) {
            var deferred = $q.defer();
            $http.post('/Quotation/RemoveByProductId', id)
                .success(function (data) {
                    deferred.resolve(data);
                })
                .error(function (data) {
                    deferred.reject(data);
                });
            return deferred.promise;
        }

        function allQuotation() {
            var deferred = $q.defer();
            $http.post('/Quotation/All')
                .success(function (data) {
                    deferred.resolve(data);
                })
                .error(function (data) {
                    deferred.reject(data);
                });
            return deferred.promise;
        }

        function save(q) {
            var deferred = $q.defer();
            $http.post('/Quotation/Save', q)
                .success(function (data) {
                    deferred.resolve(data);
                })
                .error(function (data) {
                    deferred.reject(data);
                });
            return deferred.promise;
        }

        function exportExcel() {
            var deferred = $q.defer();
            $http.post('/Quotation/ExportExcel')
                .success(function (data) {
                    deferred.resolve(data);
                })
                .error(function (data) {
                    deferred.reject(data);
                });
            return deferred.promise;
        }

        var svc = {
            exportExcel:exportExcel,
            save: save,
            addToQuotation: addToQuotation,
            removeQuotationItem: removeQuotationItem,
            allQuotation: allQuotation,
            removeQuotationItemByProductId: removeQuotationItemByProductId
        };

        return svc;
    }
})();