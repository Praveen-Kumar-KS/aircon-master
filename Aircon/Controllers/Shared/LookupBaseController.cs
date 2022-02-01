//using System.Collections.Generic;
//using System.Linq;
//using System.Text.RegularExpressions;
////using Kendo.Mvc.Extensions;
//using Kendo.Mvc.UI;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Aircon.Business.Models.Shared;
//using Aircon.Business.Services.Lookups;
//using Aircon.Data.Entities;
//using Aircon.ViewModels.Shared;

//namespace Aircon.Controllers.Shared
//{
//    [Authorize(Roles = "Admin")]
//    public class LookupBaseController<T> : Controller where T : LookupEntity
//    {

//        private readonly ILookupService<T> LookupService;
//        public LookupBaseController(ILookupService<T> lookupService)
//        {
//            LookupService = lookupService;
//        }

//        public ActionResult Index(string title)
//        {
//            ViewData["Controller"] = typeof(T).Name;
//            if (string.IsNullOrEmpty(title))
//            {
//                var r = new Regex(@"
//                (?<=[A-Z])(?=[A-Z][a-z]) |
//                 (?<=[^A-Z])(?=[A-Z]) |
//                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);
//                ViewData["Title"] = r.Replace(typeof(T).Name, " ");
//            }
//            else
//            {
//                ViewData["Title"] = title;
//            }

//            List<LookupViewModel> LookupList = new List<LookupViewModel>();
//            LookupList = LookupService.GetLookupList().Select(x => new LookupViewModel { LookupId = x.Id, Name = x.Name, Description = x.Description, Active = x.Active }).ToList();
//            return View("~/Views/Lookups/Index.cshtml", LookupList);
//        }

//        [HttpPost("Create")]
//        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<LookupViewModel> lookups)
//        {
//            var results = new List<LookupViewModel>();

//            if (lookups != null && ModelState.IsValid)
//            {
//                foreach (var lookup in lookups)
//                {
//                    LookupService.AddLookup(new LookupModel { Id = lookup.LookupId, Name = lookup.Name, Description = lookup.Description, Active = lookup.Active });
//                    results.Add(lookup);
//                }
//            }

//            return Json(results.ToDataSourceResult(request, ModelState));
//        }

//        [HttpGet("Read")]
//        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
//        {
//            IEnumerable<LookupViewModel> results;
//            results = LookupService.GetLookupList().Select(x => new LookupViewModel { LookupId = x.Id, Name = x.Name, Description = x.Description, Active = x.Active }).ToList();
//            return Json(results.ToDataSourceResult(request));
//        }


//        [AcceptVerbs("Post")]
//        [HttpPost("Update")]
//        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<LookupViewModel> lookups)
//        {
//            if (lookups != null && ModelState.IsValid)
//            {
//                foreach (var lookup in lookups)
//                {
//                    LookupService.UpdateLookup(new LookupModel { Id = lookup.LookupId, Name = lookup.Name, Description = lookup.Description, Active = lookup.Active });
//                }
//            }

//            return Json(lookups.ToDataSourceResult(request, ModelState));
//        }


//        [AcceptVerbs("Post")]
//        [HttpPost("Delete")]
//        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<LookupViewModel> lookups)
//        {
//            if (lookups.Any())
//            {
//                foreach (var lookup in lookups)
//                {
//                    LookupService.DeleteAllLookup(lookup.LookupId);
//                }
//            }

//            return Json(lookups.ToDataSourceResult(request, ModelState));
//        }
//    }
//}