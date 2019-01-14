using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Core
{
    public partial class Language : Entity<int>
    {
        /// <summary>
        /// Gets or sets the StringName of the trnaslation
        /// </summary>
        public string StringName { get; set; }

        /// <summary>
        /// Gets or sets the English
        /// </summary>
        public string English { get; set; }

        /// <summary>
        /// Gets or sets the Spanish
        /// </summary>
        public string Spanish { get; set; }

        /// <summary>
        /// Gets or sets a Hebrew value 
        /// </summary>
        public string Hebrew { get; set; }

        public string Description { get; set; } 
    }

}
