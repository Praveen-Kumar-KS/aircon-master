using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Aircon.Business.Extensions;
using Aircon.Business.Models.Shared;
using Aircon.Data;
using Aircon.Data.Entities;


namespace Aircon.Business.Services.Lookups
{
    public interface ILookupService<T>
    {
        List<LookupModel> GetLookupList();
        LookupModel AddLookup(LookupModel addlookupModel);
        LookupModel GetLookupById(int id);
        LookupModel UpdateLookup(LookupModel viewlookupModel);
        void DeleteAllLookup(int id);
    }

    public class LookupService<T> : ILookupService<T> where T: LookupEntity
    {
        private readonly AirconDbContext _airconDBContext;
        public LookupService(AirconDbContext airconDbContext)
        {
            _airconDBContext = airconDbContext;
        }
        public List<LookupModel> GetLookupList()
        {
            return _airconDBContext.Set<T>().AsNoTracking().Select(x => new LookupModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Active = x.Active
            }).ToList();
        }
        public LookupModel AddLookup(LookupModel addLookupModel)
        {
            var lookup = addLookupModel.GetLookupEntity<T>();
            lookup.Name = addLookupModel.Name;
            lookup.Active = addLookupModel.Active;
            lookup.Description = addLookupModel.Description;
            _airconDBContext.Set<T>().Add(lookup);
            _airconDBContext.SaveChanges();
            addLookupModel.Id = lookup.Id;
            return addLookupModel;
        }
        public LookupModel GetLookupById(int id)
        {
            return _airconDBContext.Set<T>().AsNoTracking().Where(x => x.Id == id).Select(x => new LookupModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Active = x.Active
            }).SingleOrDefault();
        }
        public LookupModel UpdateLookup(LookupModel updateLookupModel)
        {
            var lookup = _airconDBContext.Set<T>().AsNoTracking().Where(x => x.Id == updateLookupModel.Id).SingleOrDefault();
            lookup.Name = updateLookupModel.Name;
            lookup.Description = updateLookupModel.Description;
            lookup.Active = updateLookupModel.Active;
            _airconDBContext.Set<T>().Update(lookup);
            _airconDBContext.SaveChanges();
            return updateLookupModel;
        }

        public void DeleteAllLookup(int id)
        {
            var lookup = _airconDBContext.Set<T>().AsNoTracking().Where(x => x.Id == id).SingleOrDefault();
            _airconDBContext.Set<T>().Remove(lookup);
            _airconDBContext.SaveChanges();

        }

    }

}
