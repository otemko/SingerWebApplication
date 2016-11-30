using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSinger.Helpers
{
    public static class TextHelper
    {
        public static MvcHtmlString AddText(this HtmlHelper html, string item)
        {
            TagBuilder div = new TagBuilder("div");
            div.InnerHtml = item;
            return new MvcHtmlString(div.ToString());
        }
    }
}