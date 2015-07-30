using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etilic.Name
{
    /// <summary>
    /// Reprents a title.
    /// </summary>
    public class Title
    {
        #region Properties
        /// <summary>
        /// The globally unique ID of this title.
        /// </summary>
        public Guid ID
        {
            get;
            set;
        }
        /// <summary>
        /// The String representation of this title.
        /// </summary>
        [Required]
        public String Value
        {
            get;
            set;
        }
        #endregion
    }
}
