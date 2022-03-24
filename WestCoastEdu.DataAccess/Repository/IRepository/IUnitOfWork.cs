using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WestCoastEdu.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ILocationRepository Location { get; }
        IStatusRepository Status { get; }
        IProductRepository Product { get; }
        ICustomerRepository Customer { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IShoppingCartRepository ShoppingCart { get; }
        IOrderDetailRepository OrderDetail { get; }
        IOrderHeaderRepository OrderHeader { get; }

        void Save();
    }
}
