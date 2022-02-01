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
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aircon.Areas.Customer.Models.Quotes;
using Aircon.Business.Models.Customer.Quotes;
using Aircon.Areas.Customer.Models.Bookings;
using Aircon.Business.Models.Customer.Bookings;
using Aircon.Areas.Customer.Models.Airports;
using Aircon.Business.Models.Customer.Airports;
using Aircon.Areas.Customer.Models.ShipmentInformation;
using Aircon.Business.Models.Customer.ShipmentInformation;

namespace Aircon.Extensions
{
    public static class AutoMapperHelper
    {
        private static IServiceProvider ServiceProvider;
        public static void UseStateAutoMapper(this IApplicationBuilder applicationBuilder)
        {
            ServiceProvider = applicationBuilder.ApplicationServices;
        }

        public static TDestination Map<TDestination>(object source)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<TDestination>(source);
        }

        public static TDestination Map<TSource, TDestination>(TSource source)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();

            return mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TDestination>(this object source)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<TDestination>(source);
        }

        public static List<TDestination> MapToList<TDestination>(this IEnumerable source)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<List<TDestination>>(source);
        }


        public static List<TDestination> MapToList<TSource, TDestination>(this IEnumerable<TSource> source)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<List<TDestination>>(source);
        }

        public static CustomerContactViewModel ToViewModel(this CustomerContactModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<CustomerContactViewModel>(model);
        }
        public static CustomerContactModel ToModel(this CustomerContactViewModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<CustomerContactModel>(model);
        }

        public static UserViewModel ToViewModel(this UserModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<UserViewModel>(model);
        }
        public static UserModel ToModel(this UserViewModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<UserModel>(model);
        }

        public static AddressViewModel ToViewModel(this AddressModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<AddressViewModel>(model);
        }
        public static AddressModel ToModel(this AddressViewModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<AddressModel>(model);
        }


        public static CompanyProfileViewModel ToViewModel(this CompanyProfileModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<CompanyProfileViewModel>(model);
        }
        public static CompanyProfileModel ToModel(this CompanyProfileViewModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<CompanyProfileModel>(model);
        }


        public static ContactViewModel ToViewModel(this ContactModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<ContactViewModel>(model);
        }
        public static ContactModel ToModel(this ContactViewModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<ContactModel>(model);
        }


        public static CustomerAddressViewModel ToViewModel(this CustomerAddressModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<CustomerAddressViewModel>(model);
        }
        public static CustomerAddressModel ToModel(this CustomerAddressViewModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<CustomerAddressModel>(model);
        }


        public static CustomerAdminViewModel ToViewModel(this CustomerAdminModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<CustomerAdminViewModel>(model);
        }
        public static CustomerAdminModel ToModel(this CustomerAdminViewModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<CustomerAdminModel>(model);
        }


        public static CustomerOpportunityAddressViewModel ToViewModel(this CustomerOpportunityAddressModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<CustomerOpportunityAddressViewModel>(model);
        }
        public static CustomerOpportunityAddressModel ToModel(this CustomerOpportunityAddressViewModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<CustomerOpportunityAddressModel>(model);
        }


        public static CustomerOpportunityAdminViewModel ToViewModel(this CustomerOpportunityAdminModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<CustomerOpportunityAdminViewModel>(model);
        }
        public static CustomerOpportunityAdminModel ToModel(this CustomerOpportunityAdminViewModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<CustomerOpportunityAdminModel>(model);
        }


        public static CustomerOpportunityViewModel ToViewModel(this CustomerOpportunityModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<CustomerOpportunityViewModel>(model);
        }
        public static CustomerOpportunityModel ToModel(this CustomerOpportunityViewModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<CustomerOpportunityModel>(model);
        }


        public static CustomerViewModel ToViewModel(this CustomerModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<CustomerViewModel>(model);
        }
        public static CustomerModel ToModel(this CustomerViewModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<CustomerModel>(model);
        }


        public static NoteViewModel ToViewModel(this NoteModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<NoteViewModel>(model);
        }
        public static NoteModel ToModel(this NoteViewModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<NoteModel>(model);
        }


        public static NotificationSettingViewModel ToViewModel(this NotificationSettingModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<NotificationSettingViewModel>(model);
        }
        public static NotificationSettingModel ToModel(this NotificationSettingViewModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<NotificationSettingModel>(model);
        }


        public static OpportunityPaymentMethodViewModel ToViewModel(this OpportunityPaymentMethodModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<OpportunityPaymentMethodViewModel>(model);
        }
        public static OpportunityPaymentMethodModel ToModel(this OpportunityPaymentMethodViewModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<OpportunityPaymentMethodModel>(model);
        }


        public static PaymentMethodViewModel ToViewModel(this PaymentMethodModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<PaymentMethodViewModel>(model);
        }
        public static PaymentMethodModel ToModel(this PaymentMethodViewModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<PaymentMethodModel>(model);
        }


        public static SearchViewModel ToViewModel(this SearchModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<SearchViewModel>(model);
        }
        public static SearchModel ToModel(this SearchViewModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<SearchModel>(model);
        }


        public static SignUpCompanyProfileViewModel ToViewModel(this SignUpCompanyProfileModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<SignUpCompanyProfileViewModel>(model);
        }
        public static SignUpCompanyProfileModel ToModel(this SignUpCompanyProfileViewModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<SignUpCompanyProfileModel>(model);
        }


        public static StoredFileViewModel ToViewModel(this StoredFileModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<StoredFileViewModel>(model);
        }
        public static StoredFileModel ToModel(this StoredFileViewModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<StoredFileModel>(model);
        }


        public static SubscriptionPageViewModel ToViewModel(this SubscriptionPageModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<SubscriptionPageViewModel>(model);
        }
        public static SubscriptionPageModel ToModel(this SubscriptionPageViewModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<SubscriptionPageModel>(model);
        }


        public static SubscriptionTypeViewModel ToViewModel(this SubscriptionTypeModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<SubscriptionTypeViewModel>(model);
        }
        public static SubscriptionTypeModel ToModel(this SubscriptionTypeViewModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<SubscriptionTypeModel>(model);
        }


        public static SystemSettingsViewModel ToViewModel(this SystemSettingModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<SystemSettingsViewModel>(model);
        }
        public static SystemSettingModel ToModel(this SystemSettingsViewModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<SystemSettingModel>(model);
        }


        public static TemplateDefinitionViewModel ToViewModel(this TemplateDefinitionModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<TemplateDefinitionViewModel>(model);
        }
        public static TemplateDefinitionModel ToModel(this TemplateDefinitionViewModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<TemplateDefinitionModel>(model);
        }


        public static TermsAndConditionsViewModel ToViewModel(this TermsAndConditionsModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<TermsAndConditionsViewModel>(model);
        }
        public static TermsAndConditionsModel ToModel(this TermsAndConditionsViewModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<TermsAndConditionsModel>(model);
        }


        public static UserDetailViewModel ToViewModel(this UserDetailModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<UserDetailViewModel>(model);
        }
        public static UserDetailModel ToModel(this UserDetailViewModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<UserDetailModel>(model);
        }


        public static UserNotificationSettingViewModel ToViewModel(this UserNotificationSettingModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<UserNotificationSettingViewModel>(model);
        }
        public static UserNotificationSettingModel ToModel(this UserNotificationSettingViewModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<UserNotificationSettingModel>(model);
        }


        public static UserPreferenceViewModel ToViewModel(this UserPreferenceModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<UserPreferenceViewModel>(model);
        }
        public static UserPreferenceModel ToModel(this UserPreferenceViewModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<UserPreferenceModel>(model);
        }


        public static UserProfileViewModel ToViewModel(this UserProfileModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<UserProfileViewModel>(model);
        }
        public static UserProfileModel ToModel(this UserProfileViewModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<UserProfileModel>(model);
        }


        public static UserUpdateNotificationSettingViewModel ToViewModel(this UserUpdateNotificationSettingModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<UserUpdateNotificationSettingViewModel>(model);
        }
        public static UserUpdateNotificationSettingModel ToModel(this UserUpdateNotificationSettingViewModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<UserUpdateNotificationSettingModel>(model);
        }
        public static ShipmentInformationHeaderViewModel ToViewModel(this ShipmentInformationHeaderModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<ShipmentInformationHeaderViewModel>(model);
        }
        public static ShipmentInformationHeaderModel ToModel(this ShipmentInformationHeaderViewModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<ShipmentInformationHeaderModel>(model);
        }
         public static ShipmentDetailViewModel ToViewModel(this ShipmentDetailModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<ShipmentDetailViewModel>(model);
        }
        public static ShipmentDetailModel ToModel(this ShipmentDetailViewModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<ShipmentDetailModel>(model);
        }

        public static AirportViewModel ToViewModel(this AirportModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<AirportViewModel>(model);
        }
        public static AirportModel ToModel(this AirportViewModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<AirportModel>(model);
        }
        public static QuoteViewModel ToViewModel(this QuoteModel model)
        {  
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<QuoteViewModel>(model);
        }
        public static QuoteModel ToModel(this QuoteViewModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<QuoteModel>(model);
        }
        public static BookingViewModel ToViewModel(this BookingModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<BookingViewModel>(model);
        }
        public static BookingModel ToModel(this BookingViewModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<BookingModel>(model);
        }
        public static QuotesPricingModel ToModel(this QuotePricingViewModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<QuotesPricingModel>(model);
        }
        public static QuotePricingViewModel ToViewModel(this QuotesPricingModel model)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<QuotePricingViewModel>(model);
        }
    }
}
