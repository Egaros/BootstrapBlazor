﻿(function ($) {
    $.extend({
        bb_getLocation: function (el, result, getPosition=true) {
            var $el = $(el);
            console.log('start getLocation');
            var totalDistance = 0.0;
            var lastLat;
            var lastLong;

            if (getPosition) getCurrentPosition(); else watchPosition();

            Number.prototype.toRadians = function () {
                return this * Math.PI / 180;
            }


            function distance(latitude1, longitude1, latitude2, longitude2) {
                // R is the radius of the earth in kilometers
                var R = 6371;

                var deltaLatitude = (latitude2 - latitude1).toRadians();
                var deltaLongitude = (longitude2 - longitude1).toRadians();
                latitude1 = latitude1.toRadians(), latitude2 = latitude2.toRadians();

                var a = Math.sin(deltaLatitude / 2) *
                    Math.sin(deltaLatitude / 2) +
                    Math.cos(latitude1) *
                    Math.cos(latitude2) *
                    Math.sin(deltaLongitude / 2) *
                    Math.sin(deltaLongitude / 2);

                var c = 2 * Math.atan2(Math.sqrt(a),
                    Math.sqrt(1 - a));
                var d = R * c;
                return d;
            }


            function updateStatus(message) {
                document.getElementById("status").innerHTML = message;
            }

            function watchPosition() {
                if (navigator.geolocation) {
                    updateStatus("HTML5 Geolocation is supported in your browser.");
                    navigator.geolocation.watchPosition(updateLocation,
                        handleLocationError,
                        { maximumAge: 20000 });
                }
            }

            function getCurrentPosition() {
                if (navigator.geolocation) {
                    updateStatus("HTML5 Geolocation is supported in your browser.");
                    navigator.geolocation.getCurrentPosition(updateLocation,
                        handleLocationError);
                }
            }

            function updateLocation(position) {
                var latitude = position.coords.latitude;
                var longitude = position.coords.longitude;
                var accuracy = position.coords.accuracy;
                var timestamp = position.timestamp;

                document.getElementById("latitude").innerHTML = latitude;
                document.getElementById("longitude").innerHTML = longitude;
                document.getElementById("accuracy").innerHTML = accuracy;
                document.getElementById("timestamp").innerHTML = timestamp;

                // sanity test... don't calculate distance if accuracy
                // value too large
                if (accuracy >= 500) {
                    updateStatus("Need more accurate values to calculate distance.");
                    return;
                }

                // calculate distance

                if ((lastLat != null) && (lastLong != null)) {
                    var currentDistance = distance(latitude, longitude, lastLat, lastLong);
                    document.getElementById("currDist").innerHTML =
                        "Current distance traveled: " + currentDistance.toFixed(4) + " km";

                    totalDistance += currentDistance;

                    document.getElementById("totalDist").innerHTML =
                        "Total distance traveled: " + currentDistance.toFixed(4) + " km";
                }


                lastLat = latitude;
                lastLong = longitude;

                updateStatus("Location successfully updated.");
            }

            function handleLocationError(error) {
                switch (error.code) {
                    case 0:
                        updateStatus("There was an error while retrieving your location: " + error.message);
                        break;
                    case 1:
                        updateStatus("The user prevented this page from retrieving a location.");
                        break;
                    case 2:
                        updateStatus("The browser was unable to determine your location: " + error.message);
                        break;
                    case 3:
                        updateStatus("The browser timed out before retrieving the location.");
                        break;
                }
            }
         }
     });
})(jQuery);
