var i = 0;
//var scrollStart = 0;
//var bShow = true;
//var bTrack = true;

//$(window).scroll(function () {
//    var hviewport = $(window).height();
//    if ($(document).scrollTop() > hviewport / 2) {
//        $('.navbar-custom').addClass('fade-background');
//    } else if ($(document).scrollTop() < hviewport / 4) {
//        $('.navbar-custom').removeClass('fade-background');
//    }

//    if ($(document).scrollTop() > hviewport) {
//        if (bTrack) {
//            scrollStart = $(document).scrollTop();
//            bTrack = false;
//        }
//        var hdiff = scrollStart - $(document).scrollTop();
//        var hdiffmove = (hviewport / 8);

//        // start checking if we need to hide
//        if (hdiff < -hdiffmove) {
//            $('.navbar-custom').addClass('slide-up');
//            scrollStart = $(document).scrollTop();
//            bTrack = true;
//        }

//        // start checking if we need to hide
//        if (hdiff > hdiffmove) {
//            $('.navbar-custom').removeClass('slide-up');
//            scrollStart = $(document).scrollTop();
//            bTrack = true;
//        }
//    } else {
//        // always show
//        bTrack = true;
//        bShow = true;
//        $('.navbar-custom').removeClass('slide-up');
//    }
//});


$(document).ready(function () {
    $(".navbar-custom").peekaboo();
    $(".data-table").DataTable();
    $('textarea#Html').froalaEditor({
        //paragraphStyles: {
        //    class1: 'Class 1',
        //    class2: 'Class 2'
        //},
        heightMin: 300,
        toolbarButtons: ['bold', 'italic', 'underline', 'strikeThrough', 'insertLink', 'insertTable', 'fontFamily', 'fontSize', '|', 'paragraphStyle', 'paragraphFormat', 'align', 'undo', 'redo', 'html']
    });
});