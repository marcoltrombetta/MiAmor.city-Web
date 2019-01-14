/**=========================================================
 * Module: UIGridController
  =========================================================*/
App.controller('ProductReviewUIGridController', ['$scope', 'uiGridConstants', 'uigridUtils', '$http', '$state', '$stateParams', function ($scope, uiGridConstants, uigridUtils, $http, $state, $stateParams) {

    var data = [];
    var filtersObject = [];
    var headerTemplate = uigridUtils.headerTemplateVal();
    var paginationOptions = uigridUtils.paginationOptionsVal();
    var editBtnProductReview = uigridUtils.editBtn('app.mainProductReview.editProductReview({id:row.entity.Id})');
    var delBtn = uigridUtils.delBtn();
    var watchBtn = uigridUtils.watchBtn();
    var colDef = [
         { name: 'Id', field: 'Id', enableCellEdit: false, width: 50 },
         { name: 'customForm.firstName', field: 'Customer.firstName', headerCellTemplate: headerTemplate },
         { name: 'customForm.name', field: 'Product.name', headerCellTemplate: headerTemplate },
         { name: 'customForm.title', field: 'title', headerCellTemplate: headerTemplate },
         { name: 'customForm.rating', field: 'rating', headerCellTemplate: headerTemplate },
         editBtnProductReview,
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
            var url = '/ProductReviewAdmin/EditProductReview';
            uigridUtils.cellEdit(url, rowEntity, colDef, newValue, oldValue);
        });
    };

    $scope.Delete = function (row) {
        var url = '/ProductReviewAdmin/DeleteProductReview';
        uigridUtils.delRow(url, row)
        var index = $scope.gridOptionsComplex.data.indexOf(row.entity);
        $scope.gridOptionsComplex.data.splice(index, 1);
    };

    $scope.getCustNameById = function (id) {
        $http({
            url: '/CustomerAdmin/GetCustomerById',
            method: "POST",
            data: JSON.stringify({ Id: id }),
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.cust = data;
        }).
    error(function (data, status, headers, config) {
        var O = 0;
    });
        $scope.nameCust = $scope.cust.firstName;

        return $scope.nameCust;
    
    };

    $scope.editProductReview = function () {
        $state.go('app.editProductReview');
    };

    var getPage = function (FilterValue) {
        var url = '/ProductReviewAdmin/GetFilteredProductReviews';
        uigridUtils.getPage(url, filtersObject, paginationOptions, $scope);
    };

    getPage();

}]);
