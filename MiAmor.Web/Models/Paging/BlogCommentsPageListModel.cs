using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiAmor.Web.Models
{
    public class BlogCommentsPageListModel : PagingModel
    {
        public BlogCommentsPageListModel(int _PageIndex,
       int _PageSize,
       int _TotalCount)
        {
            PageSize = _PageSize;
            TotalCount = _TotalCount;
            PageIndex = _PageIndex;
        }
    }
}