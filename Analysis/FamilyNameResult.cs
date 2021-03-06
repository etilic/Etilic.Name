﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etilic.Name.Analysis
{
    public class FamilyNameResult : NameAnalysisResult
    {
        #region Instance members
        private Name name;
        private IEnumerable<NameUsage> usage;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name.
        /// </summary>
        [JsonIgnore]
        public Name Name
        {
            get { return this.name; }
        }
        /// <summary>
        /// Gets the matched ways in which this name is used.
        /// </summary>
        [JsonProperty("usage")]
        public IEnumerable<NameUsage> Usage
        {
            get { return this.usage; }
        }
        #endregion

        public FamilyNameResult(Name name, IEnumerable<NameUsage> usage)
            : base(name.Value)
        {
            this.name = name;
            this.usage = usage;
        }
    }
}
