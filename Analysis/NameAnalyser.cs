using Etilic.Core.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etilic.Name.Analysis
{
    public class NameAnalyser
    {
        #region Instance members
        private NameContext context;
        private NameAnalysisOptions options;
        #endregion

        #region Constructors
        public NameAnalyser(NameContext context, NameAnalysisOptions options)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (options == null)
                throw new ArgumentNullException("options");

            this.context = context;
            this.options = options;
        }
        #endregion

        private NameAnalysisResult AnalyseGivenName(String nameStr)
        {
            NameAnalysisResult result = new FragmentResult(nameStr);

            // try to find the name in the database
            Name name = this.context.Lookup(nameStr);

            if(name != null)
            {
                // filter out surname cases
                var givenNameUses = name.Usage.Where(x => !x.FamilyName);

                // should we filter by culture, too?
                if (this.options.CultureHint != null)
                {
                    givenNameUses = givenNameUses.Where(
                        x => x.CultureName.Equals(this.options.CultureHint.Name));
                }

                if (givenNameUses.Count() == 0)
                    return result;

                result = new GivenNameResult(name, givenNameUses.ToList());
            }

            return result;
        }

        private NameAnalysisResult AnalyseFamilyName(String nameStr)
        {
            NameAnalysisResult result = new FragmentResult(nameStr);

            // try to find the name in the database
            Name name = this.context.Lookup(nameStr);

            if (name != null)
            {
                // filter out surname cases
                var givenNameUses = name.Usage.Where(x => x.FamilyName);

                // should we filter by culture, too?
                if (this.options.CultureHint != null)
                {
                    givenNameUses = givenNameUses.Where(
                        x => x.CultureName.Equals(this.options.CultureHint.Name));
                }

                if (givenNameUses.Count() == 0)
                    return result;

                result = new FamilyNameResult(name, givenNameUses.ToList());
            }

            return result;
        }

        private NameAnalysisResult AnalyseCompoundFamilyName(String nameStr)
        {
            String[] parts = nameStr.Split('-');

            if (parts.Length == 1)
                return this.AnalyseFamilyName(parts[0]);

            CompoundNameResult result = new CompoundNameResult(nameStr);

            foreach (String part in parts)
                result.Parts.Add(this.AnalyseFamilyName(part));

            return result;
        }

        /// <summary>
        /// Analyses a name.
        /// </summary>
        /// <param name="data">The name to analyse.</param>
        /// <returns></returns>
        public List<NameAnalysisResult> Analyse(String data)
        {
            // trim whitespaces from both ends
            data = data.Trim();

            List<NameAnalysisResult> results = new List<NameAnalysisResult>();
            CompoundNameResult result = new CompoundNameResult(data);

            // bit simplistic -- will improve later
            String[] parts = data.Split(' ');

            // for now, we will assume that the first name always comes first
            String firstName = parts[0];
            result.Parts.Add(this.AnalyseGivenName(firstName));

            // if there's another part, we'll assume that it's the surname
            if(parts.Length == 2)
            {
                String surname = parts[1];
                result.Parts.Add(this.AnalyseCompoundFamilyName(surname));
            }
            else if(parts.Length > 2)
            {
                // if there are more than 2 parts, we treat everything in the middle
                // as more given names
                for(Int32 i = 1; i < parts.Length - 1; i++)
                {
                    result.Parts.Add(this.AnalyseGivenName(parts[i]));
                }

                String surname = parts.Last();
                result.Parts.Add(this.AnalyseCompoundFamilyName(surname));
            }

            results.Add(result);
            return results;
        }
    }
}
