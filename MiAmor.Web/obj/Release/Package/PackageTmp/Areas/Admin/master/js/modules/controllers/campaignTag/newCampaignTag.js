
App.controller('newCampaignTag', ['$scope', '$http', '$state', '$stateParams', function ($scope, $http, $state, $stateParams) {
    $scope.campaignTag = {};
    $scope.myid = $stateParams.id;

    if ($scope.myid === null || $scope.myid === undefined) {
    } else {

        $http({
            url: '/CampaignTagAdmin/GetCampaignTagById',
            method: "POST",
            data: JSON.stringify({ Id: $scope.myid }),
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.campaignTag = data;
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
                    url: '/CampaignTagAdmin/InsertNewCampaignTag',
                    method: "POST",
                    data: JSON.stringify({ NewCampaignTag: $scope.campaignTag }),
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $state.go('app.mainCampaignTag');
                }).error(function (data, status, headers, config) {
                    var O = 0;
                });
            } else {
                $http({
                    url: '/CampaignTagAdmin/SaveChanges',
                    method: "POST",
                    data: JSON.stringify({ CampaignTagEdited: $scope.campaignTag }),
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $state.go('app.mainCampaignTag.gridCampaignTag'); //to do (you have to refresh the grid to see the changes)
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