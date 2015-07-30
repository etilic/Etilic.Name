﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etilic.Name
{
    /// <summary>
    /// Represents a name.
    /// </summary>
    public class Name
    {
        #region Properties
        /// <summary>
        /// Gets or sets the globally unique ID of this name.
        /// </summary>
        public Guid ID
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the System.String that this Name represents.
        /// </summary>
        [Required]
        public String Value
        {
            get;
            set;
        }
        #endregion

        #region Navigational Properties
        /// <summary>
        /// Gets or sets the ways in which this name is used.
        /// </summary>
        public List<NameUsage> Usage
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public Name()
        {
            this.ID = Guid.NewGuid();
            this.Usage = new List<NameUsage>();
        }
        /// <summary>
        /// Constructs a new name.
        /// </summary>
        /// <param name="value"></param>
        public Name(String value)
            : this()
        {
            this.Value = value;
        }
        #endregion

        public NameUsage UsageFor(CultureInfo culture)
        {
            return this.Usage.SingleOrDefault(x => 
                x.CultureName.Equals(culture.Name));
        }

        public NameUsage UsageFor(CultureInfo culture, Sex sex)
        {
            return this.Usage.SingleOrDefault(x => 
                x.CultureName.Equals(culture.Name) && x.Sex == sex);
        }
    }
}
