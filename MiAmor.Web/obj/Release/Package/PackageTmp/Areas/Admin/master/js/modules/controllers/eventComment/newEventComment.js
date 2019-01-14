App.controller('newEventComment', ['$scope', '$http', '$state', '$stateParams', function ($scope, $http, $state, $stateParams) {
    $scope.eventComment = {};
    $scope.myid = $stateParams.id;

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
        $scope.eventComment.customerId = customerId;
    }


    $scope.getEventPostByName = function (val) {
        return $http.get('/EventPostAdmin/GetEventPostAutoComplete', {
            params: {
                PartialName: val
            }
        }).then(function (response) {
            return response.data;
        });
    };

    $scope.setEventPost = function (eventPostId) {
        $scope.eventComment.eventPostId = eventPostId;
    }


    if ($scope.myid === null || $scope.myid === undefined) {
    } else {

        $http({
            url: '/EventCommentAdmin/GetEventCommentById',
            method: "POST",
            data: JSON.stringify({ Id: $scope.myid }),
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.eventComment = data;
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
                    url: '/EventCommentAdmin/InsertNewEventComment',
                    method: "POST",
                    data: JSON.stringify({ NewEventComment: $scope.eventComment }),
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $state.go('app.mainEventComment');
                }).error(function (data, status, headers, config) {
                    var O = 0;
                });
            } else {
                $http({
                    url: '/EventCommentAdmin/SaveChanges',
                    method: "POST",
                    data: JSON.stringify({ EventCommentEdited: $scope.eventComment }),
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $state.go('app.mainEventComment.gridEventComment'); //to do (you have to refresh the grid to see the changes)
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