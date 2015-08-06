using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etilic.Name
{
    /// <summary>
    /// Represents a full name.
    /// </summary>
    public class CompoundName
    {
        /// <summary>
        /// Gets or sets a list of titles which appear at the start of the name.
        /// </summary>
        public List<Title> Titles
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the list of first names.
        /// </summary>
        public virtual List<Name> FirstNames
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the list of middle names.
        /// </summary>
        public virtual List<Name> MiddleNames
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the list of surnames.
        /// </summary>
        public virtual List<Name> LastNames
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the list of suffix titles.
        /// </summary>
        public List<Title> SuffixTitles
        {
            get;
            set;
        }

        #region Constructors
        public CompoundName()
        {
            this.Titles = new List<Title>();
            this.FirstNames = new List<Name>();
            this.MiddleNames = new List<Name>();
            this.LastNames = new List<Name>();
            this.SuffixTitles = new List<Title>();
        }
        #endregion
    }
}
