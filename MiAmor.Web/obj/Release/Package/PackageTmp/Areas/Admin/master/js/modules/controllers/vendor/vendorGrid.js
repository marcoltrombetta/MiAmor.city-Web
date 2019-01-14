/**=========================================================
 * Module: UIGridController
  =========================================================*/
App.controller('VendorUIGridController', ['$scope', 'uiGridConstants', 'uigridUtils', '$http', '$state', '$stateParams', function ($scope, uiGridConstants, uigridUtils, $http, $state, $stateParams) {

    var data = [];
    var filtersObject = [];
    var headerTemplate = uigridUtils.headerTemplateVal();
    var paginationOptions = uigridUtils.paginationOptionsVal();
    var editBtnVendor = uigridUtils.editBtn('app.mainVendor.editVendor({id:row.entity.Id})');
    var delBtn = uigridUtils.delBtn();
    var watchBtn = uigridUtils.watchBtn();
    var colDef = [
         { name: 'Id', field: 'Id', enableCellEdit: false, width: 50 },
         { name: 'customForm.name', field: 'Name', headerCellTemplate: headerTemplate },
         { name: 'customForm.city', field: 'Addresses[0].Cities.Name', enableCellEdit: false, headerCellTemplate: headerTemplate },
         { name: 'customForm.phone', field: 'Addresses[0].PhoneNumber', headerCellTemplate: headerTemplate },
         { name: 'customForm.mobile', field: 'Addresses[0].MobileNumber', headerCellTemplate: headerTemplate },
         { name: 'customForm.email', field: 'Addresses[0].MainEmail', headerCellTemplate: headerTemplate },
         { name: 'customForm.vendorContactPersonId', field: 'VendorContactPersonId', headerCellTemplate: headerTemplate },
         editBtnVendor,
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
            var url = '/VendorAdmin/EditVendor';
            uigridUtils.cellEdit(url, rowEntity, colDef, newValue, oldValue);
        });
    };

    $scope.Delete = function (row) {
        var url = '/VendorAdmin/DeleteVendor';
        uigridUtils.delRow(url, row)
        var index = $scope.gridOptionsComplex.data.indexOf(row.entity);
        $scope.gridOptionsComplex.data.splice(index, 1);
    };

    $scope.editVendor = function () {
        $state.go('app.editVendor');
    };

    var getPage = function (FilterValue) {
        var url = '/VendorAdmin/GetFilteredVendors';
        uigridUtils.getPage(url, filtersObject, paginationOptions, $scope);
    };

    getPage();

}]);

