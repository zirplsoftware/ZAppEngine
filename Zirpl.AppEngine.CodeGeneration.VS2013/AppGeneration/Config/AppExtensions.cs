using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using EnvDTE;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config
{
    public static class AppExtensions
    {
        public static IEnumerable<DomainType> FindDomainTypes(this App app, String partialFullName)
        {
            var partialFullNameTokens = partialFullName.Split('.').Reverse().ToList();
            var className = partialFullNameTokens.First();

            var potentialMatches = (from dt in app.DomainTypes
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
