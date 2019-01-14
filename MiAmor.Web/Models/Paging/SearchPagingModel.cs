using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiAmor.Web.Models
{
    public class SearchPagingModel : PagingModel
    {
        public int Id { get; set; }
        public SearchPagingModel(int _PageIndex,
       int _PageSize,
       int _TotalCount,
         int _Id = 0)
        {
            PageSize = _PageSize;
            TotalCount = _TotalCount;
            Id = _Id;
            PageIndex = _PageIndex;
        }
    }
}