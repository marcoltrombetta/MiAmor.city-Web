App.controller('newVendor', ['$scope', '$http', '$state', '$stateParams', function ($scope, $http, $state, $stateParams) {
    $scope.vendor = {};
    $scope.cities = {};
    $scope.neighbourhoods = {};
    $scope.vendor.VendorAddressCrudModel = new Array();
    $scope.myid = $stateParams.id;

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
        $scope.vendor.cityId = cityId;        
    }

    $scope.getNeighbourhoodByName = function (val) {
        return $http.get('/AdminNeighbourhood/GetNeighbourhoodAutoComplete', {
            params: {
                PartialName: val,
                CityId: $scope.vendor.cityId
            }
        }).then(function (response) {
            return response.data;
        });
    };

    $scope.setNeighbourhood = function (neighbourhoodId) {
        $scope.vendor.neighbourhoodId = neighbourhoodId;
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

        
    //neigh
    $scope.getNeighbourhoodsByCity = function () {
        $http({
            url: '/AdminNeighbourhood/GetNeighbourhoodsByCityId',
            method: "POST",
            data: JSON.stringify({ CityId: $scope.vendor.cityId }),
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.neighbourhoods = data;
        }).
       error(function (data, status, headers, config) {
           var O = 0;
       });
    };
    //

       


    if ($scope.myid === null || $scope.myid === undefined) {       
    } else {

        $http({
            url: '/VendorAdmin/GetVendorDetailsById',
            method: "POST",
            data: JSON.stringify({ Id: $scope.myid }),
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.vendor = data;
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
                $scope.vendor.VendorAddressCrudModel[0].countryId = 1;
                $http({
                    url: '/VendorAdmin/InsertNewVendor',
                    method: "POST",
                    data: JSON.stringify({ NewVendor: $scope.vendor }),
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $state.go('app.mainVendor');
                }).error(function (data, status, headers, config) {
                    var O = 0;
                });
            } else {
                $http({
                    url: '/VendorAdmin/SaveChanges',
                    method: "POST",
                    data: JSON.stringify({ VendorEdited: $scope.vendor, VendorAddressEdited: $scope.vendorAddress }),
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $state.go('app.mainVendor.gridVendor'); //to do (you have to refresh the grid to see the changes)
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