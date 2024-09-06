using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.DataAccess.Repository.IRepository;
using Shop.Models;

namespace Shop.DataAccess.Repository.IRepository
{
    public interface ICompanyRepository : IRepository<Company> // interface implements interface
    {
        void Update(Company obj);
        //void Save();
    }
}
