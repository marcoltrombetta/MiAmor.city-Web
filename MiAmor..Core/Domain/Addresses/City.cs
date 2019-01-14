using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Core
{
    public class City :  Entity<int>
    {
        /// <summary>
        /// Gets or sets the country identifier
        /// </summary>
        /// 

        private ICollection<Neighbourhood> _Neighbourhood;

        public int CountryId { get; set; }

        /// <summary>
        /// Gets or sets the StateProvince identifier
        /// </summary>
        public int StateProvinceId { get; set; }
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the abbreviation
        /// </summary>
        public string Abbreviation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is published
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the country
        /// </summary>
        public virtual Country Country { get; set; }

        /// <summary>
        /// Gets or sets the StateProvince
        /// </summary>
        public virtual StateProvince StateProvince { get; set; }

        public virtual ICollection<Neighbourhood> Neighbourhood
        {
            get { return _Neighbourhood ?? (_Neighbourhood = new List<Neighbourhood>()); }
            protected set { _Neighbourhood = value; }
        }
    }
}
