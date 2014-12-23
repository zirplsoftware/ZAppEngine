﻿using System;
using System.Text;
using EnvDTE;

namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public interface IOutputFileManager : IDisposable
    {
        void UseDefaultFile(ITransform currentTransform);
        void StartFile(ITransform currentTransform, OutputInfo file);
        void EndFile();

        void StartCSharpFile(ITransform currentTransform, String fileName, Project destinationProject = null);
        void StartCSharpFile(ITransform currentTransform, String fileName, String folderWithinProject = null, Project destinationProject = null);
        void StartCSharpFile(ITransform currentTransform, String fileName, String folderWithinProject = null, String destinationProjectName = null);
        void StartFile(ITransform currentTransform, String fileName, String folderWithinProject = null, String destinationProjectName = null, BuildActionTypeEnum? buildAction = null, String customTool = null, bool? autoFormat = null, bool? overwrite = null, Encoding encoding = null);
        void StartFile(ITransform currentTransform, String fileName, String folderWithinProject = null,Project destinationProject = null, BuildActionTypeEnum? buildAction = null, String customTool = null,bool? autoFormat = null, bool? overwrite = null, Encoding encoding = null);
    }
}