using MiAmor.Core;
using System;

namespace MiAmor.Web.Models
{
    public class CustomerMessageModel
    {
        public int Id { set; get; }

        public int VendorId { get; set; }

        public int MessageTypeId { get; set; }

        public string Title { get; set; }

        public string MsgTxt { get; set; }

        public bool WasOpened { set; get; }

        public DateTime DateOpened { get; set; }

        public DateTime DateSend { get; set; }
         
        public CustomerMessageType CustomerMessageType { get; set; }

        public virtual Vendor Vendor { get; set; }
    }
}