(function ($) {
    var hviewport = $(window).height();
    var $obj_peek;
    var settings;
    var scrollStart = 0;
    var bShow = true;
    var bTrack = true;

    $.fn.peekaboo = function (options) {
        var defaults = {
            offset_top: hviewport
            , scroll_threshold: (hviewport / 8)
            , opaque_threshold_down: (hviewport / 4)
            , opaque_threshold_up: (hviewport / 8)
            , animation: true
            , hide_class: "slide-up"
            , fade_class: "fade-background"
            , hide_height_auto: true
        };

        settings = $.extend(defaults, options);
        if (settings.animation) {
            this.css({
                "-webkit-transition": "all 0.4s ease"
                , "transition": "all 0.4s ease"
            });
        }
        $obj_peek = this;
    };

    $(window).scroll(function () {
        $obj_peek.each(function () {
            var scrollTop = $(document).scrollTop();
            if (scrollTop > settings.opaque_threshold_down) {
                $(this).addClass(settings.fade_class);
            } else if ($(document).scrollTop() < settings.opaque_threshold_up) {
                $(this).removeClass(settings.fade_class);
            }

            if (scrollTop > settings.offset_top) {
                if (bTrack) {
                    scrollStart = scrollTop;
                    bTrack = false;
                }
                var hdiff = scrollStart - $(document).scrollTop();

                // start checking if we need to hide
                if (hdiff < -settings.scroll_threshold) {
                    if (settings.hide_height_auto) {
                        var navHeight = -($(this).height() + 10);
                        $(this).css("top", navHeight);
                    } else {
                        $(this).addClass(hide_class);
                    }
                    scrollStart = scrollTop;
                    bTrack = true;
                }

                // start checking if we need to hide
                if (hdiff > settings.scroll_threshold) {
                    if (settings.hide_height_auto) {
                        $(this).css("top", 0);
                    } else {
                        $(this).removeClass(hide_class);
                    }
                    scrollStart = scrollTop;
                    bTrack = true;
                }
            } else {
                // always show
                bTrack = true;
                bShow = true;
                $(this).removeClass(settings.hide_class);
            }
        });
    });
}(jQuery));