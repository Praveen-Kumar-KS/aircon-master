using Aircon.Areas.Customer.Models.Bookings;
using Aircon.Areas.Customer.Models.Contact;
using Aircon.Areas.Customer.Models.ShipmentInformation;
using Aircon.Business.Services.Customer;
using Aircon.Data.Helper;
using Aircon.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aircon.Areas.Customer.Controllers
{
    public class BookingController : BaseCustomerController
    {
        private readonly IBookingService _bookingService;
        private readonly IQuotesService _quotesService;

        public BookingController(IBookingService bookingService, IQuotesService quotesService)
        {
            _bookingService = bookingService;
            _quotesService = quotesService;

        }
        [HttpGet]

        public IActionResult Index(int QuoteId)
        {
            BookingViewModel bookingViewModel = new BookingViewModel();
            //bookingViewModel.ShipmentInformationDetailViewModels = new List<ShipmentInformationDetailViewModel>();
            bookingViewModel = _bookingService.GetQuoteBooking(QuoteId).ToViewModel();
            return View(bookingViewModel);
        }
        public IActionResult AddNotificationPartial()
        {
            return PartialView();
        }
        public IActionResult EditNotificationPartial()
        {
            return PartialView();
        }

        [HttpGet("AddBookingContactPartial")]
        public IActionResult AddBookingContactPartial(int quoteId)
        {
            var result = new CustomerContactListViewModel();                      
            var searchText = SearchText();
            result.Contacts = _bookingService.GetCustomerContacts(quoteId, searchText).Select(x => x.ToViewModel()).ToList();
            return PartialView("~/Areas/Customer/Views/Booking/AddBookingContactPartial.cshtml", result);

        }

    }
}
