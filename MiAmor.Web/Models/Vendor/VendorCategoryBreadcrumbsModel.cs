using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using MiAmor.Core;

namespace MiAmor.Web.Models
{
    public class VendorCategoryBreadcrumbsModel
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the ParentId
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Gets or sets the ParentId
        /// </summary>
        public int Count { get; set; }


    }
}