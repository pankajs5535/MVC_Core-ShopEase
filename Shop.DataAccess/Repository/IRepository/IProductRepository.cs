using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.DataAccess.Repository.IRepository;
using Shop.Models;
//using Shop.Models.Models;

namespace Shop.DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product> // interface implements interface
    {
        void Update(Product obj);
        //void Save();
    }
}
