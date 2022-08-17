angular.module("umbraco").controller('OperatorsPluginController', function ($scope) {

    console.log("Hi there");

    $scope.getMyMessage = function () {
        $scope.message = "Have a nice day";
    }
});