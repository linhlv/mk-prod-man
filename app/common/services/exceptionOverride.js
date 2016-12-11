(function () {
    'use strict';
    window.app.factory('$exceptionHandler', ['$log', 'alerts', function ($log, alerts) {
        return function (exception, cause) {
            alerts.error('There was a problem with your last action. Please reload the page and try again.');
            $log.error(exception, cause);
        };
    }]);
})();