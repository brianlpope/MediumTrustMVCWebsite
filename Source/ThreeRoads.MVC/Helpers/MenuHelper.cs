using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ThreeRoads.MVC.Helpers
{
    public static class MenuExtension
    {
        public static MvcHtmlString Menu(this HtmlHelper helper, int levels, bool useImages = false)
        {
            var sb = new StringBuilder();

            // Create opening unordered list tag
            TagBuilder tagBuilder = new TagBuilder("div");
            tagBuilder.AddCssClass("demo");
            tagBuilder.Attributes.Add("id", "accordion");

            // Render each top level node
            var topLevelNodes = SiteMap.RootNode.ChildNodes;
            foreach (SiteMapNode node in topLevelNodes)
            {
                sb.Append("<h3>");
                sb.AppendFormat("<a href='{0}'>{1}</a>", node.Url, useImages ? "" : helper.Encode(node.Title));
                sb.AppendLine("</h3>");

                sb.AppendLine("<div>");

                if (levels == 2 && node.HasChildNodes)
                {
                    sb.AppendLine("<ul>");
                    foreach (SiteMapNode childNode in node.ChildNodes)
                    {
                        sb.Append("<li>");

                        sb.AppendFormat("<a href='{0}'>{1}</a>", childNode.Url,
                                        useImages ? "" : helper.Encode(childNode.Title));
                        sb.AppendLine("</li>");
                    }
                    sb.AppendLine("</ul>");
                }

                sb.AppendLine("</div>");
            }

            tagBuilder.InnerHtml = sb.ToString();


            return MvcHtmlString.Create(tagBuilder.ToString());
        }

    }

}