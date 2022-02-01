using Aircon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aircon.Providers
{
    public static class GridActionProvider
    {
        // Aircon 15
        public static List<GridAction> GridActions { get; } = new List<GridAction>()
        {
            new GridAction {
                GridActionConfirmType = GridActionConfirmType.DenyUser,
                Title = "Denying User",
                AdditionalText = "You are about to deny this user. They will be notified via email.",
                ActionButtonText = "DENY USER",
                CancelButtonText = "CANCEL"
            },
            new GridAction {
                GridActionConfirmType = GridActionConfirmType.DeleteContact,
                Title = "Deleting Contact",
                AdditionalText = "Are you are sure you want to delete this contact. This action cannot be undone.",
                ActionButtonText = "DELETE CONTACT",
                CancelButtonText = "CANCEL"
            },
             new GridAction {
                GridActionConfirmType = GridActionConfirmType.DeleteAddress,
                Title = "Deleting Address",
                AdditionalText = "Are you are sure you want to delete this address. This action cannot be undone.",
                ActionButtonText = "DELETE ADDRESS",
                CancelButtonText = "CANCEL"
            },
             new GridAction {
                GridActionConfirmType = GridActionConfirmType.DeleteCustomerOpportunity,
                Title = "Deleting Customer",
                AdditionalText = "Are you are sure you want to delete this customeropportunity. This action cannot be undone.",
                ActionButtonText = "DELETE CUSTOMEROPPORTUNITY",
                CancelButtonText = "CANCEL"
            },
            new GridAction {
                GridActionConfirmType = GridActionConfirmType.ApproveUser,
                Title = "Approving User",
                AdditionalText = "You are about to approve this user. They will be notified via email.",
                ActionButtonText = "APPROVE USER",
                CancelButtonText = "CANCEL APPROVAL"
            },
            new GridAction {
                GridActionConfirmType = GridActionConfirmType.ActivateCustomer,
                Title = "Activating Company",
                AdditionalText = "You are about to activate this company.The admin for this company will be notified via email.",
                ActionButtonText = "ACTIVATE",
                CancelButtonText = "CANCEL"
            },
            new GridAction {
                GridActionConfirmType = GridActionConfirmType.ActivatingUser,
                Title = "Activating User",
                AdditionalText = "You are about to activate this user. They will be notified via email.",
                ActionButtonText = "ACTIVATE",
                CancelButtonText = "CANCEL"
            },
            new GridAction {
                GridActionConfirmType = GridActionConfirmType.ActivateEmployeeUser,
                Title = "ActivateEmployeeUser",
                AdditionalText = "You are about to activate this user. They will be notified via email.",
                ActionButtonText = "ACTIVATE",
                CancelButtonText = "CANCEL"
            },
            new GridAction {
                GridActionConfirmType = GridActionConfirmType.DeactivateCustomer,
                Title = "Deactivating Company",
                AdditionalText = "You are about to deactivate this company.The admin for this company will be notified via email.",
                ActionButtonText = "DEACTIVATE",
                CancelButtonText = "CANCEL"
            },
            new GridAction {
                GridActionConfirmType = GridActionConfirmType.DeactivatingUser,
                Title = "Deactivating User",
                AdditionalText = "You are about to deactivate this user. They will be notified via email.",
                ActionButtonText = "DEACTIVATE",
                CancelButtonText = "CANCEL"
            },
             new GridAction {
                GridActionConfirmType = GridActionConfirmType.DeactivateEmployeeUser,
                Title = "DeactivatingEmployeeUser",
                AdditionalText = "You are about to deactivate this user. They will be notified via email.",
                ActionButtonText = "DEACTIVATE",
                CancelButtonText = "CANCEL"
            },
            new GridAction {
                GridActionConfirmType = GridActionConfirmType.DenyCustomerOpportunity,
                Title = "Close Customer",
                AdditionalText = "Mark customer as closed and remove from queue?",
                ActionButtonText = "MARK AS CLOSED",
                CancelButtonText = "CANCEL"
            },
            new GridAction {
                GridActionConfirmType = GridActionConfirmType.ApproveCustomer,
                Title = "Approving Customer",
                AdditionalText = "You are about to approve this customer. They will be notified via email..",
                ActionButtonText = "APPROVE CUSTOMER",
                CancelButtonText = "CANCEL"
            }
        };
    }
}
