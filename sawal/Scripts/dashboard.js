var bEdit = false;

function getBlog(strID) {
    bEdit = true;
    var service = $.ajax({
        url: "/main/ajaxservice?service=getblog"
        , data: {
            blogid: strID
        }
    });

    service.success(function(data) {
        getBlogSuccess(data);
    });
    service.error(function (data) {
        getBlogFail(data);
    });
}


function getBlogSuccess(data) {
    var blog = JSON.parse(data);
    if (blog) {
        $("#mainform #Id").val(blog.Id);
        $("#mainform #BlogTypeId").val(blog.BlogTypeId);
        $("#mainform #Title").val(blog.Title);
        $("#mainform #Caption").val(blog.Caption);
        $("#mainform #Url").val(blog.Url);
        $("#mainform div.fr-element").html(blog.Html);
        $("#mainform #PublishDate").val(new Date(blog.PublishDate).toISOString().slice(0, 10));
        $("#mainform #ImagePath").val(blog.ImagePath);
    }
    return blog;
}

function getBlogFail(data) {
    snack("Error retrieving blog data", false);
}


function snack(strMessage, bError) {
    var strMsg = "<div id='snackbar'></div>";
    if (bError) {
        var strMsg = "<div class='warning' id='snackbar'></div>";
    }
    $("#snacks").append(strMsg);
    var $bar = $("#snackbar").last();
    $bar.html(strMessage);
    $bar.addClass("show");
    addSnackListener();

    setTimeout(function () {
        $bar.removeClass("show");
        setTimeout(function () {
            $bar.remove();
        }, 1000);
    }, 3000);
}

function addSnackListener() {
    $("#snackbar").click(function () {
        $(this).remove();
    });
}

