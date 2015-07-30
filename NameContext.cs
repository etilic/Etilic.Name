using Etilic.Core.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etilic.Name
{
    /// <summary>
    /// Provides extension methods for the EtilicContext class which allow
    /// easier access to data provided by Etilic.Names.
    /// </summary>
    public static class NameContext
    {
        #region GetNames
        /// <summary>
        /// Gets a DbSet of Name entries.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static DbSet<Name> GetNames(this EtilicContext context)
        {
            return context.Set<Name>();
        }
        #endregion
    }
}
