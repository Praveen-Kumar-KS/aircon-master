using Aircon.Business.Models.Customer.Airports;
using Aircon.Business.Models.Customer.Quotes;
using Aircon.Business.Models.Customer.ShipmentInformation;
using Aircon.Data.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Models.Customer.Quotes
{
    public class QuoteModel
    {
        public int Id { get; set; }
        public bool IsKnownShipper { get; set; }
        public bool IsDimension { get; set; }
        public string Route { get; set; }
        public int? OriginId { get; set; }
        public string OriginName { get; set; }
        public int? DestinationId { get; set; }
        public int? ShipmentHeaderId { get; set; }
        public string DestinatioName { get; set; }
        public DateTime? PickUpDate { get; set; }
        public DateTime ArrivesOn { get; set; }
        public DateTime? DeliveryBy { get; set; }
        public string PickUpZipCode { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public decimal ChargeableWeight { get; set; }
        public decimal Volume { get; set; }
        public int Quantity { get; set; }
        public decimal Weight { get; set; }
        public int TotalNoOfPieces { get; set; }
        public decimal TotalVolumetricWeight { get; set; }
        public decimal TotalChargeableWeight { get; set; }
        public decimal TotalVolume { get; set; }
        public decimal TotalChargeableVolume { get; set; }
        public ServiceLevel ServiceLevel { get; set; }
        public QuoteType QuoteType { get; set; }
        public ShipmentStatus ShipmentStatus { get; set; }
        public QuoteStatus QuoteStatus { get; set; }
        public ShipmentType ShipmentType { get; set; }
        public MeasurementUnit MeasurementUnit { get; set; }
        public ShipmentInformationHeaderModel ShipmentInformationHeader { get; set; }
        public List<ShipmentInformationDetailModel> ShipmentInformationDetail { get; set; }
        public ShipmentDetailModel ShipmentDetailModel { get; set; }
        public List<SelectListItem> Origin { get; set;}
        public List<SelectListItem> Destination { get; set; }

    }
}
