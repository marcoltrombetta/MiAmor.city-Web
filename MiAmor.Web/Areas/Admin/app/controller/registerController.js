angular.module('App').controller('registerController', ['$scope', '$http', function ($scope, $http) {

    function setLocalStorage(data) {
        try {
            localStorage["sessionStorageBackup"] = data.Token;
            alert("ok");
            return true;
        } catch (e) {
            alert(e.message);
            return false;
        }
    }

    $scope.aSubmitRegisterClick = function () {
        $('#aSubmitRegister').attr('disabled', true);
        alert('asdf')
        $('#imgLoad').show();

        var serializedForm = {
            FirstName: $scope.register.FirstName,
            LastName: $scope.register.LastName,
            Email: $scope.register.Email,
            Password: $scope.register.Password
        };

        $http({
            url: '/Customer/RegisterCustomerApp',
            method: "POST",
            data: serializedForm,
            header: { "Content-Type": "application/json" }
        }).success(function (data) {
            setLocalStorage(data);
        }).error(function (data) {
            alert(data.message);
        })

        return false;
    }
}]);