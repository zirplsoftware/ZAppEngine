﻿#if !PORTABLE
using System;
using System.IO;

namespace Zirpl.IO
{
    public static class PathUtilities
    {
        public static String AppendCurrentDateTimeToFileName(String filePath)
        {
            const String template = "{0}_{1:yyyyMMdd-HHmmss}{2}";
            String fileNameWithoutExtension = String.Format(
                template,
                Path.GetFileNameWithoutExtension(filePath),
                DateTime.Now,
                Path.GetExtension(filePath));
            return Path.Combine(Path.GetDirectoryName(filePath), fileNameWithoutExtension);
        }

        public static bool EnsureDirectoryExists(String filePath)
        {
            bool hadToCreate = false;
            String backupDirectory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(backupDirectory))
            {
                hadToCreate = true;
                Directory.CreateDirectory(backupDirectory);
            }
            return hadToCreate;
        }

        public static String AppendDirectory(String filePath, String directoriesDeeper, Boolean keepFile)
        {
            String newPath = null;

            String directory = Path.GetDirectoryName(filePath);
            String fileName = Path.GetFileName(filePath);

            newPath = Path.Combine(directory, directoriesDeeper);
            if (keepFile
                && !String.IsNullOrEmpty(fileName))
            {
                newPath = Path.Combine(newPath, fileName);//String.Format("{0}{1}{2}", newPath, Path.PathSeparator, fileName);
            }
            return newPath;
        }
    }
}
#endif