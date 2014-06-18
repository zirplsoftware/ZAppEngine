namespace Zirpl.AppEngine.Web.Mvc.Ioc.Autofac
{
    public static class IocUtils
    {
        public static IAutofacWebMvcDependencyResolver DependencyResolver
        {
            get { return WebDependencyResolver.Instance; }
        }
    }
}
