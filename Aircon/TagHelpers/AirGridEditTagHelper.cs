using System;
using System.Threading.Tasks;
using Aircon.Extensions;
using Aircon.Models;
using Aircon.Providers;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;

namespace Aircon.TagHelpers
{

    /// <summary>
    /// "nop-delete-confirmation" tag helper
    /// </summary>
    [HtmlTargetElement("air-grid-edit", Attributes = CLASS_ID_ATTRIBUTE_NAME + "," + PARTIAL_ATTRIBUTE_NAME, TagStructure = TagStructure.WithoutEndTag)]
    public class AirGridEditTagHelper : TagHelper
    {
        #region Constants

        private const string CLASS_ID_ATTRIBUTE_NAME = "asp-class-id";
        private const string PARTIAL_ATTRIBUTE_NAME = "asp-partial";

        #endregion

        #region Properties

        protected IHtmlGenerator Generator { get; set; }


        /// <summary>
        /// Button identifier
        /// </summary>
        [HtmlAttributeName(CLASS_ID_ATTRIBUTE_NAME)]
        public string ClassId { get; set; }

        /// <summary>
        /// Delete action name
        /// </summary>
        [HtmlAttributeName(PARTIAL_ATTRIBUTE_NAME)]
        public string Partial { get; set; }




        /// <summary>
        /// ViewContext
        /// </summary>
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        #endregion

        #region Fields

        private readonly IHtmlHelper _htmlHelper;

        #endregion

        #region Ctor

        public AirGridEditTagHelper(IHtmlGenerator generator, IHtmlHelper htmlHelper)
        {
            Generator = generator;
            _htmlHelper = htmlHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Asynchronously executes the tag helper with the given context and output
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            //contextualize IHtmlHelper
            var viewContextAware = _htmlHelper as IViewContextAware;
            viewContextAware?.Contextualize(ViewContext);


            var modelName = _htmlHelper.ViewData.ModelMetadata.ModelType.Name.ToLower();

            var modalId = await new HtmlString("edit-partial-"+ ClassId).RenderHtmlContentAsync();


            //tag details
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.Add("id", modalId);
            output.Attributes.Add("class", "modal show");
            output.Attributes.Add("tabindex", "-1");
            output.Attributes.Add("role", "dialog");
            output.Attributes.Add("aria-labelledby", $"{modalId}-title");
            output.Attributes.Add("data-backdrop", "static");
            output.Attributes.Add("data-keyboard", "false");


            var modalDialog = new TagBuilder("div");
            modalDialog.AddCssClass("modal-dialog modal-lg");

            var modalContent = new TagBuilder("div");
            modalContent.AddCssClass("modal-content");
            var modalContentId = modalId + "content";
            modalContent.Attributes.Add("id", modalContentId);

            modalDialog.InnerHtml.AppendHtml(modalContent);

            output.Content.AppendHtml(await modalDialog.RenderHtmlContentAsync());

            var script = new TagBuilder("script");
            script.InnerHtml.AppendHtml(
                "$(document).ready(function () {" +
                    $"$(\".{ClassId}\").each(function ()" +
                    "{" +
                    $"$(this).attr(\"data-toggle\", \"modal\").attr(\"data-target\", \"#{modalId}\");" +
                    "});" +
                    $"$('#{modalId}').on('show.bs.modal',function (e) " +
                    "{var modal_id_value = $(e.relatedTarget).attr('data-id');  " +
                    $"$('#{modalContentId}').load('{Partial}?id=' + modal_id_value);" +
                    "})" +
                "});");
            var scriptTag = await script.RenderHtmlContentAsync();
            output.PostContent.SetHtmlContent(scriptTag);
        }

        #endregion
    }

}
