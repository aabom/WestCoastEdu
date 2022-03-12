using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestCoastEdu.DataAccess.Repository.IRepository;
using WestCoastEdu.Models;

namespace WestCoastEdu.DataAccess.Repository
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        private ApplicationDbContext _db;

        public LocationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Location location)
        {
            _db.Location.Update(location);
        }
    }
}
