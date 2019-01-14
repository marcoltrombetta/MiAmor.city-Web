/**=========================================================
 * Module: UIGridController
  =========================================================*/
App.controller('EventCommentUIGridController', ['$scope', 'uiGridConstants', 'uigridUtils', '$http', '$state', '$stateParams', function ($scope, uiGridConstants, uigridUtils, $http, $state, $stateParams) {

    var data = [];
    var filtersObject = [];
    var headerTemplate = uigridUtils.headerTemplateVal();
    var paginationOptions = uigridUtils.paginationOptionsVal();
    var editBtnEventComment = uigridUtils.editBtn('app.mainEventComment.editEventComment({id:row.entity.Id})');
    var delBtn = uigridUtils.delBtn();
    var watchBtn = uigridUtils.watchBtn();
    var colDef = [
         { name: 'Id', field: 'Id', enableCellEdit: false, width: 50 },
         { name: 'customForm.customer', field: 'CustomerId', headerCellTemplate: headerTemplate },
         { name: 'customForm.commentText', field: 'CommentText', headerCellTemplate: headerTemplate },
         { name: 'customForm.eventPost', field: 'EventPostId', headerCellTemplate: headerTemplate },
         { name: 'customForm.createdOnUtc', field: 'CreatedOnUtc', headerCellTemplate: headerTemplate },
         editBtnEventComment,
         delBtn,
         watchBtn
    ];
    $scope.gridOptionsComplex = uigridUtils.gridOptionsComplexVal(colDef, data);
    $scope.gridOptionsComplex.columnDefs = colDef;
    $scope.gridOptionsComplex.onRegisterApi = function (gridApi) {
        $scope.gridApi = gridApi;
        $scope.gridApi.core.on.filterChanged($scope, function () {
            filtersObject = [];
            var grid = this.grid;
            filtersObject = uigridUtils.getFilterChangedVal(grid, colDef.length - 2);
            paginationOptions.PageNumber = 1;
            getPage(filtersObject);
        });
        gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
            paginationOptions.PageNumber = newPage;
            paginationOptions.PageSize = pageSize;
            getPage(filtersObject);
        });
        ////EDIT
        gridApi.edit.on.afterCellEdit($scope, function (rowEntity, colDef, newValue, oldValue) {
            var url = '/EventCommentAdmin/EditEventComment';
            uigridUtils.cellEdit(url, rowEntity, colDef, newValue, oldValue);
        });
    };

    $scope.Delete = function (row) {
        var url = '/EventCommentAdmin/DeleteEventComment';
        uigridUtils.delRow(url, row)
        var index = $scope.gridOptionsComplex.data.indexOf(row.entity);
        $scope.gridOptionsComplex.data.splice(index, 1);
    };

    $scope.editEventComment = function () {
        $state.go('app.editEventComment');
    };

    var getPage = function (FilterValue) {
        var url = '/EventCommentAdmin/GetFilteredEventComments';
        uigridUtils.getPage(url, filtersObject, paginationOptions, $scope);
    };

    getPage();

}]);
