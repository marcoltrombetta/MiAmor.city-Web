App.controller('newBlogComment', ['$scope', '$http', '$state', '$stateParams', function ($scope, $http, $state, $stateParams) {
    $scope.blogComment = {};
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
        $scope.blogComment.customerId = customerId;
    }


    $scope.getBlogPostByName = function (val) {
        return $http.get('/BlogPostAdmin/GetBlogPostAutoComplete', {
            params: {
                PartialName: val
            }
        }).then(function (response) {
            return response.data;
        });
    };

    $scope.setBlogPost = function (blogPostId) {
        $scope.blogComment.blogPostId = blogPostId;
    }


    if ($scope.myid === null || $scope.myid === undefined) {
    } else {

        $http({
            url: '/BlogCommentAdmin/GetBlogCommentById',
            method: "POST",
            data: JSON.stringify({ Id: $scope.myid }),
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.blogComment = data;
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
                    url: '/BlogCommentAdmin/InsertNewBlogComment',
                    method: "POST",
                    data: JSON.stringify({ NewBlogComment: $scope.blogComment }),
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $state.go('app.mainBlogComment');
                }).error(function (data, status, headers, config) {
                    var O = 0;
                });
            } else {
                $http({
                    url: '/BlogCommentAdmin/SaveChanges',
                    method: "POST",
                    data: JSON.stringify({ BlogCommentEdited: $scope.blogComment }),
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $state.go('app.mainBlogComment.gridBlogComment'); //to do (you have to refresh the grid to see the changes)
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