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
    [HtmlTargetElement("air-grid-action-confirmation", Attributes = CLASS_ID_ATTRIBUTE_NAME + "," + ACTION_CONFIRM_TYPE, TagStructure = TagStructure.WithoutEndTag)]
    public class AirGridActionConfirmationTagHelper : TagHelper
    {
        #region Constants

        private const string CLASS_ID_ATTRIBUTE_NAME = "asp-class-id";
        private const string ACTION_ATTRIBUTE_NAME = "asp-action";
        private const string ACTION_CONFIRM_TYPE = "asp-action-type";
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
        [HtmlAttributeName(ACTION_ATTRIBUTE_NAME)]
        public string Action { get; set; }


        [HtmlAttributeName(ACTION_CONFIRM_TYPE)]
        public GridActionConfirmType ActionType { get; set; }


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

        public AirGridActionConfirmationTagHelper(IHtmlGenerator generator, IHtmlHelper htmlHelper)
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

            if (string.IsNullOrEmpty(Action))
                Action = "Delete";

            var modelName = _htmlHelper.ViewData.ModelMetadata.ModelType.Name.ToLower();
            if (!string.IsNullOrEmpty(Action))
                modelName += "-" + Action;
            //var modalId = await new HtmlString(modelName + "-delete-confirmation").RenderHtmlContentAsync();
            var modalId = await new HtmlString("action-confirm-" + ClassId).RenderHtmlContentAsync();

            var gridAction = GridActionProvider.GridActions.Where(x => x.GridActionConfirmType == ActionType).SingleOrDefault();

            var gridActionConfirmationModel = new GridActionConfirmationModel
            {
                ControllerName = _htmlHelper.ViewContext.RouteData.Values["controller"].ToString(),
                ActionName = Action,
                WindowId = modalId,
                GridAction = gridAction
            };

            //tag details
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.Add("id", modalId);
            output.Attributes.Add("class", "modal fade");
            output.Attributes.Add("tabindex", "-1");
            output.Attributes.Add("role", "dialog");
            output.Attributes.Add("aria-labelledby", $"{modalId}-title");

            var partialView = await _htmlHelper.PartialAsync("GridActionConfirm", gridActionConfirmationModel);
            output.Content.SetHtmlContent(partialView);

            //modal script
            var script = new TagBuilder("script");
            script.InnerHtml.AppendHtml(
                "$(document).ready(function () {" +
                    $"$(\".{ClassId}\").each(function ()" +
                    "{" +
                    $"$(this).attr(\"data-toggle\", \"modal\").attr(\"data-target\", \"#{modalId}\");"+
                    "});"+
                    $"$('.{ClassId}').click(function () " +
                    "{var modal_id_value = $(this).data('id');  " +
                    "  $(\".modal-body #Id\").val(modal_id_value); " +
                    "})" +
                "});");
            var scriptTag = await script.RenderHtmlContentAsync();
            output.PostContent.SetHtmlContent(scriptTag);
        }

        #endregion
    }

    
}
