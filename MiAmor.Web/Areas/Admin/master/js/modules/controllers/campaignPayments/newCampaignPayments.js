
App.controller('newCampaignPayments', ['$scope', '$http', '$state', '$stateParams', function ($scope, $http, $state, $stateParams) {
    $scope.campaignPayments = {};
    $scope.myid = $stateParams.id;
    $scope.statuss = {};

    $scope.selected = undefined;
    // Any function returning a promise object can be used to load values asynchronously
    $scope.getCustomerByName = function (val) {
        return $http.get('/CustomerAdmin/GetCustomerAutoComplete', {
            params: {
                PartialName: val
            }
        }).then(function (response) {
            return response.data;
        });
    };

    $scope.setCustomer = function (customerId) {
        $scope.campaignPayments.customerId = customerId;
    }


    $scope.getCampaignByName = function (val) {
        return $http.get('/CampaignAdmin/GetCampaignAutoComplete', {
            params: {
                PartialName: val
            }
        }).then(function (response) {
            return response.data;
        });
    };

    $scope.setCampaign = function (campaignId) {
        $scope.campaignPayments.campaignId = campaignId;
    }


    $http({
        url: '/CampaignStatusAdmin/GetAllStatus',
        method: "POST",
        data: JSON.stringify(),
        headers: { 'Content-Type': 'application/json' }
    }).success(function (data) {
        $scope.statuss = data;
    }).
     error(function (data, status, headers, config) {
         var O = 0;
     });


    if ($scope.myid === null || $scope.myid === undefined) {
    } else {

        $http({
            url: '/CampaignPaymentsAdmin/GetCampaignPaymentsById',
            method: "POST",
            data: JSON.stringify({ Id: $scope.myid }),
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.campaignPayments = data;
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
                    url: '/CampaignPaymentsAdmin/InsertNewCampaignPayments',
                    method: "POST",
                    data: JSON.stringify({ NewCampaignPayments: $scope.campaignPayments }),
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $state.go('app.mainCampaignPayments');
                }).error(function (data, status, headers, config) {
                    var O = 0;
                });
            } else {
                $http({
                    url: '/CampaignPaymentsAdmin/SaveChanges',
                    method: "POST",
                    data: JSON.stringify({ CampaignPaymentsEdited: $scope.campaignPayments }),
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $state.go('app.mainCampaignPayments.gridCampaignPayments'); //to do (you have to refresh the grid to see the changes)
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