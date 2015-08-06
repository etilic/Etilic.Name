using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etilic.Name
{
    /// <summary>
    /// Enumerates sexes which may be associated with names.
    /// </summary>
    public enum Sex
    {
        /// <summary>
        /// Male.
        /// </summary>
        Male = 0,
        /// <summary>
        /// Female.
        /// </summary>
        Female = 1,
        /// <summary>
        /// Unknown / can't have a sex.
        /// </summary>
        NotApplicable = 2,
    }
}
