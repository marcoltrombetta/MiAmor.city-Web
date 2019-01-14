/**=========================================================
 * Module: UIGridController
  =========================================================*/
App.controller('ProductUIGridController', ['$scope', 'uiGridConstants', 'uigridUtils', '$http', '$state', '$stateParams', function ($scope, uiGridConstants, uigridUtils, $http, $state, $stateParams) {

    var data = [];
    var filtersObject = [];
    var headerTemplate = uigridUtils.headerTemplateVal();
    var paginationOptions = uigridUtils.paginationOptionsVal();
    var editBtnProduct = uigridUtils.editBtn('app.mainProduct.editProduct({id:row.entity.Id})');
    var delBtn = uigridUtils.delBtn();
    var watchBtn = uigridUtils.watchBtn();
    var colDef = [
         { name: 'Id', field: 'Id', enableCellEdit: false, width: 50 },
          { name: 'customForm.name', field: 'Name', headerCellTemplate: headerTemplate },
         {
             name: 'ProductTypeId',displayName:'ProductType', cellTemplate: '<p ng-switch="row.entity[col.field]"><span ng-switch-when="1">Regular</span><span ng-switch-when="2">Silver</span></span><span ng-switch-when="3">Gold</span></span><span ng-switch-when="4">Platinum</span></p>',//todo change to translate
             filter: {
                 term: '1',
                 type: uiGridConstants.filter.SELECT,
                 selectOptions: [{ value: 1, label: 'Regular' }, { value: 2, label: 'Silver' }, { value: 3, label: 'Gold' }, { value: 4, label: 'Platinum' }] //todo change to get from DB
             }
         },
         //{ name: 'ProductTypeId' },
          { name: 'customForm.price', field: 'Price', headerCellTemplate: headerTemplate },
          { name: 'customForm.productCost', field: 'ProductCost', headerCellTemplate: headerTemplate },
         editBtnProduct,
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
            var url = '/ProductAdmin/EditProduct';
            uigridUtils.cellEdit(url, rowEntity, colDef, newValue, oldValue);
        });
    };

     $scope.Delete = function (row) {
         var url = '/ProductAdmin/DeleteProduct';
        uigridUtils.delRow(url, row)
        var index = $scope.gridOptionsComplex.data.indexOf(row.entity);
        $scope.gridOptionsComplex.data.splice(index, 1);
    };

    $scope.editProduct = function () {
        $state.go('app.editProduct');
    };

    var getPage = function (FilterValue) {
        var url = '/ProductAdmin/GetFilteredProducts';
        uigridUtils.getPage(url, filtersObject, paginationOptions, $scope);
    };

    getPage();

}]);

