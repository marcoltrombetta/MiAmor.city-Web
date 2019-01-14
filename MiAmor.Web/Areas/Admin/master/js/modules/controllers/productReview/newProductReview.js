App.controller('newProductReview', ['$scope', '$http', '$state', '$stateParams', function ($scope, $http, $state, $stateParams) {
    $scope.productReview = {};
    $scope.customers = {};
    $scope.products = {};
    $scope.myid = $stateParams.id;

    $http({
        url: '/CustomerAdmin/GetAllCustomers',
        method: "POST",
        data: JSON.stringify(),
        headers: { 'Content-Type': 'application/json' }
    }).success(function (data) {
        $scope.customers = data;
    }).
        error(function (data, status, headers, config) {
            var O = 0;
    });

    $http({
        url: '/ProductAdmin/GetAllProducts',
        method: "POST",
        data: JSON.stringify(),
        headers: { 'Content-Type': 'application/json' }
    }).success(function (data) {
        $scope.products = data;
    }).
       error(function (data, status, headers, config) {
           var O = 0;
       });

    if ($scope.myid === null || $scope.myid === undefined) {
    } else {

        $http({
            url: '/ProductReviewAdmin/GetProductReviewById',
            method: "POST",
            data: JSON.stringify({ Id: $scope.myid }),
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.productReview = data;
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
                    url: '/ProductReviewAdmin/InsertNewProductReview',
                    method: "POST",
                    data: JSON.stringify({ NewProductReview: $scope.productReview }),
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $state.go('app.mainProductReview');
                }).error(function (data, status, headers, config) {
                    var O = 0;
                });
            } else {
                $http({
                    url: '/ProductReviewAdmin/SaveChanges',
                    method: "POST",
                    data: JSON.stringify({ ProductReviewEdited: $scope.productReview }),
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $state.go('app.mainProductReview.gridProductReview'); //to do (you have to refresh the grid to see the changes)
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