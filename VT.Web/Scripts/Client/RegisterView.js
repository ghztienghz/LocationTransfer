$(function () {
    // thay đổi tỉnh - tp
    $("#IdProvince").change(function () {
        var url_send = $("#get-area-cbo").val();
        var idArea = parseInt($(this).val());
        $.ajax({
            url: url_send,
            dataType: 'json',
            type: 'POST',
            data: { id: idArea },
            success: function (rs) {
                $("#IdDistrict> option:gt(0)").remove();
                $("#IdWard > option:gt(0)").remove();
                var strAppend = '';
                $.each(rs, function (k, v) {
                    strAppend += '<option value="' + v.AreaId + '">' + v.AreaName + '</option>';
                });
                $("#IdDistrict").append($(strAppend));
            }, error: function () {
                location.reload();
            }
        });
    });

    // thay đổi quận - huyện
    $("#IdDistrict").change(function () {
        var url_send = $("#get-area-cbo").val();
        var idArea = parseInt($(this).val());
        $.ajax({
            url: url_send,
            dataType: 'json',
            type: 'POST',
            data: { id: idArea },
            success: function (rs) {
                $("#IdWard > option:gt(0)").remove();
                var strAppend = '';
                $.each(rs, function (k, v) {
                    strAppend += '<option value="' + v.AreaId + '">' + v.AreaName + '</option>';
                });
                $("#IdWard").append($(strAppend));
            }, error: function () {
                location.reload();
            }
        });
    });

    // thay đổi phường -xã
    $("#IdWard").change(function () {
        $("#FullAddress").val(' ,' + $("#IdWard > option:selected").text() + ' ,' + $("#IdDistrict > option:selected").text() + ' ,' + $("#IdProvince > option:selected").text()).focus();
        $("#last-address").css("display", "block");
        $("#map").css("display", "block");
        initMap();
    });
});