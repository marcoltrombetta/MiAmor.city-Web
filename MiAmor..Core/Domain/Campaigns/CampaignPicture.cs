using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Core
{
    public class CampaignPicture : Entity<int>
    {
        public int CampaignId { get; set; }

        public int PictureId { get; set; }

        public int? DisplayOrder { get; set; }

        /// <summary>
        /// Gets the picture
        /// </summary>
        public virtual Picture Picture { get; set; }



    }
}
