using System.Text;

namespace Zirpl.AppEngine.Text
{
    public static class StringBuilderExtensions
    {
        public static void AppendIfNotEmpty(this StringBuilder sb, object val)
        {
            if (sb.Length > 0
                && val != null)
            {
                sb.Append(val);
            }
        }
    }
}
