(function () {
    'use strict';

    window.app.directive('equalHeight', buildDirective);

    function buildDirective() {
        return {
            restrict: 'AE', //describes how we can assign an element to our directive in this case like <div master></div
            link: link // the function to link to our element
        };

        function link(scope, element, attrs) { //scope we are in, element we are bound to, attrs of that element
            scope.$watch(function () { //watch any changes to our element
                scope.style = { //scope variable style, shared with our controller
                    height: element[0].offsetHeight + 'px', //set the height in style to our elements height
                    width: element[0].offsetWidth + 'px' //same with width
                };
            });
        }
    }

})();