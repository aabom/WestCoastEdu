using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestCoastEdu.Models;

namespace WestCoastEdu.DataAccess.Repository.IRepository
{
    public interface IStatusRepository : IRepository<Status>
    {
        void Update(Status status);
    }
}
