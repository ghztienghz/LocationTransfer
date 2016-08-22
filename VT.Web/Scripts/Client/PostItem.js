$(function () {
    $('#Price').keyup(function (e) {
        var price = parseFloat($(this).val());
        if ((price / 1000) >= 1) {
            $(this).val(price / 1000);
            $('.radio_ty').prop('checked', true);
        }
        if ((price / 1000) < 1 && $(this).val().indexOf('.') == -1) {
            $(this).val($(this).val().replace(/\./, ''));
            $('.radio_trieu').prop('checked', true);
        }
    });

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
                //location.reload();
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
                //location.reload();
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
    $('#Files').change(function () {
        $('.review-file-img').empty();
        var files = this.files;
        if (files.length > 6) {
            alert('Tối đa 6 tấm hình. Vui lòng chọn lại');
            $(this).val('');
            return;
        }
        for (var i = 0; i < files.length; i++) {
            var file = files[i];
            var check = file.size / 1024;
            if (check > 20480) {
                alert('Kích thước tập tin phải nhỏ hơn 20MB');
                $(this).val('');
                return;
            }
        }

        for (var i = 0; i < files.length; i++) {
            var file = files[i];
            var reader = new FileReader();
            reader.onload = function (load) {
                var html = '<div class="review-item">' +
                    '<img src="'+load.target.result+'" alt="Ảnh đăng bài" />' +
                    '</div>';
                $('.review-file-img').append($(html));
            }
            reader.readAsDataURL(file);
        }
        $('body,html').animate({
            scrollTop: $('.review-file-img').offset().top
        });
    });;
});