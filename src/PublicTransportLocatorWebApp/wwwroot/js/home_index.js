﻿var ref_to_map;
var added_markers = [];

function update_markersOnMap() {
    $.ajax(
        {
            method: "GET",
            url: "transportLocations/getAllLocations"
        }
    ).done(
        function (data) {
            for (index in added_markers) {
                var var_marker = added_markers[index];
                var_marker.setMap(null);
            }
            added_markers = [];
            for (index in data) {
                var element = data[index];
                var var_location = new google.maps.LatLng(element.Latitude, element.Longitude);
                var var_marker = new google.maps.Marker({
                    position: var_location,
                    map: ref_to_map,
                    title: "RouteId: " + element.TransportRouteID
                });
                var_marker.setMap(ref_to_map);
                added_markers.push(var_marker);
            }
        });
}

function drawRouteOnMap() {
    $.ajax(
        {
            method: "GET",
            url: "RoutePoints/GetSortedPointsByRouteId",
            data: "routeId=1"
        }
    ).done(
        function (data) {

            var route = [];

            for (var i in data) {
                var point = data[i];
                route.push({ lat: point.Latitude, lng: point.Longitude });
            }

            var routePolyLine = new google.maps.Polyline({
                path: route,
                geodesic: true,
                strokeColor: '#FF0000',
                strokeOpacity: 1.0,
                strokeWeight: 2
            });

            routePolyLine.setMap(ref_to_map);

        });
}

function updateLocationsForDemo() {
    $.ajax(
        {
            method: "POST",
            url: "transportLocations/UpdateLocationsForDemo"
        }
    );
}

function autoUpdateMarkers() {
    update_markersOnMap();
    updateLocationsForDemo();
    setTimeout(autoUpdateMarkers, 1000);
}

function init_map() {

    var var_location = new google.maps.LatLng(46.468032, 30.741727);

    var var_mapoptions = {
        center: var_location,
        zoom: 12
    };

    var var_map = new google.maps.Map(document.getElementById("map-container"),
        var_mapoptions);

    ref_to_map = var_map;

    update_markersOnMap();

    drawRouteOnMap();

    setTimeout(autoUpdateMarkers, 1000);

}

google.maps.event.addDomListener(window, 'load', init_map);