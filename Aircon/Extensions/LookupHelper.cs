using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Aircon.Data;
using Aircon.Data.Entities;

namespace Aircon.Extensions
{
    public static class LookupHelper
    {
        public static List<SelectListItem> GetAllLookupItems<T>() where T : LookupEntity
        {
            DesignTimeDbContextFactory a = new DesignTimeDbContextFactory();
            using (var db = a.CreateDbContext(new string[0]))
            {
                return db.Set<T>().AsNoTracking().Where(x=> x.Active==true).Select(x => new SelectListItem
                {
                    Text = x.Description,
                    Value = x.Id.ToString()
                }).OrderBy(x => x.Text).ToList();
            }
        }
        public static List<SelectListItem> GetCustomerAddressTypeItems<T>() where T : AddressType
        {
            DesignTimeDbContextFactory a = new DesignTimeDbContextFactory();
            using (var db = a.CreateDbContext(new string[0]))
            {
                return db.Set<T>().AsNoTracking().Where(x=> x.Active==true).Where(x=> x.IsCustomerAddressType == true).Select(x => new SelectListItem
                {
                    Text = x.Description,
                    Value = x.Id.ToString()
                }).OrderBy(x => x.Text).ToList();
            }
        }
    }   

}
