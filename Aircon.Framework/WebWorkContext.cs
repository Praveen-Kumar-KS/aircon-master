using Aircon.Core;
using Aircon.Data.Entities;
using Aircon.Data.Helper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Framework
{
    public partial class WebWorkContext : IWorkContext
    {
        private readonly UserManager<User> _userManager;
        private User _cachedUser;

        public WebWorkContext(UserManager<User> userManager)
        {
            _userManager = userManager;
        }


        public virtual User CurrentUser
        {
            get
            {
                return _cachedUser;
            }
        }

        public virtual async Task<User> SetCurrentUser()
        {
            User user = null;
            if (!(HttpContextHelper.Current == null))
                user = await _userManager.GetUserAsync(HttpContextHelper.Current.User);
            return _cachedUser = user ?? throw new Exception("No user could be loaded");
        }
    }
}
