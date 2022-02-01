using Aircon.Business.Models.Customer.Profile;
using Aircon.Business.Models.Customer.Quotes;
using Aircon.Business.Models.Customer.ShipmentInformation;
using Aircon.Data;
using Aircon.Data.Entities;
using Aircon.Data.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Services.Customer
{
    public interface IQuotesService
    {
        List<QuoteModel> GetQuote(string searchText, QuoteStatus QuoteStatus, int recordCountQuotesQueue);
        QuoteModel GetShipmentInformationHeaders(int Id);
        QuoteModel GetQuoteForCloneById(int id);
        QuoteModel AddQuote(QuoteModel quoteModel);
        QuoteModel UpdateQuote(QuoteModel quoteUpdateModel);
        QuoteModel GetQuote(int id);
        void DeleteQuote(int id);
        QuotesPricingModel GetQuoteById(int id);
        QuotesPricingModel SaveReviewQuote(QuotesPricingModel quotesPricingModel);
        QuoteModel GetReviewAndInitiateBooking(int Id);
        List<SelectListItem> GetOrigin();
        List<SelectListItem> GetDestination();

    }

    public class QuotesService : IQuotesService
    {
        private readonly AirconDbContext _airconDbContext;
        public QuotesService(AirconDbContext airconDbContext)
        {
            _airconDbContext = airconDbContext;
        }
        public List<QuoteModel> GetQuote(string searchText, QuoteStatus quoteStatus, int recordCountQuotesQueue)
        {
            var quotes = _airconDbContext.Quotes.Include(x => x.ShipmentInformationHeader).Include(x => x.Customer).Where(x => x.QuoteStatus == quoteStatus)
            .Select(x => new QuoteModel
            {
                Id = x.Id,
                ShipmentType = x.ShipmentType,
                ArrivesOn = x.ArrivesOn,
                ServiceLevel = x.ServiceLevel,
                QuoteStatus = x.QuoteStatus,
                CustomerName = x.Customer == null ? null :x.Customer.AdminName,
                Route = x.Origin == null ? null : x.Origin.IataCode + ">" + x.Destination == null ? null : x.Destination.IataCode,
                ChargeableWeight = x.ShipmentInformationHeader == null ? 0 : x.ShipmentInformationHeader.TotalChargeableWeight,
                Quantity = x.ShipmentInformationHeader == null ? 0 : x.ShipmentInformationHeader.TotalNoOfPieces,
                Volume = x.ShipmentInformationHeader == null ? 0 : x.ShipmentInformationHeader.TotalVolume,

            });
            if (searchText != null)
            {
             // var shipmentType = Enum.GetName(typeof(ShipmentType), 1);
                quotes =
                   quotes.Where(x =>
                          (x.CustomerName == null ? false : x.CustomerName.ToUpper().Contains(searchText.ToUpper())) 
                          //(x.OriginName == null ? false : x.OriginName.ToUpper().Contains(searchText.ToUpper())) ||
                          //(x.DestinatioName == null ? false : x.DestinatioName.ToUpper().Contains(searchText.ToUpper()))
                          //(x.ShipmentType.ToString() == null ? false : x.ShipmentType.ToString().Contains(searchText))
                          //Enum.GetName(typeof(ShipmentType), x.ShipmentType).ToUpper().Contains(searchText.ToUpper())
                          ).Select(y => y);
            }
            quotes = quotes.Take(recordCountQuotesQueue);
            return quotes.ToList();
            
        }
        public QuoteModel GetShipmentInformationHeaders(int QuoteId)
        {
            return _airconDbContext.ShippingDetails.Where(x => x.Id == QuoteId)
                 .Select(x => new QuoteModel
                 {
                     Id = x.Id,
                     TotalChargeableVolume = x.TotalChargeableVolume,
                     TotalChargeableWeight = x.TotalChargeableWeight,
                     TotalNoOfPieces = x.TotalNoOfPieces,
                     TotalVolume = x.TotalVolume,
                     TotalVolumetricWeight = x.TotalVolumetricWeight

                 }).SingleOrDefault();
        }
        public QuoteModel GetQuote(int id)
        {

            var quote = _airconDbContext.Quotes.Where(x => x.Id == id).Include(x => x.Customer).Include(x => x.ShipmentInformationHeader)
                            .Select(x => new QuoteModel
                            {
                                Id = x.Id,
                                OriginId = x.OriginId,
                                DestinationId = x.DestinationId,
                                ShipmentHeaderId = x.ShipmentHeaderId == null ? 0 : x.ShipmentHeaderId,
                                CustomerId = x.CustomerId == null ? 0 : x.CustomerId,
                                MeasurementUnit = x.MeasurementUnit,
                                ShipmentType = x.ShipmentType,
                                QuoteType = x.QuoteType,
                                IsKnownShipper = x.IsKnownShipper,
                                IsDimension = x.IsDimension,
                                OriginName = x.Origin.IataCode,
                                DestinatioName = x.Destination.IataCode,
                                ArrivesOn = x.ArrivesOn,
                                ServiceLevel = x.ServiceLevel,
                                PickUpZipCode = x.PickUpZipCode,
                                PickUpDate = x.PickUpDate,
                                DeliveryBy=x.DeliveryBy,
                                ShipmentInformationDetail = x.ShipmentInformationDetails.Where(x => x.QuoteId == id).Select(x => new ShipmentInformationDetailModel
                                {
                                    Volume = x.Volume,
                                    Quantity = x.Quantity,
                                    Length = x.Length,
                                    Height = x.Height,
                                    Width = x.Width,
                                    Weight = x.Weight
                                }).ToList(),
                                ShipmentDetailModel = x.ShipmentInformationDetails.Where(x => x.QuoteId == id).Select(x => new ShipmentDetailModel
                                {
                                    Volume = x.Volume,
                                    Quantity =  x.Quantity,
                                    Weight = x.Weight
                                }).SingleOrDefault(),
                                TotalChargeableWeight = x.ShipmentInformationHeader == null ? 0 : x.ShipmentInformationHeader.TotalChargeableWeight,
                                TotalChargeableVolume = x.ShipmentInformationHeader == null ? 0 : x.ShipmentInformationHeader.TotalChargeableVolume,
                                TotalVolumetricWeight = x.ShipmentInformationHeader == null ? 0 : x.ShipmentInformationHeader.TotalVolumetricWeight,
                                TotalNoOfPieces = x.ShipmentInformationHeader == null ? 0 : x.ShipmentInformationHeader.TotalNoOfPieces,
                                TotalVolume = x.ShipmentInformationHeader == null ? 0 : x.ShipmentInformationHeader.TotalVolume
                            }).SingleOrDefault();
            return quote;
        }
        public QuoteModel AddQuote(QuoteModel quoteModel)
        {
            Quote quote = new Quote();
            quote.IsKnownShipper = quoteModel.IsKnownShipper;
            quote.IsDimension = quoteModel.IsDimension;
            quote.MeasurementUnit = quoteModel.MeasurementUnit;
            quote.OriginId = quoteModel.OriginId;
            quote.DestinationId = quoteModel.DestinationId;
            quote.QuoteType = quoteModel.QuoteType;
            quote.ShipmentType = quoteModel.ShipmentType;
            quote.DeliveryBy = quoteModel.DeliveryBy;
            quote.PickUpDate = quoteModel.PickUpDate;
            quote.PickUpZipCode = quoteModel.PickUpZipCode;
            quote.ServiceLevel = quoteModel.ServiceLevel;
            quote.ArrivesOn = quoteModel.ArrivesOn;
            quote.CustomerId = 1;
            quote.QuoteStatus = QuoteStatus.InProgress;
            _airconDbContext.Quotes.Add(quote);
            _airconDbContext.SaveChanges();
            if(quote.Id != 0)
            {
                quoteModel.Id = quote.Id;
                if (quote.QuoteType == QuoteType.Itemized)
                {
                    foreach (var i in quoteModel.ShipmentInformationDetail)
                    {
                        ShipmentInformationDetail shipmentInformationDetail = new ShipmentInformationDetail();
                        shipmentInformationDetail.QuoteId = quote.Id;
                        shipmentInformationDetail.Volume = i.Volume;
                        shipmentInformationDetail.Weight = i.Weight;
                        shipmentInformationDetail.Quantity = i.Quantity;
                        shipmentInformationDetail.Length = i.Length;
                        shipmentInformationDetail.Width = i.Width;
                        shipmentInformationDetail.Height = i.Height;
                        _airconDbContext.ShipmentInformationDetails.Add(shipmentInformationDetail);
                        _airconDbContext.SaveChanges();
                    }
                }
                else {

                    ShipmentInformationDetail shipmentInformationDetail = new ShipmentInformationDetail();
                    shipmentInformationDetail.QuoteId = quote.Id;
                    shipmentInformationDetail.Quantity = quoteModel.ShipmentDetailModel.Quantity;
                    shipmentInformationDetail.Volume = quoteModel.ShipmentDetailModel.Volume;
                    shipmentInformationDetail.Weight = quoteModel.ShipmentDetailModel.Weight;
                    _airconDbContext.ShipmentInformationDetails.Add(shipmentInformationDetail);
                    _airconDbContext.SaveChanges();
                }
                var shipmentDetails = _airconDbContext.ShipmentInformationDetails.Where(x => x.QuoteId == quote.Id).ToList();
                if(shipmentDetails.Count > 0)
                {
                    ShipmentInformationHeader shipmentInformationHeader = new ShipmentInformationHeader();
                    //shipmentInformationHeader.QuoteId = quote.Id;
                    shipmentInformationHeader.TotalNoOfPieces = (shipmentDetails.Sum(x => x.Quantity));
                    shipmentInformationHeader.TotalVolumetricWeight = (shipmentDetails.Sum(x => x.Length) * shipmentDetails.Sum(x => x.Width) * shipmentDetails.Sum(x => x.Height) / 166);
                    shipmentInformationHeader.TotalVolume = (shipmentDetails.Sum(x => x.Length) * shipmentDetails.Sum(x => x.Width) * shipmentDetails.Sum(x => x.Height));
                    shipmentInformationHeader.TotalChargeableWeight = (shipmentDetails.Sum(x => x.Length) * shipmentDetails.Sum(x => x.Width) * shipmentDetails.Sum(x => x.Height) / 166);
                    shipmentInformationHeader.TotalChargeableVolume= (shipmentDetails.Sum(x => x.Length) * shipmentDetails.Sum(x => x.Width) * shipmentDetails.Sum(x => x.Height));
                    _airconDbContext.ShippingDetails.Add(shipmentInformationHeader);
                    _airconDbContext.SaveChanges();
                    quote.ShipmentHeaderId = shipmentInformationHeader.Id;
                   _airconDbContext.Quotes.Update(quote);
                    _airconDbContext.SaveChanges();
                }
            }
            return quoteModel;
        }


        public QuoteModel UpdateQuote(QuoteModel quoteUpdateModel)
        {
            Quote quote = _airconDbContext.Quotes.Find(quoteUpdateModel.Id);
            quote.IsKnownShipper = quoteUpdateModel.IsKnownShipper;
            quote.IsDimension = quoteUpdateModel.IsDimension;
            quote.MeasurementUnit = quoteUpdateModel.MeasurementUnit;
            quote.QuoteType = quoteUpdateModel.QuoteType;
            quote.ShipmentType = quoteUpdateModel.ShipmentType;
            quote.OriginId = quoteUpdateModel.OriginId;
            quote.DestinationId = quoteUpdateModel.DestinationId;
            quote.DeliveryBy = quoteUpdateModel.DeliveryBy;
            quote.PickUpDate = quoteUpdateModel.PickUpDate;
            quote.PickUpZipCode = quoteUpdateModel.PickUpZipCode;
            quote.ServiceLevel = quoteUpdateModel.ServiceLevel;
            quote.ArrivesOn = quoteUpdateModel.ArrivesOn;
            quote.CustomerId = quoteUpdateModel.CustomerId;
            _airconDbContext.Quotes.Update(quote);
            _airconDbContext.SaveChanges();
            if (quote.Id != 0)
            {
                quoteUpdateModel.Id = quote.Id;
                if (quote.QuoteType == QuoteType.Itemized)
                {
                    foreach (var i in quoteUpdateModel.ShipmentInformationDetail)
                    {
                        ShipmentInformationDetail shipmentInformationDetail = new ShipmentInformationDetail();
                        shipmentInformationDetail.QuoteId = quote.Id;
                        shipmentInformationDetail.Volume = i.Volume;
                        shipmentInformationDetail.Weight = i.Weight;
                        shipmentInformationDetail.Quantity = i.Quantity;
                        shipmentInformationDetail.Length = i.Length;
                        shipmentInformationDetail.Width = i.Width;
                        shipmentInformationDetail.Height = i.Height;
                        _airconDbContext.ShipmentInformationDetails.Add(shipmentInformationDetail);
                        _airconDbContext.SaveChanges();
                    }
                }
                else
                {

                    ShipmentInformationDetail shipmentInformationDetail = new ShipmentInformationDetail();
                    shipmentInformationDetail.QuoteId = quote.Id;
                    shipmentInformationDetail.Quantity = quoteUpdateModel.ShipmentDetailModel.Quantity;
                    shipmentInformationDetail.Volume = quoteUpdateModel.ShipmentDetailModel.Volume;
                    shipmentInformationDetail.Weight = quoteUpdateModel.ShipmentDetailModel.Weight;
                    _airconDbContext.ShipmentInformationDetails.Add(shipmentInformationDetail);
                    _airconDbContext.SaveChanges();
                }
                var shipmentDetails = _airconDbContext.ShipmentInformationDetails.Where(x => x.QuoteId == quote.Id).ToList();
                if (shipmentDetails.Count > 0)
                {
                    ShipmentInformationHeader shipmentInformationHeader = new ShipmentInformationHeader();
                    //shipmentInformationHeader.QuoteId = quote.Id;
                    shipmentInformationHeader.TotalNoOfPieces = (shipmentDetails.Sum(x => x.Quantity));
                    shipmentInformationHeader.TotalVolumetricWeight = (shipmentDetails.Sum(x => x.Length) * shipmentDetails.Sum(x => x.Width) * shipmentDetails.Sum(x => x.Height) / 166);
                    shipmentInformationHeader.TotalVolume = (shipmentDetails.Sum(x => x.Length) * shipmentDetails.Sum(x => x.Width) * shipmentDetails.Sum(x => x.Height));
                    shipmentInformationHeader.TotalChargeableWeight = (shipmentDetails.Sum(x => x.Length) * shipmentDetails.Sum(x => x.Width) * shipmentDetails.Sum(x => x.Height) / 166);
                    shipmentInformationHeader.TotalChargeableVolume = (shipmentDetails.Sum(x => x.Length) * shipmentDetails.Sum(x => x.Width) * shipmentDetails.Sum(x => x.Height));
                    _airconDbContext.ShippingDetails.Add(shipmentInformationHeader);
                    _airconDbContext.SaveChanges();
                    quote.ShipmentHeaderId = shipmentInformationHeader.Id;
                    _airconDbContext.Quotes.Update(quote);
                    _airconDbContext.SaveChanges();
                }
            }
            return quoteUpdateModel;
        }

        public void DeleteQuote(int id)
        {
            var bookingQuoteId = _airconDbContext.Bookings.Where(x => x.QuoteId == id).Select(x => x.QuoteId).ToList();
            if (bookingQuoteId.Count == 0)
            {
                Quote quote = _airconDbContext.Quotes.Find(id);
                _airconDbContext.Quotes.Remove(quote);
                _airconDbContext.SaveChanges();
            }
        }
        public QuoteModel GetQuoteForCloneById(int id)
        {
            QuoteModel cloneQuote = new QuoteModel();
            cloneQuote = (from x in _airconDbContext.Quotes.AsQueryable().Include(x => x.Customer).Include(x => x.ShipmentInformationHeader)
                               .Where(x => x.Id == id)
                          select new QuoteModel
                          {
                              Id = x.Id,
                              OriginId = x.OriginId,
                              DestinationId = x.DestinationId,
                              ShipmentHeaderId = x.ShipmentHeaderId == null ? 0 : x.ShipmentHeaderId,
                              CustomerId = x.CustomerId == null ? 0 : x.CustomerId,
                              MeasurementUnit = x.MeasurementUnit,
                              ShipmentType = x.ShipmentType,
                              QuoteType = x.QuoteType,
                              IsKnownShipper = x.IsKnownShipper,
                              IsDimension = x.IsDimension,
                              OriginName = x.Origin.IataCode,
                              DestinatioName = x.Destination.IataCode,
                              ArrivesOn = x.ArrivesOn,
                              ServiceLevel = x.ServiceLevel,
                              PickUpZipCode = x.PickUpZipCode,
                              PickUpDate = x.PickUpDate,
                              ShipmentInformationDetail = x.ShipmentInformationDetails.Where(x => x.QuoteId ==id).Select(x => new ShipmentInformationDetailModel {
                                  Volume = x.Volume,
                                  Quantity = x.Quantity,
                                  Length = x.Length,
                                  Height = x.Height,
                                  Width = x.Width,
                                  Weight = x.Weight
                              }).ToList(),
                              TotalChargeableWeight = x.ShipmentInformationHeader == null ? 0 : x.ShipmentInformationHeader.TotalChargeableWeight,
                              TotalChargeableVolume = x.ShipmentInformationHeader == null ? 0 : x.ShipmentInformationHeader.TotalChargeableVolume,
                              TotalVolumetricWeight = x.ShipmentInformationHeader == null ? 0 : x.ShipmentInformationHeader.TotalVolumetricWeight,
                              TotalNoOfPieces = x.ShipmentInformationHeader == null ? 0 : x.ShipmentInformationHeader.TotalNoOfPieces,
                              TotalVolume = x.ShipmentInformationHeader == null ? 0 : x.ShipmentInformationHeader.TotalVolume
                          }).SingleOrDefault();
            return cloneQuote;
        }

        public QuotesPricingModel GetQuoteById(int id)
        {
            return _airconDbContext.Quotes.Include(x=>x.Customer).Include(x=>x.Origin).Include(x=>x.Destination).Include(x=>x.Destination)
                .Where(x => x.Id == id)
                .Select(x => new QuotesPricingModel
                {
                    QuoteId = x.Id,
                    Quotes = new QuoteModel
                    {
                        CustomerName = x.Customer == null ? null : x.Customer.AdminName,
                        ShipmentType = x.ShipmentType,
                        Route = x.Origin == null ? null : x.Origin.IataCode + ">" + x.Destination == null ? null : x.Destination.IataCode,
                        ArrivesOn = x.ArrivesOn,
                        ServiceLevel = x.ServiceLevel,
                        PickUpZipCode = x.PickUpZipCode,
                        ChargeableWeight = x.ShipmentInformationHeader == null ? 0 : x.ShipmentInformationHeader.TotalChargeableWeight,
                        Quantity = x.ShipmentInformationHeader == null ? 0 : x.ShipmentInformationHeader.TotalNoOfPieces,
                        Volume = x.ShipmentInformationHeader == null ? 0 : x.ShipmentInformationHeader.TotalVolume

                    }
                }).SingleOrDefault();
        }

        public QuoteModel GetReviewAndInitiateBooking(int Id)
        {
            return _airconDbContext.Quotes.Where(x => x.Id == Id)
                 .Select(x => new QuoteModel
                 {
                     Id = x.Id,
                     CustomerId = x.CustomerId == null ? 0 :x.CustomerId,
                     CustomerName = x.Customer.AdminName,
                     ShipmentType = x.ShipmentType,
                     Route = x.Origin.IataCode == null ? null : x.Origin.IataCode + ">" + x.Destination.IataCode == null ? null : x.Destination.IataCode,
                     ArrivesOn = x.ArrivesOn,
                     ServiceLevel = x.ServiceLevel,
                     ChargeableWeight = x.ShipmentInformationHeader == null ? 0 : x.ShipmentInformationHeader.TotalChargeableWeight,
                     Quantity = x.ShipmentInformationHeader == null ? 0 : x.ShipmentInformationHeader.TotalNoOfPieces,
                     Volume = x.ShipmentInformationHeader == null ? 0 :x.ShipmentInformationHeader.TotalVolume

                 }).SingleOrDefault();
        }
        public QuotesPricingModel SaveReviewQuote(QuotesPricingModel quotesPricingModel)
        {
            var ShipmentType = _airconDbContext.Quotes.Where(x => x.Id == quotesPricingModel.QuoteId).Select(x => x.ShipmentType).SingleOrDefault();
            if (ShipmentType == ShipmentType.DoorToAirport)
            {
                QuotePricing saveQuotePricing = new QuotePricing();
                saveQuotePricing.QuoteId = quotesPricingModel.QuoteId;
                saveQuotePricing.Cost = quotesPricingModel.DoorTruckingCost;
                saveQuotePricing.MarkupPercent = quotesPricingModel.DoorTruckingMarkupPercent;
                saveQuotePricing.CustomerPrice = quotesPricingModel.DoorTrucingTotalCost;
                saveQuotePricing.PricingType = PricingType.DoorTrucking;
                saveQuotePricing.AddAdditionalTerms = quotesPricingModel.AddAdditionalTerms;
                _airconDbContext.QuotePricings.Add(saveQuotePricing);
                _airconDbContext.SaveChanges();
            }
                QuotePricing quotePricing = new QuotePricing();
                quotePricing.QuoteId = quotesPricingModel.QuoteId;
                quotePricing.Cost = quotesPricingModel.AirFreightCost;
                quotePricing.MarkupPercent = quotesPricingModel.AirFreightMarkupPercent;
                quotePricing.CustomerPrice = quotesPricingModel.AirFreightTotalCost;
                quotePricing.PricingType = PricingType.Airport;
                quotePricing.AddAdditionalTerms = quotesPricingModel.AddAdditionalTerms;
                _airconDbContext.QuotePricings.Add(quotePricing);
                _airconDbContext.SaveChanges();

            foreach (var i in quotesPricingModel.chargeableList)
            {
                QuotePricing quotePricinglist = new QuotePricing();
                quotePricinglist.QuoteId = quotesPricingModel.QuoteId;
                quotePricinglist.ItemName = i.ChargeableName;
                quotePricinglist.CustomerPrice = i.ChargeableAmount;
                quotePricinglist.PricingType = PricingType.ChargeableCost;
                quotePricinglist.AddAdditionalTerms = quotePricinglist.AddAdditionalTerms;
                _airconDbContext.QuotePricings.Add(quotePricinglist);
                _airconDbContext.SaveChanges();
            }
            return quotesPricingModel;
          
        }
        public List<SelectListItem> GetOrigin()
        {
            return _airconDbContext.Airports.AsQueryable().Select(x => new SelectListItem
            {
                Text = x.IataCode,
                Value = x.Id.ToString()
            }).ToList();
        }
        public List<SelectListItem> GetDestination()
        {
            return _airconDbContext.Airports.AsQueryable().Select(x => new SelectListItem
            {
                Text = x.IataCode,
                Value = x.Id.ToString()
            }).ToList();
        }
    }
}