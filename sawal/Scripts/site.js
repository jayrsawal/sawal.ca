var i = 0;

$(document).ready(function () {
    $(".transparent").removeClass("transparent");
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