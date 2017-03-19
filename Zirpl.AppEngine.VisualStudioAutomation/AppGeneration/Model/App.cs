using System;
using System.Collections.Generic;
using System.Linq;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Model
{
    public class App
    {
        public App()
        {
            this.DomainTypes = new List<DomainType>();
        }

        public IList<DomainType> DomainTypes { get; private set; }


        //public ProjectIndex CodeGenerationProjectIndex { get; internal set; }
        //public ProjectIndex ModelProjectIndex { get; internal set; }
        //public ProjectIndex DataServiceProjectIndex { get; internal set; }
        //public ProjectIndex DataServiceTestsProjectIndex { get; internal set; }
        //public ProjectIndex ServiceProjectIndex { get; internal set; }
        //public ProjectIndex WebProjectIndex { get; internal set; }
        //public ProjectIndex WebCommonProjectIndex { get; internal set; }
        //public ProjectIndex ServiceTestsProjectIndex { get; internal set; }
        //public ProjectIndex TestsCommonProjectIndex { get; internal set; }


        public IEnumerable<DomainType> FindDomainTypes(String partialFullName)
        {
            var partialFullNameTokens = partialFullName.Split('.').Reverse().ToList();
            var className = partialFullNameTokens.First();

            var potentialMatches = (from dt in DomainTypes
                                    where dt.Name.ToLowerInvariant() == className.ToLowerInvariant()
                                    select dt).ToList();

            // even though there may only be one, we still take this step because
            // we want to ensure the entire namespace matches
            //
            for (int i = potentialMatches.Count() - 1; i >= 0; i--)
            {
                var potentialMatch = potentialMatches[i];
                var potentialMatchFullNameTokens = potentialMatch.FullName.Split('.').Reverse().ToList();
                if (partialFullNameTokens.Count() > potentialMatchFullNameTokens.Count())
                {
                    // by definition this can't be a match since the
                    // number of namespace tokens is greater than the match
                    //
                    potentialMatches.Remove(potentialMatch);
                }
                else
                {
                    for (int j = 0; j < partialFullNameTokens.Count(); j++)
                    {
                        if (potentialMatchFullNameTokens[j] != partialFullNameTokens[j])
                        {
                            potentialMatches.Remove(potentialMatch);
                            break;
                        }
                    }
                }
            }
            return potentialMatches;
        }
    }
}
