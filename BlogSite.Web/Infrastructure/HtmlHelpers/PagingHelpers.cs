using System;
using System.Text;
using System.Web.Mvc;

using BlogSite.Web.Models;

namespace BlogSite.Web.Infrastructure.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("li");
                TagBuilder link = new TagBuilder("a");
                link.MergeAttribute("href", "#");
                link.MergeAttribute("value", i.ToString());
                link.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                {
                    link.AddCssClass("active");
                }
                tag.InnerHtml = link.ToString();
                result.Append(tag);
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}