//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.AspNetCore.Mvc.TagHelpers;
//using Microsoft.AspNetCore.Razor.TagHelpers;
//using System.Threading.Tasks;

//namespace Aircon.TagHelpers
//{
//    [HtmlTargetElement(Attributes = CardTargetAttributeName)]
//    public class AirGridToggleTagHelper : TagHelper
//    {
//        public const string CardTargetAttributeName = "asp-toggle-card";

//        /// <summary>
//        /// The id of the modal that will be toggled by this element
//        /// </summary>
//        [HtmlAttributeName(CardTargetAttributeName)]
//        public string ToggleCard { get; set; }



//        public override void Process(TagHelperContext context, TagHelperOutput output)
//        {


//            output.TagName = "span";

//            var template = $@"<p class='card-header float-right  panel-title'><span class='tab1'>SHOW<input class='spinner' value='3' type='number'/></span>
//                                <a  role = 'button' data-toggle='collapse' href='#{ToggleCard}' aria-expanded='true' aria-controls='{ToggleCard}'>
//                                <span class='p-l-10 tab1'><span class='collapse-text'>COLLAPSE</span><i class='p-l-10 mdi mdi-fullscreen-exit mdi-20'></i></span>
//                                </a>
//                              </p>";

//            output.Content.SetHtmlContent(template);


//            //var script = new TagBuilder("script");
//            //script.InnerHtml.AppendHtml(
//            //    "$(document).ready(function () {" +
//            //        $"$('#{ButtonId}').attr(\"data-toggle\", \"modal\").attr(\"data-target\", \"#{modalId}\");" +
//            //        $"$('#{modalId}-submit-button').attr(\"name\", $(\"#{ButtonId}\").attr(\"name\"));" +
//            //        $"$(\"#{ButtonId}\").attr(\"name\", \"\");" +
//            //        $"if($(\"#{ButtonId}\").attr(\"type\") == \"submit\")$(\"#{ButtonId}\").attr(\"type\", \"button\");" +
//            //    "});");
//            //var scriptTag = await script.RenderHtmlContentAsync();
//            //output.PostContent.SetHtmlContent(scriptTag);

//        }

//    }
//}
