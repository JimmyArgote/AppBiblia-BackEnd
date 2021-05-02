using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BibliaFrontEnd
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{livro}/{capitulo}/{versiculo}",
                defaults: new
                {
                    controller = "Livros",
                    action = "Index",
                    livro = UrlParameter.Optional,
                    capitulo = UrlParameter.Optional,
                    versiculo = UrlParameter.Optional
                }
            ).RouteHandler = new MyRouteHander();

            routes.MapRoute(
                name: "Search",
                url: "{controller}/{palavra:string}",
                defaults: new
                {
                    controller = "Search",
                    action = "Index",
                    palavra = UrlParameter.Optional
                }
            ).RouteHandler = new MyRouteHander();
            /*
            routes.MapRoute(
                name: "Biblia",
                url: "{controller}/{action}/{versiculo}",
                defaults: new
                {
                    controller = "Livro",
                    action = UrlParameter.Optional,
                    versiculo = UrlParameter.Optional,
                    standardId = UrlParameter.Optional
                },
                constraints: new
                {
                    versao = new VersaoRouteConstraint()
                }
            );
            */
        }

        /*
        private class VersaoRouteConstraint : IRouteConstraint
        {
            public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
            {
                //these would usually come from a database, or cache.
                var livros = new[] { "restaurants", "cafes", "bistros" };

                if (values[parameterName] == null)
                    return false;

                //get the category passed in to the route
                var livro = values[parameterName].ToString();

                //now we check our categories, and see if it exists
                return livros.Any(x => x == livro.ToLower());

                // url such as /restaurants/Camberley--Surrey will match
                // url such as /pubs/Camberley--Surrey will not
            }
        }
        */

        
        public class MyRouteHander : IRouteHandler
        {
            public IHttpHandler GetHttpHandler(RequestContext requestContext)
            {
                var rd = requestContext.RouteData;
                var action = rd.GetRequiredString("action");
                var controller = rd.GetRequiredString("controller");
                if (string.Equals(action, "ListarCapitulos", StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(controller, "Livros", StringComparison.OrdinalIgnoreCase))
                {
                    // The action name is dynamic
                    string actionName = "ListarCapitulos";
                    requestContext.RouteData.Values["action"] = actionName;
                }
                return new MvcHandler(requestContext);
            }
        }
        
    }
}
