using MiAmor.Core;
using System;
using System.Collections.Generic;
namespace MiAmor.Web.Models
{
    public class CampaignModel : Entity<int>
    {
        /// <summary>
        /// Gets or sets the vendor identifier
        /// </summary>
        public int VendorId { get; set; }
        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }
             
        /// <summary>
        /// Gets or sets the short description
        /// </summary>
        public string ShortDescription { get; set; }
        /// <summary>
        /// Gets or sets the full description
        /// </summary>
        public string FullDescription { get; set; }        

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int? DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the logo
        /// </summary>
        public string BackgroundImgPath { get; set; }
   
        /// Gets or sets the StartDateTime
        /// </summary>
        public DateTime? StartDateTime { get; set; }
        /// <summary>
        /// Gets or sets the EndDateTime
        /// </summary>
        public DateTime? EndDateTime { get; set; }

        public DateTime? ExpirationDateTime { get; set; }

        public virtual List<CampaignPicture> CampaignPictures { get; set; }
    }
}