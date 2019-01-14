/**=========================================================
 * Module: UIGridController
  =========================================================*/
App.controller('CustUIGridController', ['$scope', 'uiGridConstants', 'uigridUtils', '$http', '$state', '$stateParams', function ($scope, uiGridConstants, uigridUtils, $http, $state, $stateParams) {
  
   var data = [];
   var filtersObject = [];
   var headerTemplate = uigridUtils.headerTemplateVal();
   var paginationOptions = uigridUtils.paginationOptionsVal();
   var editBtn = uigridUtils.editBtn('app.mainCustomer.editCustomer({id:row.entity.Id})');
   var delBtn = uigridUtils.delBtn();
   var watchBtn = uigridUtils.watchBtn();
   var colDef = [
        { name: 'Id', field: 'Id', enableCellEdit: false, width: 50 },
        { name: 'customForm.lastName', field: 'LastName', headerCellTemplate: headerTemplate },
        { name: 'customForm.firstName', field: 'FirstName', headerCellTemplate: headerTemplate },
        { name: 'customForm.email', field: 'Email', headerCellTemplate: headerTemplate },
        { name: 'customForm.phone', field: 'Phone', headerCellTemplate: headerTemplate },
        { name: 'customForm.mobile', field: 'Mobile', headerCellTemplate: headerTemplate },
        { name: 'customForm.city', field: 'City.Name', headerCellTemplate: headerTemplate },
        { name: 'customForm.mainAddress', field: 'MainAddress', headerCellTemplate: headerTemplate },
        {
            name: 'CustomerType', field: 'CustomerType.Name',
            filter: {
                term: '1',
                type: uiGridConstants.filter.SELECT,
                selectOptions: [{ value: 1, label: 'Regular' }, { value: 2, label: 'Silver' }, { value: 3, label: 'Gold' }, { value: 4, label: 'Platinum' }] //todo change to get from DB
            }
        }, 
        {
            name: 'Gender', cellTemplate: '<p ng-switch="row.entity[col.field]"><span ng-switch-when="true">Male</span><span ng-switch-when="false">Female</span></p>',
            filter: {
                term: '1',
                type: uiGridConstants.filter.SELECT,
                selectOptions: [{ value: '1', label: 'male' }, { value: '2', label: 'female' }, { value: '3', label: 'unknown' }]
            }},
        editBtn,
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
          filtersObject = uigridUtils.getFilterChangedVal(grid,colDef.length-2);
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
          var url = '/CustomerAdmin/EditCustomer';
          uigridUtils.cellEdit(url, rowEntity, colDef, newValue, oldValue);
      });
   };    

   $scope.Delete = function (row) {
       var url = '/CustomerAdmin/DeleteCustomer';
       uigridUtils.delRow(url, row)
       var index = $scope.gridOptionsComplex.data.indexOf(row.entity);
       $scope.gridOptionsComplex.data.splice(index, 1);
   };

   $scope.editCustomer = function () {
       $state.go('app.editCustomer');
   };

  var getPage = function (FilterValue) {
      var url = '/CustomerAdmin/GetFilteredCustomers';       
      uigridUtils.getPage(url, filtersObject, paginationOptions,$scope);    
  };

  getPage();   

}]);

