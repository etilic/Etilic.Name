using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etilic.Name.Analysis
{
    public class NameAnalysisOptions
    {
        public CultureInfo CultureHint
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the input format (if available).
        /// </summary>
        public String Format
        {
            get;
            set;
        }

        public NameAnalysisOptions()
        {
            this.CultureHint = null;
        }
    }
}
