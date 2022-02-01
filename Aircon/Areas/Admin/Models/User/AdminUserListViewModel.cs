using Aircon.ViewModels.Shared;
using System.Collections.Generic;

namespace Aircon.Areas.Admin.Models.User
{
    public class AdminUserListViewModel
    {
        public List<UserViewModel> QueueUsers { get; set; }
        public List<UserViewModel> AdminUsers { get; set; }
    }
}
