App.service('uigridUtils', ["$http", function($http) {
    "use strict";
    var paginationOptions = {
        PageNumber: 1,
        PageSize: 25,
        Sort: null
    };
    var colDef ;
    var data = [];

    var gridOptions={
        showGridFooter: true,
        useExternalFiltering: true,
        pagingOptions: paginationOptions,
        enableGridMenu:true,
        enableFiltering: true,
        columnDefs:colDef,       
        enableSelectAll: true,
        useExternalPagination: true,
        paginationPageSizes: [25, 50, 75],
        exporterCsvFilename: 'myFile.csv',
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location"))
    }
    var headerTemplate = '<div ng-class="{ \'sortable\': sortable }">' +
          '<div class="ui-grid-vertical-bar">&nbsp;</div>' +
          '<div class="ui-grid-cell-contents" col-index="renderIndex">{{col.name|translate}}' +
          '<span ui-grid-visible="col.sort.direction" ng-class="{ \'ui-grid-icon-up-dir\': col.sort.direction == asc, \'ui-grid-icon-down-dir\': col.sort.direction == desc, \'ui-grid-icon-blank\': !col.sort.direction }">' +
          '&nbsp;' +
          '</span>' +
          '</div>' +
          '<div class="ui-grid-column-menu-button" ng-if="grid.options.enableColumnMenus && !col.isRowHeader  && col.colDef.enableColumnMenu !== false" class="ui-grid-column-menu-button" ng-click="toggleMenu($event)">' +
          '<i class="ui-grid-icon-angle-down">&nbsp;</i>' +
          '</div>' +
          '<div ng-if="filterable" class="ui-grid-filter-container" ng-repeat="colFilter in col.filters">' +
          '<input type="text" class="ui-grid-filter-input" ng-model="colFilter.term" ng-attr-placeholder="{{col.name|translate}}" />' +
          '<div class="ui-grid-filter-button" ng-click="colFilter.term = null">' +
          '<i class="ui-grid-icon-cancel" ng-show="!!colFilter.term">&nbsp;</i>' +
          '</div>' +
          '</div>' +
          '</div>';
    // factory function body that constructs shinyNewServiceInstance
    this.headerTemplateVal = function () {
        return headerTemplate;
    };
    this.paginationOptionsVal = function () {
        return paginationOptions;
    }
    this.gridOptionsComplexVal=function( colDefGrid,dataGrid ){    
        data =dataGrid ;
        colDef =colDefGrid;
        return gridOptions;
    }
    this.editBtn = function (uisref) {
        return { name: 'Edit', width: 50, cellTemplate: '<button class="btn primary" ui-sref="' + uisref + '">Edit</button>', enableCellEdit: false, enableFiltering: false };
    }
   
    this.delBtn = function () {
        return { name: 'Delete', width: 50, cellTemplate: '<button class="btn primary" ng-click="grid.appScope.Delete(row)"">Delete</button>', enableCellEdit: false, enableFiltering: false };
    }
    this.watchBtn = function () {
        return { name: 'Watch', width: 50, cellTemplate: '<button class="btn primary" ui-sref="app.mainCustomer.WatchCustomer({id:row.entity.Id})">Show</button>', enableCellEdit: false, enableFiltering: false };
    }
    this.getFilterChangedVal=function( grid,numOfClos){    
        var filtersObject = [];
        for (var i = 0; i < numOfClos; i++) {
            var KeyValuePer = { "MyKey": grid.columns[i].colDef.field, "MyValue": grid.columns[i].filters[0].term }
            filtersObject.push(KeyValuePer)
        }
        return filtersObject;
    }
    this.getPage = function (url, filtersObject, paginationOptions,$scope) {
        $http({
            url: url,
            method: "POST",
            data: JSON.stringify({ FilterElements: filtersObject, PageOptions: paginationOptions }),
            headers: { 'content-Type': 'application/json' }
        }).success(function (data) {
            $scope.gridOptionsComplex.totalItems = data.total
            $scope.gridOptionsComplex.data = data.rows;
        }).
     error(function (data, status, headers, config) {
         var O = 0;
     });
    }
    this.delRow = function (url, row) {
        $http({
            url: url,
            method: "POST",
            data: JSON.stringify({ Id: row.entity.Id }),
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            alert('Deleted.')
        }).
       error(function (data, status, headers, config) {
           var O = 0;
       });
    }
    this.cellEdit = function (url, rowEntity, colDef, newValue, oldValue) {
        if (oldValue !== newValue) {
            var colChanged = colDef.field;
            $http({
                url: url,
                method: "POST",
                data: JSON.stringify({ Id: rowEntity.Id, ColumnChanged: colChanged, Value: newValue }),
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {

            }).error(function (data, status, headers, config) {
                var O = 0;
            });
        }
    }
}]);
    