;(function ( $, window, document, undefined ) {

    "use strict"; // jshint ;_;

    /* CLASS DEFINITION
	* ====================== */

    var App = function (element, options) {
        this.init(element, options);
    };

    App.prototype = {

        constructor: App,

        init: function (element, options) {
            this.$element = $(element);
            this.options = $.extend({}, $.fn.app.defaults, this.$element.data(), typeof options == 'object' && options);
        },

        toggle: function () {
            return this[!this.isShown ? 'show' : 'hide']();
        }
    };

    /* PRIVATE METHODS
	* ======================= */

    // comment method1
    var method1 = (function () {
        var z, index = {};

        return function () {
            
            return index;

        };
    }());

    // comment method2
    function method2(callback) {
        return function (e) {
            if (this === e.target) {
                if (typeof callback == 'function')
                    return callback.apply(this, arguments);
                else
                    return null;
            }
        };
    }

    /* PLUGIN DEFINITION
	* ======================= */

    $.fn.app = function (option, args) {
        console.log(this, option, args);
        return this.each(function() {
            var $this = $(this),
	            data = $this.data('app');

            if (!data) $this.data('app', (data = new App(this, option)));
            if (typeof option === 'string') data[option].apply(data, [].concat(args));
        });
    };

    $.fn.app.defaults = {
        backdropLimit: 999,
        resize: true,
        spinner: '<div class="loading-spinner fade" style="width: 200px; margin-left: -100px;"><div class="progress progress-striped active"><div class="bar" style="width: 100%;"></div></div></div>'
    };

    $.fn.app.Constructor = App;

})(jQuery, window, document);
