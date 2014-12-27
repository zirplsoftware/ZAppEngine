namespace Zirpl
{
    public static class ObjectExtensions
    {
        public static T Or<T>(this T obj, T replacement)
        {
            return obj != null ? obj : replacement;
        }
    }
}
