using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiAmor.Web.Models
{
    public class VendorCampaignPagingModel : PagingModel
    {
        public int VendorId { get; set; }
        public VendorCampaignPagingModel(int _PageIndex,
       int _PageSize,
       int _TotalCount,
         int _VendorId=0)
        {
            PageSize = _PageSize;
            TotalCount = _TotalCount;
            VendorId = _VendorId;
            PageIndex = _PageIndex;
        }
    }
}