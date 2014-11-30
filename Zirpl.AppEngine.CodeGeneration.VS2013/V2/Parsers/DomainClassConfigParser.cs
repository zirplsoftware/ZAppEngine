using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EnvDTE;
using Newtonsoft.Json;
using Zirpl.AppEngine.CodeGeneration.TextTemplating;
using Zirpl.AppEngine.CodeGeneration.V2.ConfigModel;
using Zirpl.AppEngine.CodeGeneration.V2.Parsers.JsonModel;
using Zirpl.IO;

namespace Zirpl.AppEngine.CodeGeneration.V2.Parsers
{
    public class DomainClassConfigParser
    {
        public IEnumerable<DomainClassConfigBase> Parse(AppConfig app, IEnumerable<ProjectItem> configProjectItems)
        {
            var domainClassConfigList = new List<DomainClassConfigBase>();
            foreach (var projectItem in configProjectItems)
            {
                var path = projectItem.GetFullPath();
                this.LogLineToBuildPane("Domain config file: " + path);

                DomainClassConfigJson domainJson = null;
                using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    var content = fileStream.ReadAllToString();
                    domainJson = JsonConvert.DeserializeObject<DomainClassConfigJson>(content);
                }
                DomainClassConfigBase domainType = null;
                if (domainJson.IsPersistable.GetValueOrDefault(true))
                {
                    if (domainJson.IsStaticLookup.GetValueOrDefault())
                    {
                        domainType = new StaticLookupDomainClassConfig();
                    }
                    else
                    {
                        domainType = new PersistableDomainClassConfig();
                    }
                }
                else
                {
                    domainType = new TransientDomainClassConfig();
                }


                // NOTES:
                // for this to work, we are expecting the following conventions
                //
                // - all DomainType config files are in _config
                // - they are in a folder specifying which Project (ModelProject, DataServiceProject etc etc)
                // - the name of the file (minus extension) is the class name
                // - the file path (minus the file name) is the subnamespace within the project


                // BASE fields
                //
                //public String UniqueName { get; set; }
                //public DomainTypeDefinitionBase ParentDomainClass { get; set; }
                //
                domainType.Config = domainJson;
                domainType.SourceProjectItem = projectItem;
                domainType.IsAbstract = domainJson.IsAbstract.GetValueOrDefault();
                domainType.ClassName = Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(path));
                var relativeDirectory = Path.GetDirectoryName(path).SubstringAfterLastInstanceOf("_config\\");
                var tempUniqueName = relativeDirectory.Replace('\\', '.') + "." + domainType.ClassName;
                var whichProject = tempUniqueName.SubstringUntilFirstInstanceOf("Project.", true);
                var whichProjectLower = whichProject.ToLower();
                if (whichProjectLower == "model")
                {
                    domainType.DestinationProject = app.ModelProject;
                }
                else if (whichProjectLower == "dataservice")
                {
                    domainType.DestinationProject = app.DataServiceProject;
                }
                else if (whichProjectLower == "service")
                {
                    domainType.DestinationProject = app.ServiceProject;
                }
                else if (whichProjectLower == "webcore")
                {
                    domainType.DestinationProject = app.WebCoreProject;
                }
                else if (whichProjectLower == "web")
                {
                    domainType.DestinationProject = app.WebProject;
                }
                else if (whichProjectLower == "testscommon")
                {
                    domainType.DestinationProject = app.TestsCommonProject;
                }
                else if (whichProjectLower == "dataservicetests")
                {
                    domainType.DestinationProject = app.DataServiceTestsProject;
                }
                else if (whichProjectLower == "servicetests")
                {
                    domainType.DestinationProject = app.ServiceTestsProject;
                }
                else
                {
                    throw new Exception("Project unknown");
                }
                var subNamespace = tempUniqueName.SubstringAfterFirstInstanceOf(whichProject + "Project.", true)
                                                        .SubstringUntilLastInstanceOf("." + domainType.ClassName)
                                                        .SubstringUntilLastInstanceOf(domainType.ClassName);
                domainType.Namespace = domainType.DestinationProject.GetDefaultNamespace() +
                                        (String.IsNullOrEmpty(subNamespace) ? "" : ".") + subNamespace;
                domainType.FullClassName = domainType.Namespace + "." + domainType.ClassName;

                if (!String.IsNullOrEmpty(domainJson.PluralName))
                {
                    domainType.PluralName = domainJson.PluralName;
                }
                else if (domainType.ClassName.EndsWith("s"))
                {
                    domainType.PluralName = domainType.ClassName + "es";
                }
                else if (domainType.ClassName.EndsWith("y"))
                {
                    domainType.PluralName = domainType.ClassName.Substring(0, domainType.ClassName.Length - 1) + "ies";
                }
                else
                {
                    domainType.PluralName = domainType.ClassName + "s";
                }

                // StaticLookup-specific fields
                var staticLookupDomainType = domainType as StaticLookupDomainClassConfig;
                if (staticLookupDomainType != null)
                {
                    if (domainJson.EnumValues != null
                        && domainJson.EnumValues.Count() > 0)
                    {
                        var enumConfig = new EnumConfig();
                        enumConfig.DestinationProject = staticLookupDomainType.DestinationProject;
                        enumConfig.EnumName = staticLookupDomainType.ClassName + "Enum";
                        enumConfig.FullEnumName = staticLookupDomainType.FullClassName + "Enum";
                        enumConfig.Namespace = staticLookupDomainType.Namespace;
                        enumConfig.SourceProjectItem = staticLookupDomainType.SourceProjectItem;
                        enumConfig.StaticLookupParent = staticLookupDomainType;
                        // TODO: this needs to be fixed when we decide how to set Id's
                        enumConfig.ValueType = "int"; 
                        foreach (var enumValue in domainJson.EnumValues)
                        {
                            enumConfig.Values.Add(new EnumValueConfig() { Value = enumValue.Id, Name = enumValue.Name });
                        }
                    }
                }

                // Persistable-specific fields
                var persistableDomainType = domainType as PersistableDomainClassConfig;
                if (persistableDomainType != null)
                {
                    persistableDomainType.IsVersionable = domainJson.IsVersionable.GetValueOrDefault(true);
                    persistableDomainType.IsAuditable = domainJson.IsAuditable.GetValueOrDefault(true);
                    persistableDomainType.IsUserCustomizable = domainJson.IsUserCustomizable.GetValueOrDefault();
                    persistableDomainType.IsInsertable = domainJson.IsInsertable.GetValueOrDefault(true);
                    persistableDomainType.IsUpdatable = domainJson.IsUpdatable.GetValueOrDefault(true);
                    persistableDomainType.IsDeletable = domainJson.IsDeletable.GetValueOrDefault(true);
                    persistableDomainType.IsMarkDeletable = domainJson.IsMarkDeletable.GetValueOrDefault();
                }

                domainClassConfigList.Add(domainType);
            }

            foreach (var domainType in domainClassConfigList)
            {
                if (!String.IsNullOrEmpty(domainType.Config.InheritsFrom))
                {
                    var match = from dt in domainClassConfigList
                                where
                                    dt.FullClassName.ToLowerInvariant()
                                        .EndsWith(domainType.Config.InheritsFrom.ToLowerInvariant())
                                select dt;
                    if (match.Count() == 0)
                    {
                        throw new Exception("Could not find a domain type matching ParentDomainClass: " + domainType.Config.InheritsFrom);
                    }
                    else if (match.Count() > 1)
                    {
                        throw new Exception("Found multiple domain types matching ParentDomainClass: " + domainType.Config.InheritsFrom);
                    }
                    domainType.ParentDomainClass = match.Single();
                    domainType.ParentDomainClass.ChildDomainClasses.Add(domainType);

                    // reset any fields to match if ON in base
                    var persistableDomainType = domainType as PersistableDomainClassConfig;
                    var staticLookupDomainType = domainType as StaticLookupDomainClassConfig;
                    var transientDomainType = domainType as TransientDomainClassConfig;
                    if (persistableDomainType != null)
                    {
                        var persistableInheritsFrom = domainType.ParentDomainClass as PersistableDomainClassConfig;
                        if (persistableInheritsFrom == null)
                        {
                            throw new Exception("A persistable domain class can only inherit from another persistable domain class: " + domainType.ClassName);
                        }

                        // if inheriting an interface, then this class HAS to have it
                        persistableDomainType.IsVersionable = persistableInheritsFrom.IsVersionable ? true : persistableDomainType.IsVersionable;
                        persistableDomainType.IsAuditable = persistableInheritsFrom.IsAuditable ? true : persistableDomainType.IsAuditable;
                        persistableDomainType.IsUserCustomizable = persistableInheritsFrom.IsUserCustomizable ? true : persistableDomainType.IsUserCustomizable;
                        persistableDomainType.IsMarkDeletable = persistableInheritsFrom.IsMarkDeletable ? true : persistableDomainType.IsMarkDeletable;

                        // not so for these properties which could differ if using Table-per-Concrete class
                        //persistableDomainType.IsInsertable = persistableInheritsFrom.IsInsertable ? false : persistableDomainType.IsInsertable;
                        //persistableDomainType.IsUpdatable = domainJson.IsUpdatable.GetValueOrDefault(true);
                        //persistableDomainType.IsDeletable = domainJson.IsDeletable.GetValueOrDefault(true);
                    }
                    if (staticLookupDomainType != null)
                    {
                        throw new Exception("A static lookup domain class does not support inheritance: " + domainType.ClassName);
                    }
                    if (transientDomainType != null)
                    {
                        var transientInheritsFrom = domainType.ParentDomainClass as TransientDomainClassConfig;
                        if (transientInheritsFrom == null)
                        {
                            throw new Exception("A transient domain class can only inherit from another transient domain class: " + domainType.ClassName);
                        }
                    }
                }
            }

            this.LogLineToBuildPane(JsonConvert.SerializeObject(domainClassConfigList, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects }));
            
            return domainClassConfigList;
        }
    }
}
