using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Core
{
    public class CampaignDelivery : Entity<int>
    {
        /// <summary>
        /// Gets or sets the vendor identifier
        /// </summary>
        public int CampaignId { get; set; }
        /// <summary>
        /// Gets or sets the user who created the campaign
        /// </summary>
        public string Description{ get; set; }
        /// <summary>
        /// Gets or sets the bruto % of the manager handling this deal
        /// </summary>
        public double DeliveryPrice { get; set; }
        /// <summary>
        /// Gets or sets the % the site earn from this deal
        /// </summary>
        public Int16? MaxUnitsPerDelivery { get; set; }
        /// <summary>
        /// Gets or sets the real price of the campaign
        /// </summary>
        public bool? Active { get; set; }

        public virtual Campaign Campaign { get; set; }

    }
}
