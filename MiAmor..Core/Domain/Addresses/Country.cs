﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Core
{
    public partial class Country :  Entity<int> 
    {
        private ICollection<StateProvince> _stateProvinces;

        private ICollection<City> _City;
     

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }       

        /// Gets or sets the two letter ISO code
        /// </summary>
        public string TwoLetterIsoCode { get; set; }

        /// <summary>
        /// Gets or sets the three letter ISO code
        /// </summary>
        public string ThreeLetterIsoCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether customers in this country must be charged EU VAT
        /// </summary>
        public bool SubjectToVat { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is published
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is limited/restricted to certain stores
        /// </summary>
        public bool LimitedToStores { get; set; }

        public virtual int LanguageId { get; set; }
        public virtual Language Language { get; set; }

        /// <summary>
        /// Gets or sets the state/provinces
        /// </summary>
        public virtual ICollection<StateProvince> StateProvinces
        {
            get { return _stateProvinces ?? (_stateProvinces = new List<StateProvince>()); }
            protected set { _stateProvinces = value; }
        }

        public virtual ICollection<City> City
        {
            get { return _City ?? (_City = new List<City>()); }
            protected set { _City = value; }
        }
        /// <summary>
        /// Gets or sets the restricted shipping methods
        /// </summary>
      
    }
}