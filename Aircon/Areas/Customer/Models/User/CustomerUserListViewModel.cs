using Aircon.ViewModels.Shared;
using System.Collections.Generic;

namespace Aircon.Areas.Customer.Models.User
{
    // 5 - User screen
    public class CustomerUserListViewModel : SearchViewModel
    {
        public List<UserViewModel> QueueUsers { get; set; }
        public List<UserViewModel> CustomerUsers { get; set; }

    }
}
