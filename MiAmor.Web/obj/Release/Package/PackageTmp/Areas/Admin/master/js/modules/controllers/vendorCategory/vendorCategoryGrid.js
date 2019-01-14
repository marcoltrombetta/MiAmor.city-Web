/**=========================================================
 * Module: UIGridController
  =========================================================*/
App.controller('VendorCategoryUIGridController', ['$scope', 'uiGridConstants', 'uigridUtils', '$http', '$state', '$stateParams', function ($scope, uiGridConstants, uigridUtils, $http, $state, $stateParams) {

    var data = [];
    var filtersObject = [];
    var headerTemplate = uigridUtils.headerTemplateVal();
    var paginationOptions = uigridUtils.paginationOptionsVal();
    var editBtnVendorCategory = uigridUtils.editBtn('app.mainVendorCategory.editVendorCategory({id:row.entity.Id})');
    var delBtn = uigridUtils.delBtn();
    var watchBtn = uigridUtils.watchBtn();
    var colDef = [
         { name: 'Id', field: 'Id', enableCellEdit: false, width: 50 },
          { name: 'customForm.name', field: 'name', headerCellTemplate: headerTemplate },
          {
              name: 'published', cellTemplate: '<p ng-switch="row.entity[col.field]"><span ng-switch-when="true">Yes</span><span ng-switch-when="false">No</span></p>',
              filter: {
                  term: '1',
                  type: uiGridConstants.filter.SELECT,
                  selectOptions: [{ value: '1', label: 'Yes' }, { value: '2', label: 'No' }]
              }
          },
           { name: 'customForm.cssClass', field: 'cssClass', headerCellTemplate: headerTemplate },
           { name: 'customForm.displayOrder', field: 'displayOrder', headerCellTemplate: headerTemplate },
          {
              name: 'showOnHomePage', cellTemplate: '<p ng-switch="row.entity[col.field]"><span ng-switch-when="true">Yes</span><span ng-switch-when="false">No</span></p>',
              filter: {
                  term: '1',
                  type: uiGridConstants.filter.SELECT,
                  selectOptions: [{ value: '1', label: 'Yes' }, { value: '2', label: 'No' }]
              }
          },        
         editBtnVendorCategory,
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
            var url = '/VendorCategoryAdmin/EditVendorCategory';
            uigridUtils.cellEdit(url, rowEntity, colDef, newValue, oldValue);
        });
    };

    $scope.Delete = function (row) {
        var url = '/VendorCategoryAdmin/DeleteVendorCategory';
        uigridUtils.delRow(url, row)
        var index = $scope.gridOptionsComplex.data.indexOf(row.entity);
        $scope.gridOptionsComplex.data.splice(index, 1);
    };

    $scope.editVendorCategory = function () {
        $state.go('app.editVendorCategory');
    };

    var getPage = function (FilterValue) {
        var url = '/VendorCategoryAdmin/GetFilteredVendorCategories';
        uigridUtils.getPage(url, filtersObject, paginationOptions, $scope);
    };

    getPage();

}]);

