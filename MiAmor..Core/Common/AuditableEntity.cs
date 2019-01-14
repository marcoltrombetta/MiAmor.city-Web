using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Core
{
    /// <summary>
    /// Inherering classes will be audited
    /// </summary>
    public abstract class AuditableEntity<T> : Entity<T>, IAuditableEntity    
    {
        /// <summary>
        /// Gets or sets the CreatedDate identifier
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the CreatedBy identifier
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedDate 
        ////// </summary>
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedBy identifier
        /// </summary>
        public string UpdatedBy { get; set; }
    }
}
