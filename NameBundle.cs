using Etilic.Core.Extensibility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etilic.Name
{
    public class NameBundle : Bundle
    {
        #region Constants
        /// <summary>
        /// The globally unique ID of this bundle.
        /// </summary>
        private const String GUID = "6A1D1A77-1AE3-4B39-AF72-0E888FCBCD84";
        #endregion

        public static Guid ID = new Guid(GUID);

        #region Properties
        /// <summary>
        /// Gets the globally unique ID
        /// </summary>
        public override Guid BundleID
        {
            get { return ID; }
        }

        /// <summary>
        /// Gets an empty array.
        /// </summary>
        public override Guid[] Dependencies
        {
            get { return new Guid[] {}; }
        }
        #endregion

        #region RegisterEntities
        public override void RegisterEntities(DbModelBuilder modelBuilder)
        {
            modelBuilder.RegisterEntityType(typeof(Name));
            modelBuilder.RegisterEntityType(typeof(NameUsage));
            modelBuilder.RegisterEntityType(typeof(NameFrequency));
        }
        #endregion
    }
}
