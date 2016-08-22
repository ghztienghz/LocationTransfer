function initMap() {
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 13
    });
    var marker = new google.maps.Marker({
        //label: 'T',
        map: map,
        draggable: true,
        icon: '/Content/Images/mapmarker.png'
    });
    google.maps.event.addListener(marker, 'dragend', function (event) {
        $('#Lat').val(this.getPosition().lat());
        $('#Lng').val(this.getPosition().lng());
    });
    var infowindow = new google.maps.InfoWindow({
        content: 'Đây có phải là vị trí của bạn.\nVui lòng di chuyển ô đánh dấu này nều không phải'
    });
    var geocoder = new google.maps.Geocoder();
    var address = $('#FullAddress').val();

    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status === google.maps.GeocoderStatus.OK) {

            var pos = {
                lat: results[0].geometry.location.lat(),
                lng: results[0].geometry.location.lng()
            };
            map.setCenter(pos);
            marker.setPosition(pos);
            infowindow.open(map, marker);
            $('#Lat').val(pos.lat);
            $('#Lng').val(pos.lng);
        }
    });

    // sử dụng ô textbox để tìm địa chỉ trên google.
    var input = document.getElementById('FullAddress');
    var searchBox = new google.maps.places.SearchBox(input);
    //map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

    // Bias the SearchBox results towards current map's viewport.
    map.addListener('bounds_changed', function () {
        searchBox.setBounds(map.getBounds());
    });

    // Listen for the event fired when the user selects a prediction and retrieve
    // more details for that place.
    searchBox.addListener('places_changed', function () {
        var places = searchBox.getPlaces();
        if (places.length == 0) {
            var address2 = $('#FullAddress').val();
            geocoder.geocode({ 'address': address2 }, function (results, status) {
                if (status === google.maps.GeocoderStatus.OK) {

                    var pos = {
                        lat: results[0].geometry.location.lat(),
                        lng: results[0].geometry.location.lng()
                    };
                    map.setCenter(pos);
                    marker.setPosition(pos);
                    infowindow.open(map, marker);
                    $('#Lat').val(pos.lat);
                    $('#Lng').val(pos.lng);
                }
            });
            return;
        }

        // xoá marker đã tạo.
        //marker.setPosition(map.getCenter());

        // For each place, get the icon, name and location.
        var bounds = new google.maps.LatLngBounds();
        places.forEach(function (place) {
            if (!place.geometry) {
                var address2 = $('#FullAddress').val();
                geocoder.geocode({ 'address': address2 }, function (results, status) {
                    if (status === google.maps.GeocoderStatus.OK) {

                        var pos = {
                            lat: results[0].geometry.location.lat(),
                            lng: results[0].geometry.location.lng()
                        };
                        map.setCenter(pos);
                        marker.setPosition(pos);
                        infowindow.open(map, marker);
                        $('#Lat').val(pos.lat);
                        $('#Lng').val(pos.lng);
                    }
                });
                return;
            }
            var pos = {
                lat: place.geometry.location.lat(),
                lng: place.geometry.location.lng()
            }
            map.setCenter(place.geometry.location);
            marker.setPosition(place.geometry.location);
            marker.setVisible(true);
            $('#Lat').val(pos.lat);
            $('#Lng').val(pos.lng);
            if (place.geometry.viewport) {
                // Only geocodes have viewport.
                bounds.union(place.geometry.viewport);
            } else {
                bounds.extend(place.geometry.location);
            }

        });
        map.fitBounds(bounds);
    });
}

function handleLocationError(browserHasGeolocation, infoWindow, pos) {
    infoWindow.setPosition(pos);
    infoWindow.setContent(browserHasGeolocation ?
                          'Lỗi: Định vị vị trí thất bại.' :
                          'Lỗi: trình duyệt của bạn không hỗ trợ định vị vị trí.');
}

function InitGeolocation()
{
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 13
    });
    var marker = new google.maps.Marker({
        //label: 'T',
        map: map,
        draggable: true,
        icon: '/Content/Images/mapmarker.png'
    });
    google.maps.event.addListener(marker, 'dragend', function (event) {
        $('#Lat').val(this.getPosition().lat());
        $('#Lng').val(this.getPosition().lng());
    });
    var infowindow = new google.maps.InfoWindow({
        content: 'Đây có phải là vị trí của bạn.\nVui lòng di chuyển ô đánh dấu này nều không phải'
    });

    var geocoder = new google.maps.Geocoder();
    // sử dụng ô textbox để tìm địa chỉ trên google.
    var input = document.getElementById('FullAddress');
    var searchBox = new google.maps.places.SearchBox(input);
    //map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

    // Bias the SearchBox results towards current map's viewport.
    map.addListener('bounds_changed', function () {
        searchBox.setBounds(map.getBounds());
    });

    // Listen for the event fired when the user selects a prediction and retrieve
    // more details for that place.
    searchBox.addListener('places_changed', function () {
        var places = searchBox.getPlaces();
        if (places.length == 0) {
            var address2 = $('#FullAddress').val();
            geocoder.geocode({ 'address': address2 }, function (results, status) {
                if (status === google.maps.GeocoderStatus.OK) {

                    var pos = {
                        lat: results[0].geometry.location.lat(),
                        lng: results[0].geometry.location.lng()
                    };
                    map.setCenter(pos);
                    marker.setPosition(pos);
                    infowindow.open(map, marker);
                    $('#Lat').val(pos.lat);
                    $('#Lng').val(pos.lng);
                }
            });
            return;
        }

        // xoá marker đã tạo.
        //marker.setPosition(map.getCenter());

        // For each place, get the icon, name and location.
        var bounds = new google.maps.LatLngBounds();
        places.forEach(function (place) {
            if (!place.geometry) {
                var address2 = $('#FullAddress').val();
                geocoder.geocode({ 'address': address2 }, function (results, status) {
                    if (status === google.maps.GeocoderStatus.OK) {

                        var pos = {
                            lat: results[0].geometry.location.lat(),
                            lng: results[0].geometry.location.lng()
                        };
                        map.setCenter(pos);
                        marker.setPosition(pos);
                        infowindow.open(map, marker);
                        $('#Lat').val(pos.lat);
                        $('#Lng').val(pos.lng);
                    }
                });
                return;
            }
            var pos = {
                lat: place.geometry.location.lat(),
                lng: place.geometry.location.lng()
            }
            map.setCenter(place.geometry.location);
            marker.setPosition(place.geometry.location);
            marker.setVisible(true);
            $('#Lat').val(pos.lat);
            $('#Lng').val(pos.lng);
            if (place.geometry.viewport) {
                // Only geocodes have viewport.
                bounds.union(place.geometry.viewport);
            } else {
                bounds.extend(place.geometry.location);
            }

        });
        map.fitBounds(bounds);
    });

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            var pos = {
                lat: position.coords.latitude,
                lng: position.coords.longitude
            };
            map.setCenter(pos);
            marker.setPosition(pos);
            infowindow.open(map, marker);
            marker.setMap(map);
            $('#Lat').val(pos.lat);
            $('#Lng').val(pos.lng);
        }, function () {
            handleLocationError(true, infoWindow, map.getCenter());
        });
    } else {
        // Khi trình duyệt không hỗ trợ định vị vị trí
        handleLocationError(false, infoWindow, map.getCenter());
    }
}