using Aircon.Business.Models.Customer.Bookings;
using Aircon.Business.Models.Customer.Contact;
using Aircon.Business.Models.Shared;
using Aircon.Business.Models.Customer.Quotes;
using Aircon.Data;
using Microsoft.EntityFrameworkCore;
using Aircon.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Aircon.Business.Models.Customer.ShipmentInformation;

namespace Aircon.Business.Services.Customer
{
    public interface IBookingService
    {
        List<BookingModel> GetBooking(string searchText, ShipmentStatus ShipmentStatus, int recordCountBookingsQueue);
        BookingModel GetQuoteBooking(int QuoteId);
        List<CustomerContactModel> GetCustomerContacts(int customerId, string searchText);
        PopulateAddressModel GetPopulateAddress(int AddressId, int ContactId);


    }
    public class BookingService : IBookingService
    {
        private readonly AirconDbContext _airconDbContext;
        public BookingService(AirconDbContext airconDbContext)
        {
            _airconDbContext = airconDbContext;
        }

        public List<BookingModel> GetBooking(string searchText, ShipmentStatus ShipmentStatus, int recordCountBookingsQueue)
        {
            var bookings = _airconDbContext.Bookings.Where(x => x.ShipmentStatus == ShipmentStatus)
                .Select(x => new BookingModel
                {
                    Id = x.Id,
                    CustomerName = x.CustomerName == null ? null : x.CustomerName,
                    Route = x.Route == null ? null : x.Route,
                    ChargeableWeight = x.ChargeableWeight,
                    Quantity = x.Quantity,
                    Volume = x.Volume,
                    ServiceLevel = x.Quote.ServiceLevel,
                    ArrivesOn = x.ArrivesOn,
                    QuoteId = x.QuoteId,
                    ShipmentStatus = x.ShipmentStatus
                });
            if (searchText != null)
            {
                bookings =
                     bookings.Where(x =>
                          (x.CustomerName == null ? false : x.CustomerName.ToUpper().Contains(searchText.ToUpper())) 
                          //(x.Route == null ? false : x.Route.ToUpper().Contains(searchText.ToUpper()))
                          //(x.Type == null ? false : x.Type.ToUpper().Contains(searchText.ToUpper()))
                          ).Select(y => y);
            }
            bookings = bookings.Take(recordCountBookingsQueue);
            return bookings.ToList();
        }

        public BookingModel GetQuoteBooking(int QuoteId)
        {
            BookingModel booking = new BookingModel();
            booking = _airconDbContext.Quotes.Where(x => x.Id == QuoteId).Include(x => x.Customer).Include(x => x.Origin).Include(x => x.Destination)
                 .Select(x => new BookingModel
                 {
                     Id = x.Id,
                     CustomerId = x.CustomerId,
                     CustomerName = x.Customer.AdminName,
                     ShipmentType = x.ShipmentType,
                     Route = x.Origin.IataCode + ">" + x.Destination.IataCode,
                     ArrivesOn = x.ArrivesOn,
                     ServiceLevel = x.ServiceLevel,
                     QuoteId = x.Id,
                     QuoteType = x.QuoteType,
                     ChargeableWeight = x.ShipmentInformationHeader == null ? 0 : x.ShipmentInformationHeader.TotalChargeableWeight,
                     Quantity = x.ShipmentInformationHeader == null ? 0 : x.ShipmentInformationHeader.TotalNoOfPieces,
                     Volume = x.ShipmentInformationHeader == null ? 0 : x.ShipmentInformationHeader.TotalVolume,
                     ShipmentInformationDetails = x.ShipmentInformationDetails.Where(x => x.QuoteId == QuoteId).Select(x => new ShipmentInformationDetailModel
                     {
                         Volume = x.Volume,
                         Quantity = x.Quantity,
                         Length = x.Length,
                         Height = x.Height,
                         Width = x.Width,
                         Weight = x.Weight
                     }).ToList(),
                 }).SingleOrDefault();
            
            return booking;
        }

        public List<CustomerContactModel> GetCustomerContacts(int quoteId, string searchText)
        {
            var customerId = _airconDbContext.Quotes.AsQueryable().Where(x => x.Id == quoteId).Select(x => x.CustomerId).SingleOrDefault();
            var customerContacts = _airconDbContext.CustomerContacts.Where(x => x.Id == customerId)
                .Include(x => x.Contact)
                .Include(x => x.Address)
                .Select(x => new CustomerContactModel
                {
                    CustomerId = x.Id,
                    AddressId = x.AddressId,
                    ContactId = x.ContactId,
                    Contact = new ContactModel
                    {
                        FirstName = x.Contact.FirstName,
                        LastName = x.Contact.LastName,
                        CompanyName = x.Contact.CompanyName,
                        Title = x.Contact.Title,
                        Department = x.Contact.Department,
                        PhoneNumber = x.Contact.PhoneNumber,
                        Email = x.Contact.Email,
                        Active = x.Contact.Active,
                        SpecialInstruction = x.Contact.SpecialInstruction,
                        NickName = x.Address.NickName,
                        Line1 = x.Address.Line1,
                        Line2 = x.Address.Line2,
                        City = x.Address.City,
                        State = x.Address.State,
                        Zip = x.Address.Zip
                    },
                    Address = new AddressModel
                    {
                        NickName = x.Address.NickName,
                        Line1 = x.Address.Line1,
                        Line2 = x.Address.Line2,
                        City = x.Address.City,
                        State = x.Address.State,
                        Zip = x.Address.Zip,
                        IsActive = x.Address.IsActive,
                        Id = x.Address.Id
                    }
                }).ToList();
            if (searchText != null)
            {
                customerContacts =
                    customerContacts.Where(x =>
                         (x.Contact.FirstName == null ? false : x.Contact.FirstName.ToUpper().Contains(searchText.ToUpper())) ||
                         (x.Contact.LastName == null ? false : x.Contact.LastName.ToUpper().Contains(searchText.ToUpper())) ||
                         (x.Contact.CompanyName == null ? false : x.Contact.CompanyName.ToUpper().Contains(searchText.ToUpper())) ||
                         (x.Contact.Title == null ? false : x.Contact.Title.ToUpper().Contains(searchText.ToUpper())) ||
                         (x.Contact.Email == null ? false : x.Contact.Email.ToUpper().Contains(searchText.ToUpper())) ||
                         (x.Contact.PhoneNumber == null ? false : x.Contact.PhoneNumber.Contains(searchText)) ||
                         (x.Address.Line1 == null ? false : x.Address.Line1.ToUpper().Contains(searchText.ToUpper())) ||
                         (x.Address.Line2 == null ? false : x.Address.Line2.ToUpper().Contains(searchText.ToUpper())) ||
                         (x.Address.City == null ? false : x.Address.City.ToUpper().Contains(searchText.ToUpper())) ||
                         (x.Address.State == null ? false : x.Address.State.ToUpper().Contains(searchText.ToUpper())) ||
                         (x.Address.Zip == null ? false : x.Address.Zip.Contains(searchText))
                        ).Select(y => y).ToList();
            }
            return customerContacts;
        }

        public PopulateAddressModel GetPopulateAddress(int ContactId, int AddressId)
        {
            PopulateAddressModel populateAddressModel = new PopulateAddressModel();
            populateAddressModel = _airconDbContext.CustomerContacts
                .Where(x => x.ContactId == ContactId && x.AddressId == AddressId)
                .Select(x => new PopulateAddressModel
                {
                    Id = x.Id,
                    AddressLine1 = x.Address.Line1,
                    AddressLine2 = x.Address.Line2,
                    City = x.Address.City,
                    State = x.Address.State,
                    Zip = x.Address.Zip,
                    //Country = x.Address.Co,
                    //Contact = x.,
                    CompanyName = x.Contact.CompanyName

                }).SingleOrDefault();
            return populateAddressModel;

        }

    }
}
