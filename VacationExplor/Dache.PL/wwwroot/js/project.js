
$(function () {
    $('.sliding-navbar').hide();
    
    $('.hamburger-menu').click(function () {
        $('.sliding-navbar').toggle("slow", "swing");  
        $('.sliding-navbar').toggleClass('sliding-navbar--open');
        $('.hamburger').toggleClass('menu-opened');
        if ($("#buth").attr('class') == 'hamburger') {
            $("#buth").css("background", "white")
        }
        else {
            $("#buth").css("background", "none")
        }
    });
});

$(window).on('load', function () {
    $('.loading').hide();
})

function doitAjax(methodd, url, func) {
    $.ajax({
        url: url,
        type: methodd,
        success: function (response) {
            func(response);
        },
        cache: false,
        contentType: false,
        processData: false,
    });
}
var forInfo = (resp) => {
    $("#desc").html(resp);
}
var forCreate = (resp) => {
    var sid = document.getElementById("Id").innerHTML;
    $("#desc").html(resp);
    $("#supid").val(sid);
}

var forEdit = (resp) => {
    $("#desc").html(resp);
}


