(function () {
    window.app.factory('colorSvc', svcf);

    svcf.$inject = ['$http', '$q'];
    function svcf($http, $q) {
        var svc = {
            all: all,
            search: search,
            itemDelete: itemDelete
        };

        return svc;

        function all() {
            var deferred = $q.defer();
            $http.post('/Color/All')
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
            $http.post('/Color/Search', filter)
                .success(function (data) {
                    deferred.resolve(data);
                })
                .error(function (data) {
                    deferred.reject(data);
                });
            return deferred.promise;
        }

        function itemDelete(deletingItem) {
            var deferred = $q.defer();
            $http.post('/Color/Delete', deletingItem)
				 .success(function (data) {
				     deferred.resolve(data);
				 })
                .error(function (data) {
                    deferred.reject(data);
                });
            return deferred.promise;
        }
    }
})();