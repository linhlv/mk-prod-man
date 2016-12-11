(function () {
    'use strict';
    window.onerror = function (msg) {
       
    };   

    var id = 'crm';

    window.app = angular.module(id, ['ui.router', 'ngAnimate', 'ui.bootstrap', 'ui.grid', 'ui.grid']);

    window.app.directive('datetimez', function () {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attrs, ngModelCtrl) {
                element.datetimepicker({
                    format: "MM-YYYY",
                    viewMode: "months",
                    defaultDate: scope.dt
                }).on('dp.change', function (e) {
                    scope.dt = e.date;
                    ngModelCtrl.$setViewValue(e.date);
                    scope.$apply();
                });
            }
        };
    });

    window.app.run([
		function() {
			//Startup code goes here!
		}
	]);
})();