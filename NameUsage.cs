using Etilic.Core.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
        [JsonIgnore]
        public Guid ID
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the globally unique ID of the name name which is being used.
        /// </summary>
        [Index]
        [Key]
        [Column(Order=1)]
        [JsonIgnore]
        public Guid NameID
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the name of the culture in which the name is used.
        /// </summary>
        [Required]
        [Key]
        [Column(Order=2)]
        [JsonProperty("culture")]
        public String CultureName
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the sex for which the name is used.
        /// </summary>
        [Key]
        [Column(Order=3)]
        [JsonProperty("sex")]
        public Sex Sex
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the probability that the name is used for this sex in this culture.
        /// </summary>
        [JsonProperty("probability")]
        public Single Probability
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets a value indicating whether this name is used as a family name.
        /// </summary>
        [JsonProperty("family")]
        public Boolean FamilyName
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
        [JsonIgnore]
        public virtual Name Name
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets information about the usage of this name.
        /// </summary>
        [JsonIgnore]
        public virtual List<NameFrequency> Frequency
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
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
        #endregion

        #region RecalculateProbability
        /// <summary>
        /// 
        /// </summary>
        public void RecalculateProbability()
        {
            //if (context == null)
              //  throw new ArgumentNullException("context");

            // get other uses of this name in this culture
            var otherUses = this.Name.Usage.Where(x =>
                x.CultureName.Equals(this.CultureName, StringComparison.OrdinalIgnoreCase) 
                    && x.Sex != this.Sex 
                    && x.Sex != Sex.NotApplicable).ToList();

            if (otherUses.Count() > 0)
            {
                Double otherOccurrences = otherUses.Sum(x => x.Frequency.Sum(y => y.Occurrences));
                Double thisOccurrences = this.Frequency.Sum(x => x.Occurrences); 

                this.Probability = (float)(thisOccurrences / (thisOccurrences + otherOccurrences));
            }
            else
            {
                this.Probability = 1.0f;
            }

            
        }
        #endregion
    }
}
