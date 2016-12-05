using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebSinger.Helpers
{
    public static class SortHelper
    {
        public static IHtmlString SortIdentifier(this HtmlHelper htmlHelper, string sortName, string field, bool isDesc)
        {
            string glyph;

            if (sortName == field)
            {
                glyph = isDesc ? "glyphicon glyphicon-chevron-down" : "glyphicon glyphicon-chevron-up";
            }
            else
            {
                glyph = "glyphicon glyphicon-sort";
            }

            var span = new TagBuilder("span");
            span.Attributes["class"] = glyph;

            return MvcHtmlString.Create(span.ToString());
        }

        public static RouteValueDictionary ToRouteValueDictionary(this HtmlHelper htmlHelper, string currentNameSort,
            bool currentIsDesc, string newNameSort)
        {
            var routeValueDictionary = new RouteValueDictionary();

            routeValueDictionary.Add("page", 1);
            routeValueDictionary.Add("name", newNameSort);

            if (currentNameSort == newNameSort)
                routeValueDictionary.Add("isDesc", !currentIsDesc);
            else
            {
                routeValueDictionary.Add("isDesc", true);
            }

            return routeValueDictionary;
        }
    }
}