
App.controller('newCampaign', ['$scope', '$http', '$state', '$stateParams', function ($scope, $http, $state, $stateParams) {
    $scope.campaign = {};
    $scope.myid = $stateParams.id;

    $scope.selected = undefined;   
    // Any function returning a promise object can be used to load values asynchronously
    $scope.getVendorByName = function (val) {
        return $http.get('/VendorAdmin/GetVendorAutoComplete', {
            params: {
                PartialName: val
            }
        }).then(function (response) {
            return response.data;
        });
    };

    $scope.setVendor = function (vendorId)
    {
        $scope.campaign.vendorId = vendorId;
    }

    if ($scope.myid === null || $scope.myid === undefined) {
    } else {

        $http({
            url: '/CampaignAdmin/GetCampaignById',
            method: "POST",
            data: JSON.stringify({ Id: $scope.myid }),
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.campaign = data;
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
                    url: '/CampaignAdmin/InsertNewCampaign',
                    method: "POST",
                    data: JSON.stringify({ NewCampaign: $scope.campaign }),
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $state.go('app.mainCampaign');
                }).error(function (data, status, headers, config) {
                    var O = 0;
                });
            } else {
                $http({
                    url: '/CampaignAdmin/SaveChanges',
                    method: "POST",
                    data: JSON.stringify({ CampaignEdited: $scope.campaign }),
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $state.go('app.mainCampaign.gridCampaign'); //to do (you have to refresh the grid to see the changes)
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