using Aircon.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Core
{
    public interface IWorkContext
    {
        User CurrentUser { get; }
        Task<User> SetCurrentUser();

    }

}
