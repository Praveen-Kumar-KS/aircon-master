using Aircon.Data.Entities;
using Aircon.Data.Enums;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.SampleData.Bogus
{
    public class QuotesBookingBogusData
    {
        public static Faker<Quote> Quotes { get; } =
            new Faker<Quote>()
            .RuleFor(x => x.OriginId, f => f.Random.Number(1, 20))
            .RuleFor(x => x.DestinationId, f => f.Random.Number(1, 20))
            .RuleFor(x => x.ArrivesOn, f => f.Date.Past(1))
            .RuleFor(x => x.PickUpDate, f => f.Date.Past(1))
            .RuleFor(x => x.DeliveryBy, f => f.Date.Past(1))
            .RuleFor(x => x.PickUpZipCode, f => f.Address.ZipCode())
            .RuleFor(x => x.IsKnownShipper, f => f.Random.Bool(1))
            .RuleFor(x => x.IsDimension, f => f.Random.Bool(1))
            .RuleFor(x => x.ServiceLevel, f => f.PickRandom(ServiceLevel.Standard, ServiceLevel.Expedited))
            .RuleFor(x => x.QuoteType, f => f.PickRandom(QuoteType.QuoteByVolume, QuoteType.Itemized))
            .RuleFor(x => x.ShipmentType, f => f.PickRandom(ShipmentType.DoorToAirport, ShipmentType.AirportToAirport))
            .RuleFor(x => x.QuoteStatus, f => f.PickRandom(QuoteStatus.InProgress, QuoteStatus.Drafts, QuoteStatus.Closed));


        public static List<Quote> GetQuotes()
        {
            return Quotes.Generate(new Faker().Random.Number(1, 20));
        }

        public static Faker<QuotePricing> QuotesPricings { get; } =
        new Faker<QuotePricing>()
        .RuleFor(x => x.QuoteId, f => f.Random.Number(8, 19))
        .RuleFor(x => x.PricingType, f => f.PickRandom(PricingType.ChargeableCost, PricingType.Airport, PricingType.DoorTrucking))
        .RuleFor(x => x.ItemName, f => f.Commerce.ProductName())
        .RuleFor(x => x.MarkupPercent, f => f.Random.Number(10, 100))
        .RuleFor(x => x.CustomerPrice, f => f.Random.Number(100, 1000))
        .RuleFor(x => x.Cost, f => f.Random.Number(100, 1000));
        public static List<QuotePricing> GetQuotePricing()
        {
            return QuotesPricings.Generate(20);
        }

        public static Faker<ShipmentInformationHeader> ShippingDetails { get; } =
           new Faker<ShipmentInformationHeader>()
            .RuleFor(x => x.TotalNoOfPieces, f => f.Random.Number(1, 100))
            .RuleFor(x => x.TotalVolume, f => f.Random.Decimal())
            .RuleFor(x => x.TotalChargeableVolume, f => f.Random.Decimal())
            .RuleFor(x => x.TotalChargeableWeight, f => f.Random.Decimal())
            .RuleFor(x => x.TotalVolumetricWeight, f => f.Random.Decimal());
            //.RuleFor(x => x.QuoteId, f => f.Random.Number(2,14));



        public static List<ShipmentInformationHeader> GetShipmentInformationHeader()
        {
            return ShippingDetails.Generate(20);
        }

        public static Faker<ShipmentInformationDetail> ShipmentInformationDetails { get; } =
         new Faker<ShipmentInformationDetail>()
          .RuleFor(x => x.Quantity, f => f.Random.Number(1, 100))
          .RuleFor(x => x.Length, f => f.Random.Decimal())
          .RuleFor(x => x.Height, f => f.Random.Decimal())
          .RuleFor(x => x.Width, f => f.Random.Decimal())
          .RuleFor(x => x.Weight, f => f.Random.Decimal())
          .RuleFor(x => x.Volume, f => f.Random.Decimal())
          .RuleFor(x => x.QuoteId, f => f.Random.Int(8, 19));

        public static List<ShipmentInformationDetail> GetShipmentInformationDetail()
        {
            return ShipmentInformationDetails.Generate(new Faker().Random.Number(1, 20));
        }

        public static Faker<Booking> Bookings { get; } =
        new Faker<Booking>()
            .RuleFor(x => x.CustomerName, f => f.Person.FullName)
            .RuleFor(x => x.FirstName, f => f.Person.FirstName)
            .RuleFor(x => x.LastName, f => f.Person.LastName)
            .RuleFor(x => x.Route, f => f.Random.ArrayElement<string>(Route().ToArray()))
            .RuleFor(x => x.Type, f => f.PickRandom("Door to Airport", "Airport to Airport"))
            .RuleFor(x => x.Volume, f => f.Random.Decimal())
            .RuleFor(x => x.Quantity, f => f.Random.Decimal())
            .RuleFor(x => x.ChargeableWeight, f => f.Random.Decimal())
            .RuleFor(x => x.ArrivesOn, f => f.Date.Past())
            .RuleFor(x => x.CutOffTime, f => f.Date.Past())
            .RuleFor(x => x.MainAddress, f => GetAddress())
            .RuleFor(x => x.BookingNotes, f => GetBookingNotes())
            .RuleFor(x => x.QuoteId, f => f.Random.Number(8,19))
            .RuleFor(x => x.MainAddress, f => BogusCustomerData.GetAddress())
            .RuleFor(x => x.UserId, f => f.Random.Number(1, 20));


        public static List<Booking> GetBooking()
        {
            return Bookings.Generate(new Faker().Random.Number(1, 25));
        }
        public static List<string> Route()
        {
            return new List<string> { "DAL-ABD", "DFW-AEH", "ELP-ABR", "HOU-ABJ", "IAH-ABI", "CRP-ABV" };
        }
        public static Faker<Address> SingleAddress { get; } =
           new Faker<Address>()
           .RuleFor(x => x.NickName, f => f.Address.BuildingNumber())
           .RuleFor(x => x.Line1, f => f.Address.StreetAddress())
           .RuleFor(x => x.Line2, f => f.Address.SecondaryAddress())
           .RuleFor(x => x.City, f => f.Address.City())
           .RuleFor(x => x.State, f => f.Address.State())
           .RuleFor(x => x.Zip, f => f.Address.ZipCode())
            ;
        public static Address GetAddress()
        {
            return SingleAddress.Generate();
        }
        public static Note GetSingleNote()
        {
            return Note.Generate();
        }

        public static Faker<Note> Note { get; } =
            new Faker<Note>()
            .RuleFor(x => x.Text, f => f.Lorem.Text())
            .RuleFor(x => x.CreatedOnUtc, f => f.Date.Past(1 / 12))
            .RuleFor(x => x.CreatedById, 1);

        public static Faker<BookingNote> BookingNotes { get; } =
            new Faker<BookingNote>()
           .RuleFor(x => x.Note, f => GetSingleNote())
             ;

        public static List<BookingNote> GetBookingNotes()
        {
            return BookingNotes.Generate(10);
        }
        public static Faker<Airport> Airport { get; } =
            new Faker<Airport>()
            .RuleFor(x => x.Ident, f => f.Random.ArrayElement<string>(Ident().ToArray()))
            .RuleFor(x => x.Name, f => f.Random.ArrayElement<string>(AirportName().ToArray()))
            .RuleFor(x => x.LongitudeDeg, f => f.Address.Longitude().ToString())
            .RuleFor(x => x.LatitudeDeg, f => f.Address.Latitude().ToString())
            .RuleFor(x => x.ElevationFt, f => f.Random.Number().ToString())
            .RuleFor(x => x.Continent, f => f.Random.ArrayElement<string>(Continent().ToArray()))
            .RuleFor(x => x.IsoRegion, f => f.Random.ArrayElement<string>(IsoRegion().ToArray()))
            .RuleFor(x => x.IsoCountry, f => f.Random.ArrayElement<string>(IsoCountry().ToArray()))
            .RuleFor(x => x.Municipality, f => f.Random.ArrayElement<string>(Municipality().ToArray()))
            .RuleFor(x => x.GpsCode, f => f.Random.ArrayElement<string>(GpsCode().ToArray()))
            .RuleFor(x => x.IataCode, f => f.Random.ArrayElement<string>(IataCode().ToArray()))
            .RuleFor(x => x.LocalCode, f => f.Random.ArrayElement<string>(LocalCode().ToArray()));


        public static List<string> Ident()
        {
            return new List<string> { "00AA", "00AK", "00AL", "00AR", "00AS", "00AZ", "00CA", "00CL" };

        }
        public static List<string> AirportName()
        {
            return new List<string> {
                                       "Total Rf Heliport",
                                       "Aero B Ranch Airport","Lowell Field",
                                       "Epps Airpark",
                                       "Newport Hospital & Clinic Heliport",
                                       "Fulton Airport",
                                       "Cordes Airport",
                                       "Goldstone (GTS) Airport"
                                    };
        }
        public static List<string> Continent()
        {
            return new List<string> { "NA", "SA", "EU", "AF", "AS", "OC" };

        }
        public static List<string> IsoRegion()
        {
            return new List<string> { "US-PA", "US-KS", "US-AK", "US-AL", "US-AR", "US-OK", "US-AZ", "US-CA", "US-CA" };

        }
        public static List<string> IsoCountry()
        {
            return new List<string> { "US", "KS", "AK", "AL", "AR", "AZ", "GB", "TF", "CA", "JP" };

        }
        public static List<string> Municipality()
        {
            return new List<string> { "Plain City", "Toledo", "Cleveland", "Columbus", "Kingfisher", "Tahlequah", "Chickasha", "Tipton", "Tulsa", "Reydon", "Blackwell" };

        }
        public static List<string> GpsCode()
        {
            return new List<string> { "00FD", "00FL", "00GA", "00GE", "00HI", "00ID", "00IG" };

        }
        public static List<string> IataCode()
        {
            return new List<string> { "ABD", "ABF", "AHJ", "ABA", "JEG", "LCG", "ZMH" };

        }
        public static List<string> LocalCode()
        {
            return new List<string> { "00IL", "00IN", "00IS", "00KS", "00KY", "00LA", "00LL" };

        }
        public static List<Airport> GetAirport()
        {
            return Airport.Generate(20);
        }
        public static Faker<BookingAddress> BookingAddresses { get; } =
        new Faker<BookingAddress>()
           .RuleFor(x => x.BookingId, f => f.Random.Int(3, 10))
           .RuleFor(x => x.ContactId, f => f.Random.Int(1, 16))
           .RuleFor(x => x.BookingAddressType, f => f.PickRandom(BookingAddressType.ShipperOrExporter, BookingAddressType.ConsigneeOrImporter))
            ;

        public static List<BookingAddress> GetBookingAddress()
        {
            return BookingAddresses.Generate(new Faker().Random.Number(1, 10));
        }

        public static Faker<BookingDocumentMaster> BookingDocumentMasters { get; } =
        new Faker<BookingDocumentMaster>()
          .RuleFor(x => x.BookingId, f => f.Random.Int(3, 10))
          .RuleFor(x => x.DocumentName, f => f.Company.CompanyName())
          //.RuleFor(x => x.AttachmentId, f => f.Random.Int(3, 10))
          .RuleFor(x => x.BookingDocumentType, f => f.PickRandom(BookingDocumentType.BillOfLading, BookingDocumentType.Photos, BookingDocumentType.Documents, BookingDocumentType.PackingList, BookingDocumentType.CommercialInvoice, BookingDocumentType.Other))
           ;
        public static List<BookingDocumentMaster> GetBookingDocMaster()
        {
            return BookingDocumentMasters.Generate(new Faker().Random.Number(1, 10));
        }

        public static Faker<BookingNotification> BookingNotifications { get; } =
        new Faker<BookingNotification>()
         .RuleFor(x => x.BookingId, f => f.Random.Int(3, 10))
         .RuleFor(x => x.ContactId, f => f.Random.Int(1, 40))
          ;
        public static List<BookingNotification> GetBookingNotification()
        {
            return BookingNotifications.Generate(new Faker().Random.Number(1, 10));
        }
    }
}
