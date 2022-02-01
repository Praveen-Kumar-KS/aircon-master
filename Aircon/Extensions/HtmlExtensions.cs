using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aircon.Extensions
{
    /// <summary>
    /// HTML extensions
    /// </summary>
    public static class HtmlExtensions
    {

        #region Form fields

        /// <summary>
        /// Generate hint control
        /// </summary>
        /// <param name="helper">HTML helper</param>
        /// <param name="value">TexHint text</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the result
        /// </returns>
        public static async Task<IHtmlContent> HintAsync(this IHtmlHelper helper, string value)
        {
            //create tag builder
            var builder = new TagBuilder("div");
            builder.MergeAttribute("title", value);
            builder.MergeAttribute("class", "ico-help");
            builder.MergeAttribute("data-toggle", "tooltip");
            var icon = new StringBuilder();
            icon.Append("<i class='fas fa-question-circle'></i>");
            builder.InnerHtml.AppendHtml(icon.ToString());

            //render tag
            return new HtmlString(await builder.RenderHtmlContentAsync());
        }

        #endregion


        #region Common extensions

        /// <summary>
        /// Convert IHtmlContent to string
        /// </summary>
        /// <param name="htmlContent">HTML content</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the result
        /// </returns>
        public static async Task<string> RenderHtmlContentAsync(this IHtmlContent htmlContent)
        {
            await using var writer = new StringWriter();
            htmlContent.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }

        #endregion
    }
}