using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestCoastEdu.DataAccess.Repository.IRepository;
using WestCoastEdu.Models;

namespace WestCoastEdu.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private ApplicationDbContext _db;

        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ApplicationUser user)
        {
            var userFromDb = _db.ApplicationUsers.FirstOrDefault(u => u.Id == user.Id);
            if (userFromDb != null)
            {
                userFromDb.FirstName = user.FirstName;
                userFromDb.LastName = user.LastName;
                userFromDb.PhoneNumber = user.PhoneNumber;
                userFromDb.Email = user.Email;
                userFromDb.StreetAddress = user.StreetAddress;
                userFromDb.PostalCode = user.PostalCode;
                userFromDb.City = user.City;
            }
        }
    }
}
