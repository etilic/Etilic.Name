using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etilic.Name.Import
{
    public delegate void ProcessingDelegate(Int64 processed, String name);
    public delegate void CommittingDelegate(Int64 cached);

    /// <summary>
    /// Imports name data from a file.
    /// </summary>
    public class NameImporter
    {
        #region Instance members
        /// <summary>
        /// Stores the database context.
        /// </summary>
        private NameContext context;
        /// <summary>
        /// Stores the mapping of properties to column indices.
        /// </summary>
        private Dictionary<NameProperty, Int32> mappings;
        /// <summary>
        /// Stores the number of database entries which have been added, but not
        /// committed to the database.
        /// </summary>
        private Int64 cached = 0;
        /// <summary>
        /// Stores the number of entries in the input source that were processed.
        /// </summary>
        private Int64 entries = 0;

        private event CommittingDelegate onCommitting;

        private event ProcessingDelegate onProcessing;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indictating whether the database context should be 
        /// recycled after each commit. Setting this property to true may increase 
        /// the speed at which data is imported.
        /// </summary>
        public Boolean RecycleContext
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the maximum number of database entries the importer will
        /// add to the database context before committing the changes to the database.
        /// Setting this property to a larger value will increase import speed at the
        /// cost of higher memory consumption. Setting this value too high may cause
        /// a <see cref="System.OutOfMemoryException"/>. 
        /// </summary>
        public Int64 CommitSize
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the year which is used if no column in the input data source
        /// provides this information.
        /// </summary>
        public Int32 DefaultYear
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the culture which the imported name data is for.
        /// </summary>
        public CultureInfo Culture
        {
            get;
            set;
        }
        #endregion

        #region Events
        /// <summary>
        /// 
        /// </summary>
        public event CommittingDelegate OnCommitting
        {
            add
            {
                this.onCommitting += value;
            }
            remove
            {
                this.onCommitting -= value;
            }
        }

        public event ProcessingDelegate OnProcessing
        {
            add
            {
                this.onProcessing += value;
            }
            remove
            {
                this.onProcessing -= value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initialises a new instance of this class.
        /// </summary>
        /// <param name="culture">The culture for which name data will be imported.</param>
        public NameImporter(CultureInfo culture)
        {
            this.Culture = culture;
            this.DefaultYear = DateTime.Now.Year;
            this.RecycleContext = true;
            this.CommitSize = 10000;
            this.mappings = new Dictionary<NameProperty, Int32>();
        }
        #endregion

        #region AddMapping
        /// <summary>
        /// Adds a mapping from a column in the input data source to a name property.
        /// </summary>
        /// <param name="property">The property to map to.</param>
        /// <param name="column">The column index to map from.</param>
        public void AddMapping(NameProperty property, Int32 column)
        {
            this.mappings.Add(property, column);
        }
        #endregion

        #region GetField
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private String GetField(INameImportSource source, NameProperty property)
        {
            if(this.mappings.ContainsKey(property))
                return source.Get(this.mappings[property]);

            return null;
        }
        #endregion

        #region RecycleContext
        /// <summary>
        /// Disposes of the database context if possible and then initialises a new instance.
        /// </summary>
        private void InitContext()
        {
            // if the context is not null and RecycleContext is enabled,
            // dispose of the current context
            if(this.context != null && this.RecycleContext)
            {
                this.context.Dispose();
                this.context = null;
            }

            // initialise the database context
            if (this.context == null)
            {
                this.context = new NameContext();
                this.context.Configuration.ValidateOnSaveEnabled = false;
                this.context.Configuration.AutoDetectChangesEnabled = false;
            }
        }
        #endregion

        #region Commit
        /// <summary>
        /// Used to indicate that a new entry has been added to the database context. This
        /// method will also commit changes to the database if the cache size has been exceeded.
        /// </summary>
        private void Commit()
        {
            // we have added a new local entry
            this.cached++;

            // commit changes if we have cached more than we are willing to
            if(this.cached >= this.CommitSize)
            {
                // fire an event to allow e.g. a UI to display an
                // appropriate message
                if (this.onCommitting != null)
                    this.onCommitting(this.cached);

                // save local changes to the database
                this.context.SaveChanges();

                // this may be a good opportunity to recycle the database context
                this.InitContext();

                // reset the counter
                this.cached = 0;
            }
        }
        #endregion

        #region AddFrequency
        /// <summary>
        /// Adds frequency data for a name.
        /// </summary>
        /// <param name="name">The name to add data to.</param>
        /// <param name="sex">The sex for which this data is being added.</param>
        /// <param name="year">The year in which the name was used.</param>
        /// <param name="occs">The number of times the name was used.</param>
        private void AddFrequency(Name name, Sex sex, Int32 year, Int64 occs)
        {
            // try to get the usage version of this name
            NameUsage usage = name.UsageFor(this.Culture, sex);

            // create it if it does not exist
            if (usage == null)
            {
                usage = new NameUsage(this.Culture, sex);
                usage.NameID = name.ID;
                usage.Name = name;

                name.Usage.Add(usage);

                this.context.Usage.Add(usage);
                this.Commit();
            }

            // try to get an existing frequency entry
            DateTime periodStart = new DateTime(year, 1, 1);
            DateTime periodEnd = new DateTime(year, 12, 31);

            NameFrequency frequency = usage.Frequency.SingleOrDefault(x =>
                x.PeriodStart.Equals(periodStart) && x.PeriodEnd.Equals(periodEnd));

            // create it if it doesn't exist
            if (frequency == null)
            {
                frequency = new NameFrequency(periodStart, periodEnd);
                frequency.NameUsedID = usage.ID;
                frequency.NameUsed = usage;
                frequency.Type = FrequencyType.Birth;
                frequency.Occurrences = occs;

                usage.Frequency.Add(frequency);

                this.context.Frequency.Add(frequency);
                this.Commit();
            }
        }
        #endregion

        private Name FindOrCreateName(String str)
        {
            // try to find an existing entry for this name in the database
            Name name = context.Names.SingleOrDefault(
                x => x.Value.Equals(str, StringComparison.OrdinalIgnoreCase));

            if (name == null)
            {
                // if it's null in the database, check locally
                name = context.Names.Local.SingleOrDefault(
                    x => x.Value.Equals(str, StringComparison.OrdinalIgnoreCase));

                if (name == null)
                {
                    // we haven't found it, create a new entry and store it locally
                    name = new Name(str);
                    context.Names.Add(name);

                    // we have added something
                    this.Commit();
                }
            }

            return name;
        }

        #region Import
        /// <summary>
        /// Imports name data from <paramref name="source"/>.
        /// </summary>
        /// <param name="source">
        /// The data source to import name data from.
        /// </param>
        public void Import(INameImportSource source)
        {
            this.InitContext();

            // read from the input source until that's no longer possible
            while(source.Read())
            {
                // try to load the field values from the current row
                String nameStr = this.GetField(source, NameProperty.Name);
                String maleOccsStr = this.GetField(source, NameProperty.MaleOccurrences);
                String femaleOccsStr = this.GetField(source, NameProperty.FemaleOccurrences);
                String yearStr = this.GetField(source, NameProperty.Year);
                String surnameStr = this.GetField(source, NameProperty.Surname);

                // set some default values
                Int32 year = this.DefaultYear; 
                Int64 maleOccs = 0;
                Int64 femaleOccs = 0;

                // try to parse field data, if available
                if(!String.IsNullOrWhiteSpace(maleOccsStr))
                    maleOccs = Int64.Parse(maleOccsStr, NumberStyles.AllowThousands);
                if(!String.IsNullOrWhiteSpace(femaleOccsStr))
                    femaleOccs = Int64.Parse(femaleOccsStr, NumberStyles.AllowThousands);
                if(!String.IsNullOrWhiteSpace(yearStr))
                    year = Convert.ToInt32(yearStr);

                if (!String.IsNullOrWhiteSpace(surnameStr))
                {
                    surnameStr = surnameStr.Trim();

                    if (this.onProcessing != null)
                        this.onProcessing(this.entries, surnameStr);

                    // try to find an existing entry for this name in the database
                    Name name = this.FindOrCreateName(surnameStr);

                    NameUsage familyUsage = name.UsageFor(this.Culture, true);

                    if(familyUsage == null)
                    {
                        familyUsage = new NameUsage(this.Culture, Sex.NotApplicable);
                        familyUsage.FamilyName = true;
                        familyUsage.NameID = name.ID;
                        familyUsage.Name = name;

                        name.Usage.Add(familyUsage);

                        this.context.Usage.Add(familyUsage);
                        this.Commit();
                    }
                }

                if (!String.IsNullOrWhiteSpace(nameStr))
                {
                    nameStr = nameStr.Trim();

                    if (this.onProcessing != null)
                        this.onProcessing(this.entries, nameStr);

                    // try to find an existing entry for this name in the database
                    Name name = this.FindOrCreateName(nameStr);

                    // add usage and frequency data for the current name
                    if (maleOccs > 0)
                    {
                        this.AddFrequency(name, Sex.Male, year, maleOccs);
                    }
                    if (femaleOccs > 0)
                    {
                        this.AddFrequency(name, Sex.Female, year, femaleOccs);
                    }

                    // update the static probabilities for the current name
                    foreach (NameUsage usage in name.Usage)
                    {
                        usage.RecalculateProbability();
                    }

                    entries++;
                }
            }

            // save all remaining changes
            context.SaveChanges();
            context.Dispose();
            context = null;
        }
        #endregion
    }
}
