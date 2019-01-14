/**=========================================================
 * Module: UIGridController
  =========================================================*/
App.controller('EventPostUIGridController', ['$scope', 'uiGridConstants', 'uigridUtils', '$http', '$state', '$stateParams', function ($scope, uiGridConstants, uigridUtils, $http, $state, $stateParams) {

    var data = [];
    var filtersObject = [];
    var headerTemplate = uigridUtils.headerTemplateVal();
    var paginationOptions = uigridUtils.paginationOptionsVal();
    var editBtnEventPost = uigridUtils.editBtn('app.mainEventPost.editEventPost({id:row.entity.Id})');
    var delBtn = uigridUtils.delBtn();
    var watchBtn = uigridUtils.watchBtn();
    var colDef = [
         { name: 'Id', field: 'Id', enableCellEdit: false, width: 50 },
         { name: 'customForm.title', field: 'Title', headerCellTemplate: headerTemplate },
         { name: 'customForm.commentCount', field: 'CommentCount', headerCellTemplate: headerTemplate },
         { name: 'customForm.startDateUtc', field: 'StartDateUtc', headerCellTemplate: headerTemplate, cellFilter: 'date:\'dd/MM/yyyy HH:MM\'' },
         { name: 'customForm.endDateUtc', field: 'EndDateUtc', headerCellTemplate: headerTemplate },
         editBtnEventPost,
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
            var url = '/EventPostAdmin/EditEventPost';
            uigridUtils.cellEdit(url, rowEntity, colDef, newValue, oldValue);
        });
    };

    $scope.Delete = function (row) {
        var url = '/EventPostAdmin/DeleteEventPost';
        uigridUtils.delRow(url, row)
        var index = $scope.gridOptionsComplex.data.indexOf(row.entity);
        $scope.gridOptionsComplex.data.splice(index, 1);
    };

    $scope.editEventPost = function () {
        $state.go('app.editEventPost');
    };

    var getPage = function (FilterValue) {
        var url = '/EventPostAdmin/GetFilteredEventPosts';
        uigridUtils.getPage(url, filtersObject, paginationOptions, $scope);
    };

    getPage();

}]);

