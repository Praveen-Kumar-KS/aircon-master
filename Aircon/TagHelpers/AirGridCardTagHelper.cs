using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aircon.TagHelpers
{
    [HtmlTargetElement("air-grid-card", Attributes = NAME_ATTRIBUTE_NAME + "," + BLOCK_ATTRIBUTE_NAME + "," + HIDE_BLOCK_ATTRIBUTE_NAME_ATTRIBUTE_NAME + "," + IS_HIDE_ATTRIBUTE_NAME + "," + DATA_CONTAINER + "," + BLOCK_SHOW_RECORD_COUNT )]
    [RestrictChildren("air-grid-card-filter", "air-grid-card-content")]
    public class AirGridCardTagHelper : TagHelper
    {
        #region Constants

        private const string NAME_ATTRIBUTE_NAME = "asp-name";
        private const string BLOCK_ATTRIBUTE_NAME = "asp-block-attribute-name";
        private const string HIDE_BLOCK_ATTRIBUTE_NAME_ATTRIBUTE_NAME = "asp-hide-block-attribute-name";
        private const string IS_HIDE_ATTRIBUTE_NAME = "asp-hide";
        private const string DATA_CONTAINER = "asp-container";
        private const string BLOCK_SHOW_RECORD_COUNT = "asp-show-record-count";

        //private const string REQUIRED_ATTRIBUTES = "asp-name,asp-block-attribute-name,asp-hide-block-attribute-name,asp-hide,asp-container,asp-show-record-count";
        #endregion

        [HtmlAttributeName(NAME_ATTRIBUTE_NAME)]
        public string CardName { get; set; }

        [HtmlAttributeName(BLOCK_ATTRIBUTE_NAME)]
        public string CardBlockAttributeName { get; set; }

        [HtmlAttributeName(HIDE_BLOCK_ATTRIBUTE_NAME_ATTRIBUTE_NAME)]
        public string CardHideBlockAttributeName { get; set; }

        [HtmlAttributeName(DATA_CONTAINER)]
        public string CardDataContainer { get; set; }

        [HtmlAttributeName(IS_HIDE_ATTRIBUTE_NAME)]
        public bool CardIsHide { get; set; }

        [HtmlAttributeName(BLOCK_SHOW_RECORD_COUNT)]
        public int CardBlockShowRecordCount { get; set; }


        private AirGridCardModel AirGridCard { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }


        private readonly IHtmlHelper _htmlHelper;



        public AirGridCardTagHelper(IHtmlHelper htmlHelper)
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

            AirGridCard = new AirGridCardModel { 
                 Name = CardName,
                  BlockAttributeName = CardBlockAttributeName,
                  BlockShowRecordDisplayCount = CardBlockShowRecordCount,
                  DataContainer = CardDataContainer,
                  HideBlock = CardIsHide,
                  HideBlockAttributeName = CardHideBlockAttributeName
            };
            context.Items.Add(typeof(AirGridCardTagHelper), AirGridCard);

            await output.GetChildContentAsync();
            output.SuppressOutput();
            var content = await _htmlHelper.PartialAsync("_AirGridCard", AirGridCard);
            output.Content.SetHtmlContent(content);



        }
    }

    [HtmlTargetElement("air-grid-card-filter",Attributes = FILTER_NAME_NAME + "," + FILTER_DISPLAY_NAME + "," + DATA_ACTION, ParentTag = "air-grid-card")]
    public class AirGridCardFilterTagHelper : TagHelper
    {

        private const string FILTER_NAME_NAME = "asp-filter-name";
        private const string FILTER_DISPLAY_NAME = "asp-filter-display-name";
        private const string DATA_ACTION = "asp-data-action";


        //private const string REQUIRED_ATTRIBUTES = "asp-filter-name,asp-filter-display-name, asp-data-action";

        [HtmlAttributeName(FILTER_NAME_NAME)]
        public string Name { get; set; }

        [HtmlAttributeName(FILTER_DISPLAY_NAME)]
        public string DisplayName { get; set; }

        [HtmlAttributeName(DATA_ACTION)]
        public string Action { get; set; }


        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await output.GetChildContentAsync();
            output.SuppressOutput();
            var airGridCard = (AirGridCardModel)context.Items[typeof(AirGridCardTagHelper)];
            var model = new AirGridCardFilterModel { DataAction = Action, FilterDisplayName = DisplayName, FilterName = Name };
            airGridCard.Filters.Add(model);
            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }


    [HtmlTargetElement("air-grid-card-content", ParentTag = "air-grid-card")]
    public class AirGridCardContentTagHelper : TagHelper
    {


        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();
            var airGridCard = (AirGridCardModel)context.Items[typeof(AirGridCardTagHelper)];
            airGridCard.Content = childContent;
            output.SuppressOutput();
            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }
    public class AirGridCardFilterModel
    {
        public string FilterName { get; set; }
        public string FilterDisplayName { get; set; }
        public string DataAction { get; set; }
    }

    public class AirGridCardModel
    {
        public string Name { get; set; }
        public string BlockAttributeName { get; set; }
        public string HideBlockAttributeName { get; set; }
        public bool HideBlock { get; set; }
        public int BlockShowRecordDisplayCount { get; set; }
        public string DataContainer { get; set; }
        public List<AirGridCardFilterModel> Filters { get; set; }
        public IHtmlContent Content { get; set; }
        public AirGridCardModel()
        {
            Filters = new List<AirGridCardFilterModel>();
        }
    }
}
