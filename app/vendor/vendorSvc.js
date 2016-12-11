(function() {
    window.app.factory('vendorSvc', svcf);

    svcf.$inject = ['$http', '$q'];
    function svcf($http, $q) {
		var svc = {
			add: add,
			update: update,
			getVendor: getVendor,
			search: search,
			all: all,
			vendorDelete: vendorDelete
		};

		return svc;

		function all() {
		    var deferred = $q.defer();
		    $http.post('/Vendor/All')
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
		    $http.post('/Vendor/Search', filter)
                .success(function (data) {
                    deferred.resolve(data);
                })
                .error(function (data) {
                    deferred.reject(data);
                });
		    return deferred.promise;
		}

		function add(vendor) {
		    return $http.post('/Vendor/Add', vendor)
				.success(function (vendor) {
				    vendors.unshift(vendor);
				});
		}

		function update(existingVendor, updatedVendor) {
		    return $http.post('/Vendor/Update', updatedVendor)
				.success(function (vendor) {
				    angular.extend(existingVendor, vendor);
				});
		}

		function vendorDelete(deletingVendor) {
		    var deferred = $q.defer();
		    $http.post('/Vendor/Delete', deletingVendor)
				 .success(function (data) {
				     deferred.resolve(data);
				 })
                .error(function (data) {
                    deferred.reject(data);
                });
		    return deferred.promise;
		}

		function getVendor(id) {
		    for (var i = 0; i < vendors.length; i++) {
			    if (vendors[i].Id == id) return vendors[i];
			}

			return null;
		}
	}
})();