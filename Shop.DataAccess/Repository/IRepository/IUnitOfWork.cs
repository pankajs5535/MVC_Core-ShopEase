using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category{  get; } // add it by manually
        IProductRepository Product { get; } // add it by manually
        ICompanyRepository Company { get; }
        IShoppingCartRepository ShoppingCart { get; }
        
        IApplicationUserRepository ApplicationUser { get; } 
        
        IOrderDetailRepository OrderDetail { get; }

        IOrderHeaderRepository OrderHeader { get; }

        // this method globally use this method so we can implment this here in unit of work

        void Save();

    }
}
