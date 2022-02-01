using Aircon.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Services
{
    public interface IAdminCommonService
    {
        List<SelectListItem> Customers();
    }
    public class AdminCommonService : IAdminCommonService
    {
        private readonly AirconDbContext _airconDbContext;
        public AdminCommonService(AirconDbContext airconDbContext)
        {
            _airconDbContext = airconDbContext;
        }

        public List<SelectListItem> Customers()
        {
            var customers = new List<SelectListItem>();
            customers = _airconDbContext.Customers
                .Where(x => x.IsActive == true)
                .Select(x => new SelectListItem { Text = x.CompanyName, Value = x.Id.ToString() }).ToList();
            return customers;
        }

    }
}
