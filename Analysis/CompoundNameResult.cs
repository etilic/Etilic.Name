using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Etilic.Name.Analysis
{
    /// <summary>
    /// Represents a name, consisting of several components.
    /// </summary>
    public class CompoundNameResult : NameAnalysisResult
    {
        private List<NameAnalysisResult> parts;

        /// <summary>
        /// Gets the list of components which make up this name.
        /// </summary>
        [JsonProperty("parts")]
        public List<NameAnalysisResult> Parts
        {
            get
            {
                return this.parts;
            }
        }

        public CompoundNameResult(String data) 
            : base(data)
        {
            this.parts = new List<NameAnalysisResult>();
        }
    }
}
