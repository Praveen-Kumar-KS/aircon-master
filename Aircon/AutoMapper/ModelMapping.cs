using Aircon.Areas.Admin.Models.Customer;
using Aircon.Areas.Customer.Models.Company;
using Aircon.Areas.Customer.Models.Contact;
using Aircon.Areas.Customer.Models.Preference;
using Aircon.Areas.Customer.Models.Profile;
using Aircon.Areas.Identity.Models.SignUp;
using Aircon.Business.Models.Admin.Customer;
using Aircon.Business.Models.Customer.Company;
using Aircon.Business.Models.Customer.Contact;
using Aircon.Business.Models.Customer.Preference;
using Aircon.Business.Models.Customer.Profile;
using Aircon.Business.Models.Identity.SignUp;
using Aircon.Business.Models.Shared;
using Vg.Common.Notification.Data;
using Aircon.ViewModels.Shared;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aircon.Business.Models.Customer.Quotes;
using Aircon.Areas.Customer.Models.Quotes;
using Aircon.Business.Models.Customer.Bookings;
using Aircon.Areas.Customer.Models.Bookings;
using Aircon.Areas.Customer.Models.ShipmentInformation;
using Aircon.Business.Models.Customer.ShipmentInformation;
using Aircon.Business.Models.Customer.Airports;
using Aircon.Areas.Customer.Models.Airports;

        
namespace Aircon.AutoMapper
{
    public class ModelMapping : Profile
    {
        public ModelMapping()
        {

            RecognizePostfixes("Utc");
            RecognizeDestinationPostfixes("Utc");

            ForAllPropertyMaps(x => x.DestinationType == typeof(DateTime) && x.SourceType == typeof(DateTime) && x.SourceMember.Name.ToLowerInvariant().EndsWith("utc") && !x.DestinationMember.Name.ToLowerInvariant().EndsWith("utc"), (map, expression) =>
            {
                expression.MapFrom<TimeZoneUtcToLocalContextDateTimeResolver, DateTime>(map.SourceMember.Name);
            });
            ForAllPropertyMaps(x => x.DestinationType == typeof(DateTime?) && x.SourceType == typeof(DateTime?) && x.SourceMember.Name.ToLowerInvariant().EndsWith("utc") && !x.DestinationMember.Name.ToLowerInvariant().EndsWith("utc"), (map, expression) =>
            {
                expression.MapFrom<TimeZoneUtcToLocalContextDateTimeResolver, DateTime?>(map.SourceMember.Name);
            });

            ForAllPropertyMaps(x => x.DestinationType == typeof(DateTime) && x.SourceType == typeof(DateTime) && !x.SourceMember.Name.ToLowerInvariant().EndsWith("utc") && x.DestinationMember.Name.ToLowerInvariant().EndsWith("utc"), (map, expression) =>
            {
                expression.MapFrom<TimeZoneLocalToUtcContextDateTimeResolver, DateTime>(map.SourceMember.Name);
            });
            ForAllPropertyMaps(x => x.DestinationType == typeof(DateTime?) && x.SourceType == typeof(DateTime?) && !x.SourceMember.Name.ToLowerInvariant().EndsWith("utc") && x.DestinationMember.Name.ToLowerInvariant().EndsWith("utc"), (map, expression) =>
            {
                expression.MapFrom<TimeZoneLocalToUtcContextDateTimeResolver, DateTime?>(map.SourceMember.Name);
            });

            CreateMap<AddressModel, AddressViewModel>().ReverseMap();
            CreateMap<CompanyProfileModel, CompanyProfileViewModel>().ReverseMap();
            CreateMap<ContactModel, ContactViewModel>().ReverseMap();
            CreateMap<CustomerAddressModel, CustomerAddressViewModel>().ReverseMap();
            CreateMap<CustomerAdminModel, CustomerAdminViewModel>().ReverseMap();
            CreateMap<CustomerContactModel, CustomerContactViewModel>().ReverseMap();
            CreateMap<CustomerOpportunityAddressModel, CustomerOpportunityAddressViewModel>().ReverseMap();
            CreateMap<CustomerOpportunityAdminModel, CustomerOpportunityAdminViewModel>().ReverseMap();
            CreateMap<CustomerOpportunityModel, CustomerOpportunityViewModel>().ReverseMap();
            CreateMap<CustomerViewModel, CustomerModel>().ReverseMap();
            CreateMap<NoteModel, NoteViewModel>().ReverseMap();
            CreateMap<NotificationSettingModel, NotificationSettingViewModel>().ReverseMap();
            CreateMap<OpportunityPaymentMethodModel, OpportunityPaymentMethodViewModel>().ReverseMap();
            CreateMap<PaymentMethodModel, PaymentMethodViewModel>().ReverseMap();
            CreateMap<SearchModel, SearchViewModel>().ReverseMap();
            CreateMap<SignUpCompanyProfileModel, SignUpCompanyProfileViewModel>().ReverseMap();
            CreateMap<StoredFileModel, StoredFileViewModel>().ReverseMap();
            CreateMap<SubscriptionPageModel, SubscriptionPageViewModel>().ReverseMap();
            CreateMap<SubscriptionTypeModel, SubscriptionTypeViewModel>().ReverseMap();
            CreateMap<SystemSettingModel, SystemSettingsViewModel>().ReverseMap();
            CreateMap<TemplateDefinitionModel, TemplateDefinitionViewModel>().ReverseMap();
            CreateMap<TermsAndConditionsModel, TermsAndConditionsViewModel>().ReverseMap();
            CreateMap<UserDetailModel, UserDetailViewModel>().ReverseMap();
            CreateMap<UserModel, UserViewModel>().ReverseMap();
            CreateMap<UserNotificationSettingModel, UserNotificationSettingViewModel>().ReverseMap();
            CreateMap<UserPreferenceModel, UserPreferenceViewModel>().ReverseMap();
            CreateMap<UserProfileModel, UserProfileViewModel>().ReverseMap();
            CreateMap<UserUpdateNotificationSettingModel, UserUpdateNotificationSettingViewModel>().ReverseMap();
            CreateMap<ShipmentInformationDetailModel, ShipmentInformationDetailViewModel>().ReverseMap();
            CreateMap<ShipmentDetailModel, ShipmentDetailViewModel>().ReverseMap();
            CreateMap<QuoteModel, QuoteViewModel>().ReverseMap();
            CreateMap<BookingModel, BookingViewModel>().ReverseMap();
            CreateMap<AirportModel, AirportViewModel>().ReverseMap();
            CreateMap<QuotesPricingModel, QuotePricingViewModel>().ReverseMap();
            CreateMap<ShipmentInformationHeaderModel, ShipmentInformationHeaderViewModel>().ReverseMap();


        }
    }
}
