(function() {
    window.app.factory('userSvc', svcf);

    svcf.$inject = ['$http', '$q'];

    function svcf($http, $q) {
		var svc = {
			add: add,
			update: update,
			getVendor: getVendor,
			search: search,
			setPassword: setPassword,
			createUser: createUser,
			deleteUser: deleteUser,
			changePassword: changePassword
		};

		return svc;

		function search(filter) {
		    var deferred = $q.defer();
		    $http.post('/UserManagement/Search', filter)
                .success(function (data) {
                    deferred.resolve(data);
                })
                .error(function (data) {
                    deferred.reject(data);
                });
		    return deferred.promise;
		}

		function add(vendor) {
		    return $http.post('/UserManagement/Add', vendor)
				.success(function (vendor) {
				    vendors.unshift(vendor);
				});
		}

		function createUser(viewModel) {
		    var deferred = $q.defer();
		    $http.post('/UserManagement/Create', viewModel)
                .success(function (data) {
                    deferred.resolve(data);
                })
                .error(function (data) {
                    deferred.reject(data);
                });
		    return deferred.promise;
		}

		function deleteUser(viewModel) {
		    var deferred = $q.defer();
		    $http.post('/UserManagement/Delete', viewModel)
                .success(function (data) {
                    deferred.resolve(data);
                })
                .error(function (data) {
                    deferred.reject(data);
                });
		    return deferred.promise;
		}

		function setPassword(viewModel) {
		    var deferred = $q.defer();
		    $http.post('/UserManagement/SetPassword', viewModel)
                .success(function (data) {
                    deferred.resolve(data);
                })
                .error(function (data) {
                    deferred.reject(data);
                });
		    return deferred.promise;
		}

		function changePassword(viewModel) {
		    var deferred = $q.defer();
		    $http.post('/UserManagement/ChangePassword', viewModel)
                .success(function (data) {
                    deferred.resolve(data);
                })
                .error(function (data) {
                    deferred.reject(data);
                });
		    return deferred.promise;
		}

		function update(existingVendor, updatedVendor) {
		    return $http.post('/UserManagement/Update', updatedVendor)
				.success(function (vendor) {
				    angular.extend(existingVendor, vendor);
				});
		}

		function getVendor(id) {
		    for (var i = 0; i < vendors.length; i++) {
			    if (vendors[i].Id == id) return vendors[i];
			}

			return null;
		}
	}
})();