App.controller('newProductCategory', ['$scope', '$http', '$state', '$stateParams', function ($scope, $http, $state, $stateParams) {
    $scope.productCategory = {};
    $scope.myid = $stateParams.id;

    $scope.getParentCategoryByName = function (val) {
        return $http.get('/ProductCategoryAdmin/GetParentCategoryAutoComplete', {
            params: {
                PartialName: val
            }
        }).then(function (response) {
            return response.data;
        });
    };

    $scope.setParentCategory = function (parentCategoryId) {
        $scope.productCategory.parentCategoryId = parentCategoryId;
    }



    if ($scope.myid === null || $scope.myid === undefined) {
    } else {

        $http({
            url: '/ProductCategoryAdmin/GetProductCategoryById',
            method: "POST",
            data: JSON.stringify({ Id: $scope.myid }),
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.productCategory = data;
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
                    url: '/ProductCategoryAdmin/InsertNewProductCategory',
                    method: "POST",
                    data: JSON.stringify({ NewProductCategory: $scope.productCategory }),
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $state.go('app.mainProductCategory');
                }).error(function (data, status, headers, config) {
                    var O = 0;
                });
            } else {
                $http({
                    url: '/ProductCategoryAdmin/SaveChanges',
                    method: "POST",
                    data: JSON.stringify({ ProductCategoryEdited: $scope.productCategory }),
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $state.go('app.mainProductCategory.gridProductCategory'); //to do (you have to refresh the grid to see the changes)
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