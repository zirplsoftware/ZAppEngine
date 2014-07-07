﻿using Zirpl.AppEngine.CodeGeneration.TextTemplating;

namespace Zirpl.AppEngine.CodeGeneration.V1.Templates
{
	public partial class DataContextTemplate: IPreprocessedTextTransformation
    {
        public DataContextTemplate(TransformationHelper transformationHelper)
        {
            this.TransformationHelper = transformationHelper;
            this.Host = this.TransformationHelper.CallingTemplate.Host;
            this.GenerationEnvironment = this.TransformationHelper.CallingTemplate.GenerationEnvironment;
        }

        public TransformationHelper TransformationHelper { get; private set; }
    }
	public partial class DataServiceInterfaceTemplate: IPreprocessedTextTransformation
    {
        public DataServiceInterfaceTemplate(TransformationHelper transformationHelper)
        {
            this.TransformationHelper = transformationHelper;
            this.Host = this.TransformationHelper.CallingTemplate.Host;
            this.GenerationEnvironment = this.TransformationHelper.CallingTemplate.GenerationEnvironment;
        }

        public TransformationHelper TransformationHelper { get; private set; }
    }
	public partial class DataServiceTemplate: IPreprocessedTextTransformation
    {
        public DataServiceTemplate(TransformationHelper transformationHelper)
        {
            this.TransformationHelper = transformationHelper;
            this.Host = this.TransformationHelper.CallingTemplate.Host;
            this.GenerationEnvironment = this.TransformationHelper.CallingTemplate.GenerationEnvironment;
        }

        public TransformationHelper TransformationHelper { get; private set; }
    }
	public partial class EntityFrameworkMappingTemplate: IPreprocessedTextTransformation
    {
        public EntityFrameworkMappingTemplate(TransformationHelper transformationHelper)
        {
            this.TransformationHelper = transformationHelper;
            this.Host = this.TransformationHelper.CallingTemplate.Host;
            this.GenerationEnvironment = this.TransformationHelper.CallingTemplate.GenerationEnvironment;
        }

        public TransformationHelper TransformationHelper { get; private set; }
    }
	public partial class ModelEnumTemplate: IPreprocessedTextTransformation
    {
        public ModelEnumTemplate(TransformationHelper transformationHelper)
        {
            this.TransformationHelper = transformationHelper;
            this.Host = this.TransformationHelper.CallingTemplate.Host;
            this.GenerationEnvironment = this.TransformationHelper.CallingTemplate.GenerationEnvironment;
        }

        public TransformationHelper TransformationHelper { get; private set; }
    }
	public partial class ModelMetadataTemplate: IPreprocessedTextTransformation
    {
        public ModelMetadataTemplate(TransformationHelper transformationHelper)
        {
            this.TransformationHelper = transformationHelper;
            this.Host = this.TransformationHelper.CallingTemplate.Host;
            this.GenerationEnvironment = this.TransformationHelper.CallingTemplate.GenerationEnvironment;
        }

        public TransformationHelper TransformationHelper { get; private set; }
    }
	public partial class ModelTemplate: IPreprocessedTextTransformation
    {
        public ModelTemplate(TransformationHelper transformationHelper)
        {
            this.TransformationHelper = transformationHelper;
            this.Host = this.TransformationHelper.CallingTemplate.Host;
            this.GenerationEnvironment = this.TransformationHelper.CallingTemplate.GenerationEnvironment;
        }

        public TransformationHelper TransformationHelper { get; private set; }
    }
}
