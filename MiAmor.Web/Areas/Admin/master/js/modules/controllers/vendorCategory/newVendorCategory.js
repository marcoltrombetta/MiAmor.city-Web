App.controller('newVendorCategory', ['$scope', '$http', '$state', '$stateParams', function ($scope, $http, $state, $stateParams) {
    $scope.vendorCategory = {};
    $scope.myid = $stateParams.id;

    if ($scope.myid === null || $scope.myid === undefined) {
    } else {

        $http({
            url: '/VendorCategoryAdmin/GetVendorCategoryById',
            method: "POST",
            data: JSON.stringify({ Id: $scope.myid }),
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.vendorCategory = data;
        }).
        error(function (data, status, headers, config) {
            var O = 0;
        });
    }

    $scope.notBlackListed = function (value) {
        var blacklist = ['some@mail.com', 'another@email.com'];
        return blacklist.indexOf(value) === -1;
    };

    $scope.words = function (value) {
        return value && value.split(' ').length;
    };

    $scope.submitted = false;
    $scope.validateInput = function (name, type) {
        var input = $scope.formValidate[name];
        return (input.$dirty || $scope.submitted) && input.$error[type];
    };

    // Submit form
    $scope.submitForm = function () {
        $scope.submitted = true;
        if ($scope.formValidate.$valid || !$scope.formValidate.$valid) { //todo change the if
            console.log('Submitted!!');

            if ($scope.myid === undefined || $scope.myid === null) {
                $http({
                    url: '/VendorCategoryAdmin/InsertNewVendorCategory',
                    method: "POST",
                    data: JSON.stringify({ NewVendorCategory: $scope.vendorCategory }),
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $state.go('app.mainVendorCategory');
                }).error(function (data, status, headers, config) {
                    var O = 0;
                });
            } else {
                $http({
                    url: '/VendorCategoryAdmin/SaveChanges',
                    method: "POST",
                    data: JSON.stringify({ VendorCategoryEdited: $scope.vendorCategory }),
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $state.go('app.mainVendorCategory.gridVendorCategory'); //to do (you have to refresh the grid to see the changes)
                }).error(function (data, status, headers, config) {
                    var O = 0;
                });
            }
        } else {
            console.log('Not valid!!');
            return false;
        }
    };

}]);