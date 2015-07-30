using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etilic.Name
{
    /// <summary>
    /// Represents information about how frequently a name has been used in a time period.
    /// </summary>
    public class NameFrequency
    {
        /// <summary>
        /// Gets or sets the globally unique ID of this frequency information.
        /// </summary>
        public Guid ID
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the globally unique ID of the name usage which this frequency information is for.
        /// </summary>
        public Guid NameUsedID
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the type of frequency information.
        /// </summary>
        public FrequencyType Type
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the start of the period described.
        /// </summary>
        public DateTime PeriodStart
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the end of the period described.
        /// </summary>
        public DateTime PeriodEnd
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the number of times the name was used in this time period.
        /// </summary>
        public Int64 Occurrences
        {
            get;
            set;
        }

        #region Navigational Properties
        /// <summary>
        /// Gets or sets the name usage which this frequency information is for.
        /// </summary>
        public virtual NameUsage NameUsed
        {
            get;
            set;
        }
        #endregion

        public NameFrequency()
        {
            this.ID = Guid.NewGuid();
        }

        public NameFrequency(DateTime start, DateTime end)
        {
            if (start > end)
                throw new ArgumentOutOfRangeException("start must be earlier than end.");

            this.PeriodStart = start;
            this.PeriodEnd = end;
        }
    }
}
