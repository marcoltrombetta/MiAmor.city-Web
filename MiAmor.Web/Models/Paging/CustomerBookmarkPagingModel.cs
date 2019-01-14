using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiAmor.Web.Models
{
    public class CustomerBookmarkPagingModel : PagingModel
    {
        public int CustId;
        public CustomerBookmarkPagingModel(int _PageIndex,
       int _PageSize,
       int _TotalCount,
         int  _CustId)
        {
            PageSize = _PageSize;
            TotalCount = _TotalCount;
            PageIndex = _PageIndex;
            CustId = _CustId;
        }
    }
}