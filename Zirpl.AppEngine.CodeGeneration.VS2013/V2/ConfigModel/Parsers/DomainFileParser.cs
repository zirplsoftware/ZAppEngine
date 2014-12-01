using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Zirpl.AppEngine.CodeGeneration.TextTemplating;
using Zirpl.AppEngine.CodeGeneration.V2.ConfigModel.Parsers.JsonModel;
using Zirpl.AppEngine.Model.Metadata;
using Zirpl.IO;

namespace Zirpl.AppEngine.CodeGeneration.V2.ConfigModel.Parsers
{
    public class DomainFileParser
    {
        /// <summary> 
        /// This method currently assumes the following conventions
        /// - all Domain types are defined in files with extension .domain.zae
        /// - all Domain type config files are in \_config\
        /// - all Domain type config files are in a root folder inside \_config\ that specifies which Project (ModelProject, DataServiceProject etc etc)
        /// - the name of the file (minus extension) is the Domain type (class) name
        /// - the file path (within the Project folder, minus the file name) is the subnamespace within the project
        /// </summary>
        /// <param name="app"></param>
        /// <param name="domainFilePaths"></param>
        /// <returns></returns>
        public virtual IEnumerable<DomainTypeInfo> Parse(AppInfo app, IEnumerable<String> domainFilePaths)
        {
            var list = new List<DomainTypeInfo>();
            foreach (var path in domainFilePaths)
            {
                DomainTypeJson json = null;
                using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    var content = fileStream.ReadAllToString();
                    json = JsonConvert.DeserializeObject<DomainTypeJson>(content);
                }

                
                #region validation of input

                if (!json.IsPersistable.GetValueOrDefault(true)
                    && json.IsStaticLookup.GetValueOrDefault())
                {
                    throw new Exception("IsStaticLookup cannot be true if IsPersistable is false in: " + path);
                }
                if (json.IsStaticLookup.GetValueOrDefault()
                    && json.IsAbstract.GetValueOrDefault())
                {
                    throw new Exception("IsStaticLookup and IsAbstract cannot both be true in: " + path);
                }                
                if (!json.IsPersistable.GetValueOrDefault(true)
                    && (json.IsVersionable.GetValueOrDefault()
                        || json.IsAuditable.GetValueOrDefault()
                        || json.IsCustomizable.GetValueOrDefault()
                        || json.IsInsertable.GetValueOrDefault()
                        || json.IsUpdatable.GetValueOrDefault()
                        || json.IsDeletable.GetValueOrDefault()
                        || json.IsMarkDeletable.GetValueOrDefault()))
                {
                    throw new Exception("IsPersistable is false but one of the following is true: IsVersionable, IsAuditable, IsCustomizable, IsInsertable, IsUpdatable, IsDeletable, IsMarkDeletable in: " + path);
                }
                if (json.IsStaticLookup.GetValueOrDefault()
                    && (json.IsVersionable.GetValueOrDefault()
                        || json.IsAuditable.GetValueOrDefault()
                        || json.IsCustomizable.GetValueOrDefault()
                        || json.IsInsertable.GetValueOrDefault()
                        || json.IsUpdatable.GetValueOrDefault()
                        || json.IsDeletable.GetValueOrDefault()
                        || json.IsMarkDeletable.GetValueOrDefault()))
                {
                    throw new Exception("IsStaticLookup is false but one of the following is true: IsVersionable, IsAuditable, IsCustomizable, IsInsertable, IsUpdatable, IsDeletable, IsMarkDeletable in: " + path);
                }
                if (!json.IsStaticLookup.GetValueOrDefault()
                    && json.EnumValues.Any())
                {
                    throw new Exception("IsStaticLookup is false but EnumValues are present in: " + path);
                }
                if (json.IsStaticLookup.GetValueOrDefault()
                    && !String.IsNullOrEmpty(json.InheritsFrom))
                {
                    throw new Exception("StaticLookup types cannot inherit from anything in: " + path);
                }
                if (!json.IsPersistable.GetValueOrDefault(true)
                    && json.Id != null)
                {
                    throw new Exception("Id can only be specific if IsPersistable is true in: " + path);
                }
                if (json.Id != null
                    && json.Id.AutoGenerationBehavior.HasValue
                    && json.Id.AutoGenerationBehavior.Value == AutoGenerationBehaviorTypeEnum.None)
                {
                    throw new Exception("AutoGenerationBehavior of None is not supported in: " + path);
                }
                if (json.Id != null
                    && json.Id.DataType.HasValue
                    && (json.Id.DataType.Value != DataTypeEnum.Guid
                        && json.Id.DataType.Value != DataTypeEnum.Int
                        && json.Id.DataType.Value != DataTypeEnum.Long
                        && json.Id.DataType.Value != DataTypeEnum.SByte
                        && json.Id.DataType.Value != DataTypeEnum.Short
                        && json.Id.DataType.Value != DataTypeEnum.UInt
                        && json.Id.DataType.Value != DataTypeEnum.ULong
                        && json.Id.DataType.Value != DataTypeEnum.UShort
                        && json.Id.DataType.Value != DataTypeEnum.Byte))
                {
                    throw new Exception("Invalid Id DataType in: " + path);
                }
                if (json.Id != null
                    && json.Id.DataType.HasValue
                    && json.Id.DataType.Value == DataTypeEnum.String
                    && json.Id.IsNullable.GetValueOrDefault())
                {
                    throw new Exception("Id DataType of String cannot be Nullable: " + path);
                }

                #endregion

                DomainTypeInfo domainType = new DomainTypeInfo();
                domainType.Config = json;
                domainType.ConfigFilePath = path;

                domainType.IsAbstract = json.IsAbstract.GetValueOrDefault();
                domainType.IsPersistable = json.IsPersistable.GetValueOrDefault(true);
                domainType.IsStaticLookup = json.IsStaticLookup.GetValueOrDefault();
                domainType.IsVersionable = json.IsVersionable.GetValueOrDefault(domainType.IsPersistable && !domainType.IsStaticLookup);
                domainType.IsAuditable = json.IsAuditable.GetValueOrDefault(domainType.IsPersistable && !domainType.IsStaticLookup);
                domainType.IsCustomizable = json.IsCustomizable.GetValueOrDefault();
                domainType.IsInsertable = json.IsInsertable.GetValueOrDefault(domainType.IsPersistable && !domainType.IsStaticLookup);
                domainType.IsUpdatable = json.IsUpdatable.GetValueOrDefault(domainType.IsPersistable && !domainType.IsStaticLookup);
                domainType.IsDeletable = json.IsDeletable.GetValueOrDefault(domainType.IsPersistable && !domainType.IsStaticLookup);
                domainType.IsMarkDeletable = json.IsMarkDeletable.GetValueOrDefault();

                domainType.Name = Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(path));
                // TODO: validate the class name
                var relativeDirectory = Path.GetDirectoryName(path).SubstringAfterLastInstanceOf("_config\\");
                var tempUniqueName = relativeDirectory.Replace('\\', '.') + "." + domainType.Name;
                var whichProject = tempUniqueName.SubstringUntilFirstInstanceOf("Project.", StringComparison.InvariantCultureIgnoreCase);
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
                    throw new Exception("DestinationProject unknown: " + whichProject);
                }
                var subNamespace = tempUniqueName.SubstringAfterFirstInstanceOf(whichProject + "Project.", StringComparison.InvariantCultureIgnoreCase)
                                                        .SubstringUntilLastInstanceOf("." + domainType.Name)
                                                        .SubstringUntilLastInstanceOf(domainType.Name);
                domainType.Namespace = domainType.DestinationProject.GetDefaultNamespace() +
                                        (String.IsNullOrEmpty(subNamespace) ? "" : ".") + subNamespace;
                // TODO: validate the namespace
                if (!String.IsNullOrEmpty(json.PluralName))
                {
                    domainType.PluralName = json.PluralName;
                }
                else
                {
                    domainType.PluralName = this.GetPluralName(domainType.Name);
                }
                // TODO: validate the PluralName

                list.Add(domainType);
            }


            // handle inheritance
            //
            foreach (var domainType in list.Where(o => !String.IsNullOrEmpty(o.Config.InheritsFrom)))
            {
                var inheritsFromFullNameTokens = domainType.Config.InheritsFrom.Split('.').Reverse().ToList();
                var inheritsFromClassName = inheritsFromFullNameTokens.First();

                var potentialMatches = (from dt in list
                                        where dt.Name.ToLowerInvariant() == inheritsFromClassName.ToLowerInvariant()
                                        select dt).ToList();

                // even though there may only be one, we still take this step because
                // we want to ensure the entire namespace matches
                //
                for (int i = potentialMatches.Count() - 1; i >= 0; i--)
                {
                    var potentialMatch = potentialMatches[i];
                    var potentialMatchFullNameTokens = potentialMatch.FullName.Split('.').Reverse().ToList();
                    if (inheritsFromFullNameTokens.Count() > potentialMatchFullNameTokens.Count())
                    {
                        // by definition this can't be a match since the
                        // number of namespace tokens is greater than the match
                        //
                        potentialMatches.Remove(potentialMatch);
                    }
                    else
                    {
                        for (int j = 0; j < inheritsFromFullNameTokens.Count(); j++)
                        {
                            if (potentialMatchFullNameTokens[j] != inheritsFromFullNameTokens[j])
                            {
                                potentialMatches.Remove(potentialMatch);
                                break;
                            }
                        }
                    }
                }
                if (!potentialMatches.Any())
                {
                    throw new Exception("Could not find domain type matching InheritsFrom in: " + domainType.ConfigFilePath);
                }
                else if (potentialMatches.Count() > 1)
                {
                    throw new Exception("Found more than 1 matching domain type for InheritsFrom in: " + domainType.ConfigFilePath);
                }

                domainType.InheritsFrom = potentialMatches.Single();
                domainType.InheritsFrom.InheritedBy.Add(domainType);
            }
            // this is to ensure the interfaces that are on base classes are handled appropriately
            //
            foreach (var domainType in list.Where(o => o.InheritedBy.Any() && o.InheritsFrom == null))
            {
                // these are the base-most domain types of each heirarchy
                this.AlignInterfacePropertiesInHeirarchy(domainType);
            }


            #region validate inheritance
            
            foreach (var domainType in list.Where(o => o.InheritsFrom != null))
            {
                if (domainType.InheritsFrom.IsStaticLookup)
                {
                    throw new Exception("StaticLookups cannot be used as InheritsFrom in: " + domainType.ConfigFilePath);
                }
                if (domainType.InheritsFrom.IsEnum)
                {
                    throw new Exception("Enums cannot be used as InheritsFrom in: " + domainType.ConfigFilePath);
                }
                if (domainType.InheritsFrom.IsCustomValue)
                {
                    throw new Exception("CustomValue classes cannot be used as InheritsFrom in: " + domainType.ConfigFilePath);
                }
                if (domainType.Config.Id != null)
                {
                    throw new Exception("Id, if defined, must be defined at the bottom of the Heirarchy: " + domainType.ConfigFilePath);
                }
            }

            #endregion


            //create implicit DomainTypeInfos
            //
            var newDomainTypes = new List<DomainTypeInfo>();
            foreach (var domainType in list)
            {
                // validation checks for these have already been done- we can trust the config is right 
                // if we get to this point
                //
                if (domainType.IsCustomizable
                    && (domainType.InheritsFrom == null
                        || !domainType.InheritsFrom.IsCustomizable))
                {
                    var customValueDomainType = new DomainTypeInfo();
                    customValueDomainType.DestinationProject = domainType.DestinationProject;
                    customValueDomainType.Name = domainType.Name + "CustomValue";
                    customValueDomainType.PluralName = customValueDomainType.Name + "s";
                    customValueDomainType.Namespace = domainType.Namespace;
                    customValueDomainType.IsPersistable = true;
                    customValueDomainType.IsCustomValue = true;
                    customValueDomainType.Customizes = domainType;
                    domainType.CustomizedBy = customValueDomainType;

                    // TODO: some of these may not be correct- need to decide how these things get handled for CustomValue classes
                    customValueDomainType.IsVersionable = domainType.IsVersionable;
                    customValueDomainType.IsAuditable = domainType.IsAuditable;
                    customValueDomainType.IsInsertable = domainType.IsInsertable;
                    customValueDomainType.IsUpdatable = domainType.IsUpdatable;
                    customValueDomainType.IsDeletable = domainType.IsDeletable;
                    customValueDomainType.IsMarkDeletable = domainType.IsMarkDeletable;

                    newDomainTypes.Add(customValueDomainType);
                }
                if (domainType.Config.EnumValues.Any())
                {
                    // create DomainTypeInfo for the enum
                    //
                    var enumDomainType = new DomainTypeInfo();
                    enumDomainType.DestinationProject = domainType.DestinationProject;
                    enumDomainType.Name = domainType.Name + "Enum";
                    enumDomainType.Namespace = domainType.Namespace;
                    enumDomainType.PluralName = enumDomainType.Name + "s";
                    enumDomainType.IsEnum = true;
                    enumDomainType.EnumDescribes = domainType;
                    domainType.DescribedByEnum = enumDomainType;
                    enumDomainType.EnumDataType = (domainType.Config != null 
                                                    && domainType.Config.Id != null 
                                                    && domainType.Config.Id.DataType.HasValue) 
                                                   ? domainType.Config.Id.DataType.Value 
                                                   : DataTypeEnum.Byte;
                    foreach (var enumValue in domainType.Config.EnumValues)
                    {
                        enumDomainType.EnumValues.Add(enumValue.Id, enumValue.Name);
                    }
                    newDomainTypes.Add(enumDomainType);
                }
            }
            list.AddRange(newDomainTypes);

            // create implicit properties
            //
            // Id properties, which are ALWAYS at the bottom of the heirarchy
            //
            foreach (var domainType in list.Where(o => o.IsPersistable && o.InheritsFrom == null))
            {
                var configToUse = domainType.IsCustomValue
                    ? this.GetBaseMostDomainType(domainType.Customizes).Config
                    : domainType.Config;

                var property = new DomainPropertyInfo();
                property.Name = "Id";
                property.IsPrimaryKey = true;
                
                property.AutoGenerationBehavior = (configToUse != null 
                                                && configToUse.Id != null 
                                                && configToUse.Id.AutoGenerationBehavior.HasValue)
                    ? configToUse.Id.AutoGenerationBehavior.Value
                    : AutoGenerationBehaviorTypeEnum.ByDataStore;
                property.DataType = (configToUse != null
                                    && configToUse.Id != null
                                    && configToUse.Id.DataType.HasValue)
                    ? configToUse.Id.DataType.Value
                    : domainType.IsStaticLookup
                        ? DataTypeEnum.Byte
                        : DataTypeEnum.Long;
                property.IsNullable = (configToUse != null
                                                && configToUse.Id != null
                                                && configToUse.Id.IsNullable.HasValue)
                    ? configToUse.Id.IsNullable.Value
                    : false;
                property.Owner = domainType;

                domainType.Properties.Add(property);
                domainType.IdProperty = property;
            }

            foreach (var domainType in list.Where(o => o.IsVersionable && (o.InheritsFrom == null || !o.InheritsFrom.IsVersionable)))
            {
                var property = new DomainPropertyInfo();
                property.Name = "RowVersion";
                property.DataType = DataTypeEnum.ByteArray;
                property.IsRowVersion = true;
                property.AutoGenerationBehavior = AutoGenerationBehaviorTypeEnum.ByDataStore;
                domainType.Properties.Add(property);
                property.Owner = domainType;
            }
            foreach (var domainType in list.Where(o => o.IsAuditable && (o.InheritsFrom == null || !o.InheritsFrom.IsAuditable)))
            {
                var property = new DomainPropertyInfo();
                property.Name = "CreatedUserId";
                property.IsForAuditableInterface = true;
                property.DataType = DataTypeEnum.String;
                property.AutoGenerationBehavior = AutoGenerationBehaviorTypeEnum.ByFramework;
                property.IsRequired = true;
                property.MaxLength = 256.ToString();
                property.MinLength = 1.ToString();
                property.UniquenessType = UniquenessTypeEnum.NotUnique;
                property.Owner = domainType;
                domainType.Properties.Add(property);

                property = new DomainPropertyInfo();
                property.Name = "UpdatedUserId";
                property.IsForAuditableInterface = true;
                property.DataType = DataTypeEnum.String;
                property.AutoGenerationBehavior = AutoGenerationBehaviorTypeEnum.ByFramework;
                property.IsRequired = true;
                property.MaxLength = 256.ToString();
                property.MinLength = 1.ToString();
                property.UniquenessType = UniquenessTypeEnum.NotUnique;
                property.Owner = domainType;
                domainType.Properties.Add(property);

                property = new DomainPropertyInfo();
                property.Name = "CreatedDate";
                property.IsForAuditableInterface = true;
                property.DataType = DataTypeEnum.DateTime;
                property.AutoGenerationBehavior = AutoGenerationBehaviorTypeEnum.ByFramework;
                property.IsNullable = true;
                property.IsRequired = true;
                property.UniquenessType = UniquenessTypeEnum.NotUnique;
                property.Owner = domainType;
                domainType.Properties.Add(property);

                property = new DomainPropertyInfo();
                property.Name = "UpdatedDate";
                property.IsForAuditableInterface = true;
                property.DataType = DataTypeEnum.DateTime;
                property.AutoGenerationBehavior = AutoGenerationBehaviorTypeEnum.ByFramework;
                property.IsNullable = true;
                property.IsRequired = true;
                property.UniquenessType = UniquenessTypeEnum.NotUnique;
                property.Owner = domainType;
                domainType.Properties.Add(property);
            }

            foreach (var domainType in list.Where(o => o.IsMarkDeletable))
            {
                var property = new DomainPropertyInfo();
                property.Name = "IsMarkedDeleted";
                property.AutoGenerationBehavior = AutoGenerationBehaviorTypeEnum.ByFramework;
                property.DataType = DataTypeEnum.Boolean;
                property.IsForMarkDeletedInterface = true;
                property.IsNullable = false;
                property.IsRequired = true;
                property.UniquenessType = UniquenessTypeEnum.NotUnique;
                property.Owner = domainType;
                domainType.Properties.Add(property);
            }

            // TODO: IsCustomizable
            // TODO: CustomValue
            // TODO: LookupList???
            

            //this.LogLineToBuildPane(JsonConvert.SerializeObject(list, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects }));
            
            return list;
        }

        private void AlignInterfacePropertiesInHeirarchy(DomainTypeInfo domainType)
        {
                foreach (var child in domainType.InheritedBy)
                {
                    // if inheriting an interface, then this class HAS to have it
                    //
                    child.IsVersionable = domainType.IsVersionable
                        ? true
                        : child.IsVersionable;
                    child.IsAuditable = domainType.IsAuditable
                        ? true
                        : child.IsAuditable;
                    child.IsCustomizable = domainType.IsCustomizable
                        ? true
                        : child.IsCustomizable;
                    child.IsMarkDeletable = domainType.IsMarkDeletable
                        ? true
                        : child.IsMarkDeletable;

                    this.AlignInterfacePropertiesInHeirarchy(child);
                    // TODO: if we start using IsDeletable, IsUpdatable, IsInsertable as interfaces on the domain type, then we need to apply those here
                }
        }

        protected DomainTypeInfo GetBaseMostDomainType(DomainTypeInfo domainType)
        {
            if (domainType.InheritsFrom != null)
            {
                domainType = domainType.InheritsFrom;
            }
            return domainType;
        }

        protected virtual String GetPluralName(String className)
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
    }
}
