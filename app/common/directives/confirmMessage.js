(function () {
    'use strict';

    window.app.directive('confirmMessage', buildDirective);

    function buildDirective() {
        return {
            scope: {
                title: '=',
                message: '=',
                yes: '&'
            },
            templateUrl: '/common/templates/confirmMessage.tmpl.cshtml',
            controller: controller,
            controllerAs: 'vm'
        }
    }

    controller.$inject = ['$scope'];

    function controller($scope) {
        var vm = this;
        vm.title = $scope.title;
        vm.message = $scope.message;
        vm.yes = function () {
            $scope.yes();
        }
    }
})();