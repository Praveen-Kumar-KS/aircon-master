using Aircon.Business.Models.Customer.ShipmentInformation;
using Aircon.Business.Models.Shared;
using Aircon.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Models.Customer.Bookings
{
    public class BookingModel
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Route { get; set; }
        public string Type { get; set; }
        public decimal ChargeableWeight { get; set; }
        public decimal Volume { get; set; }
        public decimal Quantity { get; set; }
        public ShipmentStatus ShipmentStatus { get; set; }
        public QuoteStatus QuoteStatus { get; set; }
        public QuoteType QuoteType { get; set; }
        public ServiceLevel ServiceLevel { get; set; }
        public ShipmentType ShipmentType { get; set; }
        public DateTime ArrivesOn { get; set; }
        public DateTime CutOffTime { get; set; }
        public int? QuoteId { get; set; }
        public string InternalReferenceNum { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Contact { get; set; }
        public string CompanyName { get; set; }
        public string SpecialInstruction { get; set; }
        public DateTime DropOffDateAndTime { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsPick { get; set; }
        public List<AddressModel> Addresses { get; set; }
        public List<NoteModel> Notes { get; set; }
        public List<ShipmentInformationDetailModel> ShipmentInformationDetails { get; set; }

    }
}
