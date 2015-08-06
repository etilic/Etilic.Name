using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etilic.Name.Import
{
    /// <summary>
    /// Enumerates properties of names which an importer may map to fields in a file.
    /// </summary>
    public enum NameProperty
    {
        /// <summary>
        /// The field contains a given name.
        /// </summary>
        Name,
        /// <summary>
        /// The field contains the year in which the name was used.
        /// </summary>
        Year,
        /// <summary>
        /// The field contains the number of times the name was used for males.
        /// </summary>
        MaleOccurrences,
        /// <summary>
        /// The field contains the number of times the name was used for females.
        /// </summary>
        FemaleOccurrences,
        /// <summary>
        /// The field contains a surname.
        /// </summary>
        Surname
    }
}
