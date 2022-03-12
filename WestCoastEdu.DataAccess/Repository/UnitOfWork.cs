using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestCoastEdu.DataAccess.Repository.IRepository;

namespace WestCoastEdu.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Location = new LocationRepository(_db);
            Status = new StatusRepository(_db);
            Product = new ProductRepository(_db);
        }
        public ILocationRepository Location { get; private set; }
        public IStatusRepository Status { get; private set; }
        public IProductRepository Product { get; private set; }
        
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
