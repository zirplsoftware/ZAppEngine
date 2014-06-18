using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace Zirpl.AppEngine.Web.Mvc.Html
{
    public static class LinkExtensions
    {
        public static MvcHtmlString ActionLinkDoNotPreserveRouteValues(this HtmlHelper htmlHelper, string linkText, string actionName)
        {
            return htmlHelper.ActionLinkDoNotPreserveRouteValues(linkText, actionName, null, new RouteValueDictionary(), new RouteValueDictionary());
        }

        public static MvcHtmlString ActionLinkDoNotPreserveRouteValues(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues)
        {
            return htmlHelper.ActionLinkDoNotPreserveRouteValues(linkText, actionName, null, new RouteValueDictionary(routeValues), new RouteValueDictionary());
        }

        public static MvcHtmlString ActionLinkDoNotPreserveRouteValues(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName)
        {
            return htmlHelper.ActionLinkDoNotPreserveRouteValues(linkText, actionName, controllerName, new RouteValueDictionary(), new RouteValueDictionary());
        }

        public static MvcHtmlString ActionLinkDoNotPreserveRouteValues(this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues)
        {
            return htmlHelper.ActionLinkDoNotPreserveRouteValues(linkText, actionName, null, routeValues, new RouteValueDictionary());
        }

        public static MvcHtmlString ActionLinkDoNotPreserveRouteValues(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues, object htmlAttributes)
        {
            return htmlHelper.ActionLinkDoNotPreserveRouteValues(linkText, actionName, null, new RouteValueDictionary(routeValues), HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString ActionLinkDoNotPreserveRouteValues(this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            return htmlHelper.ActionLinkDoNotPreserveRouteValues(linkText, actionName, null, routeValues, htmlAttributes);
        }

        public static MvcHtmlString ActionLinkDoNotPreserveRouteValues(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes)
        {
            return htmlHelper.ActionLinkDoNotPreserveRouteValues(linkText, actionName, controllerName, new RouteValueDictionary(routeValues), HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString ActionLinkDoNotPreserveRouteValues(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            if (string.IsNullOrEmpty(linkText))
            {
                throw new ArgumentException("", "linkText");
            }

            // WAY internally, code merges the routeValues passed in with the existing RouteData on the RequestContext.
            // this method provides this way of IGNORING the existing route values and only using the ones passed in explicitly
            //
            var requestContext = new RequestContext(htmlHelper.ViewContext.RequestContext.HttpContext, new RouteData());
            return MvcHtmlString.Create(HtmlHelper.GenerateLink(requestContext, htmlHelper.RouteCollection, linkText, null, actionName, controllerName, routeValues, htmlAttributes));
            //return MvcHtmlString.Create(HtmlHelper.GenerateLink(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection, linkText, null, actionName, controllerName, routeValues, htmlAttributes));
        }

        public static MvcHtmlString ActionLinkDoNotPreserveRouteValues(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes)
        {
            return htmlHelper.ActionLinkDoNotPreserveRouteValues(linkText, actionName, controllerName, protocol, hostName, fragment, new RouteValueDictionary(routeValues), HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString ActionLinkDoNotPreserveRouteValues(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            if (string.IsNullOrEmpty(linkText))
            {
                throw new ArgumentException("", "linkText");
            }

            // WAY internally, code merges the routeValues passed in with the existing RouteData on the RequestContext.
            // this method provides this way of IGNORING the existing route values and only using the ones passed in explicitly
            //
            var requestContext = new RequestContext(htmlHelper.ViewContext.RequestContext.HttpContext, new RouteData());
            return MvcHtmlString.Create(HtmlHelper.GenerateLink(requestContext, htmlHelper.RouteCollection, linkText, null, actionName, controllerName, protocol, hostName, fragment, routeValues, htmlAttributes));
            //return MvcHtmlString.Create(HtmlHelper.GenerateLink(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection, linkText, null, actionName, controllerName, protocol, hostName, fragment, routeValues, htmlAttributes));
        }

        public static MvcHtmlString RouteLinkDoNotPreserveRouteValues(this HtmlHelper htmlHelper, string linkText, object routeValues)
        {
            return htmlHelper.RouteLinkDoNotPreserveRouteValues(linkText, new RouteValueDictionary(routeValues));
        }

        public static MvcHtmlString RouteLinkDoNotPreserveRouteValues(this HtmlHelper htmlHelper, string linkText, string routeName)
        {
            return htmlHelper.RouteLinkDoNotPreserveRouteValues(linkText, routeName, null);
        }

        public static MvcHtmlString RouteLinkDoNotPreserveRouteValues(this HtmlHelper htmlHelper, string linkText, RouteValueDictionary routeValues)
        {
            return htmlHelper.RouteLinkDoNotPreserveRouteValues(linkText, routeValues, new RouteValueDictionary());
        }

        public static MvcHtmlString RouteLinkDoNotPreserveRouteValues(this HtmlHelper htmlHelper, string linkText, object routeValues, object htmlAttributes)
        {
            return htmlHelper.RouteLinkDoNotPreserveRouteValues(linkText, new RouteValueDictionary(routeValues), HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString RouteLinkDoNotPreserveRouteValues(this HtmlHelper htmlHelper, string linkText, string routeName, object routeValues)
        {
            return htmlHelper.RouteLinkDoNotPreserveRouteValues(linkText, routeName, new RouteValueDictionary(routeValues));
        }

        public static MvcHtmlString RouteLinkDoNotPreserveRouteValues(this HtmlHelper htmlHelper, string linkText, string routeName, RouteValueDictionary routeValues)
        {
            return htmlHelper.RouteLinkDoNotPreserveRouteValues(linkText, routeName, routeValues, new RouteValueDictionary());
        }

        public static MvcHtmlString RouteLinkDoNotPreserveRouteValues(this HtmlHelper htmlHelper, string linkText, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            return htmlHelper.RouteLinkDoNotPreserveRouteValues(linkText, null, routeValues, htmlAttributes);
        }

        public static MvcHtmlString RouteLinkDoNotPreserveRouteValues(this HtmlHelper htmlHelper, string linkText, string routeName, object routeValues, object htmlAttributes)
        {
            return htmlHelper.RouteLinkDoNotPreserveRouteValues(linkText, routeName, new RouteValueDictionary(routeValues), HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString RouteLinkDoNotPreserveRouteValues(this HtmlHelper htmlHelper, string linkText, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            if (string.IsNullOrEmpty(linkText))
            {
                throw new ArgumentException("", "linkText");
            }

            // WAY internally, code merges the routeValues passed in with the existing RouteData on the RequestContext.
            // this method provides this way of IGNORING the existing route values and only using the ones passed in explicitly
            //
            var requestContext = new RequestContext(htmlHelper.ViewContext.RequestContext.HttpContext, new RouteData());
            return MvcHtmlString.Create(HtmlHelper.GenerateRouteLink(requestContext, htmlHelper.RouteCollection, linkText, routeName, routeValues, htmlAttributes));
            //return MvcHtmlString.Create(HtmlHelper.GenerateRouteLink(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection, linkText, routeName, routeValues, htmlAttributes));
        }

        public static MvcHtmlString RouteLinkDoNotPreserveRouteValues(this HtmlHelper htmlHelper, string linkText, string routeName, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes)
        {
            return htmlHelper.RouteLinkDoNotPreserveRouteValues(linkText, routeName, protocol, hostName, fragment, new RouteValueDictionary(routeValues), HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString RouteLinkDoNotPreserveRouteValues(this HtmlHelper htmlHelper, string linkText, string routeName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            if (string.IsNullOrEmpty(linkText))
            {
                throw new ArgumentException("", "linkText");
            }

            // WAY internally, code merges the routeValues passed in with the existing RouteData on the RequestContext.
            // this method provides this way of IGNORING the existing route values and only using the ones passed in explicitly
            //
            var requestContext = new RequestContext(htmlHelper.ViewContext.RequestContext.HttpContext, new RouteData());
            return MvcHtmlString.Create(HtmlHelper.GenerateRouteLink(requestContext, htmlHelper.RouteCollection, linkText, routeName, protocol, hostName, fragment, routeValues, htmlAttributes));
            //return MvcHtmlString.Create(HtmlHelper.GenerateRouteLink(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection, linkText, routeName, protocol, hostName, fragment, routeValues, htmlAttributes));
        }
    }

}
