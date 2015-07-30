using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etilic.Name
{
    public class CompoundName
    {
        public virtual List<Name> FirstNames
        {
            get;
            set;
        }

        public virtual List<Name> MiddleNames
        {
            get;
            set;
        }

        public virtual List<Name> LastNames
        {
            get;
            set;
        }
    }
}
