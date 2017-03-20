namespace Zirpl.AppEngine.VisualStudioAutomation.TextTemplating
{
    public class DotNetTypeOutputInfo : OutputInfo
    {
        public string Namespace { get; set; }

        public string TypeName => FileNameWithoutExtension;

        public string FullTypeName
        {
            get
            {
                var nameSpace = Namespace;
                var typeName = TypeName;
                if (!string.IsNullOrEmpty(nameSpace)
                    && !string.IsNullOrEmpty(typeName))
                {
                    return nameSpace + "." + typeName;
                }
                // one is empty, so no . needed
                return nameSpace + typeName;
            }
        }
    }
}
