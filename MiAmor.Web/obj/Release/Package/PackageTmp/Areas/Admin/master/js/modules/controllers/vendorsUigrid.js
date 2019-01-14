
/**=========================================================
 * Module: VendorsUIGridController
  =========================================================*/
App.controller('VendorsUIGridController', ['$scope', 'uiGridConstants', 'uigridUtils', '$http', function ($scope, uiGridConstants, uigridUtils, $http) {

    var data = [];


    // Basic example
    // ----------------------------------- 
    var filtersObject = [];
    var headerTemplate = uigridUtils.headerTemplateVal();
    var paginationOptions = uigridUtils.paginationOptionsVal();
    var colDef = [
           { name: 'Id' },
           { name: 'Name' },
         { name: 'ManagerId' },
         { name: 'SiteUrl' },
         { name: 'VendorContactPersonId' },
         { name: 'Edit', cellTemplate: '<button class="btn primary" ui-sref="editCustomer({id: row.entity.Id})">Edit</button>' },
         { name: 'Delete', cellTemplate: '<button class="btn primary" ng-click="grid.appScope.Delete(row)">Delete</button>' }
    ];
    $scope.gridOptionsComplexV = uigridUtils.gridOptionsComplexVal(colDef, data);
    $scope.gridOptionsComplexV.columnDefs = colDef;

    // Complex example
    // ----------------------------------- 

    $scope.gridOptionsComplexV.onRegisterApi = function (gridApi) {
        $scope.gridApi = gridApi;
        $scope.gridApi.core.on.filterChanged($scope, function () {
            filtersObject = [];
            var grid = this.grid;
            filtersObject = uigridUtils.getFilterChangedVal(grid, colDef.length - 6);
            paginationOptions.pageNumber = 1;
            getPage(filtersObject);
        });
    };

    /* $http.get('server/uigrid-complex.json')
       .success(function(data) {
         data.forEach( function(row) {
           row.registered = Date.parse(row.registered);
         });
         $scope.gridOptionsComplex.data = data;
       });*/


    var getPage = function (FilterValue) {
        var url;
        switch (paginationOptions.sort) {
            case uiGridConstants.ASC:
                url = 'https://cdn.rawgit.com/angular-ui/ui-grid.info/gh-pages/data/100_ASC.json';
                break;
            case uiGridConstants.DESC:
                url = 'https://cdn.rawgit.com/angular-ui/ui-grid.info/gh-pages/data/100_DESC.json';
                break;
            default:
                if (undefined == FilterValue || !FilterValue) {
                    url = '/CustomerAdmin/GetFilteredCustomers';
                } else {
                    url = '/CustomerAdmin/GetFilteredCustomers';
                    break;
                }
        }
        var req = {
            method: 'POST',
            data: JSON.stringify(filtersObject),
            url: url
        }
        $http({
            url: url,
            method: "POST",
            data: JSON.stringify({ FilterElements: filtersObject, PageOptions: paginationOptions }),
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.gridOptionsComplexV.totalItems = 320;
            var firstRow = (paginationOptions.pageNumber - 1) * paginationOptions.pageSize;
            paginationOptions.pageNumber = 1;
            $scope.gridOptionsComplexV.data = data;//.slice(firstRow, firstRow + paginationOptions.pageSize);
        }).
        error(function (data, status, headers, config) {
            var O = 0;
        });
    };

    getPage();




}]);
