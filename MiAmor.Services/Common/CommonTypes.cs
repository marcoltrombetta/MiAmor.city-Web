using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace MiAmor.Services
{
    public class PagedDataRequest
    {
        public int PageNumber { get; set; }
        public int RowsPerPage { get; set; }
        public string SortIndexName { get; set; }
        public bool SortAscending { get; set; }
        public string[] SearchIn { get; set; } // Array with Type property names to search in.
        public string SearchFor { get; set; }
        //public List<PagedDataFilter> PrimaryFilters { get; set; }
        //public List<PagedDataFilter> Filters { get; set; }

        public PagedDataRequest(int _PageNumber,string _SearchFor,string _SearchIn,int _RowsPerPage=20)
        {
            RowsPerPage = _RowsPerPage;
            SortIndexName = "DisplayOrder";
            PageNumber = _PageNumber;
            SearchFor = _SearchFor;
            if (_SearchIn=="")
            {
                SearchIn = null;
            }
        
        }


    }

    public class PagedDataResponse<T>
    {
        public int total { get; set; }
        public int totalFiltered { get; set; }
        public double Distance { get; set; }
        public List<T> rows { get; set; }

        public PagedDataResponse(int totalItems, int totalFiltered, List<T> items,double _Distance=0)
        {
            Distance = _Distance;
            this.total = totalItems;
            this.totalFiltered = totalFiltered;
            if (items != null)
            {
                rows = items;
                //rows = items.AsQueryable();
            }
        }

    }

   

    public class PagedDataFilter
    {
        public string Field { get; set; }		// e.g. "Title"
        public string Value { get; set; }		// e.g. "some search string"
        public string Operation { get; set; }	// e.g. "eq"  / "neq" / "gt" / "lt"
        public string Type { get; set; }		// e.g. "string"
    }

    public class GridPageOptions
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Sort { get; set; }
    }

    public class FilterKeyValue
    {
        public string MyKey { get; set; }
        public string MyValue { get; set; }
    }

    public class FilterElements
    {
        public List<FilterKeyValue> Filters { get; set; }
    }

    public class PictureUpdateTableData
    {
        public string url{get; set;} 
        public string TableName{get; set;}
        public string ColName { get; set; } 
        public string ColValue{get; set;}
    }
}
