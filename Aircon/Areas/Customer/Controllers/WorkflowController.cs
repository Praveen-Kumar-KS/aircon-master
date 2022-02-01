using Aircon.Areas.Customer.Models.Quotes;
using Aircon.Business.Models.Customer.Bookings;
using Aircon.Business.Models.Customer.Quotes;
using Aircon.Business.Models.Shared;
using Aircon.Business.Services.Customer;
using Aircon.Business.Services.Shared;
using Aircon.Data.Enums;
using Aircon.Data.Helper;
using Aircon.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aircon.Areas.Customer.Controllers
{
    public class WorkflowController : BaseCustomerController
    {
        private readonly IQuotesService _quotesService;
        private readonly IBookingService _bookingService;
        private readonly IGenericAttributeService _genericAttributeService;

        public WorkflowController(IQuotesService quotesService, IGenericAttributeService genericAttributeService, IBookingService bookingService)
        {
            _quotesService = quotesService;
            _bookingService = bookingService;
            _genericAttributeService = genericAttributeService;
        }

        public async Task<IActionResult> Index()
        {
            QuotesBookingListViewModel quotesBookingListViewModel = new QuotesBookingListViewModel();
            quotesBookingListViewModel.Quotes = new List<QuoteModel>();
            quotesBookingListViewModel.Bookings = new List<BookingModel>();
            var searchText = SearchText();
            int recordCountQuotesQueue = await _genericAttributeService.GetAttributeAsync<UserModel, int>(HttpContextHelper.UserId.Value, CardGridSetting.QuotesandBookings.Quotes, 5);
            int recordCountBookingsQueue = await _genericAttributeService.GetAttributeAsync<UserModel, int>(HttpContextHelper.UserId.Value, CardGridSetting.QuotesandBookings.Bookings, 5);
            quotesBookingListViewModel.Quotes = _quotesService.GetQuote(searchText,QuoteStatus.InProgress,recordCountQuotesQueue);
            quotesBookingListViewModel.Bookings = _bookingService.GetBooking(searchText,ShipmentStatus.Pending,recordCountBookingsQueue);
            return View(quotesBookingListViewModel);
        }
        public async Task<IActionResult> BookingsQueuePartial(ShipmentStatus shipmentStatus)
        {
            var searchText = SearchText();
            int recordCountBookingsQueue = await _genericAttributeService.GetAttributeAsync<UserModel, int>(HttpContextHelper.UserId.Value, CardGridSetting.QuotesandBookings.Bookings, 5);
            List<BookingModel> bookingsQueue = _bookingService.GetBooking(searchText, shipmentStatus, recordCountBookingsQueue);
            return PartialView("BookingContainer", bookingsQueue);
        }

        public async Task<IActionResult> QuotesQueuePartial(QuoteStatus quoteStatus)
        {
            List<QuoteModel> quotesQueue = new List<QuoteModel>();
            var searchText = SearchText();
            int recordCountQuotesQueue = await _genericAttributeService.GetAttributeAsync<UserModel, int>(HttpContextHelper.UserId.Value, CardGridSetting.QuotesandBookings.Quotes, 5);
            quotesQueue = _quotesService.GetQuote(searchText, quoteStatus, recordCountQuotesQueue);
            return PartialView("QuotesContainer", quotesQueue);
        }
        public IActionResult PendingQueueBookings()
        {
            return RedirectToAction("BookingsQueuePartial", new { shipmentStatus = ShipmentStatus.Pending});
        }
        public IActionResult RecentlyCancelledQueueBookings()
        {
            return RedirectToAction("BookingsQueuePartial", new { shipmentStatus = ShipmentStatus.RecentlyCancelled });
        }

        public IActionResult InprogressQuotes()
        {
            return RedirectToAction("QuotesQueuePartial", new { quoteStatus = QuoteStatus.InProgress});
        }

        public IActionResult DraftsQuotes()
        {
            return RedirectToAction("QuotesQueuePartial", new { quoteStatus = QuoteStatus.Drafts });
        }
        public IActionResult ClosedQuotes()
        {
            return RedirectToAction("QuotesQueuePartial", new { quoteStatus = QuoteStatus.Closed });
        }
        [HttpGet("ReviewInitiateBookingPartial")]
        public IActionResult ReviewInitiateBookingPartial(int Id)
        {
            QuoteViewModel editReviewBooking = new QuoteViewModel();
            editReviewBooking = _quotesService.GetReviewAndInitiateBooking(Id).ToViewModel();
            return PartialView("~/Areas/Customer/Views/Workflow/ReviewInitiateBookingPartial.cshtml", editReviewBooking);
        }

    }
}
