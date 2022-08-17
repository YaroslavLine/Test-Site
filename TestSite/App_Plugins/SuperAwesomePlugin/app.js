angular.module("umbraco").controller('SuperAwesomePluginController', function ($scope) {

    console.log("Hi there");

    $scope.getMyMessage = function () {
        $scope.message = "Have a nice day";
    }
});