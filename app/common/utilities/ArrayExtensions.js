(function() {
    'use strict';

    Object.defineProperty(Array.prototype, 'count', {
        get: function () { return this.length;}
    });

	if (Array.prototype.addRange) return;

	Array.prototype.addRange = function (target) {
		this.push.apply(this, target);
	};

	Array.prototype.insert = function (index, item) {
	    this.splice(index, 0, item);
	};
})();