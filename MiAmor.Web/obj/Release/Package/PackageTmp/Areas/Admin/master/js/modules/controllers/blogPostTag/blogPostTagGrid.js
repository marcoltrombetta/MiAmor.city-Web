﻿/**=========================================================
 * Module: UIGridController
  =========================================================*/
App.controller('BlogPostTagUIGridController', ['$scope', 'uiGridConstants', 'uigridUtils', '$http', '$state', '$stateParams', function ($scope, uiGridConstants, uigridUtils, $http, $state, $stateParams) {

    var data = [];
    var filtersObject = [];
    var headerTemplate = uigridUtils.headerTemplateVal();
    var paginationOptions = uigridUtils.paginationOptionsVal();
    var editBtnBlogPostTag = uigridUtils.editBtn('app.mainBlogPostTag.editBlogPostTag({id:row.entity.Id})');
    var delBtn = uigridUtils.delBtn();
    var watchBtn = uigridUtils.watchBtn();
    var colDef = [
         { name: 'Id', field: 'Id', enableCellEdit: false, width: 50 },
         { name: 'customForm.name', field: 'Name', headerCellTemplate: headerTemplate },
         { name: 'customForm.blogPostCount', field: 'BlogPostCount', headerCellTemplate: headerTemplate },
         { name: 'customForm.cssName', field: 'CssName', headerCellTemplate: headerTemplate },
         editBtnBlogPostTag,
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
            var url = '/BlogPostTagAdmin/EditBlogPostTag';
            uigridUtils.cellEdit(url, rowEntity, colDef, newValue, oldValue);
        });
    };

    $scope.Delete = function (row) {
        var url = '/BlogPostTagAdmin/DeleteBlogPostTag';
        uigridUtils.delRow(url, row)
        var index = $scope.gridOptionsComplex.data.indexOf(row.entity);
        $scope.gridOptionsComplex.data.splice(index, 1);
    };

    $scope.editBlogPostTag = function () {
        $state.go('app.editBlogPostTag');
    };

    var getPage = function (FilterValue) {
        var url = '/BlogPostTagAdmin/GetFilteredBlogPostTags';
        uigridUtils.getPage(url, filtersObject, paginationOptions, $scope);
    };

    getPage();

}]);

