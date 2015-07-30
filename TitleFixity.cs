using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etilic.Name
{
    public enum TitleFixity
    {
        /// <summary>
        /// The title is only used as a prefix.
        /// </summary>
        Prefix,
        /// <summary>
        /// The title is only used as a suffix.
        /// </summary>
        Suffix,
        /// <summary>
        /// The title may be used as both.
        /// </summary>
        Both
    }
}
