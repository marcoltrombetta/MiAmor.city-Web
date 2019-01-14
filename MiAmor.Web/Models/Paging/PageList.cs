using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiAmor.Web.Models
{
    public class PageList :PagingModel
    {
        public int CategoryId { get; set; }
        public PageList(int _PageIndex,
       int _PageSize,
       int _TotalCount,
         int _CategoryId)
        {
            PageSize = _PageSize;
            TotalCount = _TotalCount;
            CategoryId = _CategoryId;
            PageIndex = _PageIndex;
        }                       
    }
}