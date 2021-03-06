﻿/**=========================================================
 * Module: UIGridController
  =========================================================*/
App.controller('VendorCategoryCountUIGridController', ['$scope', 'uiGridConstants', 'uigridUtils', '$http', '$state', '$stateParams', function ($scope, uiGridConstants, uigridUtils, $http, $state, $stateParams) {

    var data = [];
    var filtersObject = [];
    var headerTemplate = uigridUtils.headerTemplateVal();
    var paginationOptions = uigridUtils.paginationOptionsVal();
    var editBtnVendorCategoryCount = uigridUtils.editBtn('app.mainVendorCategoryCount.editVendorCategoryCount({id:row.entity.Id})');
    var delBtn = uigridUtils.delBtn();
    var watchBtn = uigridUtils.watchBtn();
    var colDef = [
         { name: 'Id', field: 'Id', enableCellEdit: false, width: 50 },
         { name: 'customForm.name', field: 'name', headerCellTemplate: headerTemplate },
         { name: 'customForm.vendorCategory', field: 'vendorCategoryId', headerCellTemplate: headerTemplate },
         { name: 'customForm.countVendors', field: 'countVendors', headerCellTemplate: headerTemplate },
         { name: 'customForm.cssClass', field: 'cssClass', headerCellTemplate: headerTemplate },
         { name: 'customForm.displayOrder', field: 'displayOrder', headerCellTemplate: headerTemplate },       
         editBtnVendorCategoryCount,
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
            var url = '/VendorCategoryCountAdmin/EditVendorCategoryCount';
            uigridUtils.cellEdit(url, rowEntity, colDef, newValue, oldValue);
        });
    };

    $scope.Delete = function (row) {
        var url = '/VendorCategoryCountAdmin/DeleteVendorCategoryCount';
        uigridUtils.delRow(url, row)
        var index = $scope.gridOptionsComplex.data.indexOf(row.entity);
        $scope.gridOptionsComplex.data.splice(index, 1);
    };

    $scope.editVendorCategoryCount = function () {
        $state.go('app.editVendorCategoryCount');
    };

    var getPage = function (FilterValue) {
        var url = '/VendorCategoryCountAdmin/GetFilteredVendorCategoryCounts';
        uigridUtils.getPage(url, filtersObject, paginationOptions, $scope);
    };

    getPage();

}]);
