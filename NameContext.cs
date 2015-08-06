using Etilic.Core.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etilic.Name
{
    /// <summary>
    /// Provides extension methods for the EtilicContext class which allow
    /// easier access to data provided by Etilic.Names.
    /// </summary>
    public class NameContext : DbContext
    {
        #region Properties
        public DbSet<Name> Names
        {
            get;
            set;
        }

        public DbSet<NameUsage> Usage
        {
            get;
            set;
        }

        public DbSet<NameFrequency> Frequency
        {
            get;
            set;
        }
        #endregion

        #region NameContext
        public NameContext()
            : base("EtilicContext")
        {

        }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); 
            modelBuilder.Conventions.Add(new DateTimeConvention());

            base.OnModelCreating(modelBuilder);
        }

        #region Lookup
        /// <summary>
        /// Looks up a name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Name Lookup(String name)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");

            name = name.ToUpper();

            return this.Names.SingleOrDefault(
                x => x.Value.Equals(name));
        }
        #endregion
    }
}
