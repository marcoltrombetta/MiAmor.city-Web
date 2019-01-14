using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiAmor.Core;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace MiAmor.Core
{
    public class VendorCategoryCount : Entity<int>
    {

            /// <summary>
        /// Gets or sets the VendorCategoryId
        /// </summary>
        public int VendorCategoryId { get; set; }

        /// <summary>
        /// Gets or sets a CountVendors of used category 
        /// </summary>
        public int CountVendors { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating css class
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets a the order to fetch categories
        /// </summary>
        int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the parent category identifier
        /// </summary>
        public int? ParentCategoryId { get; set; }
    }
}
