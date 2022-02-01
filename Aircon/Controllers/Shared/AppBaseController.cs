using Aircon.Core;
using Aircon.Core.UI;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Vg.Common.ToastNotify;

namespace Aircon.Controllers.Shared
{

    /// <summary>
    /// Base app controller
    /// </summary>
    //[SignOutFromExternalAuthentication]
    //[ValidatePassword]
    //[SaveIpAddress]
    //[SaveLastActivity]
    //[SaveLastVisitedPage]

    public abstract partial class AppBaseController : Controller
    {
        #region Rendering

        /// <summary>
        /// Render componentto string
        /// </summary>
        /// <param name="componentName">Component name</param>
        /// <returns>Result</returns>
        protected virtual string RenderViewComponentToString(string componentName, object arguments = null)
        {
            //original implementation: https://github.com/aspnet/Mvc/blob/dev/src/Microsoft.AspNetCore.Mvc.ViewFeatures/Internal/ViewComponentResultExecutor.cs
            //we customized it to allow running from controllers

            //TODO add support for parameters (pass ViewComponent as input parameter)
            if (String.IsNullOrEmpty(componentName))
                throw new ArgumentNullException("componentName");

            var actionContextAccessor = HttpContext.RequestServices.GetService(typeof(IActionContextAccessor)) as IActionContextAccessor;
            if (actionContextAccessor == null)
                throw new Exception("IActionContextAccessor cannot be resolved");

            var context = new ActionContext(HttpContext, RouteData, ControllerContext.ActionDescriptor, ModelState);

            var viewComponentResult = ViewComponent(componentName, arguments);

            var viewData = ViewData;
            if (viewData == null)
            {
                throw new NotImplementedException();
                //TODO viewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState);
            }

            var tempData = TempData;
            if (tempData == null)
            {
                throw new NotImplementedException();
                //TODO tempData = _tempDataDictionaryFactory.GetTempData(context.HttpContext);
            }

            using (var writer = new StringWriter())
            {
                var viewContext = new ViewContext(
                    context,
                    NullView.Instance,
                    viewData,
                    tempData,
                    writer,
                    new HtmlHelperOptions());

                // IViewComponentHelper is stateful, we want to make sure to retrieve it every time we need it.
                var viewComponentHelper = context.HttpContext.RequestServices.GetRequiredService<IViewComponentHelper>();
                (viewComponentHelper as IViewContextAware)?.Contextualize(viewContext);

                Task<IHtmlContent> result = viewComponentResult.ViewComponentType == null ?
                    viewComponentHelper.InvokeAsync(viewComponentResult.ViewComponentName, viewComponentResult.Arguments) :
                    viewComponentHelper.InvokeAsync(viewComponentResult.ViewComponentType, viewComponentResult.Arguments);

                result.Result.WriteTo(writer, HtmlEncoder.Default);
                return writer.ToString();
            }
        }

        /// <summary>
        /// Render partial view to string
        /// </summary>
        /// <returns>Result</returns>
        protected virtual async Task<string> RenderPartialViewToString()
        {
            return await RenderPartialViewToString(null, null);
        }

        /// <summary>
        /// Render partial view to string
        /// </summary>
        /// <param name="viewName">View name</param>
        /// <returns>Result</returns>
        protected virtual async Task<string> RenderPartialViewToString(string viewName)
        {
            return await RenderPartialViewToString(viewName, null);
        }

        /// <summary>
        /// Render partial view to string
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>Result</returns>
        protected virtual async Task<string> RenderPartialViewToString(object model)
        {
            return await RenderPartialViewToString(null, model);
        }

        /// <summary>
        /// Render partial view to string
        /// </summary>
        /// <param name="viewName">View name</param>
        /// <param name="model">Model</param>
        /// <returns>Result</returns>
        protected virtual async Task<string> RenderPartialViewToString(string viewName, object model)
        {
            //get Razor view engine
            var razorViewEngine = HttpContext.RequestServices.GetRequiredService<IRazorViewEngine>();

            //create action context
            var actionContext = new ActionContext(HttpContext, RouteData, ControllerContext.ActionDescriptor, ModelState);

            //set view name as action name in case if not passed
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.ActionDescriptor.ActionName;

            //set model
            ViewData.Model = model;
            var viewResult = razorViewEngine.FindView(actionContext, viewName, false);
            if (viewResult.View == null)
            {
                //or try to get a view by the path
                viewResult = razorViewEngine.GetView(null, viewName, false);
                if (viewResult.View == null)
                    throw new ArgumentNullException($"{viewName} view was not found");
            }


            using (var stringWriter = new StringWriter())
            {
                var viewContext = new ViewContext(actionContext, viewResult.View, ViewData, TempData, stringWriter, new HtmlHelperOptions());

                await viewResult.View.RenderAsync(viewContext);
                return stringWriter.GetStringBuilder().ToString();
            }
        }

        #endregion

        #region Notifications

        /// <summary>
        /// Display success notification
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="persistForTheNextRequest">A value indicating whether a message should be persisted for the next request</param>
        protected virtual void SuccessNotification(string? message = null, bool persistForTheNextRequest = true)
        {
            AddNotification(NotifyType.Success, message, persistForTheNextRequest);
        }


        protected virtual void InfoNotification(string? message = null, bool persistForTheNextRequest = true)
        {
            AddNotification(NotifyType.Information, message, persistForTheNextRequest);
        }

        protected virtual void AddSuccessNotification(string? message = null, bool persistForTheNextRequest = true)
        {
            AddNotification(NotifyType.Add, message, persistForTheNextRequest);
        }

        protected virtual void DeleteSuccessNotification(string? message = null, bool persistForTheNextRequest = true)
        {
            AddNotification(NotifyType.Delete, message, persistForTheNextRequest);
        }

        protected virtual void UpdateSuccessNotification(string? message = null, bool persistForTheNextRequest = true)
        {
            AddNotification(NotifyType.Update, message, persistForTheNextRequest);
        }
        /// <summary>
        /// Display warning notification
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="persistForTheNextRequest">A value indicating whether a message should be persisted for the next request</param>
        protected virtual void WarningNotification(string? message = null, bool persistForTheNextRequest = true)
        {
            AddNotification(NotifyType.Warning, message, persistForTheNextRequest);
        }

        /// <summary>
        /// Display error notification
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="persistForTheNextRequest">A value indicating whether a message should be persisted for the next request</param>
        protected virtual void ErrorNotification(string? message = null, bool persistForTheNextRequest = true)
        {
            AddNotification(NotifyType.Error, message, persistForTheNextRequest);
        }

        /// <summary>
        /// Display error notification
        /// </summary>
        /// <param name="persistForTheNextRequest">A value indicating whether a message should be persisted for the next request</param>
        protected virtual void ErrorNotification(ModelStateDictionary ModelState, bool persistForTheNextRequest = true)
        {
            var modelErrors = new List<string>();
            foreach (var modelState in ModelState.Values)
            {
                foreach (var modelError in modelState.Errors)
                {
                    modelErrors.Add(modelError.ErrorMessage);
                }
            }
            AddNotification(NotifyType.Error, string.Join(',', modelErrors), persistForTheNextRequest);
        }

        /// <summary>
        /// Display error notification
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <param name="persistForTheNextRequest">A value indicating whether a message should be persisted for the next request</param>
        /// <param name="logException">A value indicating whether exception should be logged</param>
        protected virtual void ErrorNotification(Exception exception, bool persistForTheNextRequest = true, bool logException = true)
        {
            if (logException)
                LogException(exception);

            AddNotification(NotifyType.Error, exception.Message, persistForTheNextRequest);
        }

        /// <summary>
        /// Log exception
        /// </summary>
        /// <param name="exception">Exception</param>
        protected void LogException(Exception exception)
        {
            //var workContext = HttpContext.RequestServices.GetRequiredService<IWorkContext>();
            var logger = HttpContext.RequestServices.GetRequiredService<ILogger>();
            //var customer = workContext.CurrentCustomer;
            //logger.Error(exception.Message, exception, customer);
            logger.LogError(exception.Message, exception);
        }

        /// <summary>
        /// Display notification
        /// </summary>
        /// <param name="type">Notification type</param>
        /// <param name="message">Message</param>
        /// <param name="persistForTheNextRequest">A value indicating whether a message should be persisted for the next request</param>
        protected virtual void AddNotification(NotifyType type, string message, bool persistForTheNextRequest)
        {

            


            var _toastNotification = HttpContext.RequestServices.GetRequiredService<IToastNotification>();

            if(type == NotifyType.Success)
            {
                //var mess = String.Format("<div id=\"snackbar\">{0}</div>", message);
                _toastNotification.AddSuccessToastMessage(message, new ToastrOptions { 
                    Title= string.Empty,
                    IconClass = "mdi mdi-bell"
                });
            }

            if (type == NotifyType.Warning)
            {

                _toastNotification.AddWarningToastMessage(message, new ToastrOptions
                {
                    Title = string.Empty,
                });
            }

            if (type == NotifyType.Error)
            {

                _toastNotification.AddErrorToastMessage(message, new ToastrOptions
                {
                    Title = string.Empty,
                });
            }


            if (type == NotifyType.Information)
            {

                _toastNotification.AddInfoToastMessage(message, new ToastrOptions
                {
                    Title = string.Empty,
                });
            }

            if (type == NotifyType.Add)
            {

                _toastNotification.AddSuccessToastMessage(message, new ToastrOptions
                {
                    Title = string.Empty,
                    IconClass = "mdi mdi-bell"
                });
            }
            if (type == NotifyType.Delete)
            {

                _toastNotification.AddSuccessToastMessage(message, new ToastrOptions
                {
                    Title = string.Empty,
                    IconClass = "mdi mdi-bell"
                });
            }
            if (type == NotifyType.Update)
            {

                _toastNotification.AddSuccessToastMessage(message, new ToastrOptions
                {
                    Title = string.Empty,
                    IconClass = "mdi mdi-bell"
                });
            }
            //var dataKey = string.Format("app.notifications.{0}", type);

            //if (persistForTheNextRequest)
            //{
            //    //1. Compare with null (first usage)
            //    //2. For some unknown reasons sometimes List<string> is converted to string[]. And it throws exceptions. That's why we reset it
            //    if (TempData[dataKey] == null || !(TempData[dataKey] is List<string>))
            //        TempData[dataKey] = new List<string>();
            //    ((List<string>)TempData[dataKey]).Add(message);
            //}
            //else
            //{
            //    //1. Compare with null (first usage)
            //    //2. For some unknown reasons sometimes List<string> is converted to string[]. And it throws exceptions. That's why we reset it
            //    if (ViewData[dataKey] == null || !(ViewData[dataKey] is List<string>))
            //        ViewData[dataKey] = new List<string>();
            //    ((List<string>)ViewData[dataKey]).Add(message);
            //}
        }
        #endregion
        /// <summary>
        /// Access denied view
        /// </summary>
        /// <returns>Access denied view</returns>
        protected virtual IActionResult AccessDeniedView()
        {
            var webHelper = HttpContext.RequestServices.GetRequiredService<IWebHelper>();
            return RedirectToAction("AccessDenied", "Security", new { Area = "Identity", pageUrl = webHelper.GetRawUrl(this.Request) });
        }

        //public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        //{
        //    // event notification before execute
        //    var mediator = context.HttpContext.RequestServices.GetService<IMediator>();
        //    await mediator.Publish(new ActionExecutingContextNotification(context, true));

        //    await next();

        //    //event notification after execute
        //    await mediator.Publish(new ActionExecutingContextNotification(context, false));
        //}

        /// <summary>
        /// Creates a <see cref="T:System.Web.Mvc.JsonResult"/> object that serializes the specified object to JavaScript Object Notation (JSON) format using the content type, content encoding, and the JSON request behavior.
        /// </summary>
        /// 
        /// <returns>
        /// The result object that serializes the specified object to JSON format.
        /// </returns>
        /// <param name="data">The JavaScript object graph to serialize.</param>
        /// <param name="contentType">The content type (MIME type).</param>
        /// <param name="contentEncoding">The content encoding.</param>
        /// <param name="behavior">The JSON request behavior</param>
        public override JsonResult Json(object data)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat
            };
            return base.Json(data, serializerSettings);
        }

        public ActionResult Search(string search, string SearchActionName)
        {
            TempData["SearchText"] = search;
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            TempData["SearchController"] = controllerName;
            return RedirectToAction(SearchActionName, new { SearchText = search });
        }

        public ActionResult ResetSearch(string SearchActionName)
        {
            TempData.Remove("SearchText");
            TempData.Remove("SearchController");
            return RedirectToAction(SearchActionName);
        }

        protected string SearchText()
        {
            var searchText = TempData.Peek("SearchText");
            if (searchText != null)
            {
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                string searchControllerName = TempData.Peek("SearchController").ToString();
                if (controllerName.Equals(searchControllerName))
                {
                    return searchText.ToString();
                }
                else
                {
                    TempData.Remove("SearchText");
                    TempData.Remove("SearchController");
                }
            }
            return null;
        }
    }
}
