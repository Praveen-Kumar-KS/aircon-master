using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Aircon.Extensions;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace Aircon.TagHelpers
{
    /// <summary>
    /// "input" tag helper
    /// </summary>
    //[HtmlTargetElement("input", Attributes = FOR_ATTRIBUTE_NAME)]
    [HtmlTargetElement("air-input", Attributes = FOR_ATTRIBUTE_NAME, TagStructure = TagStructure.NormalOrSelfClosing)]
    public class AirInputTagHelper : Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper
    {
        #region Constants

        private const string FOR_ATTRIBUTE_NAME = "asp-for";
        private const string DISABLED_ATTRIBUTE_NAME = "asp-disabled";
        private const string REQUIRED_ATTRIBUTE_NAME = "asp-required";


        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether the input is disabled
        /// </summary>
        [HtmlAttributeName(DISABLED_ATTRIBUTE_NAME)]
        public string IsDisabled { set; get; }

        [HtmlAttributeName(REQUIRED_ATTRIBUTE_NAME)]
        public string IsRequired { set; get; }
        #endregion



        #region Ctor

        public AirInputTagHelper(IHtmlGenerator generator) : base(generator)
        {
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

            //add disabled attribute
            if (bool.TryParse(IsDisabled, out var disabled) && disabled)
                output.Attributes.Add(new TagHelperAttribute("disabled", "disabled"));


            try
            {
                await base.ProcessAsync(context, output);
            }
            catch
            {
                //If the passed values differ in data type according to the model, we should try to initialize the component with a default value. 
                //If this is not possible, then we suppress the generation of html for this imput.
                try
                {
                    ViewContext.ModelState[For.Name].RawValue = Activator.CreateInstance(For.ModelExplorer.ModelType);
                    await base.ProcessAsync(context, output);
                }
                catch
                {
                    output.SuppressOutput();
                }
            }
            TagHelperAttributeList tagHelperAttributes = output.Attributes;
            output.SuppressOutput();


            var input = new TagBuilder("input");
            foreach (var keyPair in tagHelperAttributes)
            {
                input.Attributes.Add(new System.Collections.Generic.KeyValuePair<string, string>(keyPair.Name, keyPair.Value.ToString()));
            }
            input.Attributes.Add(new System.Collections.Generic.KeyValuePair<string, string>("autofocus", string.Empty));
            input.AddCssClass("form__field");

            var inputGroup = new TagBuilder("div");
            inputGroup.AddCssClass("form__group m-b-20");

            var label = Generator.GenerateLabel(
                ViewContext,
                For.ModelExplorer,
                For.Name, null,
                new { @class = "form__label" });

            TagHelperOutput validationMessageElement = await CreateValidationMessageElement(context);
            if (bool.TryParse(IsRequired, out var required) && required)
            {
                label.InnerHtml.AppendHtml("<span class=\"required\"> * </span>");
            }

            inputGroup.InnerHtml.AppendHtml(input);
            inputGroup.InnerHtml.AppendHtml(label);
            inputGroup.InnerHtml.AppendHtml(validationMessageElement);
            output.Content.AppendHtml(await inputGroup.RenderHtmlContentAsync());
        }

        private async Task<TagHelperOutput> CreateValidationMessageElement(TagHelperContext context)
        {
            ValidationMessageTagHelper validationMessageTagHelper =
                new ValidationMessageTagHelper(Generator)
                {
                    For = this.For,
                    ViewContext = this.ViewContext
                };

            TagHelperOutput validationMessageOutput = CreateTagHelperOutput("span");

            await validationMessageTagHelper.ProcessAsync(context, validationMessageOutput);

            return validationMessageOutput;
        }

        private TagHelperOutput CreateTagHelperOutput(string tagName)
        {
            return new TagHelperOutput(
                tagName: tagName,
                attributes: new TagHelperAttributeList(),
                getChildContentAsync: (s, t) =>
                {
                    return Task.Factory.StartNew<TagHelperContent>(
                            () => new DefaultTagHelperContent());
                }
            );
        }
        #endregion
    }
}