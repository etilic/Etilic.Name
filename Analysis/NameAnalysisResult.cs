using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etilic.Name.Analysis
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class NameAnalysisResult
    {
        /// <summary>
        /// Gets the part of the input data which is responsible for
        /// this result.
        /// </summary>
        [JsonProperty("data")]
        public String Data
        {
            get;
            private set;
        }

        public NameAnalysisResult(String data)
        {
            this.Data = data;
        }
    }
}
