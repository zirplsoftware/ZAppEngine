using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EnvDTE;
using EnvDTE80;
using Newtonsoft.Json;
using Zirpl.AppEngine.AppGeneration;
using Zirpl.AppEngine.Model.Metadata;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Parsing.Json;
using Zirpl.AppEngine.VisualStudioAutomation.VisualStudio;
using Zirpl.Collections;
using Zirpl.IO;
using Zirpl.Logging;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Parsing
{
    internal sealed class DomainFileParser
    {
        private readonly DTE2 _visualStudio;

        internal DomainFileParser(DTE2 visualStudio)
        {
            _visualStudio = visualStudio;
        }

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
        internal void ParseDomainTypes(App app, IEnumerable<string> domainFilePaths)
        {
            var jsonDictionary = new Dictionary<DomainType, JsonTypes.DomainTypeJson>();

            #region create DomainTypeInfos specified by directly by the files
            foreach (var path in domainFilePaths)
            {
                JsonTypes.DomainTypeJson json = null;
                using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    var content = fileStream.ReadAllToString();
                    json = JsonConvert.DeserializeObject<JsonTypes.DomainTypeJson>(content);
                }

                
                #region validation of input

                if (!json.IsPersistable.GetValueOrDefault(true)
                    && json.IsStaticLookup.GetValueOrDefault())
                {
                    throw new ConfigFileException("IsStaticLookup cannot be true if IsPersistable is false", path);
                }
                if (json.IsStaticLookup.GetValueOrDefault()
                    && json.IsAbstract.GetValueOrDefault())
                {
                    throw new ConfigFileException("IsStaticLookup and IsAbstract cannot both be true", path);
                }                
                if (!json.IsPersistable.GetValueOrDefault(true)
                    && (json.IsVersionable.GetValueOrDefault()
                        || json.IsAuditable.GetValueOrDefault()
                        || json.IsExtensible.GetValueOrDefault()
                        || json.IsInsertable.GetValueOrDefault()
                        || json.IsUpdatable.GetValueOrDefault()
                        || json.IsDeletable.GetValueOrDefault()
                        || json.IsMarkDeletable.GetValueOrDefault()))
                {
                    throw new ConfigFileException("IsPersistable is false but one of the following is true: IsVersionable, IsAuditable, IsExtensible, IsInsertable, IsUpdatable, IsDeletable, IsMarkDeletable", path);
                }
                if (json.IsStaticLookup.GetValueOrDefault()
                    && (json.IsVersionable.GetValueOrDefault()
                        || json.IsAuditable.GetValueOrDefault()
                        || json.IsExtensible.GetValueOrDefault()
                        || json.IsInsertable.GetValueOrDefault()
                        || json.IsUpdatable.GetValueOrDefault()
                        || json.IsDeletable.GetValueOrDefault()
                        || json.IsMarkDeletable.GetValueOrDefault()))
                {
                    throw new ConfigFileException("IsStaticLookup is false but one of the following is true: IsVersionable, IsAuditable, IsExtensible, IsInsertable, IsUpdatable, IsDeletable, IsMarkDeletable", path);
                }
                if (!json.IsStaticLookup.GetValueOrDefault()
                    && json.EnumValues.Any())
                {
                    throw new ConfigFileException("IsStaticLookup is false but EnumValues are present", path);
                }
                if (json.IsStaticLookup.GetValueOrDefault()
                    && !String.IsNullOrEmpty(json.InheritsFrom))
                {
                    throw new ConfigFileException("StaticLookup types cannot inherit from anything in", path);
                }
                if (!json.IsPersistable.GetValueOrDefault(true)
                    && json.Id != null)
                {
                    throw new ConfigFileException("Id can only be specific if IsPersistable is true", path);
                }
                if (json.Id != null
                    && json.Id.AutoGenerationBehavior.HasValue
                    && json.Id.AutoGenerationBehavior.Value == AutoGenerationBehaviorTypeEnum.None)
                {
                    throw new ConfigFileException("AutoGenerationBehavior of None is not supported", path);
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
                    throw new ConfigFileException("Invalid Id DataType", path);
                }
                if (json.Id != null
                    && json.Id.DataType.HasValue
                    && json.Id.DataType.Value == DataTypeEnum.String
                    && json.Id.IsNullable.GetValueOrDefault())
                {
                    throw new ConfigFileException("Id DataType of String cannot be Nullable" + path);
                }

                #endregion

                #region create DomainType for the file
                var domainType = new DomainType();
                jsonDictionary.Add(domainType, json);
                domainType.ConfigFilePath = path;

                domainType.IsAbstract = json.IsAbstract.GetValueOrDefault();
                domainType.IsPersistable = json.IsPersistable.GetValueOrDefault(true);
                domainType.IsStaticLookup = json.IsStaticLookup.GetValueOrDefault();
                domainType.IsVersionable = json.IsVersionable.GetValueOrDefault(domainType.IsPersistable && !domainType.IsStaticLookup);
                domainType.IsAuditable = json.IsAuditable.GetValueOrDefault(domainType.IsPersistable && !domainType.IsStaticLookup);
                domainType.IsExtensible = json.IsExtensible.GetValueOrDefault();
                domainType.IsInsertable = json.IsInsertable.GetValueOrDefault(domainType.IsPersistable && !domainType.IsStaticLookup);
                domainType.IsUpdatable = json.IsUpdatable.GetValueOrDefault(domainType.IsPersistable && !domainType.IsStaticLookup);
                domainType.IsDeletable = json.IsDeletable.GetValueOrDefault(domainType.IsPersistable && !domainType.IsStaticLookup);
                domainType.IsMarkDeletable = json.IsMarkDeletable.GetValueOrDefault();

                domainType.Name = Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(path));
                if (!IsValidTypeName(domainType.Name))
                {
                    throw new Exception(String.Format("Invalid resulting class name of '{0}'. Rename file to be in format 'ValidClassName.domain.zae': {1}", domainType.Name, domainType.ConfigFilePath));
                }
                var relativeDirectory = Path.GetDirectoryName(path).SubstringAfterLastInstanceOf("_config\\");
                var tempUniqueName = relativeDirectory.Replace('\\', '.') + "." + domainType.Name;
                Project destinationProject;
                if (tempUniqueName.StartsWith("_"))
                {
                    var projectSuffix = tempUniqueName
                        .SubstringAfterFirstInstanceOf("_")
                        .SubstringUntilFirstInstanceOf(".")
                        .Replace("_", ".");
                    destinationProject = _visualStudio.GetAllProjects().SingleOrDefault(o => o.FullName.EndsWith(projectSuffix + ".csproj", StringComparison.InvariantCultureIgnoreCase));
                    if (destinationProject == null)
                    {
                        throw new Exception("Could not find project ending with: " + projectSuffix);
                    }
                }
                else
                {
                    // default to Model
                    destinationProject = _visualStudio.GetAllProjects().SingleOrDefault(o => o.FullName.EndsWith("Model.csproj", StringComparison.InvariantCultureIgnoreCase));
                    if (destinationProject == null)
                    {
                        throw new Exception("Could not find project ending with 'Model'");
                    }
                }


                if (domainType.IsPersistable
                    && !destinationProject.FullName.EndsWith("Model.csproj", StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new Exception("Persistable DomainTypes must be in the Model project");
                }
                domainType.DestinationProjectFullName = destinationProject.FullName;

                String subNamespace = tempUniqueName;
                if (subNamespace.StartsWith("_"))
                {
                    subNamespace = subNamespace.SubstringAfterFirstInstanceOf(".");
                }
                subNamespace = subNamespace.SubstringUntilLastInstanceOf("." + domainType.Name)
                                           .SubstringUntilLastInstanceOf(domainType.Name);

                domainType.Namespace = destinationProject.GetDefaultNamespace() + (String.IsNullOrEmpty(subNamespace) ? "" : ".") + subNamespace;
    
                if (!IsValidNamespace(domainType.Namespace))
                {
                    throw new Exception(String.Format("Invalid resulting Namespace of '{0}' at: {1}", domainType.Namespace, domainType.ConfigFilePath));
                }
                if (!String.IsNullOrEmpty(json.PluralName))
                {
                    domainType.PluralName = json.PluralName;
                }
                else
                {
                    domainType.PluralName = GetPluralName(domainType.Name);
                }
                if (!IsValidTypeName(domainType.PluralName))
                {
                    throw new Exception(String.Format("Invalid resulting PluralName of '{0}' at: {1}", domainType.PluralName, domainType.ConfigFilePath));
                }
                if (json.EnumValues.Any())
                {
                    foreach (var enumValue in json.EnumValues)
                    {
                        domainType.EnumValues.Add(new EnumValue() { Id = Int32.Parse(enumValue.Id), Name = enumValue.Name });
                    }
                }

                app.DomainTypes.Add(domainType);
                #endregion
            }
            #endregion

            #region handle inheritance
            //
            foreach (var domainType in app.DomainTypes.Where(o => !String.IsNullOrEmpty(jsonDictionary[o].InheritsFrom)).ToList())
            {
                var inheritsFromFullNameTokens = jsonDictionary[domainType].InheritsFrom.Split('.').Reverse().ToList();
                var inheritsFromClassName = inheritsFromFullNameTokens.First();

                var potentialMatches = app.FindDomainTypes(jsonDictionary[domainType].InheritsFrom);
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
            foreach (var domainType in app.DomainTypes.Where(o => o.InheritedBy.Any() && o.InheritsFrom == null))
            {
                // these are the base-most domain types of each heirarchy
                this.AlignInterfacePropertiesInHeirarchy(domainType);
            }


            #region validate inheritance
            
            foreach (var domainType in app.DomainTypes.Where(o => o.InheritsFrom != null))
            {
                if (domainType.InheritsFrom.IsStaticLookup)
                {
                    throw new Exception("StaticLookups cannot be used as InheritsFrom in: " + domainType.ConfigFilePath);
                }
                if (domainType.InheritsFrom.IsExtendedEntityFieldValue)
                {
                    throw new Exception("CustomValue classes cannot be used as InheritsFrom in: " + domainType.ConfigFilePath);
                }
                if (jsonDictionary[domainType].Id != null)
                {
                    throw new Exception("Id, if defined, must be defined at the bottom of the Heirarchy: " + domainType.ConfigFilePath);
                }
            }

            #endregion

            #endregion

            #region create implicit DomainTypeInfos
            //
            var newDomainTypes = new List<DomainType>();
            foreach (var domainType in app.DomainTypes)
            {
                // validation checks for these have already been done- we can trust the config is right 
                // if we get to this point
                //
                if (domainType.IsExtensible
                    && (domainType.InheritsFrom == null
                        || !domainType.InheritsFrom.IsExtensible))
                {
                    var extendedFieldValueDomainType = new DomainType();
                    extendedFieldValueDomainType.DestinationProjectFullName = domainType.DestinationProjectFullName;
                    extendedFieldValueDomainType.Name = domainType.Name + "ExtendedFieldValue";
                    extendedFieldValueDomainType.PluralName = extendedFieldValueDomainType.Name + "s";
                    extendedFieldValueDomainType.Namespace = domainType.Namespace;
                    extendedFieldValueDomainType.IsPersistable = true;
                    extendedFieldValueDomainType.IsExtendedEntityFieldValue = true;
                    extendedFieldValueDomainType.Extends = domainType;
                    domainType.ExtendedBy = extendedFieldValueDomainType;

                    extendedFieldValueDomainType.IsVersionable = domainType.IsVersionable;
                    extendedFieldValueDomainType.IsAuditable = false; //domainType.IsAuditable;
                    extendedFieldValueDomainType.IsInsertable = domainType.IsInsertable;
                    extendedFieldValueDomainType.IsUpdatable = domainType.IsUpdatable;
                    extendedFieldValueDomainType.IsDeletable = domainType.IsDeletable;
                    extendedFieldValueDomainType.IsMarkDeletable = false; //domainType.IsMarkDeletable;

                    newDomainTypes.Add(extendedFieldValueDomainType);
                }
            }
            app.DomainTypes.AddRange(newDomainTypes);
            #endregion

            #region create implicit properties
            //
            // Id properties, which are ALWAYS at the bottom of the heirarchy
            //
            foreach (var domainType in app.DomainTypes.Where(o => o.IsPersistable && o.InheritsFrom == null))
            {
                var configToUse = domainType.IsExtendedEntityFieldValue
                    ? jsonDictionary[domainType.Extends.GetBaseMostDomainType()]
                    : jsonDictionary[domainType];

                var property = new DomainProperty();
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

            foreach (var domainType in app.DomainTypes.Where(o => o.IsVersionable && (o.InheritsFrom == null || !o.InheritsFrom.IsVersionable)))
            {
                var property = new DomainProperty();
                property.Name = "RowVersion";
                property.DataType = DataTypeEnum.ByteArray;
                property.IsRowVersion = true;
                property.AutoGenerationBehavior = AutoGenerationBehaviorTypeEnum.ByDataStore;
                domainType.Properties.Add(property);
                property.Owner = domainType;
            }
            foreach (var domainType in app.DomainTypes.Where(o => o.IsAuditable && (o.InheritsFrom == null || !o.InheritsFrom.IsAuditable)))
            {
                var property = new DomainProperty();
                property.Name = "CreatedUserId";
                property.IsForAuditableInterface = true;
                property.DataType = DataTypeEnum.String;
                property.AutoGenerationBehavior = AutoGenerationBehaviorTypeEnum.ByFramework;
                property.IsRequired = true;
                property.MaxLength = 256;
                property.MinLength = 1;
                property.UniquenessType = UniquenessTypeEnum.NotUnique;
                property.Owner = domainType;
                domainType.Properties.Add(property);

                property = new DomainProperty();
                property.Name = "UpdatedUserId";
                property.IsForAuditableInterface = true;
                property.DataType = DataTypeEnum.String;
                property.AutoGenerationBehavior = AutoGenerationBehaviorTypeEnum.ByFramework;
                property.IsRequired = true;
                property.MaxLength = 256;
                property.MinLength = 1;
                property.UniquenessType = UniquenessTypeEnum.NotUnique;
                property.Owner = domainType;
                domainType.Properties.Add(property);

                property = new DomainProperty();
                property.Name = "CreatedDate";
                property.IsForAuditableInterface = true;
                property.DataType = DataTypeEnum.DateTime;
                property.AutoGenerationBehavior = AutoGenerationBehaviorTypeEnum.ByFramework;
                property.IsNullable = true;
                property.IsRequired = true;
                property.UniquenessType = UniquenessTypeEnum.NotUnique;
                property.Owner = domainType;
                domainType.Properties.Add(property);

                property = new DomainProperty();
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

            foreach (var domainType in app.DomainTypes.Where(o => o.IsMarkDeletable && (o.InheritsFrom == null || !o.InheritsFrom.IsMarkDeletable)))
            {
                var property = new DomainProperty();
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
            foreach (var domainType in app.DomainTypes.Where(o => o.IsExtensible && (o.InheritsFrom == null || !o.InheritsFrom.IsExtensible)))
            {
                var fromProperty = new DomainProperty();
                fromProperty.Name = "ExtendedFieldValues";
                fromProperty.DataType = DataTypeEnum.Relationship;
                fromProperty.IsForExtensibleInterface = true;
                fromProperty.Owner = domainType;
                domainType.Properties.Add(fromProperty);

                var toProperty = new DomainProperty()
                {
                    Name = "ExtendedEntity",
                    DataType = DataTypeEnum.Relationship,
                    IsForExtendedEntityFieldValueInterface = true,
                    IsRequired = true,
                    Owner = domainType.ExtendedBy
                };
                domainType.ExtendedBy.Properties.Add(toProperty);

                var foreignKeyOnTo = new DomainProperty()
                {
                    Name = "ExtendedEntityId",
                    DataType = domainType.IdProperty.DataType,
                    IsForeignKey = true,
                    IsForExtendedEntityFieldValueInterface = true,
                    IsRequired = true,
                    Owner = domainType.ExtendedBy
                };
                domainType.ExtendedBy.Properties.Add(foreignKeyOnTo);

                var relationship = new Relationship()
                {
                    Type = RelationshipTypeEnum.OneToMany,
                    DeletionBehavior = RelationshipDeletionBehaviorTypeEnum.CascadeDelete,
                    From = domainType,
                    To = domainType.ExtendedBy,
                    NavigationPropertyOnFrom = fromProperty,
                    NavigationPropertyOnTo = toProperty,
                    ForeignKeyOnTo = foreignKeyOnTo
                };

                fromProperty.Relationship = relationship;
                toProperty.Relationship = relationship;
                foreignKeyOnTo.Relationship = relationship;
                domainType.Relationships.Add(relationship);
                domainType.ExtendedBy.Relationships.Add(relationship);

                // also the Value field
                var valueProperty = new DomainProperty()
                {
                    Name = "Value",
                    DataType = DataTypeEnum.String,
                    IsForExtendedEntityFieldValueInterface = true,
                    IsMaxLength = true,
                    Owner = domainType.ExtendedBy
                };
                domainType.ExtendedBy.Properties.Add(valueProperty);
            }
            foreach (var domainType in app.DomainTypes.Where(o => o.IsStaticLookup))
            {
                var nameProperty = new DomainProperty()
                {
                    Name = "Name",
                    IsForIsStaticLookupInterface = true,
                    DataType = DataTypeEnum.String,
                    IsRequired = true,
                    MinLength = 1,
                    MaxLength = 1024,
                    Owner = domainType
                };
                domainType.Properties.Add(nameProperty);
            }

            #endregion

            #region create Properties specified by the file
            foreach (var domainType in app.DomainTypes.Where(o => jsonDictionary.ContainsKey(o) && jsonDictionary[o].Properties.Any()))
            {
                foreach (var json in jsonDictionary[domainType].Properties)
                {
                    #region Property validation
                    if (String.IsNullOrWhiteSpace(json.Name))
                    {
                        throw new ConfigFileException("Invalid PropertyName: " + json.Name, domainType.ConfigFilePath);
                    }
                    Action verifyMinMaxLengthPropertiesNotUsed = delegate() {
                        if (!String.IsNullOrEmpty(json.MinLength)
                            || !String.IsNullOrEmpty(json.MaxLength)
                            || json.IsMaxLength.GetValueOrDefault())
                        {
                            throw new ConfigFileException("Cannot use MinLength, MaxLength or IsMaxLength with integer DataTypes: " + json.Name, domainType.ConfigFilePath);
                        }
                    }; 
                    Action verifyRelationshipPropertiesNotUsed = delegate()
                    {
                        if (json.Relationship != null)
                        {
                            throw new ConfigFileException("Cannot use RelationshipTo, NavigationPropertyName, RelationshipDeletionBehavior or RelationshipType with non-relationship DataTypes: " + json.Name, domainType.ConfigFilePath);
                        }
                    };
                    Action verifyNullablePropertyNotUsed = delegate()
                    {
                        if (json.IsNullable.GetValueOrDefault())
                        {
                            throw new ConfigFileException("Cannot use Nullable for specified DataType: " + json.Name, domainType.ConfigFilePath);
                        }
                    };
                    Action verifyMinMaxValuePropertiesNotUsed = delegate()
                    {
                        if (!String.IsNullOrEmpty(json.MinValue)
                            || !String.IsNullOrEmpty(json.MaxValue))
                        {
                            throw new ConfigFileException("Cannot use Nullable for specified DataType: " + json.Name, domainType.ConfigFilePath);
                        }
                    };
                    Action verifyPrecisionPropertiesNotUsed = delegate()
                    {
                        if (!String.IsNullOrEmpty(json.Precision))
                        {
                            throw new ConfigFileException("Cannot use Precision for specified DataType: " + json.Name, domainType.ConfigFilePath);
                        }
                    };
                    DataTypeEnum dataType = json.DataType.GetValueOrDefault(DataTypeEnum.String);
                    switch (dataType)
	                {
                        case DataTypeEnum.None:
                        case DataTypeEnum.Object:
                        case DataTypeEnum.SByte:
                        case DataTypeEnum.UShort:
                        case DataTypeEnum.UInt:
                        case DataTypeEnum.ULong:
                            throw new ConfigFileException("Unsupported or invalid Property DataType: " + json.Name, domainType.ConfigFilePath);
                        case DataTypeEnum.Byte:
                        case DataTypeEnum.Short:
                        case DataTypeEnum.Int:
                        case DataTypeEnum.Long:
	                        verifyMinMaxLengthPropertiesNotUsed();
	                        verifyRelationshipPropertiesNotUsed();
	                        verifyPrecisionPropertiesNotUsed();

                            // TODO: validate Min/Max Value
                            break;
                        case DataTypeEnum.Char:
                            verifyMinMaxLengthPropertiesNotUsed();
	                        verifyRelationshipPropertiesNotUsed();
	                        verifyMinMaxValuePropertiesNotUsed();
	                        verifyPrecisionPropertiesNotUsed();
                            break;
                        case DataTypeEnum.Boolean:
                            verifyMinMaxLengthPropertiesNotUsed();
	                        verifyRelationshipPropertiesNotUsed();
	                        verifyMinMaxValuePropertiesNotUsed();
	                        verifyPrecisionPropertiesNotUsed();
                            break;
                        case DataTypeEnum.Float:
                        case DataTypeEnum.Double:
                        case DataTypeEnum.Decimal:
                        case DataTypeEnum.Currency:
                        case DataTypeEnum.Percentage:
                            verifyMinMaxLengthPropertiesNotUsed();
	                        verifyRelationshipPropertiesNotUsed();

                            // TODO: validate Min/Max Value, Precision
                            break;
                        case DataTypeEnum.String:
                        case DataTypeEnum.EmailAddress:
                        case DataTypeEnum.Url:
                            verifyRelationshipPropertiesNotUsed();
	                        verifyNullablePropertyNotUsed();
	                        verifyMinMaxValuePropertiesNotUsed();
	                        verifyPrecisionPropertiesNotUsed();

                            // TODO: validate Min/Max Length, IsMaxLength
                            break;
                        case DataTypeEnum.Date:
                        case DataTypeEnum.Time:
                        case DataTypeEnum.DateTime:
                            verifyMinMaxLengthPropertiesNotUsed();
	                        verifyRelationshipPropertiesNotUsed();
	                        verifyPrecisionPropertiesNotUsed();

                            // TODO: validate Min/Max Value
                            break;
                        case DataTypeEnum.Relationship:
                            verifyMinMaxLengthPropertiesNotUsed();
	                        verifyRelationshipPropertiesNotUsed();
	                        verifyNullablePropertyNotUsed();
	                        verifyMinMaxValuePropertiesNotUsed();
	                        verifyPrecisionPropertiesNotUsed();

	                        if (json.Relationship == null)
	                        {
	                            throw new ConfigFileException("Relationsip Data missing for Relationship DataType", domainType.ConfigFilePath);
	                        }

                            // TODO: more validation here for actual relationship properties
                            break;
                        case DataTypeEnum.Guid:
                            verifyMinMaxLengthPropertiesNotUsed();
	                        verifyRelationshipPropertiesNotUsed();
	                        verifyMinMaxValuePropertiesNotUsed();
	                        verifyPrecisionPropertiesNotUsed();
                            break;
                        case DataTypeEnum.Image:
                        case DataTypeEnum.ByteArray:
                            verifyRelationshipPropertiesNotUsed();
	                        verifyNullablePropertyNotUsed();
	                        verifyMinMaxValuePropertiesNotUsed();
	                        verifyPrecisionPropertiesNotUsed();

                            // TODO: validate Min/Max Length, IsMaxLength
                            break;
                        default:
                            throw new ConfigFileException("Invalid Property DataType: " + json.Name, domainType.ConfigFilePath);
                    }
                    #endregion
                    
                    if (json.DataType != DataTypeEnum.Relationship)
                    {
                        // Value type
                        var property = new DomainProperty();
                        property.Name = json.Name;
                        property.DataType = json.DataType.GetValueOrDefault(DataTypeEnum.String);
                        property.AutoGenerationBehavior = AutoGenerationBehaviorTypeEnum.None;
                        property.IsNullable = json.IsNullable.GetValueOrDefault();
                        property.IsRequired = json.IsRequired.GetValueOrDefault();
                        property.IsMaxLength = json.IsMaxLength.GetValueOrDefault();
                        property.MinLength = String.IsNullOrEmpty(json.MinLength) ? 0 : Int64.Parse(json.MinLength);
                        property.MaxLength = String.IsNullOrEmpty(json.MaxLength) ? 0 : Int64.Parse(json.MaxLength);
                        property.MinValue = json.MinValue;
                        property.MaxValue = json.MaxValue;
                        property.Precision = json.Precision;
                        property.UniquenessType = json.Uniqueness.GetValueOrDefault(UniquenessTypeEnum.NotUnique);
                        property.Owner = domainType;
                        domainType.Properties.Add(property);
                    }
                    else
                    {
                        var potentialMatches = app.FindDomainTypes(json.Relationship.To);
                        if (!potentialMatches.Any())
                        {
                            throw new Exception("Could not find domain type matching To in: " + domainType.ConfigFilePath);
                        }
                        else if (potentialMatches.Count() > 1)
                        {
                            throw new Exception("Found more than 1 matching domain type for To in: " + domainType.ConfigFilePath);
                        }
                        var toEntity = potentialMatches.Single();
                        var fromEntity = domainType;
                        var fromProperty = new DomainProperty();
                        fromProperty.Name = json.Name;
                        fromProperty.DataType = DataTypeEnum.Relationship;
                        DomainProperty toProperty = null;
                        DomainProperty foreignKeyOnTo = null;
                        DomainProperty foreignKeyOnFrom = null;
                        var relationship = new Relationship();
                        
                        // TODO: support the rest of the relationship types

                        switch (json.Relationship.Type.GetValueOrDefault(RelationshipTypeEnum.None))
                        {
                            case RelationshipTypeEnum.None:
                                throw new ConfigFileException("Invalid RelationShipType: " + json.Name, domainType.ConfigFilePath);
                            case RelationshipTypeEnum.OneToMany:

                                // collection property on the from

                                if (!String.IsNullOrEmpty(json.Relationship.ToPropertyName))
                                {
                                    toProperty = new DomainProperty();
                                    toProperty.Name = json.Relationship.ToPropertyName;
                                    toProperty.DataType = DataTypeEnum.Relationship;
                                    toProperty.IsRequired = json.Relationship.ToProperyIsRequired.GetValueOrDefault();
                                    toProperty.UniquenessType =
                                        json.Relationship.ToPropertyUniqueness.GetValueOrDefault(
                                            UniquenessTypeEnum.NotUnique);

                                    foreignKeyOnTo = new DomainProperty();
                                    foreignKeyOnTo.Name = json.Relationship.ToPropertyName + "Id";
                                    foreignKeyOnTo.DataType = fromEntity.IdProperty.DataType;
                                    foreignKeyOnTo.IsForeignKey = true;
                                    foreignKeyOnTo.IsRequired = json.Relationship.ToProperyIsRequired.GetValueOrDefault();
                                    foreignKeyOnTo.IsNullable = foreignKeyOnTo.IsRequired;
                                    foreignKeyOnTo.UniquenessType = toProperty.UniquenessType;
                                }

                                relationship.Type = RelationshipTypeEnum.OneToMany;
                                relationship.DeletionBehavior = json.Relationship.DeletionBehavior.GetValueOrDefault(RelationshipDeletionBehaviorTypeEnum.CascadeDelete);
       
                                
                                break;
                            case RelationshipTypeEnum.ManyToOne:

                                // collection property on the to
                                
                                fromProperty.IsRequired = json.IsRequired.GetValueOrDefault();
                                fromProperty.UniquenessType = json.Uniqueness.GetValueOrDefault(UniquenessTypeEnum.NotUnique);

                                foreignKeyOnFrom = new DomainProperty();
                                foreignKeyOnFrom.Name = json.Relationship.ToPropertyName + "Id";
                                foreignKeyOnFrom.DataType = fromEntity.IdProperty.DataType;
                                foreignKeyOnFrom.IsForeignKey = true;
                                foreignKeyOnFrom.IsRequired = json.IsRequired.GetValueOrDefault();
                                foreignKeyOnFrom.IsNullable = foreignKeyOnFrom.IsRequired;
                                foreignKeyOnFrom.UniquenessType = fromProperty.UniquenessType;

                                if (!String.IsNullOrEmpty(json.Relationship.ToPropertyName))
                                {
                                    toProperty = new DomainProperty();
                                    toProperty.Name = json.Relationship.ToPropertyName;
                                    toProperty.DataType = DataTypeEnum.Relationship;
                                }

                                break;
                        }
                        relationship.From = fromEntity;
                        relationship.To = toEntity;
                        relationship.Type = json.Relationship.Type.GetValueOrDefault();
                        relationship.DeletionBehavior = json.Relationship.DeletionBehavior.GetValueOrDefault(
                                RelationshipDeletionBehaviorTypeEnum.Restricted);
                        relationship.NavigationPropertyOnFrom = fromProperty;
                        relationship.NavigationPropertyOnTo = toProperty;
                        relationship.ForeignKeyOnTo = foreignKeyOnTo;
                        relationship.ForeignKeyOnFrom = foreignKeyOnFrom;

                        fromProperty.Relationship = relationship;
                        fromProperty.Owner = fromEntity;
                        fromEntity.Properties.Add(fromProperty);
                        if (foreignKeyOnFrom != null)
                        {
                            foreignKeyOnFrom.Relationship = relationship;
                            foreignKeyOnFrom.Owner = fromEntity;
                            fromEntity.Properties.Add(foreignKeyOnFrom);
                        }
                        if (toProperty != null)
                        {
                            toProperty.Relationship = relationship;
                            toProperty.Owner = toEntity;
                            toEntity.Properties.Add(toProperty);
                        }
                        if (foreignKeyOnTo != null)
                        {
                            foreignKeyOnTo.Relationship = relationship;
                            foreignKeyOnTo.Owner = toEntity;
                            toEntity.Properties.Add(foreignKeyOnTo);
                        }
                        fromEntity.Relationships.Add(relationship);
                        toEntity.Relationships.Add(relationship);
                    }
                }
            }

            #endregion

            #region Set all of the DataTypeString properties of the DomainPropertyInfo

            foreach (var domainType in app.DomainTypes)
            {
                foreach (var property in domainType.Properties)
                {
                    switch (property.DataType)
                    {
                        case DataTypeEnum.SByte:
                        case DataTypeEnum.Byte:
                        case DataTypeEnum.Char:
                        case DataTypeEnum.Short:
                        case DataTypeEnum.UShort:
                        case DataTypeEnum.Int:
                        case DataTypeEnum.UInt:
                        case DataTypeEnum.Long:
                        case DataTypeEnum.ULong:
                        case DataTypeEnum.Float:
                        case DataTypeEnum.Double:
                        case DataTypeEnum.Decimal:
                            property.DataTypeString = property.IsNullable 
                                ? property.DataType.ToString().ToLowerInvariant() + "?" 
                                : property.DataType.ToString().ToLowerInvariant();
                            break;
                        case DataTypeEnum.String:
                        case DataTypeEnum.EmailAddress:
                        case DataTypeEnum.Url:
                            property.DataTypeString = "string";
                            break;
                        case DataTypeEnum.Boolean:
                            property.DataTypeString = property.IsNullable ? "bool?" : "bool";
                            break;
                        case DataTypeEnum.Date:
                        case DataTypeEnum.Time:
                        case DataTypeEnum.DateTime:
                            property.DataTypeString = property.IsNullable ? "DateTime?" : "DateTime";
                            break;
                        case DataTypeEnum.Currency:
                        case DataTypeEnum.Percentage:
                            property.DataTypeString = property.IsNullable ? "decimal?" : "decimal";
                            break;
                        case DataTypeEnum.Relationship:
                            if (property.Owner == property.Relationship.From)
                            {
                                if (property.Relationship.Type == RelationshipTypeEnum.OneToMany)
                                {
                                    property.DataTypeString = String.Format("System.Collections.Generic.IList<{0}>", property.Relationship.To.FullName);
                                    property.InitializationDataTypeString = String.Format("System.Collections.Generic.List<{0}>", property.Relationship.To.FullName);
                                }
                                else
                                {
                                    property.DataTypeString = property.Relationship.To.FullName;
                                }
                            }
                            else
                            {
                                if (property.Relationship.Type == RelationshipTypeEnum.OneToMany)
                                {
                                    property.DataTypeString = property.Relationship.From.FullName;
                                }
                                else
                                {
                                    property.DataTypeString = String.Format("System.Collections.Generic.IList<{0}>", property.Relationship.From.FullName);
                                    property.InitializationDataTypeString = String.Format("System.Collections.Generic.List<{0}>", property.Relationship.From.FullName);
                                }
                            }
                            break;
                        case DataTypeEnum.Guid:
                            property.DataTypeString = property.IsNullable ? "Guid?" : "Guid";
                            break;
                        case DataTypeEnum.Image:
                        case DataTypeEnum.ByteArray:
                            property.DataTypeString = "byte[]";
                            break;
                        case DataTypeEnum.Object:
                        default:
                            break;
                    }
                }
            }
            

            #endregion

            #region validate there are no name conflicts
            //
            if (app.DomainTypes.GroupBy(p => p.FullName).Where(g => g.Count() > 1).Any())
            {
                throw new Exception("2 DomainTypes with the same name resulted");
            }
            foreach (var domainType in app.DomainTypes)
            {
                if (domainType.GetAllPropertiesIncludingInherited().GroupBy(p => p.Name).Where(g => g.Count() > 1).Any())
                {
                    throw new Exception("2 Properties with the same name resulted in: " + domainType.ConfigFilePath);
                }
            }
            #endregion
        }

        private void AlignInterfacePropertiesInHeirarchy(DomainType domainType)
        {
            foreach (var child in domainType.InheritedBy)
            {
                // if inheriting an interface, then this class HAS to have it
                //
                child.IsVersionable = domainType.IsVersionable || child.IsVersionable;
                child.IsAuditable = domainType.IsAuditable || child.IsAuditable;
                child.IsExtensible = domainType.IsExtensible || child.IsExtensible;
                child.IsMarkDeletable = domainType.IsMarkDeletable || child.IsMarkDeletable;

                this.AlignInterfacePropertiesInHeirarchy(child);
                // TODO: if we start using IsDeletable, IsUpdatable, IsInsertable as interfaces on the domain type, then we need to apply those here
            }
        }

        private String GetPluralName(String className)
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

        private bool IsValidTypeName(String typeName)
        {
            return CodeGenerator.IsValidLanguageIndependentIdentifier(typeName);
        }

        private bool IsValidNamespace(String nameSpace)
        {
            if (String.IsNullOrWhiteSpace(nameSpace))
            {
                return false;
            }
            var tokens = nameSpace.Split('.');
            foreach (var token in tokens)
            {
                if (!IsValidTypeName(token))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
