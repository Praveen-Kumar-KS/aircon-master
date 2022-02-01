using Aircon.Areas.Customer.Models.ShipmentInformation;
using Aircon.Business.Models.Customer.Quotes;
using Aircon.Data.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Areas.Customer.Models.Quotes
{
    public class QuoteViewModel
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
        [Display(Name = "Customer")]
        public string CustomerName { get; set; }
        public string ChargeableWeight { get; set; }
        public string Volume { get; set; }
        public int Quantity { get; set; }
        public int TotalNoOfPieces { get; set; }
        public decimal TotalVolumetricWeight { get; set; }
        public decimal TotalChargeableWeight { get; set; }
        public decimal TotalVolume { get; set; }
        public decimal TotalChargeableVolume { get; set; }
        public ServiceLevel ServiceLevel { get; set; }
        public QuoteType QuoteType { get; set; }
        public ShipmentStatus ShipmentStatus { get; set; }
        public ShipmentType ShipmentType { get; set; }
        public MeasurementUnit MeasurementUnit { get; set; }
        public decimal Weight { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public ShipmentInformationHeaderViewModel ShipmentInformationHeader { get; set; } 
        public List<ShipmentInformationDetailViewModel> ShipmentInformationDetail { get; set; }
        public ShipmentDetailViewModel ShipmentDetailModel { get; set; }
        public List<SelectListItem> Origin { get; set; }
        public List<SelectListItem> Destination { get; set; }
    }
}
