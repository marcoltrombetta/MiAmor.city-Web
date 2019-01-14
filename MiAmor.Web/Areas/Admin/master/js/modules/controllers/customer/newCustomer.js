App.controller('newCustomer', ['$scope', '$http', '$state', '$stateParams', function ($scope, $http, $state, $stateParams) {
    $scope.customer = {};
    $scope.cities = {};
    $scope.customerTypes = {};
    $scope.myid = $stateParams.id;
    $scope.customerToEdit = 'kaka';

    $scope.selected = undefined;
    // Any function returning a promise object can be used to load values asynchronously
    $scope.getCityByName = function (val) {
        return $http.get('/AdminCity/GetCityAutoComplete', {
            params: {
                PartialName: val
            }
        }).then(function (response) {
            return response.data;
        });
    };

    $scope.setCity = function (cityId) {
        $scope.customer.cityId = cityId;
    }

    $http({
        url: '/AdminCity/GetCities',
        method: "POST",
        data: JSON.stringify(),
        headers: { 'Content-Type': 'application/json' }
    }).success(function (data) {
        $scope.cities = data;
    }).
      error(function (data, status, headers, config) {
          var O = 0;
      });

    $http({
        url: '/CustomerAdmin/GetCustomerTypes',
        method: "POST",
        data: JSON.stringify(),
        headers: { 'Content-Type': 'application/json' }
    }).success(function (data) {
        $scope.customerTypes = data;
    }).
      error(function (data, status, headers, config) {
          var O = 0;
      });


    if ($scope.myid === null || $scope.myid === undefined) {
        alert('no id');
    }else{

        $http({
            url: '/CustomerAdmin/GetCustomerById',
            method: "POST",
            data: JSON.stringify({ Id: $scope.myid }),
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.customer = data;
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
                //database
                $http({
                    url: '/CustomerAdmin/InsertNewCustomer',
                    method: "POST",
                    data: JSON.stringify({ NewCustomer: $scope.customer }),
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $state.go('app.mainCustomer');
                }).error(function (data, status, headers, config) {
                    var O = 0;
                });
                //
            } else {
                //database
                $http({
                    url: '/CustomerAdmin/SaveChanges',
                    method: "POST",
                    data: JSON.stringify({ CustomerEdited: $scope.customer }),
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $state.go('app.mainCustomer.gridCustomer'); //to do (you have to refresh the grid to see the changes)
                }).error(function (data, status, headers, config) {
                    var O = 0;
                });
                //
            }
        } else {
            console.log('Not valid!!');
            return false;
        }
    };
  
}]);