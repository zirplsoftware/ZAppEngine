using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using EnvDTE;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config
{
    public static class AppExtensions
    {
        public static IEnumerable<DomainProperty> GetAllPropertiesIncludingInherited(this App app, DomainType domainType)
        {
            var list = new List<DomainProperty>();
            while (domainType != null)
            {
                list.AddRange(domainType.Properties);
                domainType = domainType.InheritsFrom;
            }
            return list;
        }
        public static String GetFolderPathFromNamespace(this App app, Project project, String nameSpace)
        {
            String folderPath = nameSpace;
            folderPath = folderPath.SubstringAfterFirstInstanceOf(project.GetDefaultNamespace() + ".");
            folderPath = folderPath.Replace('.', '\\');
            return folderPath;
        }

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

        public static DomainType GetBaseMostDomainType(this DomainType domainType)
        {
            if (domainType.InheritsFrom != null)
            {
                domainType = domainType.InheritsFrom;
            }
            return domainType;
        }

        public static String GetPluralName(this App app, String className)
        {
            if (className.EndsWith("s"))
            {
                return className + "es";
            }
            else if (className.EndsWith("y"))
            {
                return className.Substring(0, className.Length - 1) + "ies";
            }
            else
            {
                return className + "s";
            }
        }

        public static bool IsValidTypeName(this App app, String typeName)
        {
            return CodeGenerator.IsValidLanguageIndependentIdentifier(typeName);
        }

        public static bool IsValidNamespace(this App app, String nameSpace)
        {
            if (String.IsNullOrWhiteSpace(nameSpace))
            {
                return false;
            }
            var tokens = nameSpace.Split('.');
            foreach (var token in tokens)
            {
                if (!app.IsValidTypeName(token))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
