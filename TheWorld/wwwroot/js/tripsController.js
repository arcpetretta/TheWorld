﻿// tripsController.js
(function () {
    "use strict";

    // gets existing module
    angular.module("app-trips")
        .controller("tripsController", tripsController);

    function tripsController() {

        var vm = this;

        vm.trips = [{
            name: "US Trip",
            created: new Date()
        }, {
                name: "WorldTrip",
                created: new Date()
        }];

        vm.newTrip = {};

        vm.addTrip = function () {
            vm.trips.push({ name: vm.newTrip.name, created: new Date() });
            vm.newTrip = {};
        }
    }

})();