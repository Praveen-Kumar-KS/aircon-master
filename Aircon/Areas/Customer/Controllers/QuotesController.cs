using Aircon.Areas.Customer.Models.Quotes;
using Aircon.Areas.Customer.Models.ShipmentInformation;
using Aircon.Business.Models.Customer.Quotes;
using Aircon.Business.Models.Shared;
using Aircon.Business.Services.Customer;
using Aircon.Core.Data;
using Aircon.Data.Entities;
using Aircon.Data.Helper;
using Aircon.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vg.Common.Notification.Message;

namespace Aircon.Areas.Customer.Controllers
{
    public class QuotesController : BaseCustomerController
    {
        private readonly IQuotesService _quotesService;
        private readonly INotify _notify;
        public QuotesController(IQuotesService quotesService, INotify notify)
        {
            _quotesService = quotesService;
            _notify = notify;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ReviewPricing(int id)
        {
            QuotePricingViewModel quotePricingViewModel = new QuotePricingViewModel();
            quotePricingViewModel = _quotesService.GetQuoteById(id).ToViewModel();
            return View(quotePricingViewModel);
        }
        public IActionResult SaveReviewQuote(QuotePricingViewModel quotePricingViewModel)
        {
            //var res = _quotesService.SaveReviewQuote(quotePricingViewModel.ToModel()).ToViewModel();
            return PartialView("~/Areas/Customer/Views/Quotes/SaveQuotePartial.cshtml");
        }
        public IActionResult AddChargeableCostPartial()
        {
            return PartialView("~/Areas/Customer/Views/Quotes/AddChargeableCostPartial.cshtml");
        }
        public IActionResult EmailQuote(int id)
        {
            QuotePricingViewModel quotePricingViewModel = new QuotePricingViewModel();
            quotePricingViewModel = _quotesService.GetQuoteById(id).ToViewModel();
            if(quotePricingViewModel.Quotes.ShipmentType == Data.Enums.ShipmentType.AirportToAirport)   
            {
                return PartialView("~/Areas/Customer/Views/Quotes/AtaEmailPartial.cshtml", quotePricingViewModel);
            }
            return PartialView("~/Areas/Customer/Views/Quotes/DtaEmailPartial.cshtml", quotePricingViewModel);
        }
        public async Task<IActionResult> AtaEmailNotification(int id)
        {
            QuotePricingViewModel quotePricingViewModel = new QuotePricingViewModel();
            quotePricingViewModel = _quotesService.GetQuoteById(id).ToViewModel();
            var notifyModel = new NotifyEmailModel { displayname = string.Format("{0}", quotePricingViewModel.Quotes.CustomerName)};
            await _notify.NotifyAsync(quotePricingViewModel.Quotes.CustomerName, TemplateDefinitionNames.Quotes.AtaQuote, notifyModel);
            return View("ReviewPricing", quotePricingViewModel);
        }
        public async Task<IActionResult> DtaEmailNotification(int id)
        {
            QuotePricingViewModel quotePricingViewModel = new QuotePricingViewModel();
            quotePricingViewModel = _quotesService.GetQuoteById(id).ToViewModel();
            var notifyModel = new NotifyEmailModel { displayname = string.Format("{0}", quotePricingViewModel.Quotes.CustomerName) };
            await _notify.NotifyAsync(quotePricingViewModel.Quotes.CustomerName, TemplateDefinitionNames.Quotes.DtaQuote, notifyModel);
            return View("ReviewPricing", quotePricingViewModel);
        }
        public IActionResult PrintQuote(int id)
        {
            QuotePricingViewModel quotePricingViewModel = new QuotePricingViewModel();
            quotePricingViewModel = _quotesService.GetQuoteById(id).ToViewModel();
            if(quotePricingViewModel.Quotes.ShipmentType == Data.Enums.ShipmentType.AirportToAirport)
            {
                return PartialView("~/Areas/Customer/Views/Quotes/AtaPdfPrintQuotePartial.cshtml");
            }
            return PartialView("~/Areas/Customer/Views/Quotes/DtaPdfPrintQuotePartial.cshtml");
        }
        public IActionResult AddQuote()
        {
            var customerId = HttpContextHelper.CustomerId;
            int id = customerId.HasValue ? customerId.Value : 0;
            QuoteViewModel quoteViewModel = new QuoteViewModel();
            quoteViewModel.CustomerId = id;
            quoteViewModel.MeasurementUnit = Data.Enums.MeasurementUnit.InchesOrKgs;
            quoteViewModel.ShipmentInformationDetail = new List<ShipmentInformationDetailViewModel>();
            quoteViewModel.Origin = _quotesService.GetOrigin();
            quoteViewModel.Destination = _quotesService.GetDestination();
            return View(quoteViewModel);

        }
        public IActionResult ShipmentInformationHeader(int QuoteId)
        {
            ShipmentInformationHeaderViewModel shipmentInformationHeaderViewModel = new ShipmentInformationHeaderViewModel();
            return PartialView("ShipmentInformationHeader", shipmentInformationHeaderViewModel);

        }
        [HttpGet("ShipmentInformationDetailViewModel")]
        public IActionResult ShipmentInformationDetailPartial()
        {
            ShipmentInformationDetailViewModel shipmentInformationDetailViewModel = new ShipmentInformationDetailViewModel();
            return PartialView("ShipmentInformationDetailPartial", shipmentInformationDetailViewModel);               
        }
        public IActionResult AddQuotePost(QuoteViewModel addQuoteViewModel)
        {
            var res = _quotesService.AddQuote(addQuoteViewModel.ToModel()).ToViewModel();
            return RedirectToAction("ReviewPricing", new { id = res.Id });
        }

        public IActionResult EditQuote(int Id)
        {
            QuoteViewModel quoteViewModel = new QuoteViewModel();
            quoteViewModel = _quotesService.GetQuote(Id).ToViewModel();
            quoteViewModel.Origin = _quotesService.GetOrigin();
            quoteViewModel.Destination = _quotesService.GetDestination();
            return View(quoteViewModel);

        }
        public IActionResult EditQuotePost(QuoteViewModel editQuoteViewModel)
        {
            var res = _quotesService.UpdateQuote(editQuoteViewModel.ToModel()).ToViewModel();
            return RedirectToAction("ReviewPricing", new { id = res.Id });

        }

        public IActionResult DeleteQuote(int id)
        {
            _quotesService.DeleteQuote(id);
            return RedirectToAction("Index", "Workflow");
        }
        public IActionResult CloneQuote(int Id)
        {
            QuoteViewModel CloneQuoteViewModel = new QuoteViewModel();
            CloneQuoteViewModel = _quotesService.GetQuoteForCloneById(Id).ToViewModel();
            return View("AddQuote", CloneQuoteViewModel);
        }
    }
}
