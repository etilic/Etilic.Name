using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etilic.Name.Import
{
    /// <summary>
    /// Represents an interface for data sources which provide name data.
    /// </summary>
    public interface INameImportSource
    {
        #region Read
        /// <summary>
        /// Reads the next row from the input, starting from the start of the file.
        /// </summary>
        /// <returns>True if a row has been read; otherwise, false.</returns>
        Boolean Read();
        #endregion

        #region Get
        /// <summary>
        /// Gets the value of the field corresponding to the column with index <paramref name="index"/> 
        /// in the current row.
        /// </summary>
        /// <param name="index">The index of the field whose value should be returned.</param>
        /// <returns>Returns the value of the field with the specified index if available; otherwise, null.</returns>
        String Get(Int32 index);
        #endregion
    }
}
