using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestCoastEdu.DataAccess.Repository.IRepository;
using WestCoastEdu.Models;

namespace WestCoastEdu.DataAccess.Repository
{
    public class StatusRepository : Repository<Status>, IStatusRepository
    {
        private ApplicationDbContext _db;

        public StatusRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Status status)
        {
            _db.Status.Update(status);
        }
    }
}
