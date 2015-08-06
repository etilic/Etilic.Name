using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etilic.Name.Analysis
{
    /// <summary>
    /// Represents part of an input string which we don't know how to deal with,
    /// or that cannot be resolved due to a constraint.
    /// </summary>
    public class FragmentResult : NameAnalysisResult
    {
        public FragmentResult(String data)
            : base(data)
        {

        }
    }
}
