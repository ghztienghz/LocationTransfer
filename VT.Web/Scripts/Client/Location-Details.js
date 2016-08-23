$(function () {
    $(".click-change-img").click(function () {
        $(".item-thumb-img-in-detail > img.click-change-img").css("border", "5px solid #eaeaea");
        $(this).css("border", "5px solid #FF9190");
        var link = $(this).attr('data-full-url');
        $(".img-show-main").attr('src',link);
    });
});