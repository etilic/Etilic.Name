using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etilic.Name
{
    /// <summary>
    /// Represents how a name is used in a specific culture.
    /// </summary>
    public class NameUsage
    {
        #region Properties
        /// <summary>
        /// Gets or sets the globally unique ID of this name usage.
        /// </summary>
        public Guid ID
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the globally unique ID of the name name which is being used.
        /// </summary>
        public Guid NameID
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the name of the culture in which the name is used.
        /// </summary>
        [Required]
        public String CultureName
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the sex for which the name is used.
        /// </summary>
        public Sex Sex
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the probability that the name is used for this sex in this culture.
        /// </summary>
        public Single Probability
        {
            get;
            set;
        }
        #endregion

        #region Navigational Properties
        /// <summary>
        /// Gets or sets the name name which is being used.
        /// </summary>
        [ForeignKey("NameID")]
        public virtual Name Name
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets information about the usage of this name.
        /// </summary>
        public virtual List<NameFrequency> Frequency
        {
            get;
            set;
        }
        #endregion

        public NameUsage()
        {
            this.ID = Guid.NewGuid();
            this.Frequency = new List<NameFrequency>();
        }

        public NameUsage(CultureInfo culture, Sex sex)
            : this()
        {
            this.CultureName = culture.Name;
            this.Sex = sex;
        }
    }
}
