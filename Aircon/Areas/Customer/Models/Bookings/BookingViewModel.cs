using Aircon.Areas.Customer.Models.ShipmentInformation;
using Aircon.Business.Models.Customer.Bookings;
using Aircon.Business.Models.Customer.ShipmentInformation;
using Aircon.Business.Models.Shared;
using Aircon.Data.Enums;
using Bogus.DataSets;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aircon.Areas.Customer.Models.Bookings
{
    public class BookingViewModel
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Route { get; set; }
        public string Type { get; set; }
        [Display(Name = "Weight")]
        public decimal ChargeableWeight { get; set; }
        [Display(Name = "Volume")]
        public decimal Volume { get; set; }
        [Display(Name = "Quantity (Number of Parcels)")]
        public decimal Quantity { get; set; }
        public ServiceLevel ServiceLevel { get; set; }
        public ShipmentType ShipmentType { get; set; }
        public QuoteType QuoteType { get; set; }
        public DateTime ArrivesOn { get; set; }
        public DateTime CutOffTime { get; set; }
        public int? QuoteId { get; set; }
        [Display(Name = "Internal Reference #")]
        public string InternalReferenceNum { get; set; }
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }
        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "State")]
        public string State { get; set; }
        [Display(Name = "Zip")]
        public string Zip { get; set; }
        [Display(Name = "Country")]
        public string Country { get; set; }
        [Display(Name = "Contact")]
        public string Contact { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Display(Name = "Special Instruction")]
        public string SpecialInstruction { get; set; }
        [Display(Name = "Drop Off Date & Time")]
        public DateTime DropOffDateAndTime { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsPick { get; set; }
        [Display(Name = "Document Type")]
        public BookingDocumentType BookingDocumentType { get; set; }
        public List<AddressModel> Addresses { get; set; }
        public List<NoteModel> Notes { get; set; }
        public List<BookingNotificationModel> BookingNotifications { get; set; }
        public List<ShipmentInformationDetailModel> ShipmentInformationDetails { get; set; }


    }
}
