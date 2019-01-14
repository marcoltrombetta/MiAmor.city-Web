using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Core
{
    public partial class PictureTableToSave : Entity<int>
    {
        public string TableName { get; set; }

        public string ColName { get; set; }

        public string ColNameValue { get; set; }        

        public string PictureIdColName { get; set; }

        public string InsertDefaultColumns { get; set; }

        public string InsertDefaultValues { get; set; }

        public bool? IsDeleted { get; set; }

        public bool? IsActive { get; set; }


    }
}
