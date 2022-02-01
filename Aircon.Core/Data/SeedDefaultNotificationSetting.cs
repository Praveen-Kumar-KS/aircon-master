using Aircon.Data.Entities;
using Aircon.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Core.Data
{
    public static class SeedDefaultNotificationSetting
    {

        public static List<DefaultNotificationSetting> GetDefaultNotificationSettings()
        {

            List<DefaultNotificationSetting> defaultNotificationSettings = new List<DefaultNotificationSetting>
            {
                new DefaultNotificationSetting{ IsActive = true, NotificationSetting = new NotificationSetting{ Name = "Dashboard", NotificationGroup = NotificationGroup.QuotesExpiring, SystemName = NotificationSettingName.QuotesExpiring.Dashboard,  TemplateDefinition = TemplateDefinationProvider.QuotesExpiringDashboard  }},
                new DefaultNotificationSetting{ IsActive = true, NotificationSetting = new NotificationSetting{ Name = "Email", NotificationGroup = NotificationGroup.QuotesExpiring, SystemName = NotificationSettingName.QuotesExpiring.Email, TemplateDefinition = TemplateDefinationProvider.QuotesExpiringEmail }},
                new DefaultNotificationSetting{ IsActive = true, NotificationSetting = new NotificationSetting{ Name = "Text", NotificationGroup = NotificationGroup.QuotesExpiring, SystemName = NotificationSettingName.QuotesExpiring.Text, TemplateDefinition = TemplateDefinationProvider.QuotesExpiringText  }},
                new DefaultNotificationSetting{ IsActive = true, NotificationSetting = new NotificationSetting{ Name = "Dashboard", NotificationGroup = NotificationGroup.BookingNeedsAttention, SystemName = NotificationSettingName.BookingNeedsAttention.Dashboard,  TemplateDefinition = TemplateDefinationProvider.BookingNeedsAttentionDashboard  }},
                new DefaultNotificationSetting{ IsActive = true, NotificationSetting = new NotificationSetting{ Name = "Email", NotificationGroup = NotificationGroup.BookingNeedsAttention, SystemName = NotificationSettingName.BookingNeedsAttention.Email,  TemplateDefinition = TemplateDefinationProvider.BookingNeedsAttentionEmail  }},
                new DefaultNotificationSetting{ IsActive = true, NotificationSetting = new NotificationSetting{ Name = "Text", NotificationGroup = NotificationGroup.BookingNeedsAttention, SystemName = NotificationSettingName.BookingNeedsAttention.Text,  TemplateDefinition = TemplateDefinationProvider.BookingNeedsAttentionText  }},
                new DefaultNotificationSetting{ IsActive = true, NotificationSetting = new NotificationSetting{ Name = "Dashboard", NotificationGroup = NotificationGroup.ShipmentNeedsAttention, SystemName = NotificationSettingName.ShipmentNeedsAttention.Dashboard,  TemplateDefinition = TemplateDefinationProvider.ShipmentNeedsAttentionDashboard  }},
                new DefaultNotificationSetting{ IsActive = true, NotificationSetting = new NotificationSetting{ Name = "Email", NotificationGroup = NotificationGroup.ShipmentNeedsAttention,SystemName = NotificationSettingName.ShipmentNeedsAttention.Email,   TemplateDefinition = TemplateDefinationProvider.ShipmentNeedsAttentionEmail  }},
                new DefaultNotificationSetting{ IsActive = true, NotificationSetting = new NotificationSetting{ Name = "Text", NotificationGroup = NotificationGroup.ShipmentNeedsAttention, SystemName = NotificationSettingName.ShipmentNeedsAttention.Text,  TemplateDefinition = TemplateDefinationProvider.ShipmentNeedsAttentionText  }},
                new DefaultNotificationSetting{ IsActive = true, NotificationSetting = new NotificationSetting{ Name = "Dashboard", NotificationGroup = NotificationGroup.ShipmentDelayed, SystemName = NotificationSettingName.ShipmentDelivered.Dashboard,  TemplateDefinition = TemplateDefinationProvider.ShipmentDelayedDashboard  }},
                new DefaultNotificationSetting{ IsActive = true, NotificationSetting = new NotificationSetting{ Name = "Email", NotificationGroup = NotificationGroup.ShipmentDelayed,SystemName = NotificationSettingName.ShipmentDelivered.Email,   TemplateDefinition = TemplateDefinationProvider.ShipmentDelayedEmail  }},
                new DefaultNotificationSetting{ IsActive = true, NotificationSetting = new NotificationSetting{ Name = "Text", NotificationGroup = NotificationGroup.ShipmentDelayed, SystemName = NotificationSettingName.ShipmentDelivered.Text,  TemplateDefinition = TemplateDefinationProvider.ShipmentDelayedText  }},
                new DefaultNotificationSetting{ IsActive = true, NotificationSetting = new NotificationSetting{ Name = "Booked", NotificationGroup = NotificationGroup.ShipmentDashboardNotification, SystemName = NotificationSettingName.ShipmentDashboardNotification.Booked,  TemplateDefinition = TemplateDefinationProvider.ShipmentDashboardNotificationBooked  }},
                new DefaultNotificationSetting{ IsActive = true, NotificationSetting = new NotificationSetting{ Name = "Pickup Requested", NotificationGroup = NotificationGroup.ShipmentDashboardNotification,SystemName = NotificationSettingName.ShipmentDashboardNotification.PickupRequested,   TemplateDefinition = TemplateDefinationProvider.ShipmentDashboardNotificationPickupRequested  }},
                new DefaultNotificationSetting{ IsActive = true, NotificationSetting = new NotificationSetting{ Name = "Tendered to Airline", NotificationGroup = NotificationGroup.ShipmentDashboardNotification, SystemName = NotificationSettingName.ShipmentDashboardNotification.TenderedToAirline,  TemplateDefinition = TemplateDefinationProvider.ShipmentDashboardNotificationTenderedToAirline  }},
                new DefaultNotificationSetting{ IsActive = true, NotificationSetting = new NotificationSetting{ Name = "Shipment Departed", NotificationGroup = NotificationGroup.ShipmentDashboardNotification,SystemName = NotificationSettingName.ShipmentDashboardNotification.ShipmentDeparted,   TemplateDefinition = TemplateDefinationProvider.ShipmentDashboardNotificationShipmentDeparted  }},
                new DefaultNotificationSetting{ IsActive = true, NotificationSetting = new NotificationSetting{ Name = "Shipment Arrived", NotificationGroup = NotificationGroup.ShipmentDashboardNotification,SystemName = NotificationSettingName.ShipmentDashboardNotification.ShipmentArrived,   TemplateDefinition = TemplateDefinationProvider.ShipmentDashboardNotificationShipmentArrived  }},
            };
            return defaultNotificationSettings;
        }
    }
}
