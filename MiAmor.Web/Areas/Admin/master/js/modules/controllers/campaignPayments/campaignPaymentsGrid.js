/**=========================================================
 * Module: UIGridController
  =========================================================*/
App.controller('CampaignPaymentsUIGridController', ['$scope', 'uiGridConstants', 'uigridUtils', '$http', '$state', '$stateParams', function ($scope, uiGridConstants, uigridUtils, $http, $state, $stateParams) {

    var data = [];
    var filtersObject = [];
    var headerTemplate = uigridUtils.headerTemplateVal();
    var paginationOptions = uigridUtils.paginationOptionsVal();
    var editBtnCampaignPayments = uigridUtils.editBtn('app.mainCampaignPayments.editCampaignPayments({id:row.entity.Id})');
    var delBtn = uigridUtils.delBtn();
    var watchBtn = uigridUtils.watchBtn();
    var colDef = [
         { name: 'Id', field: 'Id', enableCellEdit: false, width: 50 },
         { name: 'customForm.customer', field: 'CustomerId', headerCellTemplate: headerTemplate },
         { name: 'customForm.campaign', field: 'CampaignId', headerCellTemplate: headerTemplate },
         { name: 'customForm.qtyCupon', field: 'QtyCupon', headerCellTemplate: headerTemplate },
         { name: 'customForm.totalSum', field: 'TotalSum', headerCellTemplate: headerTemplate },
         { name: 'customForm.purchaseDate', field: 'PurchaseDate', headerCellTemplate: headerTemplate },
         editBtnCampaignPayments,
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
            var url = '/CampaignPaymentsAdmin/EditCampaignPayments';
            uigridUtils.cellEdit(url, rowEntity, colDef, newValue, oldValue);
        });
    };

    $scope.Delete = function (row) {
        var url = '/CampaignPaymentsAdmin/DeleteCampaignPayments';
        uigridUtils.delRow(url, row)
        var index = $scope.gridOptionsComplex.data.indexOf(row.entity);
        $scope.gridOptionsComplex.data.splice(index, 1);
    };

    $scope.editCampaignPayments = function () {
        $state.go('app.editCampaignPayments');
    };

    var getPage = function (FilterValue) {
        var url = '/CampaignPaymentsAdmin/GetFilteredCampaignPayments';
        uigridUtils.getPage(url, filtersObject, paginationOptions, $scope);
    };

    getPage();

}]);

