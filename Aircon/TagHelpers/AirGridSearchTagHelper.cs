using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aircon.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aircon.TagHelpers
{
    [HtmlTargetElement("air-grid-search", Attributes = ACTION_SEARCH_ACTION + "," + ACTION_PLACEHOLDER)]
    public class AirGridSearchTagHelper : TagHelper
    {
        #region Constants

        private const string ACTION_SEARCH_ACTION = "asp-search-action";
        private const string ACTION_PLACEHOLDER = "asp-placeholder";
        #endregion

        #region Properties        

        /// <summary>
        /// Delete action name
        /// </summary>
        [HtmlAttributeName(ACTION_SEARCH_ACTION)]
        public string SearchAction { get; set; }


        [HtmlAttributeName(ACTION_PLACEHOLDER)]
        public string PlaceHolder { get; set; }


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

        public AirGridSearchTagHelper(IHtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            //contextualize IHtmlHelper
            var viewContextAware = _htmlHelper as IViewContextAware;
            viewContextAware?.Contextualize(ViewContext);

            var airGridSearch = new AirGridSearchModel
            {
                SearchActionName = SearchAction,
                PlaceHolder = PlaceHolder
            };
            context.Items.Add(typeof(AirGridSearchTagHelper), airGridSearch);

            var content = await _htmlHelper.PartialAsync("_AirGridSearch", airGridSearch);
            output.Content.SetHtmlContent(content);
        }
    }
    
}
